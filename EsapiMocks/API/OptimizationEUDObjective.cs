using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class OptimizationEUDObjective : OptimizationObjective
    {
        public OptimizationEUDObjective()
        {
        }

        public bool IsRobustObjective { get; set; }
        public double ParameterA { get; set; }
    }
}
