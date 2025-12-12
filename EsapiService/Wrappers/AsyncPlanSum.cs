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
    public class AsyncPlanSum : AsyncPlanningItem, IPlanSum, IEsapiWrapper<VMS.TPS.Common.Model.API.PlanSum>
    {
        internal new readonly VMS.TPS.Common.Model.API.PlanSum _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncPlanSum(VMS.TPS.Common.Model.API.PlanSum inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            PlanSumComponents = inner.PlanSumComponents;
            Id = inner.Id;
            Name = inner.Name;
            PlanSetups = inner.PlanSetups;
        }

        // Simple Void Method
        public Task AddItemAsync(IPlanningItem pi) => _service.PostAsync(context => _inner.AddItem(((AsyncPlanningItem)pi)._inner));

        // Simple Method
        public Task<double> GetPlanWeightAsync(IPlanSetup planSetupInPlanSum) => _service.PostAsync(context => _inner.GetPlanWeight(((AsyncPlanSetup)planSetupInPlanSum)._inner));

        // Simple Void Method
        public Task RemoveItemAsync(IPlanningItem pi) => _service.PostAsync(context => _inner.RemoveItem(((AsyncPlanningItem)pi)._inner));

        // Simple Void Method
        public Task SetPlanWeightAsync(IPlanSetup planSetupInPlanSum, double weight) => _service.PostAsync(context => _inner.SetPlanWeight(((AsyncPlanSetup)planSetupInPlanSum)._inner, weight));

        public IEnumerable<PlanSumComponent> PlanSumComponents { get; }

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

        public IEnumerable<PlanSetup> PlanSetups { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanSum> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanSum, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.PlanSum(AsyncPlanSum wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.PlanSum IEsapiWrapper<VMS.TPS.Common.Model.API.PlanSum>.Inner => _inner;
    }
}
