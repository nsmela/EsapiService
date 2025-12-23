using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncSeries : AsyncApiDataObject, ISeries, IEsapiWrapper<VMS.TPS.Common.Model.API.Series>
    {
        internal new readonly VMS.TPS.Common.Model.API.Series _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncSeries(VMS.TPS.Common.Model.API.Series inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

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


        // Simple Void Method
        public Task SetImagingDeviceAsync(string imagingDeviceId) =>
            _service.PostAsync(context => _inner.SetImagingDevice(imagingDeviceId));

        public string FOR { get; private set; }

        public async Task<IReadOnlyList<IImage>> GetImagesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Images?.Select(x => new AsyncImage(x, _service)).ToList());
        }


        public string ImagingDeviceDepartment { get; private set; }

        public string ImagingDeviceId { get; private set; }

        public string ImagingDeviceManufacturer { get; private set; }

        public string ImagingDeviceModel { get; private set; }

        public string ImagingDeviceSerialNo { get; private set; }

        public SeriesModality Modality { get; private set; }

        public async Task<IStudy> GetStudyAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Study is null ? null : new AsyncStudy(_inner.Study, _service);
                return innerResult;
            });
        }

        public string UID { get; private set; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Series> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Series, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            FOR = _inner.FOR;
            ImagingDeviceDepartment = _inner.ImagingDeviceDepartment;
            ImagingDeviceId = _inner.ImagingDeviceId;
            ImagingDeviceManufacturer = _inner.ImagingDeviceManufacturer;
            ImagingDeviceModel = _inner.ImagingDeviceModel;
            ImagingDeviceSerialNo = _inner.ImagingDeviceSerialNo;
            Modality = _inner.Modality;
            UID = _inner.UID;
        }

        public static implicit operator VMS.TPS.Common.Model.API.Series(AsyncSeries wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Series IEsapiWrapper<VMS.TPS.Common.Model.API.Series>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Series>.Service => _service;
    }
}
