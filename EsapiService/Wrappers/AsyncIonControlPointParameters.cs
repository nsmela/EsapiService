    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    public class AsyncIonControlPointParameters : IIonControlPointParameters
    {
        internal readonly VMS.TPS.Common.Model.API.IonControlPointParameters _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncIonControlPointParameters(VMS.TPS.Common.Model.API.IonControlPointParameters inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }

        public IIonSpotParametersCollection FinalSpotList => _inner.FinalSpotList is null ? null : new AsyncIonSpotParametersCollection(_inner.FinalSpotList, _service);

        public IIonSpotParametersCollection RawSpotList => _inner.RawSpotList is null ? null : new AsyncIonSpotParametersCollection(_inner.RawSpotList, _service);

        public double SnoutPosition => _inner.SnoutPosition;
        public async Task SetSnoutPositionAsync(double value) => _service.RunAsync(() => _inner.SnoutPosition = value);

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonControlPointParameters> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonControlPointParameters, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
