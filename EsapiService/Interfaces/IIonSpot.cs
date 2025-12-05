namespace VMS.TPS.Common.Model.API
{
    public interface IIonSpot : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        VMS.TPS.Common.Model.Types.VVector Position { get; }
        float Weight { get; }
    }
}
