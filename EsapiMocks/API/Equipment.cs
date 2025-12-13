using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Equipment
    {
        public Equipment()
        {
        }

        public IEnumerable<BrachyTreatmentUnit> GetBrachyTreatmentUnits() => new List<BrachyTreatmentUnit>();
        public IEnumerable<ExternalBeamTreatmentUnit> GetExternalBeamTreatmentUnits() => new List<ExternalBeamTreatmentUnit>();
    }
}
