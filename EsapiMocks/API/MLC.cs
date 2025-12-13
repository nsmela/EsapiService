using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class MLC : AddOn
    {
        public MLC()
        {
        }

        public string ManufacturerName { get; set; }
        public double MinDoseDynamicLeafGap { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
    }
}
