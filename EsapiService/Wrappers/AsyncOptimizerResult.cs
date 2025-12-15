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
    public class AsyncOptimizerResult : AsyncCalculationResult, IOptimizerResult, IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizerResult>
    {
        internal new readonly VMS.TPS.Common.Model.API.OptimizerResult _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncOptimizerResult(VMS.TPS.Common.Model.API.OptimizerResult inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            StructureDVHs = inner.StructureDVHs;
            StructureObjectiveValues = inner.StructureObjectiveValues;
            TotalObjectiveFunctionValue = inner.TotalObjectiveFunctionValue;
            NumberOfIMRTOptimizerIterations = inner.NumberOfIMRTOptimizerIterations;
        }

        public IEnumerable<OptimizerDVH> StructureDVHs { get; }

        public IEnumerable<OptimizerObjectiveValue> StructureObjectiveValues { get; }

        public double TotalObjectiveFunctionValue { get; }

        public int NumberOfIMRTOptimizerIterations { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizerResult> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizerResult, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.OptimizerResult(AsyncOptimizerResult wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.OptimizerResult IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizerResult>.Inner => _inner;
    }
}
