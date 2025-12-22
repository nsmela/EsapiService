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
    public class AsyncSegmentVolume : AsyncSerializableObject, ISegmentVolume, IEsapiWrapper<VMS.TPS.Common.Model.API.SegmentVolume>
    {
        internal new readonly VMS.TPS.Common.Model.API.SegmentVolume _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncSegmentVolume(VMS.TPS.Common.Model.API.SegmentVolume inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

        }


        public async Task<ISegmentVolume> AndAsync(ISegmentVolume other)
        {
            return await _service.PostAsync(context => 
                _inner.And(((AsyncSegmentVolume)other)._inner) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public async Task<ISegmentVolume> AsymmetricMarginAsync(AxisAlignedMargins margins)
        {
            return await _service.PostAsync(context => 
                _inner.AsymmetricMargin(margins) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public async Task<ISegmentVolume> MarginAsync(double marginInMM)
        {
            return await _service.PostAsync(context => 
                _inner.Margin(marginInMM) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public async Task<ISegmentVolume> NotAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Not() is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public async Task<ISegmentVolume> OrAsync(ISegmentVolume other)
        {
            return await _service.PostAsync(context => 
                _inner.Or(((AsyncSegmentVolume)other)._inner) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public async Task<ISegmentVolume> SubAsync(ISegmentVolume other)
        {
            return await _service.PostAsync(context => 
                _inner.Sub(((AsyncSegmentVolume)other)._inner) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public async Task<ISegmentVolume> XorAsync(ISegmentVolume other)
        {
            return await _service.PostAsync(context => 
                _inner.Xor(((AsyncSegmentVolume)other)._inner) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.SegmentVolume> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.SegmentVolume, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.SegmentVolume(AsyncSegmentVolume wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.SegmentVolume IEsapiWrapper<VMS.TPS.Common.Model.API.SegmentVolume>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.SegmentVolume>.Service => _service;
    }
}
