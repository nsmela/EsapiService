using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using VMS.TPS.Common.Model.API;

namespace Esapi.Services.Runners {
    public static class StandaloneRunner {

        public static void Run<TWindow>(
            string patientId = null,
            string planId = null,
            Action<IServiceCollection> configureServices = null)
            where TWindow : Window 
        {
            var services = new ServiceCollection();
            configureServices?.Invoke(services);

            RunInner<TWindow>(patientId, planId, services);
        }

        public static void Run<TWindow>(
            string patientId = null,
            string planId = null,
            IServiceCollection services = null)
            where TWindow : Window
        {
            if (services is null) services = new ServiceCollection();

            RunInner<TWindow>(patientId, planId, services);
        }

        public static void RunInner<TWindow>(
            string patientId = null,
            string planId = null,
            IServiceCollection configureServices = null)
            where TWindow : Window 
        {
            // 1. We are the Main Thread. We effectively BECOME the ESAPI thread.
            // In Standalone, we own the Application lifecycle.

            VMS.TPS.Common.Model.API.Application app = null;
            try {
                // Create Varian App (Must be on STA thread, which Main usually is)
                app = VMS.TPS.Common.Model.API.Application.CreateApplication();
            } catch (Exception ex) {
                MessageBox.Show($"Failed to connect to Eclipse: {ex.Message}");
                return;
            }

            // 2. Preload the context
            Patient patient = null;
            PlanSetup plan = null;

            if (!string.IsNullOrEmpty(patientId)) {
                patient = app.OpenPatientById(patientId);
                if (patient != null && !string.IsNullOrEmpty(planId)) {
                    // Simple logic to find the plan (e.g., matching ID in any course)
                    // You might want to pass CourseId as well if IDs aren't unique.
                    foreach (var course in patient.Courses) {
                        plan = course.PlanSetups.FirstOrDefault(p => p.Id == planId);
                        if (plan != null) break;
                    }
                }
            }

            // 2. Setup Machinery
            var mailbox = new BlockingCollection<IActorMessage>();
            var cts = new CancellationTokenSource();

            // 3. Setup DI
            var services = configureServices ?? new ServiceCollection();

            // Register Core ESAPI Services
            services.AddSingleton(mailbox);
            // Hide the implementation detail (PluginContext) from the user
            // Note: StandaloneContext needs to be robust to handle null Patient/Plan initially
            services.AddSingleton<IEsapiContext>(new StandaloneContext(app, patient, plan));
            services.AddSingleton<IEsapiService, EsapiService>();

            // Register the Window
            services.AddTransient<TWindow>();

            // Build the service provider
            // NOTE: It is safer to build this on the ESAPI thread before passing it
            // to avoid race conditions during startup.
            var provider = services.BuildServiceProvider();

            // 3. Launch UI on dedicated thread
            UiRunner ui = null;
            try {
                ui = new UiRunner();
                ui.Run(() => {
                    try {
                        // This code runs on the NEW UI THREAD


                        // Resolve the main window (DI will build it
                        // and its dependencies, like the ViewModel)
                        var window = provider.GetRequiredService<TWindow>();

                        // provide an exit signal
                        //window.Closed += (s, e) => cts.Cancel();

                        // This blocks the UI thread, which is fine.
                        // The ESAPI thread is NOT blocked and will
                        // proceed to the actor loop.
                        window.ShowDialog();
                    } catch (Exception ex) {
                        // CRITICAL: Catch UI startup errors (like XAML parse exceptions)
                        // and show them, or the app will hang silently.
                        MessageBox.Show($"UI Thread Error: {ex.Message}\n\n{ex.StackTrace}", "Plugin Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    } finally {
                        // GUARANTEE: Signal the ESAPI thread to stop
                        // This ensures the ESAPI thread doesn't wait forever
                        // if the window closes or crashes.
                        cts.Cancel();

                        // Clean up the provider scope if needed
                        if (provider is IDisposable disposable) {
                            disposable.Dispose();
                        }
                    }
                });

                // Run ESAPI Loop on THIS thread (The Script Thread)
                RunActorLoop(mailbox, provider.GetRequiredService<IEsapiContext>(), cts.Token);
            } finally {
                // cleaup
                ui?.Dispose();
                mailbox.Dispose();
                cts.Cancel();
                cts.Dispose();
                app.Dispose();
                (provider as IDisposable)?.Dispose();
            }
        }

        private static void RunActorLoop(BlockingCollection<IActorMessage> mailbox, IEsapiContext context, CancellationToken token) {
            try {
                foreach (var message in mailbox.GetConsumingEnumerable(token)) {
                    message.Process(context);
                }
            } catch (OperationCanceledException) 
            { /* Normal Shutdown */ }
        }
    }
}

