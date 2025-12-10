using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class EstimatedDVH : ApiDataObject
    {
        public EstimatedDVH()
        {
        }

        public PlanSetup PlanSetup { get; set; }
        public string PlanSetupId { get; set; }
        public Structure Structure { get; set; }
        public string StructureId { get; set; }
    }
}
