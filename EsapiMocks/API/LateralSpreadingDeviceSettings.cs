using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class LateralSpreadingDeviceSettings : SerializableObject
    {
        public LateralSpreadingDeviceSettings()
        {
        }

        public double IsocenterToLateralSpreadingDeviceDistance { get; set; }
        public string LateralSpreadingDeviceSetting { get; set; }
        public double LateralSpreadingDeviceWaterEquivalentThickness { get; set; }
        public LateralSpreadingDevice ReferencedLateralSpreadingDevice { get; set; }
    }
}
