using Esapi.Interfaces; // Namespace where IActorMessage lives
using Esapi.Services; // Namespace where EsapiService lives
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Concurrent; // Required for BlockingCollection
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
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

        public static void InitializeSession()
        {
            DebugLog.Write("InitializeSession started");
            // Check if the thread is actually alive!
            if (_activeSession != null)
            {
                if (_staThread != null && _staThread.IsAlive)
                {
                    return; // truly initialized and running
                }

                // If we are here, the session exists but the thread died. Clean up.
                Shutdown();
            }

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
                        try { msg.Process(_context); }
                        catch (Exception ex) { Console.Error.WriteLine(ex); }
                    }

                    app.Dispose();
                }
                catch (OperationCanceledException) { }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("FATAL ESAPI CRASH: " + ex);
                    DebugLog.Write($"CRASH: {ex}");
                }

            });

            _staThread.SetApartmentState(ApartmentState.STA);
            _staThread.IsBackground = true;
            _staThread.Start();
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
            if (_activeSession == null)
                throw new InvalidOperationException("Session not initialized. Call InitializeSession() first.");
            var result = new TestResult(test);

            try
            {
                // We send the 'Invoke' logic to the ESAPI thread.
                // We await the result synchronously because RunTests is synchronous.
                var task = _activeSession.PostAsync(context =>
                {
                    // 1. INJECTION: Check if the test class wants the context
                    if (testClassInstance is IEsapiTest esapiTest)
                    {
                        esapiTest.Context = _context;
                    }

                    // 2. REFLECTION: Find the method
                    var type = testClassInstance.GetType();
                    var method = type.GetMethod(test.DisplayName);

                    if (method is null)
                        throw new MissingMethodException($"Method {test.DisplayName} not found on {type.FullName}");

                    // 3. EXECUTION: Run the test
                    // Since we set .Context above, the test can use it immediately
                    var invokeResult = method.Invoke(testClassInstance, null);

                    // Handle async tests (Task return type)
                    if (invokeResult is Task t)
                    {
                        t.GetAwaiter().GetResult();
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
                // Unwrap the "TargetInvocationException" to get the real assertion error
                var realError = ex.InnerException ?? ex;
                result.ErrorMessage = realError.Message;
                result.ErrorStackTrace = realError.StackTrace;
            }

            return result;
        }

        public static void Shutdown()
        {
            _cts?.Cancel();
            _mailbox?.CompleteAdding();
            _staThread?.Join(2000);

            _activeSession = null;
            _mailbox = null;
            _staThread = null;
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
        }

        public static void Log(string message)
        {
            // Change path to valid location
            System.IO.File.AppendAllText(@"C:\Users\nsmela\source\repos\nsmela\Logs.txt",
                $"\r\n[{DateTime.Now:HH:mm:ss} {Thread.CurrentThread.ManagedThreadId}] {message}\n");
        }
    }
}