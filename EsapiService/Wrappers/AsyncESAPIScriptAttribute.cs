using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

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

            IsWriteable = inner.IsWriteable;
        }


        public bool IsWriteable { get; private set; }
        public async Task SetIsWriteableAsync(bool value)
        {
            IsWriteable = await _service.RunAsync(() =>
            {
                _inner.IsWriteable = value;
                return _inner.IsWriteable;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ESAPIScriptAttribute> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ESAPIScriptAttribute, T> func) => _service.RunAsync(() => func(_inner));
    }
}
