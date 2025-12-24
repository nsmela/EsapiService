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
    public class AsyncOptimizationIMRTBeamParameter : AsyncOptimizationParameter, IOptimizationIMRTBeamParameter, IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationIMRTBeamParameter>
    {
        internal new readonly VMS.TPS.Common.Model.API.OptimizationIMRTBeamParameter _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationIMRTBeamParameter(VMS.TPS.Common.Model.API.OptimizationIMRTBeamParameter inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public async Task<IBeam> GetBeamAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Beam is null ? null : new AsyncBeam(_inner.Beam, _service);
                return innerResult;
            });
        }

        public string BeamId =>
            _inner.BeamId;


        public bool Excluded =>
            _inner.Excluded;


        public bool FixedJaws =>
            _inner.FixedJaws;


        public double SmoothX =>
            _inner.SmoothX;


        public double SmoothY =>
            _inner.SmoothY;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationIMRTBeamParameter> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationIMRTBeamParameter, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.OptimizationIMRTBeamParameter(AsyncOptimizationIMRTBeamParameter wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.OptimizationIMRTBeamParameter IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationIMRTBeamParameter>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationIMRTBeamParameter>.Service => _service;
    }
}
