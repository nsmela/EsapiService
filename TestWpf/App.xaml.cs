using Esapi.Services.Runners;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows;
using TestWpf.Ui;

namespace TestWpf {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        public App() {
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

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

                Application.Current.Shutdown();
                return;
            }
        }

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            StandaloneRunner.Run<MainWindow>();
        }

        private static bool IsEsapiAvailable() {
            string appDir = AppDomain.CurrentDomain.BaseDirectory;

            // Check for the two core DLLs
            string apiPath = Path.Combine("C:/Program Files(x86)/Varian/API/", "VMS.TPS.Common.Model.API.dll");
            string typesPath = Path.Combine("C:/Program Files(x86)/Varian/API/", "VMS.TPS.Common.Model.Types.dll");

            return File.Exists(apiPath) && File.Exists(typesPath);
        }
    }
}
