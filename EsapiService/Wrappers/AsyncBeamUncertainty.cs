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
    public class AsyncBeamUncertainty : AsyncApiDataObject, IBeamUncertainty, IEsapiWrapper<VMS.TPS.Common.Model.API.BeamUncertainty>
    {
        internal new readonly VMS.TPS.Common.Model.API.BeamUncertainty _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncBeamUncertainty(VMS.TPS.Common.Model.API.BeamUncertainty inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            BeamNumber = inner.BeamNumber;
        }

        public async Task<IBeam> GetBeamAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Beam is null ? null : new AsyncBeam(_inner.Beam, _service);
                return innerResult;
            });
        }

        public BeamNumber BeamNumber { get; }

        public async Task<IDose> GetDoseAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Dose is null ? null : new AsyncDose(_inner.Dose, _service);
                return innerResult;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BeamUncertainty> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BeamUncertainty, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.BeamUncertainty(AsyncBeamUncertainty wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.BeamUncertainty IEsapiWrapper<VMS.TPS.Common.Model.API.BeamUncertainty>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.BeamUncertainty>.Service => _service;
    }
}
