using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Series : ApiDataObject
    {
        public Series()
        {
        }

        public void SetImagingDevice(string imagingDeviceId) { }
        public string FOR { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public string ImagingDeviceDepartment { get; set; }
        public string ImagingDeviceId { get; set; }
        public string ImagingDeviceManufacturer { get; set; }
        public string ImagingDeviceModel { get; set; }
        public string ImagingDeviceSerialNo { get; set; }
        public SeriesModality Modality { get; set; }
        public Study Study { get; set; }
        public string UID { get; set; }
    }
}
