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
    public class AsyncMotorizedWedge : AsyncWedge, IMotorizedWedge
    {
        internal readonly VMS.TPS.Common.Model.API.MotorizedWedge _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncMotorizedWedge(VMS.TPS.Common.Model.API.MotorizedWedge inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.MotorizedWedge> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.MotorizedWedge, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
