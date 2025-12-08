using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;

namespace Esapi.Wrappers
{
    public class AsyncLateralSpreadingDevice : ILateralSpreadingDevice
    {
        internal readonly VMS.TPS.Common.Model.API.LateralSpreadingDevice _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncLateralSpreadingDevice(VMS.TPS.Common.Model.API.LateralSpreadingDevice inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Type = inner.Type;
        }


        public LateralSpreadingDeviceType Type { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.LateralSpreadingDevice> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.LateralSpreadingDevice, T> func) => _service.RunAsync(() => func(_inner));
    }
}
