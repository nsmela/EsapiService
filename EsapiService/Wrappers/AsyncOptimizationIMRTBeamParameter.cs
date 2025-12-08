using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncOptimizationIMRTBeamParameter : IOptimizationIMRTBeamParameter
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizationIMRTBeamParameter _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationIMRTBeamParameter(VMS.TPS.Common.Model.API.OptimizationIMRTBeamParameter inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            BeamId = inner.BeamId;
            Excluded = inner.Excluded;
            FixedJaws = inner.FixedJaws;
            SmoothX = inner.SmoothX;
            SmoothY = inner.SmoothY;
        }

        public async Task<IBeam> GetBeamAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Beam is null ? null : new AsyncBeam(_inner.Beam, _service));
        }
        public string BeamId { get; }
        public bool Excluded { get; }
        public bool FixedJaws { get; }
        public double SmoothX { get; }
        public double SmoothY { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationIMRTBeamParameter> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationIMRTBeamParameter, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
