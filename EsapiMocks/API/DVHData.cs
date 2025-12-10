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
        public double SamplingCoverage { get; set; }
        public double StdDev { get; set; }
        public double Volume { get; set; }
    }
}
