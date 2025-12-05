namespace EsapiService.Wrappers
{
    public class AsyncOptimizationPointCloudParameter : IOptimizationPointCloudParameter
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizationPointCloudParameter _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationPointCloudParameter(VMS.TPS.Common.Model.API.OptimizationPointCloudParameter inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            PointResolutionInMM = inner.PointResolutionInMM;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public double PointResolutionInMM { get; }
        public IStructure Structure => _inner.Structure is null ? null : new AsyncStructure(_inner.Structure, _service);


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationPointCloudParameter> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationPointCloudParameter, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
