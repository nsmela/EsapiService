using Esapi.Services.Runners;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TestWpf.Ui;

namespace TestWpf {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            StandaloneRunner.Run<MainWindow>("", "", new ServiceCollection());
        }
    }
}
