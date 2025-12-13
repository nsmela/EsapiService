using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class EnergyMode : ApiDataObject
    {
        public EnergyMode()
        {
        }

        public bool IsElectron { get; set; }
        public bool IsPhoton { get; set; }
        public bool IsProton { get; set; }
    }
}
