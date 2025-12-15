using System;
using System.Threading;

namespace Esapi.Services.Runners {
    /// <summary>
    /// Utility class to run a WPF UI on a new, dedicated
    /// STA (Single-Threaded Apartment) thread.
    /// This is required for Varian plugins.
    /// </summary>
    public class UiRunner : IDisposable {
        private readonly Thread _thread;
        // Used to signal that the dispatcher is ready
        private readonly ManualResetEvent _dispatcherReadyEvent = new ManualResetEvent(false);

        /// <summary>
        /// Initializes a new instance of the <see cref="UiRunner"/> class, setting up a dedicated UI thread.
        /// </summary>
        /// <remarks>This constructor creates a new thread configured to run in a single-threaded
        /// apartment (STA) state, which is necessary for UI components that require a message loop. The thread runs a
        /// dispatcher loop to process UI messages, ensuring that UI operations can be performed on this thread. The
        /// constructor blocks until the dispatcher is ready, ensuring that any UI operations can be safely invoked
        /// after the instance is created.</remarks>
        public UiRunner() {
            _thread = new Thread(() => {
                // Ensure the dispatcher is created
                var dispatcher = System.Windows.Threading.Dispatcher.CurrentDispatcher;

                // Signal that the dispatcher is ready
                _dispatcherReadyEvent.Set();

                // This 'Dispatcher' magic keeps the UI alive
                System.Windows.Threading.Dispatcher.Run();
            });

            _thread.SetApartmentState(ApartmentState.STA);
            _thread.IsBackground = true;
            _thread.Start();

            // Wait for the dispatcher to be ready before
            // the constructor returns
            _dispatcherReadyEvent.WaitOne();
        }

        /// <summary>
        /// Executes the specified action asynchronously on the UI thread associated with this instance.
        /// </summary>
        /// <remarks>This method uses <see cref="System.Windows.Threading.Dispatcher.BeginInvoke"/> to
        /// schedule the action, allowing the calling thread to continue execution without blocking. Ensure that the
        /// <paramref name="action"/> is thread-safe and does not rely on the calling thread's context.</remarks>
        /// <param name="action">The action to be executed on the UI thread. Cannot be null.</param>
        public void Run(Action action) {
            if (action is null) { throw new ArgumentNullException(nameof(action)); }

            // Get the dispatcher for the new UI thread
            var dispatcher = System.Windows.Threading.Dispatcher.FromThread(_thread);

            //
            // 'Invoke' is synchronous and blocks the caller (ESAPI Thread)
            // 'BeginInvoke' is asynchronous and does not block.
            //
            // This allows the ESAPI thread to immediately return from
            // this method and continue on to the "Actor" loop
            // where it will wait for messages.
            dispatcher?.BeginInvoke(action);
        }

        public void Dispose() {
            // Ask the UI thread's dispatcher to shut down
            var dispatcher = System.Windows.Threading.Dispatcher.FromThread(_thread);
            dispatcher?.InvokeShutdown();
            _thread.Join(); // Wait for it to close
            _dispatcherReadyEvent.Dispose(); // Clean up event
        }
    }
}
