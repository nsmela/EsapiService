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

        public DoseValue Dose { get; set; }
        public double Volume { get; set; }
    }
}
