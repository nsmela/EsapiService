namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
    public class AsyncRTPrescriptionTargetConstraints : IRTPrescriptionTargetConstraints
    {
        internal readonly VMS.TPS.Common.Model.API.RTPrescriptionTargetConstraints _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncRTPrescriptionTargetConstraints(VMS.TPS.Common.Model.API.RTPrescriptionTargetConstraints inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            TargetId = inner.TargetId;
        }

        public IReadOnlyList<IRTPrescriptionConstraint> Constraints => _inner.Constraints?.Select(x => new AsyncRTPrescriptionConstraint(x, _service)).ToList();
        public string TargetId { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RTPrescriptionTargetConstraints> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RTPrescriptionTargetConstraints, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
