    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    public class AsyncOptimizationEUDObjective : IOptimizationEUDObjective
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizationEUDObjective _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationEUDObjective(VMS.TPS.Common.Model.API.OptimizationEUDObjective inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Dose = inner.Dose;
            ParameterA = inner.ParameterA;
        }

        public DoseValue Dose { get; }
        public bool IsRobustObjective => _inner.IsRobustObjective;
        public async Task SetIsRobustObjectiveAsync(bool value) => _service.RunAsync(() => _inner.IsRobustObjective = value);
        public double ParameterA { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationEUDObjective> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationEUDObjective, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
