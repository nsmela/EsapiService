using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class PlanTreatmentSession : ApiDataObject
    {
        public PlanTreatmentSession()
        {
        }

        public PlanSetup PlanSetup { get; set; }
        public TreatmentSessionStatus Status { get; set; }
        public TreatmentSession TreatmentSession { get; set; }
    }
}
