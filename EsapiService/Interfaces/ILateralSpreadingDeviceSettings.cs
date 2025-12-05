namespace VMS.TPS.Common.Model.API
{
    public interface ILateralSpreadingDeviceSettings : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        double IsocenterToLateralSpreadingDeviceDistance { get; }
        string LateralSpreadingDeviceSetting { get; }
        double LateralSpreadingDeviceWaterEquivalentThickness { get; }
        ILateralSpreadingDevice ReferencedLateralSpreadingDevice { get; }
    }
}
