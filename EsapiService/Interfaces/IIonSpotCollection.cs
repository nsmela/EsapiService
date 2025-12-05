namespace VMS.TPS.Common.Model.API
{
    public interface IIonSpotCollection : ISerializableObject
    {
        System.Collections.Generic.IReadOnlyList<IIonSpot> GetEnumerator();
        void WriteXml(System.Xml.XmlWriter writer);
        IIonSpot this[] { get; }
        int Count { get; }
    }
}
