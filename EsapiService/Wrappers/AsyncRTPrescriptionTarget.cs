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
    public class AsyncRTPrescriptionTarget : AsyncApiDataObject, IRTPrescriptionTarget, IEsapiWrapper<VMS.TPS.Common.Model.API.RTPrescriptionTarget>
    {
        internal new readonly VMS.TPS.Common.Model.API.RTPrescriptionTarget _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncRTPrescriptionTarget(VMS.TPS.Common.Model.API.RTPrescriptionTarget inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Constraints = inner.Constraints;
            NumberOfFractions = inner.NumberOfFractions;
            TargetId = inner.TargetId;
            Value = inner.Value;
        }

        public IEnumerable<RTPrescriptionConstraint> Constraints { get; }

        public int NumberOfFractions { get; }

        public string TargetId { get; }

        public double Value { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RTPrescriptionTarget> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RTPrescriptionTarget, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.RTPrescriptionTarget(AsyncRTPrescriptionTarget wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.RTPrescriptionTarget IEsapiWrapper<VMS.TPS.Common.Model.API.RTPrescriptionTarget>.Inner => _inner;
    }
}
