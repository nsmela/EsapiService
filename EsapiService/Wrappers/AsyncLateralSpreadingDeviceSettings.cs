namespace EsapiService.Wrappers
{
    public class AsyncLateralSpreadingDeviceSettings : ILateralSpreadingDeviceSettings
    {
        internal readonly VMS.TPS.Common.Model.API.LateralSpreadingDeviceSettings _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncLateralSpreadingDeviceSettings(VMS.TPS.Common.Model.API.LateralSpreadingDeviceSettings inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            IsocenterToLateralSpreadingDeviceDistance = inner.IsocenterToLateralSpreadingDeviceDistance;
            LateralSpreadingDeviceSetting = inner.LateralSpreadingDeviceSetting;
            LateralSpreadingDeviceWaterEquivalentThickness = inner.LateralSpreadingDeviceWaterEquivalentThickness;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public double IsocenterToLateralSpreadingDeviceDistance { get; }
        public string LateralSpreadingDeviceSetting { get; }
        public double LateralSpreadingDeviceWaterEquivalentThickness { get; }
        public ILateralSpreadingDevice ReferencedLateralSpreadingDevice => _inner.ReferencedLateralSpreadingDevice is null ? null : new AsyncLateralSpreadingDevice(_inner.ReferencedLateralSpreadingDevice, _service);

    }
}
