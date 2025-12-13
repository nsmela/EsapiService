using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class DVHData : SerializableObject
    {
        public DVHData()
        {
        }

        public double Coverage { get; set; }
        public DVHPoint[] CurveData { get; set; }
        public DoseValue MaxDose { get; set; }
        public VVector MaxDosePosition { get; set; }
        public DoseValue MeanDose { get; set; }
        public DoseValue MedianDose { get; set; }
        public DoseValue MinDose { get; set; }
        public VVector MinDosePosition { get; set; }
        public double SamplingCoverage { get; set; }
        public double StdDev { get; set; }
        public double Volume { get; set; }
    }
}
