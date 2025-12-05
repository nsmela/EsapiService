namespace EsapiService.Wrappers
{
    public class AsyncOptimizationJawTrackingUsedParameter : IOptimizationJawTrackingUsedParameter
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizationJawTrackingUsedParameter _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationJawTrackingUsedParameter(VMS.TPS.Common.Model.API.OptimizationJawTrackingUsedParameter inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationJawTrackingUsedParameter> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationJawTrackingUsedParameter, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
