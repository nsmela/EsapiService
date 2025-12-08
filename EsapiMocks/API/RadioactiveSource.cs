using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class RadioactiveSource : ApiDataObject
    {
        public RadioactiveSource()
        {
        }

        public DateTime? CalibrationDate { get; set; }
        public bool NominalActivity { get; set; }
        public RadioactiveSourceModel RadioactiveSourceModel { get; set; }
        public string SerialNumber { get; set; }
        public double Strength { get; set; }
    }
}
