using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class RadioactiveSourceModel : ApiDataObject
    {
        public RadioactiveSourceModel()
        {
        }

        public double ActivityConversionFactor { get; set; }
        public string CalculationModel { get; set; }
        public double DoseRateConstant { get; set; }
        public double HalfLife { get; set; }
        public string LiteratureReference { get; set; }
        public string Manufacturer { get; set; }
        public string SourceType { get; set; }
        public string Status { get; set; }
        public DateTime? StatusDate { get; set; }
        public string StatusUserName { get; set; }
    }
}
