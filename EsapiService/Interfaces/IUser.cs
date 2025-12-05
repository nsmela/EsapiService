namespace VMS.TPS.Common.Model.API
{
    public interface IUser : ISerializableObject
    {
        string ToString();
        bool Equals(object obj);
        int GetHashCode();
        void WriteXml(System.Xml.XmlWriter writer);
        string Id { get; }
        bool IsServiceUser { get; }
        string Language { get; }
        string Name { get; }
    }
}
