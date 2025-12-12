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
    public class AsyncScriptEnvironment : IScriptEnvironment, IEsapiWrapper<VMS.TPS.Common.Model.API.ScriptEnvironment>
    {
        internal readonly VMS.TPS.Common.Model.API.ScriptEnvironment _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

public AsyncScriptEnvironment(VMS.TPS.Common.Model.API.ScriptEnvironment inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

            ApplicationName = inner.ApplicationName;
            VersionInfo = inner.VersionInfo;
            ApiVersionInfo = inner.ApiVersionInfo;
            Scripts = inner.Scripts;
            Packages = inner.Packages;
        }

        // Simple Void Method
        public Task ExecuteScriptAsync(System.Reflection.Assembly scriptAssembly, IScriptContext scriptContext, System.Windows.Window window) => _service.PostAsync(context => _inner.ExecuteScript(scriptAssembly, ((AsyncScriptContext)scriptContext)._inner, window));

        public string ApplicationName { get; }

        public string VersionInfo { get; }

        public string ApiVersionInfo { get; }

        public IEnumerable<ApplicationScript> Scripts { get; }

        public IEnumerable<ApplicationPackage> Packages { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ScriptEnvironment> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ScriptEnvironment, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.ScriptEnvironment(AsyncScriptEnvironment wrapper) => wrapper._inner;
        // Internal Explicit Implementation to expose _inner safely
        VMS.TPS.Common.Model.API.ScriptEnvironment IEsapiWrapper<VMS.TPS.Common.Model.API.ScriptEnvironment>.Inner => _inner;
    }
}
