using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class OptimizerObjectiveValue
    {
        public OptimizerObjectiveValue()
        {
        }

        public Structure Structure { get; set; }
        public double Value { get; set; }
    }
}
