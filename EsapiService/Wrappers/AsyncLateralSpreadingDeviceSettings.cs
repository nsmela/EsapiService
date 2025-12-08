using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;

namespace Esapi.Wrappers
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


        public double IsocenterToLateralSpreadingDeviceDistance { get; }

        public string LateralSpreadingDeviceSetting { get; }

        public double LateralSpreadingDeviceWaterEquivalentThickness { get; }

        public async Task<ILateralSpreadingDevice> GetReferencedLateralSpreadingDeviceAsync()
        {
            return await _service.RunAsync(() => 
                _inner.ReferencedLateralSpreadingDevice is null ? null : new AsyncLateralSpreadingDevice(_inner.ReferencedLateralSpreadingDevice, _service));
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.LateralSpreadingDeviceSettings> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.LateralSpreadingDeviceSettings, T> func) => _service.RunAsync(() => func(_inner));
    }
}
