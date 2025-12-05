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

        public void ResizeFinalSpotList(int count) => _inner.ResizeFinalSpotList(count);
        public void ResizeRawSpotList(int count) => _inner.ResizeRawSpotList(count);
        public IIonControlPointParameters EndControlPoint => _inner.EndControlPoint is null ? null : new AsyncIonControlPointParameters(_inner.EndControlPoint, _service);

        public IIonSpotParametersCollection FinalSpotList => _inner.FinalSpotList is null ? null : new AsyncIonSpotParametersCollection(_inner.FinalSpotList, _service);

        public double NominalBeamEnergy { get; }
        public IIonSpotParametersCollection RawSpotList => _inner.RawSpotList is null ? null : new AsyncIonSpotParametersCollection(_inner.RawSpotList, _service);

        public IIonControlPointParameters StartControlPoint => _inner.StartControlPoint is null ? null : new AsyncIonControlPointParameters(_inner.StartControlPoint, _service);

        public int StartIndex { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonControlPointPair> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonControlPointPair, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
