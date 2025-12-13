using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class OptimizerDVH
    {
        public OptimizerDVH()
        {
        }

        public DVHPoint[] CurveData { get; set; }
        public Structure Structure { get; set; }
    }
}
