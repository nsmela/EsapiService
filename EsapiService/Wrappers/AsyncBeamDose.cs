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
    public class AsyncBeamDose : AsyncDose, IBeamDose
    {
        internal readonly VMS.TPS.Common.Model.API.BeamDose _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncBeamDose(VMS.TPS.Common.Model.API.BeamDose inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }


        public Task<DoseValue> GetAbsoluteBeamDoseValueAsync(DoseValue relative) => _service.PostAsync(context => _inner.GetAbsoluteBeamDoseValue(relative));

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BeamDose> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BeamDose, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
