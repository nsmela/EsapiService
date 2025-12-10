using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class ProtocolPhaseMeasure : SerializableObject
    {
        public ProtocolPhaseMeasure()
        {
        }

        public double TargetValue { get; set; }
        public double ActualValue { get; set; }
        public bool? TargetIsMet { get; set; }
        public string StructureId { get; set; }
        public string TypeText { get; set; }
    }
}
