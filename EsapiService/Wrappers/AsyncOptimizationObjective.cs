using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncOptimizationObjective : AsyncSerializableObject, IOptimizationObjective
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizationObjective _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationObjective(VMS.TPS.Common.Model.API.OptimizationObjective inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Operator = inner.Operator;
            Priority = inner.Priority;
            StructureId = inner.StructureId;
        }


        public OptimizationObjectiveOperator Operator { get; }

        public double Priority { get; }

        public async Task<IStructure> GetStructureAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Structure is null ? null : new AsyncStructure(_inner.Structure, _service));
        }

        public string StructureId { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationObjective> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationObjective, T> func) => _service.RunAsync(() => func(_inner));
    }
}
