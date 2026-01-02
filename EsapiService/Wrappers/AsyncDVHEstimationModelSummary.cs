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
    public class AsyncDVHEstimationModelSummary : AsyncSerializableObject, IDVHEstimationModelSummary, IEsapiWrapper<VMS.TPS.Common.Model.API.DVHEstimationModelSummary>
    {
        internal new readonly VMS.TPS.Common.Model.API.DVHEstimationModelSummary _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncDVHEstimationModelSummary(VMS.TPS.Common.Model.API.DVHEstimationModelSummary inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public string Description =>
            _inner.Description;


        public bool IsPublished =>
            _inner.IsPublished;


        public bool IsTrained =>
            _inner.IsTrained;


        public string ModelDataVersion =>
            _inner.ModelDataVersion;


        public ParticleType ModelParticleType =>
            _inner.ModelParticleType;


        public System.Guid ModelUID =>
            _inner.ModelUID;


        public string Name =>
            _inner.Name;


        public int Revision =>
            _inner.Revision;


        public string TreatmentSite =>
            _inner.TreatmentSite;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.DVHEstimationModelSummary> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.DVHEstimationModelSummary, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.DVHEstimationModelSummary(AsyncDVHEstimationModelSummary wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.DVHEstimationModelSummary IEsapiWrapper<VMS.TPS.Common.Model.API.DVHEstimationModelSummary>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.DVHEstimationModelSummary>.Service => _service;
    }
}
