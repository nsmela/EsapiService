    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    public class AsyncESAPIActionPackAttribute : IESAPIActionPackAttribute
    {
        internal readonly VMS.TPS.Common.Model.API.ESAPIActionPackAttribute _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncESAPIActionPackAttribute(VMS.TPS.Common.Model.API.ESAPIActionPackAttribute inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

        }

        public bool IsWriteable => _inner.IsWriteable;
        public async Task SetIsWriteableAsync(bool value) => _service.RunAsync(() => _inner.IsWriteable = value);

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ESAPIActionPackAttribute> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ESAPIActionPackAttribute, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
