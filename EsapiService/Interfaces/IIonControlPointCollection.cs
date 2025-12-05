namespace VMS.TPS.Common.Model.API
{
    public interface IIonControlPointCollection : ISerializableObject
    {
        System.Collections.Generic.IReadOnlyList<IIonControlPoint> GetEnumerator();
        void WriteXml(System.Xml.XmlWriter writer);
        IIonControlPoint this[] { get; }
        int Count { get; }
    }
}
