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
    public class AsyncRangeShifter : AsyncAddOn, IRangeShifter, IEsapiWrapper<VMS.TPS.Common.Model.API.RangeShifter>
    {
        internal new readonly VMS.TPS.Common.Model.API.RangeShifter _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncRangeShifter(VMS.TPS.Common.Model.API.RangeShifter inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RangeShifter> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RangeShifter, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.RangeShifter(AsyncRangeShifter wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.RangeShifter IEsapiWrapper<VMS.TPS.Common.Model.API.RangeShifter>.Inner => _inner;
    }
}
