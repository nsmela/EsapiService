using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class IonSpot : SerializableObject
    {
        public IonSpot()
        {
        }

        public VVector Position { get; set; }
        public float Weight { get; set; }
    }
}
