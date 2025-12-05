namespace VMS.TPS.Common.Model.API
{
    public interface IIonSpotParameters : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        float Weight { get; }
        System.Threading.Tasks.Task SetWeightAsync(float value);
        float X { get; }
        System.Threading.Tasks.Task SetXAsync(float value);
        float Y { get; }
        System.Threading.Tasks.Task SetYAsync(float value);
    }
}
