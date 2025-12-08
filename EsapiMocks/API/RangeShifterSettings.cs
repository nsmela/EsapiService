using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class RangeShifterSettings : SerializableObject
    {
        public RangeShifterSettings()
        {
        }

        public double IsocenterToRangeShifterDistance { get; set; }
        public string RangeShifterSetting { get; set; }
        public double RangeShifterWaterEquivalentThickness { get; set; }
        public RangeShifter ReferencedRangeShifter { get; set; }
    }
}
