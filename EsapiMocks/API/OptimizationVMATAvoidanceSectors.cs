using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class OptimizationVMATAvoidanceSectors : OptimizationParameter
    {
        public OptimizationVMATAvoidanceSectors()
        {
        }

        public OptimizationAvoidanceSector AvoidanceSector1 { get; set; }
        public OptimizationAvoidanceSector AvoidanceSector2 { get; set; }
        public Beam Beam { get; set; }
        public bool IsValid { get; set; }
        public string ValidationError { get; set; }
    }
}
