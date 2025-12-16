using Esapi.Services.Runners;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWpf.Common;
using TestWpf.Ui;
using VMS.TPS.Common.Model.API;

namespace TestWpf
{
    public static class AppStartup
    {
        public static void ConfigureServices(IServiceCollection services = null)
        {
            if (services is null) services = new ServiceCollection();
            services.AddSingleton<MainViewModel>();
        }

        /// <summary>
        /// Register services required
        /// </summary>
        /// <returns></returns>
        public static Result<IServiceCollection> ConfigureServices()
        {
            var services = new ServiceCollection();

            //// 1. App Settings
            //AppSettings settings;
            //
            //var settingsResult = AppSettings.Load(new SystemIOFileSystem());
            //if (settingsResult.IsFailure)
            //{
            //    var errors = String.Join("\r\n", settingsResult.Errors.Select(e => e.Message));
            //    return $"Error loading app settings: \r\n{errors}";
            //}
            //else
            //{
            //    settings = settingsResult.Value;
            //}
            //services.AddSingleton<IAppSettings>(settings);
            //
            //// 2. External Libraries
            //var serilogger = Serilogger.CreateSerilogLogger(settings);
            //services.AddLogging(builder =>
            //{
            //    builder.ClearProviders();
            //    builder.AddSerilog(serilogger, dispose: true);
            //});

            // 3. Core Infrastructure
            //services.AddSingleton<IFileSystem, SystemIOFileSystem>();
            services.AddTransient<MainWindow>();
            services.AddTransient<MainViewModel>();

            return services;
        }

        public static void Start(string patientId, string planId)
        {
            StandaloneRunner.Run<MainWindow>(
                configureServices: ConfigureServices);
        }

        public static void Start(string patientId, string planId, IServiceCollection services)
        {
            ConfigureServices(services);

            StandaloneRunner.Run<MainWindow>(patientId, planId, services);
        }
    }
}
