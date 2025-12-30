using Esapi.Services; // Namespace where EsapiService lives
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Concurrent; // Required for BlockingCollection
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API; // Requires reference to WindowsBase

namespace EsapiTestAdapter
{
    internal static class AdapterTestRunner
    {
        private static EsapiService _activeSession;
        private static Thread _staThread;
        private static CancellationTokenSource _cts;
        private static BlockingCollection<IActorMessage> _mailbox;

        // Context holds the app/patient state alive between tests
        private static TestContextProxy _context;

        // THREAD-SAFETY LOCKS
        private static readonly object _lock = new object();
        private static int _activeBatches = 0; // Reference Counter

        public static void InitializeSession()
        {
            lock (_lock){
                _activeBatches++; // Register that a batch (RunTests) is starting
                                  // If already running, just return. We reuse the existing App.

                if (_activeSession != null && _staThread != null && _staThread.IsAlive)
                {
                    return;
                }
                DebugLog.Write("InitializeSession started");

                _cts = new CancellationTokenSource();
                _context = new TestContextProxy();

                _mailbox = new BlockingCollection<IActorMessage>();
                _activeSession = new EsapiService(_mailbox);

                // This allows the ESAPI thread to say "I'm ready!"
                var esapiLoadedEvent = new ManualResetEventSlim(false);

                _staThread = new Thread(() =>
                {
                    try
                    {
                        // Clean up any old zombies first (optional but recommended)
                        KillZombieProcesses();
                        DebugLog.Write("ESAPI Thread Started");

                        // We tell Varian that *this* DLL (EsapiTestAdapter) is the executable.
                        // Since this DLL references VMS.TPS.Common.Model.API, the check passes.
                        MockEntryAssembly(Assembly.GetExecutingAssembly());

                        // 1. Create Application
                        DebugLog.Write("Creating Application...");
                        var app = Application.CreateApplication();
                        DebugLog.Write("Application Created!");
                        _context.SetApp(app);

                        // SIGNAL: We have finished loading!
                        esapiLoadedEvent.Set();

                        // 2. Run Message Loop
                        foreach (var msg in _mailbox.GetConsumingEnumerable(_cts.Token))
                        {
                            DebugLog.Write("Processing message...");
                            try { msg.Process(_context); }
                            catch (Exception ex) { DebugLog.Write(ex.Message); }
                            DebugLog.Write("Message processed.");
                        }

                        app.Dispose();
                    }
                    catch (OperationCanceledException) { }
                    catch (Exception ex)
                    {
                        DebugLog.Write($"CRASH: {ex}");
                        esapiLoadedEvent.Set(); // Unblock main thread even on crash
                    }

                });

                _staThread.SetApartmentState(ApartmentState.STA);
                _staThread.IsBackground = true;
                _staThread.Start();
                DebugLog.Write("Waiting for ESAPI to load...");
            }
        }

        private static void KillZombieProcesses()
        {
            foreach (var p in System.Diagnostics.Process.GetProcessesByName("Eclipse"))
            {
                try { p.Kill(); } catch { }
            }
        }

        private static void MockEntryAssembly(Assembly assembly)
        {
            var manager = new AppDomainManager();
            var entryAssemblyfield = manager.GetType().GetField("m_entryAssembly", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            entryAssemblyfield.SetValue(manager, assembly);

            var domain = AppDomain.CurrentDomain;
            var domainManagerField = domain.GetType().GetField("_domainManager", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            domainManagerField.SetValue(domain, manager);
        }

        public static TestResult ExecuteTest(TestCase test, object testClassInstance)
        {
            if (_activeSession is null)
                throw new InvalidOperationException("Session not initialized. Call InitializeSession() first.");

            var result = new TestResult(test);

            try
            {
                // We send the 'Invoke' logic to the ESAPI thread.
                // We await the result synchronously because RunTests is synchronous.
                var task = _activeSession.PostAsync(context =>
                {
                    var type = testClassInstance.GetType();

                    // 1. INJECTION: Check if the test class wants the context
                    if (testClassInstance is IEsapiTest esapiTest)
                    {
                        esapiTest.Context = _context;
                    }

                    var setupMethods = type.GetMethods()
                    .Where(m => m.GetCustomAttributes(typeof(EsapiSetupAttribute), true).Any())
                    .ToList();

                    foreach (var setup in setupMethods)
                    {
                        DebugLog.Write($"[Adapter] Executing Setup: {setup.Name}");
                        setup.Invoke(testClassInstance, null);
                    }

                    // 2. REFLECTION: Find the method
                    var method = type.GetMethod(test.DisplayName);

                    if (method is null)
                        throw new MissingMethodException($"Method {test.DisplayName} not found on {type.FullName}");

                    // 3. FIND TEARDOWN METHODS
                    var tearDownMethods = type.GetMethods()
                        .Where(m => m.GetCustomAttributes(typeof(EsapiTearDownAttribute), true).Any())
                        .ToList();

                    try
                    {
                        DebugLog.Write($" [Runner] Executing Test: {test.DisplayName}");
                        // 4. EXECUTION: Run the test
                        // Since we set .Context above, the test can use it immediately
                        var invokeResult = method.Invoke(testClassInstance, null);

                        // Handle async tests (Task return type)
                        if (invokeResult is Task t)
                        {
                            t.GetAwaiter().GetResult();
                        }
                        DebugLog.Write($" [Runner] Test {test.DisplayName} completed.");
                    }
                    finally
                    {
                        // We forcibly close the patient here. This guarantees a clean 
                        // slate for the next test, even if the current test crashed hard.
                        if (context is IEsapiAppContext appContext && appContext.App != null)
                        {
                            try
                            {
                                DebugLog.Write(" [Runner] Auto-closing leftover patient...");
                                appContext.App.ClosePatient();
                            }
                            catch (Exception cleanupEx)
                            {
                                DebugLog.Write($" [Runner] Critical Cleanup Failure: {cleanupEx}");
                            }
                        }
                    }
                });

                // Wait with a timeout (e.g., 60 seconds)
                if (!task.Wait(TimeSpan.FromSeconds(60)))
                {
                    throw new TimeoutException("The test timed out waiting for the ESAPI thread. " +
                        "The thread might be stuck or the Application crashed.");
                }

                result.Outcome = TestOutcome.Passed;
            }
            catch (Exception ex)
            {
                result.Outcome = TestOutcome.Failed;

                // Unwrap the Reflection exception to find the REAL error
                var realError = ex;

                // 1. Unwrap Reflection wrapper
                if (realError is System.Reflection.TargetInvocationException tie && tie.InnerException != null)
                    realError = tie.InnerException;

                // 2. Unwrap Task wrapper (if async failed)
                if (realError is AggregateException ae)
                    realError = ae.Flatten().InnerException;

                result.ErrorMessage = realError.Message;       // Now shows "Assert.Fail" or "NullReference..."
                result.ErrorStackTrace = realError.StackTrace;
            }

            return result;
        }

        public static void Shutdown()
        {
            lock (_lock)
            {
                DebugLog.Write("Shutdown called");
                _activeBatches--; // One less batch running
                if (_activeBatches > 0)
                {
                    // Still active batches, do not shutdown
                    return;
                }

                _cts?.Cancel();
                _mailbox?.CompleteAdding();
                _staThread?.Join(2000);

                _activeSession = null;
                _mailbox = null;
                _staThread = null;
            }
        }

        class TestContextProxy : IEsapiAppContext
        {
            public Application App { get; private set; }
            public Patient Patient { get; private set; }
            public PlanSetup Plan { get; private set; }
            public User CurrentUser => App?.CurrentUser;
            public string CurrentUserId => App?.CurrentUser?.Id;
            public void SetApp(Application a) => App = a;
            public void Update(Patient p, PlanSetup ps) { Patient = p; Plan = ps; }
            public void Update(Patient patient) => Patient = patient;
            public void Update(PlanSetup plan) => Plan = plan;
        }
    }
}