namespace VMS.TPS.Common.Model.API
{
    public interface IApiDataObject : ISerializableObject
    {
        string ToString();
        void WriteXml(System.Xml.XmlWriter writer);
        bool Equals(object obj);
        int GetHashCode();
        string Id { get; }
        string Name { get; }
        string Comment { get; }
        string HistoryUserName { get; }
        string HistoryUserDisplayName { get; }
        System.DateTime HistoryDateTime { get; }
    }
}
