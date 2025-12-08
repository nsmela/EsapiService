using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class ExternalBeamTreatmentUnit : ApiDataObject
    {
        public ExternalBeamTreatmentUnit()
        {
        }

        public string MachineDepartmentName { get; set; }
        public string MachineModel { get; set; }
        public string MachineModelName { get; set; }
        public string MachineScaleDisplayName { get; set; }
        public TreatmentUnitOperatingLimits OperatingLimits { get; set; }
        public double SourceAxisDistance { get; set; }
    }
}
