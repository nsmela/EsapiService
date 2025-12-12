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
    public class AsyncTreatmentSession : AsyncApiDataObject, ITreatmentSession, IEsapiWrapper<VMS.TPS.Common.Model.API.TreatmentSession>
    {
        internal new readonly VMS.TPS.Common.Model.API.TreatmentSession _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncTreatmentSession(VMS.TPS.Common.Model.API.TreatmentSession inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            SessionNumber = inner.SessionNumber;
            SessionPlans = inner.SessionPlans;
        }

        public long SessionNumber { get; }

        public IEnumerable<PlanTreatmentSession> SessionPlans { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.TreatmentSession> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.TreatmentSession, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.TreatmentSession(AsyncTreatmentSession wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.TreatmentSession IEsapiWrapper<VMS.TPS.Common.Model.API.TreatmentSession>.Inner => _inner;
    }
}
