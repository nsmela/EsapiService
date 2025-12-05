namespace VMS.TPS.Common.Model.API
{
    public interface IRangeModulator : IAddOn
    {
        void WriteXml(System.Xml.XmlWriter writer);
        VMS.TPS.Common.Model.Types.RangeModulatorType Type { get; }
    }
}
