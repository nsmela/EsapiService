using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncGlobals : IGlobals
    {
        internal readonly VMS.TPS.Common.Model.API.Globals _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncGlobals(VMS.TPS.Common.Model.API.Globals inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Globals> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Globals, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
