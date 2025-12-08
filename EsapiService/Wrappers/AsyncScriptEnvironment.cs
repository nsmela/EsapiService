using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
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


        public Task ExecuteScriptAsync(Reflection.Assembly scriptAssembly, IScriptContext scriptContext, Windows.Window window) => _service.RunAsync(() => _inner.ExecuteScript(scriptAssembly, scriptContext, window));

        public string ApplicationName { get; }

        public string VersionInfo { get; }

        public string ApiVersionInfo { get; }

        public async Task<IReadOnlyList<IApplicationScript>> GetScriptsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Scripts?.Select(x => new AsyncApplicationScript(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IApplicationPackage>> GetPackagesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Packages?.Select(x => new AsyncApplicationPackage(x, _service)).ToList());
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ScriptEnvironment> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ScriptEnvironment, T> func) => _service.RunAsync(() => func(_inner));
    }
}
