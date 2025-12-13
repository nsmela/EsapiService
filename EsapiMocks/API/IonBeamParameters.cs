using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class IonBeamParameters : BeamParameters
    {
        public IonBeamParameters()
        {
        }

        public string PreSelectedRangeShifter1Id { get; set; }
        public string PreSelectedRangeShifter1Setting { get; set; }
        public string PreSelectedRangeShifter2Id { get; set; }
        public string PreSelectedRangeShifter2Setting { get; set; }
        public IonControlPointPairCollection IonControlPointPairs { get; set; }
        public string SnoutId { get; set; }
        public double SnoutPosition { get; set; }
        public Structure TargetStructure { get; set; }
    }
}
