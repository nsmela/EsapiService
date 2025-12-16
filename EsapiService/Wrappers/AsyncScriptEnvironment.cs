using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public sealed class AsyncScriptEnvironment : IScriptEnvironment, IEsapiWrapper<VMS.TPS.Common.Model.API.ScriptEnvironment>
    {
        internal readonly VMS.TPS.Common.Model.API.ScriptEnvironment _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

public AsyncScriptEnvironment(VMS.TPS.Common.Model.API.ScriptEnvironment inner, IEsapiService service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            ApplicationName = inner.ApplicationName;
            VersionInfo = inner.VersionInfo;
            ApiVersionInfo = inner.ApiVersionInfo;
        }

        // Simple Void Method
        public Task ExecuteScriptAsync(System.Reflection.Assembly scriptAssembly, IScriptContext scriptContext, System.Windows.Window window) =>
            _service.PostAsync(context => _inner.ExecuteScript(scriptAssembly, ((AsyncScriptContext)scriptContext)._inner, window));

        public string ApplicationName { get; }

        public string VersionInfo { get; }

        public string ApiVersionInfo { get; }

        public async Task<IReadOnlyList<IApplicationScript>> GetScriptsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Scripts?.Select(x => new AsyncApplicationScript(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IApplicationPackage>> GetPackagesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Packages?.Select(x => new AsyncApplicationPackage(x, _service)).ToList());
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ScriptEnvironment> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ScriptEnvironment, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.ScriptEnvironment(AsyncScriptEnvironment wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ScriptEnvironment IEsapiWrapper<VMS.TPS.Common.Model.API.ScriptEnvironment>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.ScriptEnvironment>.Service => _service;

        /* --- Skipped Members (Not generated) ---
           - .ctor: Explicitly ignored by name
        */
    }
}
