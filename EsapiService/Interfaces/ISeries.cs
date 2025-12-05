namespace VMS.TPS.Common.Model.API
{
    public interface ISeries : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        void SetImagingDevice(string imagingDeviceId);
        string FOR { get; }
        System.Collections.Generic.IReadOnlyList<IImage> Images { get; }
        string ImagingDeviceDepartment { get; }
        string ImagingDeviceId { get; }
        string ImagingDeviceManufacturer { get; }
        string ImagingDeviceModel { get; }
        string ImagingDeviceSerialNo { get; }
        VMS.TPS.Common.Model.Types.SeriesModality Modality { get; }
        IStudy Study { get; }
        string UID { get; }
    }
}
