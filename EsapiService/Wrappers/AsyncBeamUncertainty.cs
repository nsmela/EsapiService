namespace EsapiService.Wrappers
{
    public class AsyncBeamUncertainty : IBeamUncertainty
    {
        internal readonly VMS.TPS.Common.Model.API.BeamUncertainty _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncBeamUncertainty(VMS.TPS.Common.Model.API.BeamUncertainty inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            BeamNumber = inner.BeamNumber;
        }

        public IBeam Beam => _inner.Beam is null ? null : new AsyncBeam(_inner.Beam, _service);

        public BeamNumber BeamNumber { get; }
        public IDose Dose => _inner.Dose is null ? null : new AsyncDose(_inner.Dose, _service);


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BeamUncertainty> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BeamUncertainty, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
