namespace VMS.TPS.Common.Model.API
{
    public interface IPlanUncertainty : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        IDVHData GetDVHCumulativeData(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.DoseValuePresentation dosePresentation, VMS.TPS.Common.Model.Types.VolumePresentation volumePresentation, double binWidth);
        System.Collections.Generic.IReadOnlyList<IBeamUncertainty> BeamUncertainties { get; }
        double CalibrationCurveError { get; }
        string DisplayName { get; }
        IDose Dose { get; }
        VMS.TPS.Common.Model.Types.VVector IsocenterShift { get; }
        VMS.TPS.Common.Model.Types.PlanUncertaintyType UncertaintyType { get; }
    }
}
