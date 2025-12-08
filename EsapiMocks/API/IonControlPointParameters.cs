using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class IonControlPointParameters : ControlPointParameters
    {
        public IonControlPointParameters()
        {
        }

        public IonSpotParametersCollection FinalSpotList { get; set; }
        public IonSpotParametersCollection RawSpotList { get; set; }
        public double SnoutPosition { get; set; }
    }
}
