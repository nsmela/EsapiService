namespace VMS.TPS.Common.Model.API
{
    public interface IDVHData : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        double Coverage { get; }
        VMS.TPS.Common.Model.Types.DVHPoint[] CurveData { get; }
        VMS.TPS.Common.Model.Types.DoseValue MaxDose { get; }
        VMS.TPS.Common.Model.Types.VVector MaxDosePosition { get; }
        VMS.TPS.Common.Model.Types.DoseValue MeanDose { get; }
        VMS.TPS.Common.Model.Types.DoseValue MedianDose { get; }
        VMS.TPS.Common.Model.Types.DoseValue MinDose { get; }
        VMS.TPS.Common.Model.Types.VVector MinDosePosition { get; }
        double SamplingCoverage { get; }
        double StdDev { get; }
        double Volume { get; }
    }
}
