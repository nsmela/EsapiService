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
    public class AsyncRangeModulator : AsyncAddOn, IRangeModulator, IEsapiWrapper<VMS.TPS.Common.Model.API.RangeModulator>
    {
        internal new readonly VMS.TPS.Common.Model.API.RangeModulator _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncRangeModulator(VMS.TPS.Common.Model.API.RangeModulator inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            Type = inner.Type;
        }


        public RangeModulatorType Type { get; private set; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RangeModulator> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RangeModulator, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            Type = _inner.Type;
        }

        public static implicit operator VMS.TPS.Common.Model.API.RangeModulator(AsyncRangeModulator wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.RangeModulator IEsapiWrapper<VMS.TPS.Common.Model.API.RangeModulator>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.RangeModulator>.Service => _service;
    }
}
