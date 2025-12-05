namespace EsapiService.Wrappers
{
    public class AsyncOptimizationNormalTissueParameter : IOptimizationNormalTissueParameter
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationNormalTissueParameter(VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            DistanceFromTargetBorderInMM = inner.DistanceFromTargetBorderInMM;
            EndDosePercentage = inner.EndDosePercentage;
            FallOff = inner.FallOff;
            IsAutomatic = inner.IsAutomatic;
            IsAutomaticSbrt = inner.IsAutomaticSbrt;
            IsAutomaticSrs = inner.IsAutomaticSrs;
            Priority = inner.Priority;
            StartDosePercentage = inner.StartDosePercentage;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public double DistanceFromTargetBorderInMM { get; }
        public double EndDosePercentage { get; }
        public double FallOff { get; }
        public bool IsAutomatic { get; }
        public bool IsAutomaticSbrt { get; }
        public bool IsAutomaticSrs { get; }
        public double Priority { get; }
        public double StartDosePercentage { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
