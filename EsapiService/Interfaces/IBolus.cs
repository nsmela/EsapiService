namespace VMS.TPS.Common.Model.API
{
    public interface IBolus : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        string Id { get; }
        double MaterialCTValue { get; }
        string Name { get; }
    }
}
