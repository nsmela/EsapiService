using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class OptimizationPointObjective : OptimizationObjective
    {
        public OptimizationPointObjective()
        {
        }

        public bool IsRobustObjective { get; set; }
        public double Volume { get; set; }
    }
}
