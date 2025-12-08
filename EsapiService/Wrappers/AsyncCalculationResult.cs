using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncCalculationResult : ICalculationResult
    {
        internal readonly VMS.TPS.Common.Model.API.CalculationResult _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncCalculationResult(VMS.TPS.Common.Model.API.CalculationResult inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

            Success = inner.Success;
        }

        public bool Success { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.CalculationResult> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.CalculationResult, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
