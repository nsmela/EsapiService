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

        public double Priority { get; set; }
        public Structure Structure { get; set; }
        public string StructureId { get; set; }
    }
}
