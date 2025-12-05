namespace VMS.TPS.Common.Model.API
{
    public interface IControlPointCollection : ISerializableObject
    {
        System.Collections.Generic.IReadOnlyList<IControlPoint> GetEnumerator();
        void WriteXml(System.Xml.XmlWriter writer);
        IControlPoint this[] { get; }
        int Count { get; }
    }
}
