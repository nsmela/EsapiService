using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;

namespace Esapi.Wrappers
{
    public class AsyncPlanTreatmentSession : IPlanTreatmentSession
    {
        internal readonly VMS.TPS.Common.Model.API.PlanTreatmentSession _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncPlanTreatmentSession(VMS.TPS.Common.Model.API.PlanTreatmentSession inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Status = inner.Status;
        }


        public async Task<IPlanSetup> GetPlanSetupAsync()
        {
            return await _service.RunAsync(() => 
                _inner.PlanSetup is null ? null : new AsyncPlanSetup(_inner.PlanSetup, _service));
        }

        public TreatmentSessionStatus Status { get; }

        public async Task<ITreatmentSession> GetTreatmentSessionAsync()
        {
            return await _service.RunAsync(() => 
                _inner.TreatmentSession is null ? null : new AsyncTreatmentSession(_inner.TreatmentSession, _service));
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanTreatmentSession> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanTreatmentSession, T> func) => _service.RunAsync(() => func(_inner));
    }
}
