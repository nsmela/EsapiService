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
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            IsocenterToLateralSpreadingDeviceDistance = inner.IsocenterToLateralSpreadingDeviceDistance;
            LateralSpreadingDeviceSetting = inner.LateralSpreadingDeviceSetting;
            LateralSpreadingDeviceWaterEquivalentThickness = inner.LateralSpreadingDeviceWaterEquivalentThickness;
        }

        public double IsocenterToLateralSpreadingDeviceDistance { get; }

        public string LateralSpreadingDeviceSetting { get; }

        public double LateralSpreadingDeviceWaterEquivalentThickness { get; }

        public async Task<ILateralSpreadingDevice> GetReferencedLateralSpreadingDeviceAsync()
        {
            return await _service.PostAsync(context => 
                _inner.ReferencedLateralSpreadingDevice is null ? null : new AsyncLateralSpreadingDevice(_inner.ReferencedLateralSpreadingDevice, _service));
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.LateralSpreadingDeviceSettings> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.LateralSpreadingDeviceSettings, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.LateralSpreadingDeviceSettings(AsyncLateralSpreadingDeviceSettings wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.LateralSpreadingDeviceSettings IEsapiWrapper<VMS.TPS.Common.Model.API.LateralSpreadingDeviceSettings>.Inner => _inner;
    }
}
