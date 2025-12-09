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
    public class AsyncPlanSum : AsyncPlanningItem, IPlanSum
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


        public Task AddItemAsync(IPlanningItem pi) => _service.PostAsync(context => _inner.AddItem(((AsyncPlanningItem)pi)._inner));

        public Task AddItemAsync(IPlanningItem pi, PlanSumOperation operation, double planWeight) => _service.PostAsync(context => _inner.AddItem(((AsyncPlanningItem)pi)._inner, operation, planWeight));

        public Task<PlanSumOperation> GetPlanSumOperationAsync(IPlanSetup planSetupInPlanSum) => _service.PostAsync(context => _inner.GetPlanSumOperation(((AsyncPlanSetup)planSetupInPlanSum)._inner));

        public Task<double> GetPlanWeightAsync(IPlanSetup planSetupInPlanSum) => _service.PostAsync(context => _inner.GetPlanWeight(((AsyncPlanSetup)planSetupInPlanSum)._inner));

        public Task RemoveItemAsync(IPlanningItem pi) => _service.PostAsync(context => _inner.RemoveItem(((AsyncPlanningItem)pi)._inner));

        public Task SetPlanSumOperationAsync(IPlanSetup planSetupInPlanSum, PlanSumOperation operation) => _service.PostAsync(context => _inner.SetPlanSumOperation(((AsyncPlanSetup)planSetupInPlanSum)._inner, operation));

        public Task SetPlanWeightAsync(IPlanSetup planSetupInPlanSum, double weight) => _service.PostAsync(context => _inner.SetPlanWeight(((AsyncPlanSetup)planSetupInPlanSum)._inner, weight));

        public async Task<IReadOnlyList<IPlanSumComponent>> GetPlanSumComponentsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.PlanSumComponents?.Select(x => new AsyncPlanSumComponent(x, _service)).ToList());
        }


        public string Id { get; private set; }
        public async Task SetIdAsync(string value)
        {
            Id = await _service.PostAsync(context => 
            {
                _inner.Id = value;
                return _inner.Id;
            });
        }

        public string Name { get; private set; }
        public async Task SetNameAsync(string value)
        {
            Name = await _service.PostAsync(context => 
            {
                _inner.Name = value;
                return _inner.Name;
            });
        }

        public async Task<IReadOnlyList<IPlanSetup>> GetPlanSetupsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.PlanSetups?.Select(x => new AsyncPlanSetup(x, _service)).ToList());
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanSum> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanSum, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
