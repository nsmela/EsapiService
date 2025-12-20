using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Esapi.Services;
using Application = VMS.TPS.Common.Model.API.Application;

namespace Esapi.Services.Runners
{
    /// <summary>
    /// A runner specifically designed for Integration Tests.
    /// It maintains a long-lived Varian Application instance on a dedicated STA thread,
    /// but executes test delegates on background threads to prevent deadlocks.
    /// </summary>
    public static class TestRunner
    {
        private static Application _app;
        private static Thread _esapiThread;
        private static Dispatcher _esapiDispatcher;
        private static readonly ManualResetEventSlim _readySignal = new ManualResetEventSlim(false);

        /// <summary>
        /// Initializes the Varian Application. Call this once in [AssemblyInitialize].
        /// </summary>
        public static void Initialize()
        {
            if (_app != null) return;

            // 1. Spin up the dedicated ESAPI thread
            _esapiThread = new Thread(() =>
            {
                try
                {
                    // 2. Create the Application (Must be done on STA thread)
                    _app = Application.CreateApplication();

                    // Capture the dispatcher for this thread so we can Invoke back to it later
                    _esapiDispatcher = Dispatcher.CurrentDispatcher;

                    // 3. Signal that ESAPI is ready
                    _readySignal.Set();

                    // 4. Start the Message Pump
                    // This keeps the thread alive and processing COM messages/Dispatcher requests
                    Dispatcher.Run();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[TestRunner] Failed to start ESAPI: {ex}");
                    _readySignal.Set(); // Unblock waiting threads even on failure
                }
            });

            _esapiThread.SetApartmentState(ApartmentState.STA);
            _esapiThread.IsBackground = true; // Ensures thread dies if test runner crashes
            _esapiThread.Start();

            // Block the Test Framework thread until Varian is loaded
            _readySignal.Wait();

            if (_app == null)
            {
                throw new InvalidOperationException("TestRunner failed to initialize Varian Application. Check credentials or license.");
            }
        }

        /// <summary>
        /// Executes a test action.
        /// </summary>
        /// <param name="testAction">The async test logic to run. Receives an IEsapiService.</param>
        public static async Task RunAsync(Func<IEsapiService, Task> testAction)
        {
            if (_app == null || _esapiDispatcher == null)
                throw new InvalidOperationException("TestRunner not initialized. Call TestRunner.Initialize() first.");

            // 1. Prepare the Service Context (Must happen on ESAPI Thread)
            // We invoke onto the STA thread to create the context wrapper
            IEsapiService service = await _esapiDispatcher.InvokeAsync(() =>
            {
                var context = new StandaloneContext(_app, null, null);
                return new EsapiService(context);
            });

            // 2. Execute the Test Logic (Must happen on Background Thread)
            // This mirrors UiRunner: We offload the "User Code" to a Task.
            // This ensures that when the test calls 'await service.GetPlanAsync()', 
            // the main thread is free to process that request.
            await Task.Run(async () =>
            {
                try
                {
                    // The Code within the try-catch block you requested:
                    await testAction(service);
                }
                catch (Exception ex)
                {
                    // We catch exceptions here to ensure they bubble up 
                    // cleanly to the Unit Test framework wrapped in a Task exception.
                    throw new Exception($"Integration Test Failed: {ex.Message}", ex);
                }
            });
        }

        /// <summary>
        /// Cleans up the Application. Call this in [AssemblyCleanup].
        /// </summary>
        public static void Dispose()
        {
            if (_esapiDispatcher != null && !_esapiDispatcher.HasShutdownStarted)
            {
                // Marshal the shutdown command to the STA thread
                _esapiDispatcher.InvokeShutdown();

                // Wait for thread to exit
                _esapiThread?.Join(TimeSpan.FromSeconds(5));

                _app?.Dispose();
                _app = null;
                _esapiDispatcher = null;
            }
        }
    }
}