using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class LateralSpreadingDevice : AddOn
    {
        public LateralSpreadingDevice()
        {
        }

        public LateralSpreadingDeviceType Type { get; set; }
    }
}
