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
        }

        public void AddItem(PlanningItem pi) { }
        public double GetPlanWeight(PlanSetup planSetupInPlanSum) => default;
        public void RemoveItem(PlanningItem pi) { }
        public void SetPlanWeight(PlanSetup planSetupInPlanSum, double weight) { }
        public IEnumerable<PlanSumComponent> PlanSumComponents { get; set; }
        public IEnumerable<PlanSetup> PlanSetups { get; set; }
    }
}
