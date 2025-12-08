using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Compensator : ApiDataObject
    {
        public Compensator()
        {
        }

        public AddOnMaterial Material { get; set; }
        public Slot Slot { get; set; }
        public Tray Tray { get; set; }
    }
}
