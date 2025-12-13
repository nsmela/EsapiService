using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class RTPrescriptionOrganAtRisk : SerializableObject
    {
        public RTPrescriptionOrganAtRisk()
        {
        }

        public IEnumerable<RTPrescriptionConstraint> Constraints { get; set; }
        public string OrganAtRiskId { get; set; }
    }
}
