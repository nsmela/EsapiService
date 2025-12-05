namespace VMS.TPS.Common.Model.API
{
    public interface IMLC : IAddOn
    {
        void WriteXml(System.Xml.XmlWriter writer);
        string ManufacturerName { get; }
        double MinDoseDynamicLeafGap { get; }
        string Model { get; }
        string SerialNumber { get; }
    }
}
