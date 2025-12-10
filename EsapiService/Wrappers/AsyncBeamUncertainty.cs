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
    public class AsyncBeamUncertainty : AsyncApiDataObject, IBeamUncertainty
    {
        internal new readonly VMS.TPS.Common.Model.API.BeamUncertainty _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncBeamUncertainty(VMS.TPS.Common.Model.API.BeamUncertainty inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }


        public async Task<IBeam> GetBeamAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Beam is null ? null : new AsyncBeam(_inner.Beam, _service));
        }

        public async Task<IDose> GetDoseAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Dose is null ? null : new AsyncDose(_inner.Dose, _service));
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BeamUncertainty> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BeamUncertainty, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.BeamUncertainty(AsyncBeamUncertainty wrapper) => wrapper._inner;
    }
}
