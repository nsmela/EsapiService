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

        public Beam Beam { get; set; }
        public bool IsValid { get; set; }
        public string ValidationError { get; set; }
    }
}
