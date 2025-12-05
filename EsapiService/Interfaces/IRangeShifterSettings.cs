namespace VMS.TPS.Common.Model.API
{
    public interface IRangeShifterSettings : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        double IsocenterToRangeShifterDistance { get; }
        string RangeShifterSetting { get; }
        double RangeShifterWaterEquivalentThickness { get; }
        IRangeShifter ReferencedRangeShifter { get; }
    }
}
