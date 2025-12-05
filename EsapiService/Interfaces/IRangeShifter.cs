namespace VMS.TPS.Common.Model.API
{
    public interface IRangeShifter : IAddOn
    {
        void WriteXml(System.Xml.XmlWriter writer);
        VMS.TPS.Common.Model.Types.RangeShifterType Type { get; }
    }
}
