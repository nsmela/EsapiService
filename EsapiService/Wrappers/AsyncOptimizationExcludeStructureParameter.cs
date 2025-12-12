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
    public class AsyncOptimizationExcludeStructureParameter : AsyncOptimizationParameter, IOptimizationExcludeStructureParameter, IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationExcludeStructureParameter>
    {
        internal new readonly VMS.TPS.Common.Model.API.OptimizationExcludeStructureParameter _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncOptimizationExcludeStructureParameter(VMS.TPS.Common.Model.API.OptimizationExcludeStructureParameter inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }

        public async Task<IStructure> GetStructureAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Structure is null ? null : new AsyncStructure(_inner.Structure, _service));
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationExcludeStructureParameter> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationExcludeStructureParameter, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.OptimizationExcludeStructureParameter(AsyncOptimizationExcludeStructureParameter wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.OptimizationExcludeStructureParameter IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationExcludeStructureParameter>.Inner => _inner;
    }
}
