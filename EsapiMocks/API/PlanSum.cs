using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class PlanSum : PlanningItem
    {
        public PlanSum()
        {
            PlanSumComponents = new List<PlanSumComponent>();
            PlanSetups = new List<PlanSetup>();
        }

        public void AddItem(PlanningItem pi) { }
        public double GetPlanWeight(PlanSetup planSetupInPlanSum) => default;
        public void RemoveItem(PlanningItem pi) { }
        public void SetPlanWeight(PlanSetup planSetupInPlanSum, double weight) { }
        public IEnumerable<PlanSumComponent> PlanSumComponents { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<PlanSetup> PlanSetups { get; set; }
    }
}
