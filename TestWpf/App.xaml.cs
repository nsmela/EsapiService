using Esapi.Services.Runners;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System;
using System.IO;
using System.Windows;
using TestWpf.Ui;

namespace TestWpf {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public class DebugSettings
        {
            public string PatientId { get; set; }
            public string PlanId { get; set; }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                var settings = LoadDebugSettings();
                string patientId = settings.PatientId;
                string planId = settings.PlanId;

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

        private static DebugSettings LoadDebugSettings()
        {
            // Default Fallback (safe to share, or empty)
            var defaults = new DebugSettings();

            string configPath = "local.settings.json";

            if (File.Exists(configPath))
            {
                try
                {
                    string json = File.ReadAllText(configPath);
                    var loaded = JsonSerializer.Deserialize<DebugSettings>(json);
                    if (loaded != null) return loaded;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading local.settings.json: {ex.Message}. Using defaults.");
                }
            }

            return defaults;
        }
    }
}
