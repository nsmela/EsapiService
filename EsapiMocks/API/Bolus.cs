using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Bolus : SerializableObject
    {
        public Bolus()
        {
        }

        public string Id { get; set; }
        public double MaterialCTValue { get; set; }
        public string Name { get; set; }
    }
}
