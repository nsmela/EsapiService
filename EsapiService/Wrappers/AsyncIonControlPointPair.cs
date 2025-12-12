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
    public class AsyncIonControlPointPair : IIonControlPointPair, IEsapiWrapper<VMS.TPS.Common.Model.API.IonControlPointPair>
    {
        internal readonly VMS.TPS.Common.Model.API.IonControlPointPair _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

public AsyncIonControlPointPair(VMS.TPS.Common.Model.API.IonControlPointPair inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

            NominalBeamEnergy = inner.NominalBeamEnergy;
            StartIndex = inner.StartIndex;
        }

        // Simple Void Method
        public Task ResizeFinalSpotListAsync(int count) => _service.PostAsync(context => _inner.ResizeFinalSpotList(count));

        // Simple Void Method
        public Task ResizeRawSpotListAsync(int count) => _service.PostAsync(context => _inner.ResizeRawSpotList(count));

        public async Task<IIonControlPointParameters> GetEndControlPointAsync()
        {
            return await _service.PostAsync(context => 
                _inner.EndControlPoint is null ? null : new AsyncIonControlPointParameters(_inner.EndControlPoint, _service));
        }

        public async Task<IIonSpotParametersCollection> GetFinalSpotListAsync()
        {
            return await _service.PostAsync(context => 
                _inner.FinalSpotList is null ? null : new AsyncIonSpotParametersCollection(_inner.FinalSpotList, _service));
        }

        public double NominalBeamEnergy { get; }

        public async Task<IIonSpotParametersCollection> GetRawSpotListAsync()
        {
            return await _service.PostAsync(context => 
                _inner.RawSpotList is null ? null : new AsyncIonSpotParametersCollection(_inner.RawSpotList, _service));
        }

        public async Task<IIonControlPointParameters> GetStartControlPointAsync()
        {
            return await _service.PostAsync(context => 
                _inner.StartControlPoint is null ? null : new AsyncIonControlPointParameters(_inner.StartControlPoint, _service));
        }

        public int StartIndex { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonControlPointPair> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonControlPointPair, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.IonControlPointPair(AsyncIonControlPointPair wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.IonControlPointPair IEsapiWrapper<VMS.TPS.Common.Model.API.IonControlPointPair>.Inner => _inner;
    }
}
