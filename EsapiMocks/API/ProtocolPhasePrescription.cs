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

        public DoseValue TargetTotalDose { get; set; }
        public DoseValue TargetFractionDose { get; set; }
        public DoseValue ActualTotalDose { get; set; }
        public bool? TargetIsMet { get; set; }
        public PrescriptionModifier PrescModifier { get; set; }
        public double PrescParameter { get; set; }
        public PrescriptionType PrescType { get; set; }
        public string StructureId { get; set; }
    }
}
