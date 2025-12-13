using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class RTPrescriptionTarget : ApiDataObject
    {
        public RTPrescriptionTarget()
        {
        }

        public IEnumerable<RTPrescriptionConstraint> Constraints { get; set; }
        public DoseValue DosePerFraction { get; set; }
        public int NumberOfFractions { get; set; }
        public string TargetId { get; set; }
        public RTPrescriptionTargetType Type { get; set; }
        public double Value { get; set; }
    }
}
