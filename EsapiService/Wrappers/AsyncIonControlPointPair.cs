using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncIonControlPointPair : IIonControlPointPair
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

        public Task ResizeFinalSpotListAsync(int count) => _service.RunAsync(() => _inner.ResizeFinalSpotList(count));
        public Task ResizeRawSpotListAsync(int count) => _service.RunAsync(() => _inner.ResizeRawSpotList(count));
        public async Task<IIonControlPointParameters> GetEndControlPointAsync()
        {
            return await _service.RunAsync(() => 
                _inner.EndControlPoint is null ? null : new AsyncIonControlPointParameters(_inner.EndControlPoint, _service));
        }
        public async Task<IIonSpotParametersCollection> GetFinalSpotListAsync()
        {
            return await _service.RunAsync(() => 
                _inner.FinalSpotList is null ? null : new AsyncIonSpotParametersCollection(_inner.FinalSpotList, _service));
        }
        public double NominalBeamEnergy { get; }
        public async Task<IIonSpotParametersCollection> GetRawSpotListAsync()
        {
            return await _service.RunAsync(() => 
                _inner.RawSpotList is null ? null : new AsyncIonSpotParametersCollection(_inner.RawSpotList, _service));
        }
        public async Task<IIonControlPointParameters> GetStartControlPointAsync()
        {
            return await _service.RunAsync(() => 
                _inner.StartControlPoint is null ? null : new AsyncIonControlPointParameters(_inner.StartControlPoint, _service));
        }
        public int StartIndex { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonControlPointPair> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonControlPointPair, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
