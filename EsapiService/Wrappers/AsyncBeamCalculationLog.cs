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
    public class AsyncBeamCalculationLog : AsyncSerializableObject, IBeamCalculationLog, IEsapiWrapper<VMS.TPS.Common.Model.API.BeamCalculationLog>
    {
        internal new readonly VMS.TPS.Common.Model.API.BeamCalculationLog _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncBeamCalculationLog(VMS.TPS.Common.Model.API.BeamCalculationLog inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            Category = inner.Category;
        }

        public async Task<IBeam> GetBeamAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Beam is null ? null : new AsyncBeam(_inner.Beam, _service));
        }

        public string Category { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BeamCalculationLog> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BeamCalculationLog, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.BeamCalculationLog(AsyncBeamCalculationLog wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.BeamCalculationLog IEsapiWrapper<VMS.TPS.Common.Model.API.BeamCalculationLog>.Inner => _inner;

        /* --- Skipped Members (Not generated) ---
           - MessageLines: No matching factory found (Not Implemented)
        */
    }
}
