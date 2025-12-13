using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class IonControlPointCollection : SerializableObject
    {
        public IonControlPointCollection()
        {
        }


        public int Count { get; set; }
    }
}
