using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TestWpf;

namespace StandaloneDebug
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                // ENVIRONMENT CHECK
                // We check for the DLLs explicitly before attempting to run any logic 
                // that would trigger the JIT compiler to load the Varian assemblies.
                if (!IsEsapiAvailable()) {
                    MessageBox.Show(
                        "Critical Error: ESAPI Assemblies (VMS.TPS.Common.Model.API.dll) are missing.\n\n" +
                        "Please ensure you are running this application on a Varian workstation or have copied the required DLLs to the application folder.",
                        "ESAPI Environment Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                
                    return;
                }

                string patientId = string.Empty;
                string planId = string.Empty;

                var servicesResult = AppStartup.ConfigureServices();
                if (servicesResult.IsFailure)
                {
                    MessageBox.Show("Service generation failed: ");
                    return;
                }

                var services = servicesResult.Value;

                AppStartup.Start(patientId, planId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Startup Error: {ex.Message}\n\n{ex.StackTrace}");
            }
            finally
            {
                Shutdown();
            }

        }

        private static bool IsEsapiAvailable()
        {
            const string dir = "C:\\Program Files (x86)\\Varian\\RTM\\18.0\\esapi\\API";
            string appDir = AppDomain.CurrentDomain.BaseDirectory;

            // Check for the two core DLLs
            string apiPath = Path.Combine(dir, "VMS.TPS.Common.Model.API.dll");
            string typesPath = Path.Combine(dir, "VMS.TPS.Common.Model.Types.dll");

            return File.Exists(apiPath) && File.Exists(typesPath);
        }
    }
}
