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
    public class AsyncWedge : AsyncAddOn, IWedge, IEsapiWrapper<VMS.TPS.Common.Model.API.Wedge>
    {
        internal new readonly VMS.TPS.Common.Model.API.Wedge _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncWedge(VMS.TPS.Common.Model.API.Wedge inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            Direction = inner.Direction;
            WedgeAngle = inner.WedgeAngle;
        }

        public double Direction { get; }

        public double WedgeAngle { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Wedge> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Wedge, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.Wedge(AsyncWedge wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Wedge IEsapiWrapper<VMS.TPS.Common.Model.API.Wedge>.Inner => _inner;
    }
}
