using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class PlanningItem : ApiDataObject
    {
        public PlanningItem()
        {
        }

        public Course Course { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public PlanningItemDose Dose { get; set; }
        public StructureSet StructureSet { get; set; }
        public IEnumerable<Structure> StructuresSelectedForDvh { get; set; }
    }
}
