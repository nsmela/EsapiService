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

            Description = inner.Description;
            IsPublished = inner.IsPublished;
            IsTrained = inner.IsTrained;
            ModelDataVersion = inner.ModelDataVersion;
            ModelParticleType = inner.ModelParticleType;
            ModelUID = inner.ModelUID;
            Name = inner.Name;
            Revision = inner.Revision;
            TreatmentSite = inner.TreatmentSite;
        }


        public string Description { get; private set; }


        public bool IsPublished { get; private set; }


        public bool IsTrained { get; private set; }


        public string ModelDataVersion { get; private set; }


        public ParticleType ModelParticleType { get; private set; }


        public System.Guid ModelUID { get; private set; }


        public string Name { get; private set; }


        public int Revision { get; private set; }


        public string TreatmentSite { get; private set; }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.DVHEstimationModelSummary> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.DVHEstimationModelSummary, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            Description = _inner.Description;
            IsPublished = _inner.IsPublished;
            IsTrained = _inner.IsTrained;
            ModelDataVersion = _inner.ModelDataVersion;
            ModelParticleType = _inner.ModelParticleType;
            ModelUID = _inner.ModelUID;
            Name = _inner.Name;
            Revision = _inner.Revision;
            TreatmentSite = _inner.TreatmentSite;
        }

        public static implicit operator VMS.TPS.Common.Model.API.DVHEstimationModelSummary(AsyncDVHEstimationModelSummary wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.DVHEstimationModelSummary IEsapiWrapper<VMS.TPS.Common.Model.API.DVHEstimationModelSummary>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.DVHEstimationModelSummary>.Service => _service;
    }
}
