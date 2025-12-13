using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class OptimizationNormalTissueParameter : OptimizationParameter
    {
        public OptimizationNormalTissueParameter()
        {
        }

        public double DistanceFromTargetBorderInMM { get; set; }
        public double EndDosePercentage { get; set; }
        public double FallOff { get; set; }
        public bool IsAutomatic { get; set; }
        public bool IsAutomaticSbrt { get; set; }
        public bool IsAutomaticSrs { get; set; }
        public double Priority { get; set; }
        public double StartDosePercentage { get; set; }
    }
}
