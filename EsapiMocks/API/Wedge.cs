using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Wedge : AddOn
    {
        public Wedge()
        {
        }

        public double Direction { get; set; }
        public double WedgeAngle { get; set; }
    }
}
