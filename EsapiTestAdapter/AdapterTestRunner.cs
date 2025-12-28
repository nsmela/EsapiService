using Esapi.Interfaces; // Namespace where IActorMessage lives
using Esapi.Services; // Namespace where EsapiService lives
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Concurrent; // Required for BlockingCollection
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using System.Windows.Threading;
using VMS.TPS.Common.Model.API; // Requires reference to WindowsBase

namespace EsapiTestAdapter
{
    internal static class AdapterTestRunner
    {
        private static EsapiService _activeSession;
        private static Thread _staThread;
        private static CancellationTokenSource _cts;
        private static TestContextProxy _context; // Holds current App/Patient/Plan

        // Fix 1: We need to hold onto the mailbox so we can dispose it later if needed
        private static BlockingCollection<IActorMessage> _mailbox;

        public static void InitializeSession()
        {
            if (_activeSession != null) return;

            _staThread = new Thread(() =>
            {
                // Fix 2: Instantiate the mailbox required by the EsapiService constructor
                _mailbox = new BlockingCollection<IActorMessage>();

                // Create the service with the dependency
                _activeSession = new EsapiService(_mailbox);

                // Start the persistent ESAPI Thread
                _staThread = new Thread(() => {
                    try
                    {
                        // 1. Create Application
                        var app = Application.CreateApplication();
                        _context.SetApp(app);

                        // 2. Run Message Loop
                        foreach (var msg in _mailbox.GetConsumingEnumerable(_cts.Token))
                        {
                            try { msg.Process(_context); }
                            catch (Exception ex) { Console.Error.WriteLine(ex); }
                        }
                        app.Dispose();
                    }
                    catch (OperationCanceledException) { }
                });

                _staThread.SetApartmentState(ApartmentState.STA);
                _staThread.IsBackground = true;
                _staThread.Start();
            });

            _staThread.SetApartmentState(ApartmentState.STA);
            _staThread.IsBackground = true; // Ensure it dies if the host process dies
            _staThread.Start();
        }

        public static TestResult ExecuteTest(TestCase test, object testClassInstance)
        {
            if (_activeSession == null)
                throw new InvalidOperationException("Session not initialized. Call InitializeSession() first.");

            // Use the Dispatcher from the generic Application or the thread itself
            // Since EsapiService might not expose Dispatcher directly, use the standard WPF way:
            return Dispatcher.FromThread(_staThread).Invoke(() =>
            {
                try
                {
                    var method = testClassInstance.GetType().GetMethod(test.DisplayName);
                    if (method is null) return new TestResult(test) { Outcome = TestOutcome.NotFound };

                    method.Invoke(testClassInstance, null);
                    return new TestResult(test) { Outcome = TestOutcome.Passed };
                }
                catch (Exception ex)
                {
                    return new TestResult(test)
                    {
                        Outcome = TestOutcome.Failed,
                        ErrorMessage = ex.InnerException?.Message ?? ex.Message,
                        ErrorStackTrace = ex.InnerException?.StackTrace ?? ex.StackTrace
                    };
                }
            });
        }

        public static void Shutdown()
        {
            if (_activeSession != null)
            {
                // Dispose service and mailbox
                // _activeSession.Dispose(); main thread has a dispose for the app when exiting the try// Ensure your Service disposes the Application
                _mailbox?.Dispose();
                _activeSession = null;
            }

            // Kill the Dispatcher loop
            var disp = Dispatcher.FromThread(_staThread);
            if (disp != null && !disp.HasShutdownStarted)
            {
                disp.InvokeShutdown();
            }

            // Join thread to ensure clean exit
            _staThread?.Join(2000);
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
    }
}