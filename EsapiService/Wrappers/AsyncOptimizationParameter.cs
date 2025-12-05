namespace EsapiService.Wrappers
{
    public class AsyncOptimizationParameter : IOptimizationParameter
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizationParameter _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationParameter(VMS.TPS.Common.Model.API.OptimizationParameter inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public bool Equals(object obj) => _inner.Equals(obj);
        public int GetHashCode() => _inner.GetHashCode();

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationParameter> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationParameter, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
