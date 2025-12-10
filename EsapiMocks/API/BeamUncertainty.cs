using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class BeamUncertainty : ApiDataObject
    {
        public BeamUncertainty()
        {
        }

        public Beam Beam { get; set; }
        public Dose Dose { get; set; }
    }
}
