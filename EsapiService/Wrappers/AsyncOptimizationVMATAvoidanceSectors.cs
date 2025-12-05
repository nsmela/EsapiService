namespace EsapiService.Wrappers
{
    public class AsyncOptimizationVMATAvoidanceSectors : IOptimizationVMATAvoidanceSectors
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizationVMATAvoidanceSectors _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationVMATAvoidanceSectors(VMS.TPS.Common.Model.API.OptimizationVMATAvoidanceSectors inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            AvoidanceSector1 = inner.AvoidanceSector1;
            AvoidanceSector2 = inner.AvoidanceSector2;
            IsValid = inner.IsValid;
            ValidationError = inner.ValidationError;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public VMS.TPS.Common.Model.Types.OptimizationAvoidanceSector AvoidanceSector1 { get; }
        public VMS.TPS.Common.Model.Types.OptimizationAvoidanceSector AvoidanceSector2 { get; }
        public IBeam Beam => _inner.Beam is null ? null : new AsyncBeam(_inner.Beam, _service);

        public bool IsValid { get; }
        public string ValidationError { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationVMATAvoidanceSectors> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationVMATAvoidanceSectors, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
