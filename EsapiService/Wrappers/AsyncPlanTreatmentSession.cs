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
    public class AsyncPlanTreatmentSession : AsyncApiDataObject, IPlanTreatmentSession, IEsapiWrapper<VMS.TPS.Common.Model.API.PlanTreatmentSession>
    {
        internal new readonly VMS.TPS.Common.Model.API.PlanTreatmentSession _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncPlanTreatmentSession(VMS.TPS.Common.Model.API.PlanTreatmentSession inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            Status = inner.Status;
        }


        public async Task<IPlanSetup> GetPlanSetupAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.PlanSetup is null ? null : new AsyncPlanSetup(_inner.PlanSetup, _service);
                return innerResult;
            });
        }

        public TreatmentSessionStatus Status { get; private set; }

        public async Task<ITreatmentSession> GetTreatmentSessionAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.TreatmentSession is null ? null : new AsyncTreatmentSession(_inner.TreatmentSession, _service);
                return innerResult;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanTreatmentSession> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanTreatmentSession, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            Status = _inner.Status;
        }

        public static implicit operator VMS.TPS.Common.Model.API.PlanTreatmentSession(AsyncPlanTreatmentSession wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.PlanTreatmentSession IEsapiWrapper<VMS.TPS.Common.Model.API.PlanTreatmentSession>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.PlanTreatmentSession>.Service => _service;
    }
}
