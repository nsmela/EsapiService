namespace EsapiService.Wrappers
{
    public class AsyncRangeModulator : IRangeModulator
    {
        internal readonly VMS.TPS.Common.Model.API.RangeModulator _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncRangeModulator(VMS.TPS.Common.Model.API.RangeModulator inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Type = inner.Type;
        }

        public RangeModulatorType Type { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RangeModulator> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RangeModulator, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
