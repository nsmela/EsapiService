using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class SourcePosition : ApiDataObject
    {
        public SourcePosition()
        {
        }

        public double DwellTime { get; set; }
        public bool? DwellTimeLock { get; set; }
        public double NominalDwellTime { get; set; }
        public RadioactiveSource RadioactiveSource { get; set; }
        public double[,] Transform { get; set; }
        public VVector Translation { get; set; }
    }
}
