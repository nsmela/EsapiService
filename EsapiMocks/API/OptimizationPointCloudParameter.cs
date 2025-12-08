using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class OptimizationPointCloudParameter : OptimizationParameter
    {
        public OptimizationPointCloudParameter()
        {
        }

        public double PointResolutionInMM { get; set; }
        public Structure Structure { get; set; }
    }
}
