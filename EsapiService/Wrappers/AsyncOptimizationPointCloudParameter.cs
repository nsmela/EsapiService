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
    public class AsyncOptimizationPointCloudParameter : AsyncOptimizationParameter, IOptimizationPointCloudParameter, IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationPointCloudParameter>
    {
        internal new readonly VMS.TPS.Common.Model.API.OptimizationPointCloudParameter _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncOptimizationPointCloudParameter(VMS.TPS.Common.Model.API.OptimizationPointCloudParameter inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            PointResolutionInMM = inner.PointResolutionInMM;
        }

        public double PointResolutionInMM { get; }

        public async Task<IStructure> GetStructureAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Structure is null ? null : new AsyncStructure(_inner.Structure, _service));
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationPointCloudParameter> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationPointCloudParameter, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.OptimizationPointCloudParameter(AsyncOptimizationPointCloudParameter wrapper) => wrapper._inner;
        // Internal Explicit Implementation to expose _inner safely
        VMS.TPS.Common.Model.API.OptimizationPointCloudParameter IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationPointCloudParameter>.Inner => _inner;
    }
}
