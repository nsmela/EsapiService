    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
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

        }

        public void AddItem(IPlanningItem pi) => _inner.AddItem(pi);
        public void AddItem(IPlanningItem pi, PlanSumOperation operation, double planWeight) => _inner.AddItem(pi, operation, planWeight);
        public PlanSumOperation GetPlanSumOperation(IPlanSetup planSetupInPlanSum) => _inner.GetPlanSumOperation(planSetupInPlanSum);
        public double GetPlanWeight(IPlanSetup planSetupInPlanSum) => _inner.GetPlanWeight(planSetupInPlanSum);
        public void RemoveItem(IPlanningItem pi) => _inner.RemoveItem(pi);
        public void SetPlanSumOperation(IPlanSetup planSetupInPlanSum, PlanSumOperation operation) => _inner.SetPlanSumOperation(planSetupInPlanSum, operation);
        public void SetPlanWeight(IPlanSetup planSetupInPlanSum, double weight) => _inner.SetPlanWeight(planSetupInPlanSum, weight);
        public IReadOnlyList<IPlanSumComponent> PlanSumComponents => _inner.PlanSumComponents?.Select(x => new AsyncPlanSumComponent(x, _service)).ToList();
        public string Id => _inner.Id;
        public async Task SetIdAsync(string value) => _service.RunAsync(() => _inner.Id = value);
        public string Name => _inner.Name;
        public async Task SetNameAsync(string value) => _service.RunAsync(() => _inner.Name = value);
        public IReadOnlyList<IPlanSetup> PlanSetups => _inner.PlanSetups?.Select(x => new AsyncPlanSetup(x, _service)).ToList();

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanSum> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanSum, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
