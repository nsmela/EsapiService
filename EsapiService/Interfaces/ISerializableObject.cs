namespace VMS.TPS.Common.Model.API
{
    public interface ISerializableObject
    {
        System.Xml.Schema.XmlSchema GetSchema();
        void ReadXml(System.Xml.XmlReader reader);
        void WriteXml(System.Xml.XmlWriter writer);
    }
}
