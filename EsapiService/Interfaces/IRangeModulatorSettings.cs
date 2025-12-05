namespace VMS.TPS.Common.Model.API
{
    public interface IRangeModulatorSettings : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        double IsocenterToRangeModulatorDistance { get; }
        double RangeModulatorGatingStartValue { get; }
        double RangeModulatorGatingStarWaterEquivalentThickness { get; }
        double RangeModulatorGatingStopValue { get; }
        double RangeModulatorGatingStopWaterEquivalentThickness { get; }
        IRangeModulator ReferencedRangeModulator { get; }
    }
}
