using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class IonSpotCollection : SerializableObject
    {
        public IonSpotCollection()
        {
        }

        public IonSpot this[int index] { get; set; }
        public int Count { get; set; }
    }
}
