    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    public class AsyncESAPIScriptAttribute : IESAPIScriptAttribute
    {
        internal readonly VMS.TPS.Common.Model.API.ESAPIScriptAttribute _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncESAPIScriptAttribute(VMS.TPS.Common.Model.API.ESAPIScriptAttribute inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

        }

        public bool IsWriteable => _inner.IsWriteable;
        public async Task SetIsWriteableAsync(bool value) => _service.RunAsync(() => _inner.IsWriteable = value);

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ESAPIScriptAttribute> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ESAPIScriptAttribute, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
