using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class TreatmentUnitOperatingLimits : SerializableObject
    {
        public TreatmentUnitOperatingLimits()
        {
        }

        public TreatmentUnitOperatingLimit CollimatorAngle { get; set; }
        public TreatmentUnitOperatingLimit GantryAngle { get; set; }
        public TreatmentUnitOperatingLimit MU { get; set; }
        public TreatmentUnitOperatingLimit PatientSupportAngle { get; set; }
    }
}
