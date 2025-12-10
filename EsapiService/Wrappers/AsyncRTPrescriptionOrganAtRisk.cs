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
    public class AsyncRTPrescriptionOrganAtRisk : AsyncSerializableObject, IRTPrescriptionOrganAtRisk
    {
        internal new readonly VMS.TPS.Common.Model.API.RTPrescriptionOrganAtRisk _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncRTPrescriptionOrganAtRisk(VMS.TPS.Common.Model.API.RTPrescriptionOrganAtRisk inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            OrganAtRiskId = inner.OrganAtRiskId;
        }


        public async Task<IReadOnlyList<IRTPrescriptionConstraint>> GetConstraintsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Constraints?.Select(x => new AsyncRTPrescriptionConstraint(x, _service)).ToList());
        }


        public string OrganAtRiskId { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RTPrescriptionOrganAtRisk> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RTPrescriptionOrganAtRisk, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.RTPrescriptionOrganAtRisk(AsyncRTPrescriptionOrganAtRisk wrapper) => wrapper._inner;
    }
}
