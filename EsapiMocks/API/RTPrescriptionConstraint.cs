using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class RTPrescriptionConstraint : SerializableObject
    {
        public RTPrescriptionConstraint()
        {
        }

        public RTPrescriptionConstraintType ConstraintType { get; set; }
        public string Unit1 { get; set; }
        public string Unit2 { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
    }
}
