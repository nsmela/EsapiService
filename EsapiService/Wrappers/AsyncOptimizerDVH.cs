namespace EsapiService.Wrappers
{
    public class AsyncOptimizerDVH : IOptimizerDVH
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizerDVH _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncOptimizerDVH(VMS.TPS.Common.Model.API.OptimizerDVH inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

            CurveData = inner.CurveData;
        }

        public VMS.TPS.Common.Model.Types.DVHPoint[] CurveData { get; }
        public IStructure Structure => _inner.Structure is null ? null : new AsyncStructure(_inner.Structure, _service);


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizerDVH> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizerDVH, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
