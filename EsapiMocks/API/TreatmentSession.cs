using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class TreatmentSession : ApiDataObject
    {
        public TreatmentSession()
        {
            SessionPlans = new List<PlanTreatmentSession>();
        }

        public long SessionNumber { get; set; }
        public IEnumerable<PlanTreatmentSession> SessionPlans { get; set; }
    }
}
