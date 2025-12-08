using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncPlanSum : IPlanSum
    {
        internal readonly VMS.TPS.Common.Model.API.PlanSum _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncPlanSum(VMS.TPS.Common.Model.API.PlanSum inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Id = inner.Id;
            Name = inner.Name;
        }

        public Task AddItemAsync(IPlanningItem pi) => _service.RunAsync(() => _inner.AddItem(pi));
        public Task AddItemAsync(IPlanningItem pi, PlanSumOperation operation, double planWeight) => _service.RunAsync(() => _inner.AddItem(pi, operation, planWeight));
        public Task<PlanSumOperation> GetPlanSumOperationAsync(IPlanSetup planSetupInPlanSum) => _service.RunAsync(() => _inner.GetPlanSumOperation(planSetupInPlanSum));
        public Task<double> GetPlanWeightAsync(IPlanSetup planSetupInPlanSum) => _service.RunAsync(() => _inner.GetPlanWeight(planSetupInPlanSum));
        public Task RemoveItemAsync(IPlanningItem pi) => _service.RunAsync(() => _inner.RemoveItem(pi));
        public Task SetPlanSumOperationAsync(IPlanSetup planSetupInPlanSum, PlanSumOperation operation) => _service.RunAsync(() => _inner.SetPlanSumOperation(planSetupInPlanSum, operation));
        public Task SetPlanWeightAsync(IPlanSetup planSetupInPlanSum, double weight) => _service.RunAsync(() => _inner.SetPlanWeight(planSetupInPlanSum, weight));
        public async Task<IReadOnlyList<IPlanSumComponent>> GetPlanSumComponentsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.PlanSumComponents?.Select(x => new AsyncPlanSumComponent(x, _service)).ToList());
        }

        public string Id { get; private set; }
        public async Task SetIdAsync(string value)
        {
            Id = await _service.RunAsync(() =>
            {
                _inner.Id = value;
                return _inner.Id;
            });
        }
        public string Name { get; private set; }
        public async Task SetNameAsync(string value)
        {
            Name = await _service.RunAsync(() =>
            {
                _inner.Name = value;
                return _inner.Name;
            });
        }
        public async Task<IReadOnlyList<IPlanSetup>> GetPlanSetupsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.PlanSetups?.Select(x => new AsyncPlanSetup(x, _service)).ToList());
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanSum> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanSum, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
