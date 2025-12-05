namespace EsapiService.Wrappers
{
    public class AsyncOptimizationExcludeStructureParameter : IOptimizationExcludeStructureParameter
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizationExcludeStructureParameter _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationExcludeStructureParameter(VMS.TPS.Common.Model.API.OptimizationExcludeStructureParameter inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }

        public IStructure Structure => _inner.Structure is null ? null : new AsyncStructure(_inner.Structure, _service);


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationExcludeStructureParameter> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationExcludeStructureParameter, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
