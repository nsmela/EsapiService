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
    public class AsyncRTPrescriptionConstraint : AsyncSerializableObject, IRTPrescriptionConstraint, IEsapiWrapper<VMS.TPS.Common.Model.API.RTPrescriptionConstraint>
    {
        internal new readonly VMS.TPS.Common.Model.API.RTPrescriptionConstraint _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncRTPrescriptionConstraint(VMS.TPS.Common.Model.API.RTPrescriptionConstraint inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            ConstraintType = inner.ConstraintType;
            Unit1 = inner.Unit1;
            Unit2 = inner.Unit2;
            Value1 = inner.Value1;
            Value2 = inner.Value2;
        }

        public RTPrescriptionConstraintType ConstraintType { get; }

        public string Unit1 { get; }

        public string Unit2 { get; }

        public string Value1 { get; }

        public string Value2 { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RTPrescriptionConstraint> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RTPrescriptionConstraint, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.RTPrescriptionConstraint(AsyncRTPrescriptionConstraint wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.RTPrescriptionConstraint IEsapiWrapper<VMS.TPS.Common.Model.API.RTPrescriptionConstraint>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.RTPrescriptionConstraint>.Service => _service;
    }
}
