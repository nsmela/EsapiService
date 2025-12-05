    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    public class AsyncOptimizationPointObjective : IOptimizationPointObjective
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizationPointObjective _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationPointObjective(VMS.TPS.Common.Model.API.OptimizationPointObjective inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Dose = inner.Dose;
            Volume = inner.Volume;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public VMS.TPS.Common.Model.Types.DoseValue Dose { get; }
        public bool IsRobustObjective => _inner.IsRobustObjective;
        public async Task SetIsRobustObjectiveAsync(bool value) => _service.RunAsync(() => _inner.IsRobustObjective = value);
        public double Volume { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationPointObjective> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationPointObjective, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
