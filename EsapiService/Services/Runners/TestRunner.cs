using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using VMS.TPS.Common.Model.API;
using Application = VMS.TPS.Common.Model.API.Application;

namespace Esapi.Services.Runners
{
    /// <summary>
    /// The Global Host for Unit Tests. 
    /// Mimics <see cref="UiRunner"/> by managing a dedicated STA Thread.
    /// Instead of a UI Loop, it runs the ESAPI Actor Loop globally.
    /// </summary>
    public static class TestRunner
    {
        private static Thread _esapiThread;
        private static Application _app;
        private static BlockingCollection<IActorMessage> _globalMailbox;
        private static TestContextProxy _globalContext;
        private static CancellationTokenSource _globalCts;
        private static ManualResetEventSlim _readySignal = new ManualResetEventSlim(false);

        /// <summary>
        /// Initializes the global ESAPI thread. Call this ONCE in [OneTimeSetUp].
        /// </summary>
        public static void Initialize()
        {
            if (_esapiThread != null) return;

            _globalMailbox = new BlockingCollection<IActorMessage>();
            _globalCts = new CancellationTokenSource();
            _globalContext = new TestContextProxy();

            _esapiThread = new Thread(() =>
            {
                try
                {
                    // 1. Create App (STA Thread)
                    _app = Application.CreateApplication();
                    _globalContext.SetApp(_app);
                    _readySignal.Set();

                    // 2. Run the Actor Loop (Persistent)
                    // This mirrors StandaloneRunner.RunActorLoop, but it runs forever
                    // until Dispose() is called.
                    foreach (var message in _globalMailbox.GetConsumingEnumerable(_globalCts.Token))
                    {
                        try
                        {
                            message.Process(_globalContext);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[TestRunner] Error processing message: {ex.Message}");
                        }
                    }
                }
                catch (OperationCanceledException) { /* Clean Shutdown */ }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[TestRunner] FATAL: {ex}");
                }
                finally
                {
                    _app?.Dispose();
                }
            });

            _esapiThread.SetApartmentState(ApartmentState.STA);
            _esapiThread.IsBackground = true;
            _esapiThread.Start();

            _readySignal.Wait();
        }

        public static void Dispose()
        {
            _globalCts?.Cancel();
            _esapiThread?.Join(2000);
            _globalMailbox?.Dispose();
            _globalCts?.Dispose();
            _app = null;
            _esapiThread = null;
        }

        // --- Internal Helpers for IntegrationTestRunner ---

        internal static BlockingCollection<IActorMessage> Mailbox => _globalMailbox;

        internal static async Task LoadContextAsync(string patientId, string planId)
        {
            // We need to run OpenPatient on the ESAPI thread. 
            // We do this by posting a special "ActionMessage" to the mailbox.
            var tcs = new TaskCompletionSource<bool>();

            _globalMailbox.Add(new ActionMessage(context =>
            {
                var ctx = context as TestContextProxy;
                try
                {
                    // Close previous
                    ctx.App?.ClosePatient();

                    if (string.IsNullOrEmpty(patientId))
                    {
                        _globalContext.Update(null, null);
                    }
                    else
                    {
                        var pat = ctx.App.OpenPatientById(patientId);
                        PlanSetup plan = null;
                        if (pat != null && !string.IsNullOrEmpty(planId))
                        {
                            // Simple plan lookup
                            foreach (var c in pat.Courses)
                            {
                                plan = c.PlanSetups.FirstOrDefault(p => p.Id == planId);
                                if (plan != null) break;
                            }
                        }
                        _globalContext.Update(pat, plan);
                    }
                    tcs.SetResult(true);
                }
                catch (Exception ex) { tcs.SetException(ex); }
            }));

            await tcs.Task;
        }

        /// <summary>
        /// A mutable context that allows swapping patients without restarting the App.
        /// </summary>
        private class TestContextProxy : IEsapiContext
        {
            public Application App { get; private set; }
            public Patient Patient { get; private set; }
            public PlanSetup PlanSetup { get; private set; }

            public User CurrentUser => throw new NotImplementedException();

            public PlanSetup Plan => throw new NotImplementedException();

            public void SetApp(Application app) => App = app;
            public void Update(Patient p, PlanSetup plan)
            {
                Patient = p;
                PlanSetup = plan;
            }
        }

        /// <summary>
        /// A special message type to run internal commands (like OpenPatient) on the loop.
        /// </summary>
        private class ActionMessage : IActorMessage
        {
            private readonly Action<IEsapiContext> _action;
            public ActionMessage(Action<IEsapiContext> action) => _action = action;

            public Task Process(IEsapiContext context)
            {
                _action(context);
                return Task.CompletedTask;
            }

        }
    }
}