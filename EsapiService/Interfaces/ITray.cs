namespace VMS.TPS.Common.Model.API
{
    public interface ITray : IAddOn
    {
        void WriteXml(System.Xml.XmlWriter writer);
    }
}
