namespace VMS.TPS.Common.Model.API
{
    public interface IPatientSupportDevice : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        string PatientSupportAccessoryCode { get; }
        string PatientSupportDeviceType { get; }
    }
}
