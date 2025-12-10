using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class DVHEstimationModelSummary : SerializableObject
    {
        public DVHEstimationModelSummary()
        {
        }

        public string Description { get; set; }
        public bool IsPublished { get; set; }
        public bool IsTrained { get; set; }
        public string ModelDataVersion { get; set; }
        public System.Guid ModelUID { get; set; }
        public string Name { get; set; }
        public int Revision { get; set; }
        public string TreatmentSite { get; set; }
    }
}
