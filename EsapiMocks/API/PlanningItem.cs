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
            StructuresSelectedForDvh = new List<Structure>();
        }

        public List<ClinicalGoal> GetClinicalGoals() => new List<ClinicalGoal>();
        public DVHData GetDVHCumulativeData(Structure structure, DoseValuePresentation dosePresentation, VolumePresentation volumePresentation, double binWidth) => default;
        public DoseValue GetDoseAtVolume(Structure structure, double volume, VolumePresentation volumePresentation, DoseValuePresentation requestedDosePresentation) => default;
        public double GetVolumeAtDose(Structure structure, DoseValue dose, VolumePresentation requestedVolumePresentation) => default;
        public Course Course { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public PlanningItemDose Dose { get; set; }
        public DoseValuePresentation DoseValuePresentation { get; set; }
        public StructureSet StructureSet { get; set; }
        public IEnumerable<Structure> StructuresSelectedForDvh { get; set; }
    }
}
