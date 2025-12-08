using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncTreatmentSession : ITreatmentSession
    {
        internal readonly VMS.TPS.Common.Model.API.TreatmentSession _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncTreatmentSession(VMS.TPS.Common.Model.API.TreatmentSession inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            SessionNumber = inner.SessionNumber;
        }


        public long SessionNumber { get; }

        public async Task<IReadOnlyList<IPlanTreatmentSession>> GetSessionPlansAsync()
        {
            return await _service.RunAsync(() => 
                _inner.SessionPlans?.Select(x => new AsyncPlanTreatmentSession(x, _service)).ToList());
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.TreatmentSession> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.TreatmentSession, T> func) => _service.RunAsync(() => func(_inner));
    }
}
