namespace EsapiService.Wrappers
{
    public class AsyncAddOnMaterial : IAddOnMaterial
    {
        internal readonly VMS.TPS.Common.Model.API.AddOnMaterial _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncAddOnMaterial(VMS.TPS.Common.Model.API.AddOnMaterial inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.AddOnMaterial> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.AddOnMaterial, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
