using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Esapi.Services;
using VMS.TPS.Common.Model.API;
using Application = VMS.TPS.Common.Model.API.Application;

namespace Esapi.Services.Runners
{
    public static class TestRunner
    {
        private static BlockingCollection<IActorMessage> _queue;
        private static CancellationTokenSource _cts;
        private static Thread _thread;
        private static TestContextProxy _context; // Holds current App/Patient/Plan

        // The Singleton Service your tests will use
        public static IEsapiService Service { get; private set; }

        public static void Initialize()
        {
            if (_thread != null) return;

            _queue = new BlockingCollection<IActorMessage>();
            _cts = new CancellationTokenSource();
            _context = new TestContextProxy();

            // Create the Service Proxy immediately
            Service = new EsapiService(_queue);

            // Start the persistent ESAPI Thread
            _thread = new Thread(() => {
                try
                {
                    // 1. Create Application
                    var app = Application.CreateApplication();
                    _context.SetApp(app);

                    // 2. Run Message Loop
                    foreach (var msg in _queue.GetConsumingEnumerable(_cts.Token))
                    {
                        try { msg.Process(_context); }
                        catch (Exception ex) { Console.Error.WriteLine(ex); }
                    }
                    app.Dispose();
                }
                catch (OperationCanceledException) { }
            });

            _thread.SetApartmentState(ApartmentState.STA);
            _thread.IsBackground = true;
            _thread.Start();
        }

        public static void Dispose()
        {
            _cts?.Cancel();
            _thread?.Join(1000);
        }

        // Helper to switch context inside the ESAPI thread
        public static async Task LoadContext(string patientId, string planId)
        {
            var tcs = new TaskCompletionSource<bool>();

            // Enqueue a special action to switch patient/plan
            _queue.Add(new ActionMessage(context => {
                try
                {
                    var ctx = context as TestContextProxy;

                    if (ctx is null)
                    {
                        throw new Exception("Invalid context used!");
                    }

                    // Switch Patient
                    if (ctx.Patient?.Id != patientId)
                    {
                        ctx.App.ClosePatient();
                        var pat = string.IsNullOrEmpty(patientId) ? null : ctx.App.OpenPatientById(patientId);
                        _context.Update(pat, null);
                    }
                    // Switch Plan
                    if (!string.IsNullOrEmpty(planId) && _context.Plan?.Id != planId)
                    {
                        var plan = ctx.Patient?.Courses.SelectMany(c => c.PlanSetups).FirstOrDefault(x => x.Id == planId);
                        _context.Update(ctx.Patient, plan);
                    }
                    tcs.SetResult(true);
                }
                catch (Exception ex) { tcs.SetException(ex); }
            }));

            await tcs.Task;
        }

        // Minimal implementations for internal use
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