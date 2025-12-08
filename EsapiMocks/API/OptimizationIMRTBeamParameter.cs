using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class OptimizationIMRTBeamParameter : OptimizationParameter
    {
        public OptimizationIMRTBeamParameter()
        {
        }

        public Beam Beam { get; set; }
        public string BeamId { get; set; }
        public bool Excluded { get; set; }
        public bool FixedJaws { get; set; }
        public double SmoothX { get; set; }
        public double SmoothY { get; set; }
    }
}
