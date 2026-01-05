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
    public partial class AsyncScriptEnvironment : IScriptEnvironment, IEsapiWrapper<VMS.TPS.Common.Model.API.ScriptEnvironment>
    {
        internal readonly VMS.TPS.Common.Model.API.ScriptEnvironment _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncScriptEnvironment(VMS.TPS.Common.Model.API.ScriptEnvironment inner, IEsapiService service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        // Simple Void Method
        public Task ExecuteScriptAsync(System.Reflection.Assembly scriptAssembly, IScriptContext scriptContext, System.Windows.Window window) 
        {
            _service.PostAsync(context => _inner.ExecuteScript(scriptAssembly, ((AsyncScriptContext)scriptContext)._inner, window));
            return Task.CompletedTask;
        }

        public string ApplicationName =>
            _inner.ApplicationName;


        public string VersionInfo =>
            _inner.VersionInfo;


        public string ApiVersionInfo =>
            _inner.ApiVersionInfo;


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

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public bool IsNotValid() => _inner is null;

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
