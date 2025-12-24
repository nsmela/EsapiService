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
    public class AsyncLateralSpreadingDevice : AsyncAddOn, ILateralSpreadingDevice, IEsapiWrapper<VMS.TPS.Common.Model.API.LateralSpreadingDevice>
    {
        internal new readonly VMS.TPS.Common.Model.API.LateralSpreadingDevice _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncLateralSpreadingDevice(VMS.TPS.Common.Model.API.LateralSpreadingDevice inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public LateralSpreadingDeviceType Type =>
            _inner.Type;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.LateralSpreadingDevice> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.LateralSpreadingDevice, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.LateralSpreadingDevice(AsyncLateralSpreadingDevice wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.LateralSpreadingDevice IEsapiWrapper<VMS.TPS.Common.Model.API.LateralSpreadingDevice>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.LateralSpreadingDevice>.Service => _service;
    }
}
