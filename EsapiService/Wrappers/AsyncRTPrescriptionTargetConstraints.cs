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
    public class AsyncRTPrescriptionTargetConstraints : AsyncSerializableObject, IRTPrescriptionTargetConstraints, IEsapiWrapper<VMS.TPS.Common.Model.API.RTPrescriptionTargetConstraints>
    {
        internal new readonly VMS.TPS.Common.Model.API.RTPrescriptionTargetConstraints _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncRTPrescriptionTargetConstraints(VMS.TPS.Common.Model.API.RTPrescriptionTargetConstraints inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            Constraints = inner.Constraints;
            TargetId = inner.TargetId;
        }

        public IEnumerable<RTPrescriptionConstraint> Constraints { get; }

        public string TargetId { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RTPrescriptionTargetConstraints> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RTPrescriptionTargetConstraints, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.RTPrescriptionTargetConstraints(AsyncRTPrescriptionTargetConstraints wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.RTPrescriptionTargetConstraints IEsapiWrapper<VMS.TPS.Common.Model.API.RTPrescriptionTargetConstraints>.Inner => _inner;
    }
}
