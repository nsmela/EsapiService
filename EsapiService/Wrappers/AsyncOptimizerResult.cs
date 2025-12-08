using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;

namespace Esapi.Wrappers
{
    public class AsyncOptimizerResult : IOptimizerResult
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizerResult _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizerResult(VMS.TPS.Common.Model.API.OptimizerResult inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            MinMUObjectiveValue = inner.MinMUObjectiveValue;
            TotalObjectiveFunctionValue = inner.TotalObjectiveFunctionValue;
            NumberOfIMRTOptimizerIterations = inner.NumberOfIMRTOptimizerIterations;
        }


        public async Task<IReadOnlyList<IOptimizerDVH>> GetStructureDVHsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.StructureDVHs?.Select(x => new AsyncOptimizerDVH(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IOptimizerObjectiveValue>> GetStructureObjectiveValuesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.StructureObjectiveValues?.Select(x => new AsyncOptimizerObjectiveValue(x, _service)).ToList());
        }


        public double MinMUObjectiveValue { get; }

        public double TotalObjectiveFunctionValue { get; }

        public int NumberOfIMRTOptimizerIterations { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizerResult> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizerResult, T> func) => _service.RunAsync(() => func(_inner));
    }
}
