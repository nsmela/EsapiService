namespace VMS.TPS.Common.Model.API
{
    public interface IEstimatedDVH : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        VMS.TPS.Common.Model.Types.DVHPoint[] CurveData { get; }
        IPlanSetup PlanSetup { get; }
        string PlanSetupId { get; }
        IStructure Structure { get; }
        string StructureId { get; }
        VMS.TPS.Common.Model.Types.DoseValue TargetDoseLevel { get; }
        VMS.TPS.Common.Model.Types.DVHEstimateType Type { get; }
    }
}
