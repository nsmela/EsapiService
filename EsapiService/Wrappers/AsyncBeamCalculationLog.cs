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
    public class AsyncBeamCalculationLog : AsyncSerializableObject, IBeamCalculationLog
    {
        internal readonly VMS.TPS.Common.Model.API.BeamCalculationLog _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncBeamCalculationLog(VMS.TPS.Common.Model.API.BeamCalculationLog inner, IEsapiService service) : base(inner, service)
        {
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

        public Task<IReadOnlyList<string>> GetMessageLinesAsync()
        {
            return _service.PostAsync(context => _inner.MessageLines?.ToList());
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BeamCalculationLog> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BeamCalculationLog, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
