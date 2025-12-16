using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class CalculateBrachy3DDoseResult : SerializableObject
    {
        public CalculateBrachy3DDoseResult()
        {
        }

        public double RoundedDwellTimeAdjustRatio { get; set; }
        public bool Success { get; set; }
    }
}
