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
    public partial class AsyncRTPrescriptionTarget : AsyncApiDataObject, IRTPrescriptionTarget, IEsapiWrapper<VMS.TPS.Common.Model.API.RTPrescriptionTarget>
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
        }


        public async Task<IReadOnlyList<IRTPrescriptionConstraint>> GetConstraintsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Constraints?.Select(x => new AsyncRTPrescriptionConstraint(x, _service)).ToList());
        }


        public DoseValue DosePerFraction =>
            _inner.DosePerFraction;


        public int NumberOfFractions =>
            _inner.NumberOfFractions;


        public string TargetId =>
            _inner.TargetId;


        public RTPrescriptionTargetType Type =>
            _inner.Type;


        public double Value =>
            _inner.Value;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RTPrescriptionTarget> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RTPrescriptionTarget, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.RTPrescriptionTarget(AsyncRTPrescriptionTarget wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.RTPrescriptionTarget IEsapiWrapper<VMS.TPS.Common.Model.API.RTPrescriptionTarget>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.RTPrescriptionTarget>.Service => _service;
    }
}
