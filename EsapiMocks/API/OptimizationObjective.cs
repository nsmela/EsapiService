using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class OptimizationObjective : SerializableObject
    {
        public OptimizationObjective()
        {
        }

        public OptimizationObjectiveOperator Operator { get; set; }
        public double Priority { get; set; }
        public Structure Structure { get; set; }
        public string StructureId { get; set; }

        /* --- Skipped Members (Not generated) ---
           - op_Equality: No matching factory found (Not Implemented)
           - op_Inequality: No matching factory found (Not Implemented)
        */
    }
}
