namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
    public class AsyncSeries : ISeries
    {
        internal readonly VMS.TPS.Common.Model.API.Series _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncSeries(VMS.TPS.Common.Model.API.Series inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            FOR = inner.FOR;
            ImagingDeviceDepartment = inner.ImagingDeviceDepartment;
            ImagingDeviceId = inner.ImagingDeviceId;
            ImagingDeviceManufacturer = inner.ImagingDeviceManufacturer;
            ImagingDeviceModel = inner.ImagingDeviceModel;
            ImagingDeviceSerialNo = inner.ImagingDeviceSerialNo;
            Modality = inner.Modality;
            UID = inner.UID;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public void SetImagingDevice(string imagingDeviceId) => _inner.SetImagingDevice(imagingDeviceId);
        public string FOR { get; }
        public System.Collections.Generic.IReadOnlyList<IImage> Images => _inner.Images?.Select(x => new AsyncImage(x, _service)).ToList();
        public string ImagingDeviceDepartment { get; }
        public string ImagingDeviceId { get; }
        public string ImagingDeviceManufacturer { get; }
        public string ImagingDeviceModel { get; }
        public string ImagingDeviceSerialNo { get; }
        public VMS.TPS.Common.Model.Types.SeriesModality Modality { get; }
        public IStudy Study => _inner.Study is null ? null : new AsyncStudy(_inner.Study, _service);

        public string UID { get; }
    }
}
