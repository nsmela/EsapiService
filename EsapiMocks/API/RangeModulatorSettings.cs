using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class RangeModulatorSettings : SerializableObject
    {
        public RangeModulatorSettings()
        {
        }

        public double IsocenterToRangeModulatorDistance { get; set; }
        public double RangeModulatorGatingStartValue { get; set; }
        public double RangeModulatorGatingStarWaterEquivalentThickness { get; set; }
        public double RangeModulatorGatingStopValue { get; set; }
        public double RangeModulatorGatingStopWaterEquivalentThickness { get; set; }
        public RangeModulator ReferencedRangeModulator { get; set; }
    }
}
