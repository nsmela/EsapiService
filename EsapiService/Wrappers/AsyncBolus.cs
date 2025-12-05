namespace EsapiService.Wrappers
{
    public class AsyncBolus : IBolus
    {
        internal readonly VMS.TPS.Common.Model.API.Bolus _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncBolus(VMS.TPS.Common.Model.API.Bolus inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Id = inner.Id;
            MaterialCTValue = inner.MaterialCTValue;
            Name = inner.Name;
        }

        public string Id { get; }
        public double MaterialCTValue { get; }
        public string Name { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Bolus> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Bolus, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
