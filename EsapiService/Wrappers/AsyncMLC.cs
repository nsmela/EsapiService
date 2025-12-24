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
    public class AsyncMLC : AsyncAddOn, IMLC, IEsapiWrapper<VMS.TPS.Common.Model.API.MLC>
    {
        internal new readonly VMS.TPS.Common.Model.API.MLC _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncMLC(VMS.TPS.Common.Model.API.MLC inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            ManufacturerName = inner.ManufacturerName;
            MinDoseDynamicLeafGap = inner.MinDoseDynamicLeafGap;
            Model = inner.Model;
            SerialNumber = inner.SerialNumber;
        }


        public string ManufacturerName { get; private set; }


        public double MinDoseDynamicLeafGap { get; private set; }


        public string Model { get; private set; }


        public string SerialNumber { get; private set; }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.MLC> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.MLC, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            ManufacturerName = _inner.ManufacturerName;
            MinDoseDynamicLeafGap = _inner.MinDoseDynamicLeafGap;
            Model = _inner.Model;
            SerialNumber = _inner.SerialNumber;
        }

        public static implicit operator VMS.TPS.Common.Model.API.MLC(AsyncMLC wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.MLC IEsapiWrapper<VMS.TPS.Common.Model.API.MLC>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.MLC>.Service => _service;
    }
}
