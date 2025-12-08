using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncIonSpotParameters : IIonSpotParameters
    {
        internal readonly VMS.TPS.Common.Model.API.IonSpotParameters _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncIonSpotParameters(VMS.TPS.Common.Model.API.IonSpotParameters inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Weight = inner.Weight;
            X = inner.X;
            Y = inner.Y;
        }


        public float Weight { get; private set; }
        public async Task SetWeightAsync(float value)
        {
            Weight = await _service.RunAsync(() =>
            {
                _inner.Weight = value;
                return _inner.Weight;
            });
        }

        public float X { get; private set; }
        public async Task SetXAsync(float value)
        {
            X = await _service.RunAsync(() =>
            {
                _inner.X = value;
                return _inner.X;
            });
        }

        public float Y { get; private set; }
        public async Task SetYAsync(float value)
        {
            Y = await _service.RunAsync(() =>
            {
                _inner.Y = value;
                return _inner.Y;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonSpotParameters> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonSpotParameters, T> func) => _service.RunAsync(() => func(_inner));
    }
}
