namespace VMS.TPS.Common.Model.API
{
    public interface IBrachyTreatmentUnit : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        IRadioactiveSource GetActiveRadioactiveSource();
        string DoseRateMode { get; }
        double DwellTimeResolution { get; }
        string MachineInterface { get; }
        string MachineModel { get; }
        double MaxDwellTimePerChannel { get; }
        double MaxDwellTimePerPos { get; }
        double MaxDwellTimePerTreatment { get; }
        double MaximumChannelLength { get; }
        int MaximumDwellPositionsPerChannel { get; }
        double MaximumStepSize { get; }
        double MinAllowedSourcePos { get; }
        double MinimumChannelLength { get; }
        double MinimumStepSize { get; }
        int NumberOfChannels { get; }
        double SourceCenterOffsetFromTip { get; }
        string SourceMovementType { get; }
        double StepSizeResolution { get; }
    }
}
