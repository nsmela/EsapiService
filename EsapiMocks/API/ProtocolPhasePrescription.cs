using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class ProtocolPhasePrescription : SerializableObject
    {
        public ProtocolPhasePrescription()
        {
        }

        public bool? TargetIsMet { get; set; }
        public double PrescParameter { get; set; }
        public string StructureId { get; set; }
    }
}
