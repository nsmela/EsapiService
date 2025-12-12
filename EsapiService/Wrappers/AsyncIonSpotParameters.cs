using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncIonSpotParameters : AsyncSerializableObject, IIonSpotParameters, IEsapiWrapper<VMS.TPS.Common.Model.API.IonSpotParameters>
    {
        internal new readonly VMS.TPS.Common.Model.API.IonSpotParameters _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncIonSpotParameters(VMS.TPS.Common.Model.API.IonSpotParameters inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            Weight = inner.Weight;
            X = inner.X;
            Y = inner.Y;
        }

        public float Weight { get; private set; }
        public async Task SetWeightAsync(float value)
        {
            Weight = await _service.PostAsync(context => 
            {
                _inner.Weight = value;
                return _inner.Weight;
            });
        }

        public float X { get; private set; }
        public async Task SetXAsync(float value)
        {
            X = await _service.PostAsync(context => 
            {
                _inner.X = value;
                return _inner.X;
            });
        }

        public float Y { get; private set; }
        public async Task SetYAsync(float value)
        {
            Y = await _service.PostAsync(context => 
            {
                _inner.Y = value;
                return _inner.Y;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonSpotParameters> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonSpotParameters, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.IonSpotParameters(AsyncIonSpotParameters wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.IonSpotParameters IEsapiWrapper<VMS.TPS.Common.Model.API.IonSpotParameters>.Inner => _inner;
    }
}
