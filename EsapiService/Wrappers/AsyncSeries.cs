using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncSeries : AsyncApiDataObject, ISeries
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


        public Task SetImagingDeviceAsync(string imagingDeviceId) => _service.RunAsync(() => _inner.SetImagingDevice(imagingDeviceId));

        public string FOR { get; }

        public async Task<IReadOnlyList<IImage>> GetImagesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Images?.Select(x => new AsyncImage(x, _service)).ToList());
        }


        public string ImagingDeviceDepartment { get; }

        public string ImagingDeviceId { get; }

        public string ImagingDeviceManufacturer { get; }

        public string ImagingDeviceModel { get; }

        public string ImagingDeviceSerialNo { get; }

        public SeriesModality Modality { get; }

        public async Task<IStudy> GetStudyAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Study is null ? null : new AsyncStudy(_inner.Study, _service));
        }

        public string UID { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Series> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Series, T> func) => _service.RunAsync(() => func(_inner));
    }
}
