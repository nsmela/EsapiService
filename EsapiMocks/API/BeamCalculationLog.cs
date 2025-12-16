using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class BeamCalculationLog : SerializableObject
    {
        public BeamCalculationLog()
        {
        }

        public Beam Beam { get; set; }
        public string Category { get; set; }
    }
}
