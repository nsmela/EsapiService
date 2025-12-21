using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;

namespace Esapi.Services.Runners
{
    /// <summary>
    /// The Per-Test Runner. 
    /// Mimics <see cref="StandaloneRunner"/> by setting up DI and the Context for a specific run.
    /// </summary>
    public static class IntegrationTestRunner
    {
        public static async Task RunAsync(
            string patientId = null,
            string planId = null,
            Action<IServiceCollection> configureServices = null,
            Func<IEsapiService, Task> testBody = null)
        {
            // 1. Prepare the Environment (Load Patient)
            // This sends a message to the Global Loop to switch the current patient.
            await TestRunner.LoadContextAsync(patientId, planId);

            // 2. Setup DI
            var services = new ServiceCollection();
            configureServices?.Invoke(services);

            // 3. Register Core Services
            // We use the Global Mailbox managed by TestRunner
            services.AddSingleton(TestRunner.Mailbox);

            // IMPORTANT: In production, you register EsapiService.
            // Ensure you are registering the class that writes to the mailbox.
            services.AddSingleton<IEsapiService, Esapi.Services.EsapiService>();

            var provider = services.BuildServiceProvider();

            // 4. Resolve the Service
            var service = provider.GetRequiredService<IEsapiService>();

            // 5. Execute the Test Logic
            // We run this on the ThreadPool so we don't block NUnit.
            // The TestRunner thread handles the ESAPI work in the background.
            await Task.Run(async () =>
            {
                try
                {
                    if (testBody != null) await testBody(service);
                }
                finally
                {
                    if (provider is IDisposable d) d.Dispose();
                }
            });
        }
    }
}