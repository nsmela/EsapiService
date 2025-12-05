namespace EsapiService.Wrappers
{
    public class AsyncSegmentVolume : ISegmentVolume
    {
        internal readonly VMS.TPS.Common.Model.API.SegmentVolume _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncSegmentVolume(VMS.TPS.Common.Model.API.SegmentVolume inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }

        public ISegmentVolume And(ISegmentVolume other) => _inner.And(other) is var result && result is null ? null : new AsyncSegmentVolume(result, _service);
        public ISegmentVolume AsymmetricMargin(AxisAlignedMargins margins) => _inner.AsymmetricMargin(margins) is var result && result is null ? null : new AsyncSegmentVolume(result, _service);
        public ISegmentVolume Margin(double marginInMM) => _inner.Margin(marginInMM) is var result && result is null ? null : new AsyncSegmentVolume(result, _service);
        public ISegmentVolume Not() => _inner.Not() is var result && result is null ? null : new AsyncSegmentVolume(result, _service);
        public ISegmentVolume Or(ISegmentVolume other) => _inner.Or(other) is var result && result is null ? null : new AsyncSegmentVolume(result, _service);
        public ISegmentVolume Sub(ISegmentVolume other) => _inner.Sub(other) is var result && result is null ? null : new AsyncSegmentVolume(result, _service);
        public ISegmentVolume Xor(ISegmentVolume other) => _inner.Xor(other) is var result && result is null ? null : new AsyncSegmentVolume(result, _service);

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.SegmentVolume> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.SegmentVolume, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
