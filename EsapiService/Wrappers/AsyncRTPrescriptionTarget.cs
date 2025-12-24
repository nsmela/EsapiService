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
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            DosePerFraction = inner.DosePerFraction;
            NumberOfFractions = inner.NumberOfFractions;
            TargetId = inner.TargetId;
            Type = inner.Type;
            Value = inner.Value;
        }


        public async Task<IReadOnlyList<IRTPrescriptionConstraint>> GetConstraintsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Constraints?.Select(x => new AsyncRTPrescriptionConstraint(x, _service)).ToList());
        }


        public DoseValue DosePerFraction { get; private set; }


        public int NumberOfFractions { get; private set; }


        public string TargetId { get; private set; }


        public RTPrescriptionTargetType Type { get; private set; }


        public double Value { get; private set; }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RTPrescriptionTarget> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RTPrescriptionTarget, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            DosePerFraction = _inner.DosePerFraction;
            NumberOfFractions = _inner.NumberOfFractions;
            TargetId = _inner.TargetId;
            Type = _inner.Type;
            Value = _inner.Value;
        }

        public static implicit operator VMS.TPS.Common.Model.API.RTPrescriptionTarget(AsyncRTPrescriptionTarget wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.RTPrescriptionTarget IEsapiWrapper<VMS.TPS.Common.Model.API.RTPrescriptionTarget>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.RTPrescriptionTarget>.Service => _service;
    }
}
