using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncOptimizationVMATAvoidanceSectors : AsyncOptimizationParameter, IOptimizationVMATAvoidanceSectors
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizationVMATAvoidanceSectors _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationVMATAvoidanceSectors(VMS.TPS.Common.Model.API.OptimizationVMATAvoidanceSectors inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            AvoidanceSector1 = inner.AvoidanceSector1;
            AvoidanceSector2 = inner.AvoidanceSector2;
            IsValid = inner.IsValid;
            ValidationError = inner.ValidationError;
        }


        public OptimizationAvoidanceSector AvoidanceSector1 { get; }

        public OptimizationAvoidanceSector AvoidanceSector2 { get; }

        public async Task<IBeam> GetBeamAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Beam is null ? null : new AsyncBeam(_inner.Beam, _service));
        }

        public bool IsValid { get; }

        public string ValidationError { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationVMATAvoidanceSectors> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationVMATAvoidanceSectors, T> func) => _service.RunAsync(() => func(_inner));
    }
}
