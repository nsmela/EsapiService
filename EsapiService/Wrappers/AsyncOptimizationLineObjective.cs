namespace EsapiService.Wrappers
{
    public class AsyncOptimizationLineObjective : IOptimizationLineObjective
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizationLineObjective _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationLineObjective(VMS.TPS.Common.Model.API.OptimizationLineObjective inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            CurveData = inner.CurveData;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public VMS.TPS.Common.Model.Types.DVHPoint[] CurveData { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationLineObjective> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationLineObjective, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
