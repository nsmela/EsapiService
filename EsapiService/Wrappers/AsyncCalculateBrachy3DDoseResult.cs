using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncCalculateBrachy3DDoseResult : ICalculateBrachy3DDoseResult
    {
        internal readonly VMS.TPS.Common.Model.API.CalculateBrachy3DDoseResult _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncCalculateBrachy3DDoseResult(VMS.TPS.Common.Model.API.CalculateBrachy3DDoseResult inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            RoundedDwellTimeAdjustRatio = inner.RoundedDwellTimeAdjustRatio;
            Success = inner.Success;
        }


        public async Task<IReadOnlyList<string>> GetErrorsAsync()
        {
            return await _service.RunAsync(() => _inner.Errors?.ToList());
        }


        public double RoundedDwellTimeAdjustRatio { get; }

        public bool Success { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.CalculateBrachy3DDoseResult> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.CalculateBrachy3DDoseResult, T> func) => _service.RunAsync(() => func(_inner));
    }
}
