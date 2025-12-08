using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class TradeoffObjective
    {
        public TradeoffObjective()
        {
            OptimizationObjectives = new List<OptimizationObjective>();
        }

        public int Id { get; set; }
        public IEnumerable<OptimizationObjective> OptimizationObjectives { get; set; }
        public Structure Structure { get; set; }
    }
}
