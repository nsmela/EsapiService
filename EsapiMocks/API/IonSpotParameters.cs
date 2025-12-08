using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class IonSpotParameters : SerializableObject
    {
        public IonSpotParameters()
        {
        }

        public float Weight { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }
}
