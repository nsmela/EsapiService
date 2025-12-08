using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

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

            SnoutPosition = inner.SnoutPosition;
        }


        public async Task<IIonSpotParametersCollection> GetFinalSpotListAsync()
        {
            return await _service.RunAsync(() => 
                _inner.FinalSpotList is null ? null : new AsyncIonSpotParametersCollection(_inner.FinalSpotList, _service));
        }

        public async Task<IIonSpotParametersCollection> GetRawSpotListAsync()
        {
            return await _service.RunAsync(() => 
                _inner.RawSpotList is null ? null : new AsyncIonSpotParametersCollection(_inner.RawSpotList, _service));
        }

        public double SnoutPosition { get; private set; }
        public async Task SetSnoutPositionAsync(double value)
        {
            SnoutPosition = await _service.RunAsync(() =>
            {
                _inner.SnoutPosition = value;
                return _inner.SnoutPosition;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonControlPointParameters> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonControlPointParameters, T> func) => _service.RunAsync(() => func(_inner));
    }
}
