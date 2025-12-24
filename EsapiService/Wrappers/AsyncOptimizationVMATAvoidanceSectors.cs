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
    public class AsyncOptimizationVMATAvoidanceSectors : AsyncOptimizationParameter, IOptimizationVMATAvoidanceSectors, IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationVMATAvoidanceSectors>
    {
        internal new readonly VMS.TPS.Common.Model.API.OptimizationVMATAvoidanceSectors _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationVMATAvoidanceSectors(VMS.TPS.Common.Model.API.OptimizationVMATAvoidanceSectors inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public OptimizationAvoidanceSector AvoidanceSector1 =>
            _inner.AvoidanceSector1;


        public OptimizationAvoidanceSector AvoidanceSector2 =>
            _inner.AvoidanceSector2;


        public async Task<IBeam> GetBeamAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Beam is null ? null : new AsyncBeam(_inner.Beam, _service);
                return innerResult;
            });
        }

        public bool IsValid =>
            _inner.IsValid;


        public string ValidationError =>
            _inner.ValidationError;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationVMATAvoidanceSectors> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationVMATAvoidanceSectors, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.OptimizationVMATAvoidanceSectors(AsyncOptimizationVMATAvoidanceSectors wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.OptimizationVMATAvoidanceSectors IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationVMATAvoidanceSectors>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationVMATAvoidanceSectors>.Service => _service;
    }
}
