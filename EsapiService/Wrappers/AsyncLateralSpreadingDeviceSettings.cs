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
    public class AsyncLateralSpreadingDeviceSettings : AsyncSerializableObject, ILateralSpreadingDeviceSettings, IEsapiWrapper<VMS.TPS.Common.Model.API.LateralSpreadingDeviceSettings>
    {
        internal new readonly VMS.TPS.Common.Model.API.LateralSpreadingDeviceSettings _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncLateralSpreadingDeviceSettings(VMS.TPS.Common.Model.API.LateralSpreadingDeviceSettings inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public double IsocenterToLateralSpreadingDeviceDistance =>
            _inner.IsocenterToLateralSpreadingDeviceDistance;


        public string LateralSpreadingDeviceSetting =>
            _inner.LateralSpreadingDeviceSetting;


        public double LateralSpreadingDeviceWaterEquivalentThickness =>
            _inner.LateralSpreadingDeviceWaterEquivalentThickness;


        public async Task<ILateralSpreadingDevice> GetReferencedLateralSpreadingDeviceAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.ReferencedLateralSpreadingDevice is null ? null : new AsyncLateralSpreadingDevice(_inner.ReferencedLateralSpreadingDevice, _service);
                return innerResult;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.LateralSpreadingDeviceSettings> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.LateralSpreadingDeviceSettings, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.LateralSpreadingDeviceSettings(AsyncLateralSpreadingDeviceSettings wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.LateralSpreadingDeviceSettings IEsapiWrapper<VMS.TPS.Common.Model.API.LateralSpreadingDeviceSettings>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.LateralSpreadingDeviceSettings>.Service => _service;
    }
}
