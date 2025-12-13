using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class TreatmentUnitOperatingLimit : ApiDataObject
    {
        public TreatmentUnitOperatingLimit()
        {
        }

        public string Label { get; set; }
        public double MaxValue { get; set; }
        public double MinValue { get; set; }
        public int? Precision { get; set; }
        public string UnitString { get; set; }
    }
}
