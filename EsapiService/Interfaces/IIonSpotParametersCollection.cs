namespace VMS.TPS.Common.Model.API
{
    public interface IIonSpotParametersCollection : ISerializableObject
    {
        System.Collections.Generic.IReadOnlyList<IIonSpotParameters> GetEnumerator();
        void WriteXml(System.Xml.XmlWriter writer);
        IIonSpotParameters this[] { get; }
        int Count { get; }
    }
}
