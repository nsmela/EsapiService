using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;

namespace Esapi.Wrappers
{
    public class AsyncOptimizerObjectiveValue : IOptimizerObjectiveValue
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizerObjectiveValue _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncOptimizerObjectiveValue(VMS.TPS.Common.Model.API.OptimizerObjectiveValue inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

            Value = inner.Value;
        }


        public async Task<IStructure> GetStructureAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Structure is null ? null : new AsyncStructure(_inner.Structure, _service));
        }

        public double Value { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizerObjectiveValue> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizerObjectiveValue, T> func) => _service.RunAsync(() => func(_inner));
    }
}
