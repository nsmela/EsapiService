namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
    public class AsyncScriptEnvironment : IScriptEnvironment
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
        }

        public void ExecuteScript(System.Reflection.Assembly scriptAssembly, VMS.TPS.Common.Model.API.ScriptContext scriptContext, System.Windows.Window window) => _inner.ExecuteScript(scriptAssembly, scriptContext, window);
        public string ApplicationName { get; }
        public string VersionInfo { get; }
        public string ApiVersionInfo { get; }
        public System.Collections.Generic.IReadOnlyList<IApplicationScript> Scripts => _inner.Scripts?.Select(x => new AsyncApplicationScript(x, _service)).ToList();
        public System.Collections.Generic.IReadOnlyList<IApplicationPackage> Packages => _inner.Packages?.Select(x => new AsyncApplicationPackage(x, _service)).ToList();

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ScriptEnvironment> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ScriptEnvironment, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
