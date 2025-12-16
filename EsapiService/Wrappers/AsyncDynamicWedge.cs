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
    public class AsyncDynamicWedge : AsyncWedge, IDynamicWedge, IEsapiWrapper<VMS.TPS.Common.Model.API.DynamicWedge>
    {
        internal new readonly VMS.TPS.Common.Model.API.DynamicWedge _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncDynamicWedge(VMS.TPS.Common.Model.API.DynamicWedge inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.DynamicWedge> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.DynamicWedge, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.DynamicWedge(AsyncDynamicWedge wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.DynamicWedge IEsapiWrapper<VMS.TPS.Common.Model.API.DynamicWedge>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.DynamicWedge>.Service => _service;
    }
}
