using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Applicator : AddOn
    {
        public Applicator()
        {
        }

        public double ApplicatorLengthInMM { get; set; }
        public double DiameterInMM { get; set; }
        public double FieldSizeX { get; set; }
        public double FieldSizeY { get; set; }
        public bool IsStereotactic { get; set; }
    }
}
