namespace VMS.TPS.Common.Model.API
{
    public interface IPlanningItem : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        System.Collections.Generic.IReadOnlyList<VMS.TPS.Common.Model.Types.ClinicalGoal> GetClinicalGoals();
        IDVHData GetDVHCumulativeData(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.DoseValuePresentation dosePresentation, VMS.TPS.Common.Model.Types.VolumePresentation volumePresentation, double binWidth);
        VMS.TPS.Common.Model.Types.DoseValue GetDoseAtVolume(VMS.TPS.Common.Model.API.Structure structure, double volume, VMS.TPS.Common.Model.Types.VolumePresentation volumePresentation, VMS.TPS.Common.Model.Types.DoseValuePresentation requestedDosePresentation);
        double GetVolumeAtDose(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.DoseValue dose, VMS.TPS.Common.Model.Types.VolumePresentation requestedVolumePresentation);
        ICourse Course { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> CreationDateTime { get; }
        IPlanningItemDose Dose { get; }
        VMS.TPS.Common.Model.Types.DoseValuePresentation DoseValuePresentation { get; }
        System.Threading.Tasks.Task SetDoseValuePresentationAsync(VMS.TPS.Common.Model.Types.DoseValuePresentation value);
        IStructureSet StructureSet { get; }
        System.Collections.Generic.IReadOnlyList<IStructure> StructuresSelectedForDvh { get; }
    }
}
