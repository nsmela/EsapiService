namespace VMS.TPS.Common.Model.API
{
    public interface IOptimizationParameter : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        bool Equals(object obj);
        int GetHashCode();
    }
}
