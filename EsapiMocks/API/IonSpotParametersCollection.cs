using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class IonSpotParametersCollection : SerializableObject
    {
        public IonSpotParametersCollection()
        {
        }


        public int Count { get; set; }
    }
}
