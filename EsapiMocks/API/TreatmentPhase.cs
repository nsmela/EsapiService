using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class TreatmentPhase : ApiDataObject
    {
        public TreatmentPhase()
        {
            Prescriptions = new List<RTPrescription>();
        }

        public string OtherInfo { get; set; }
        public int PhaseGapNumberOfDays { get; set; }
        public IEnumerable<RTPrescription> Prescriptions { get; set; }
        public string TimeGapType { get; set; }
    }
}
