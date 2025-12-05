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

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public void AddItem(VMS.TPS.Common.Model.API.PlanningItem pi) => _inner.AddItem(pi);
        public void AddItem(VMS.TPS.Common.Model.API.PlanningItem pi, VMS.TPS.Common.Model.Types.PlanSumOperation operation, double planWeight) => _inner.AddItem(pi, operation, planWeight);
        public VMS.TPS.Common.Model.Types.PlanSumOperation GetPlanSumOperation(VMS.TPS.Common.Model.API.PlanSetup planSetupInPlanSum) => _inner.GetPlanSumOperation(planSetupInPlanSum);
        public double GetPlanWeight(VMS.TPS.Common.Model.API.PlanSetup planSetupInPlanSum) => _inner.GetPlanWeight(planSetupInPlanSum);
        public void RemoveItem(VMS.TPS.Common.Model.API.PlanningItem pi) => _inner.RemoveItem(pi);
        public void SetPlanSumOperation(VMS.TPS.Common.Model.API.PlanSetup planSetupInPlanSum, VMS.TPS.Common.Model.Types.PlanSumOperation operation) => _inner.SetPlanSumOperation(planSetupInPlanSum, operation);
        public void SetPlanWeight(VMS.TPS.Common.Model.API.PlanSetup planSetupInPlanSum, double weight) => _inner.SetPlanWeight(planSetupInPlanSum, weight);
        public System.Collections.Generic.IReadOnlyList<IPlanSumComponent> PlanSumComponents => _inner.PlanSumComponents?.Select(x => new AsyncPlanSumComponent(x, _service)).ToList();
        public string Id => _inner.Id;
        public async Task SetIdAsync(string value) => _service.RunAsync(() => _inner.Id = value);
        public string Name => _inner.Name;
        public async Task SetNameAsync(string value) => _service.RunAsync(() => _inner.Name = value);
        public System.Collections.Generic.IReadOnlyList<IPlanSetup> PlanSetups => _inner.PlanSetups?.Select(x => new AsyncPlanSetup(x, _service)).ToList();
    }
}
