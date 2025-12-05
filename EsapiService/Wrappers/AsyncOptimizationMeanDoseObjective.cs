    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    public class AsyncOptimizationMeanDoseObjective : IOptimizationMeanDoseObjective
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizationMeanDoseObjective _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationMeanDoseObjective(VMS.TPS.Common.Model.API.OptimizationMeanDoseObjective inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Dose = inner.Dose;
        }

        public DoseValue Dose { get; }
        public bool IsRobustObjective => _inner.IsRobustObjective;
        public async Task SetIsRobustObjectiveAsync(bool value) => _service.RunAsync(() => _inner.IsRobustObjective = value);

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationMeanDoseObjective> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationMeanDoseObjective, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
