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
    public class AsyncIonControlPointParameters : AsyncControlPointParameters, IIonControlPointParameters, IEsapiWrapper<VMS.TPS.Common.Model.API.IonControlPointParameters>
    {
        internal new readonly VMS.TPS.Common.Model.API.IonControlPointParameters _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncIonControlPointParameters(VMS.TPS.Common.Model.API.IonControlPointParameters inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            SnoutPosition = inner.SnoutPosition;
        }

        public async Task<IIonSpotParametersCollection> GetFinalSpotListAsync()
        {
            return await _service.PostAsync(context => 
                _inner.FinalSpotList is null ? null : new AsyncIonSpotParametersCollection(_inner.FinalSpotList, _service));
        }

        public async Task<IIonSpotParametersCollection> GetRawSpotListAsync()
        {
            return await _service.PostAsync(context => 
                _inner.RawSpotList is null ? null : new AsyncIonSpotParametersCollection(_inner.RawSpotList, _service));
        }

        public double SnoutPosition { get; private set; }
        public async Task SetSnoutPositionAsync(double value)
        {
            SnoutPosition = await _service.PostAsync(context => 
            {
                _inner.SnoutPosition = value;
                return _inner.SnoutPosition;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonControlPointParameters> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonControlPointParameters, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.IonControlPointParameters(AsyncIonControlPointParameters wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.IonControlPointParameters IEsapiWrapper<VMS.TPS.Common.Model.API.IonControlPointParameters>.Inner => _inner;
    }
}
