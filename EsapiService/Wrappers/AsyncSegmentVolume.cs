using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncSegmentVolume : AsyncSerializableObject, ISegmentVolume
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


        public async Task<ISegmentVolume> AndAsync(ISegmentVolume other)
        {
            return await _service.RunAsync(() => 
                _inner.And(other) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public async Task<ISegmentVolume> AsymmetricMarginAsync(AxisAlignedMargins margins)
        {
            return await _service.RunAsync(() => 
                _inner.AsymmetricMargin(margins) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public async Task<ISegmentVolume> MarginAsync(double marginInMM)
        {
            return await _service.RunAsync(() => 
                _inner.Margin(marginInMM) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public async Task<ISegmentVolume> NotAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Not() is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public async Task<ISegmentVolume> OrAsync(ISegmentVolume other)
        {
            return await _service.RunAsync(() => 
                _inner.Or(other) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public async Task<ISegmentVolume> SubAsync(ISegmentVolume other)
        {
            return await _service.RunAsync(() => 
                _inner.Sub(other) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public async Task<ISegmentVolume> XorAsync(ISegmentVolume other)
        {
            return await _service.RunAsync(() => 
                _inner.Xor(other) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.SegmentVolume> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.SegmentVolume, T> func) => _service.RunAsync(() => func(_inner));
    }
}
