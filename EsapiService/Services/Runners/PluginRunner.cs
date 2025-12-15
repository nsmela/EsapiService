using Esapi.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using VMS.TPS.Common.Model.API;

namespace Esapi.Services.Runners {


    public class PluginRunner {
        public void Run<TWindow>(
            ScriptContext scriptContext,
            Action<IServiceCollection> configureServices = null)
            where TWindow : Window 
        {

            var services = new ServiceCollection();
            configureServices?.Invoke(services);
            Run<TWindow>(scriptContext, services);
        }

        public void Run<TWindow>(
            ScriptContext scriptContext,
            IServiceCollection userServices = null)
            where TWindow : Window {

            // 1. Create the mailbox
            var mailbox = new BlockingCollection<IActorMessage>();
            var cts = new CancellationTokenSource();

            // 2. Setup DI
            var services = userServices ?? new ServiceCollection();

            // Register Core ESAPI Services
            services.AddSingleton(mailbox);

            // We use Replace/TryAdd to ensure we don't accidentally conflict 
            // if the user somehow tried to register these themselves.
            services.TryAddSingleton<IEsapiContext>(new PluginContext(scriptContext));
            services.TryAddSingleton<IEsapiService, EsapiService>();

            // Register the Window
            services.TryAddTransient<TWindow>();

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
                        window.Closed += (s, e) => cts.Cancel();

                        // This blocks the UI thread, which is fine.
                        // The ESAPI thread is NOT blocked and will
                        // proceed to the actor loop.
                        window.ShowDialog();
                    } catch (Exception ex) {
                        // CRITICAL: Catch UI startup errors (like XAML parse exceptions)
                        // and show them, or the app will hang silently.
                        MessageBox.Show($"UI Thread Error: {ex.Message}\n\n{ex.StackTrace}", "Plugin Error", MessageBoxButton.OK, MessageBoxImage.Error);

                        cts.Cancel();
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
                cts.Dispose();
                (provider as IDisposable)?.Dispose();
            }
        }

        private void RunActorLoop(BlockingCollection<IActorMessage> mailbox, IEsapiContext context, CancellationToken token) {
            try {
                foreach (var message in mailbox.GetConsumingEnumerable(token)) {
                    message.Process(context);
                }
            } catch (OperationCanceledException) { /* Normal Shutdown */ }
        }
    }
}