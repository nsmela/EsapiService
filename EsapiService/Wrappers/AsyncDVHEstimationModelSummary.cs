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
    public class AsyncDVHEstimationModelSummary : AsyncSerializableObject, IDVHEstimationModelSummary
    {
        internal new readonly VMS.TPS.Common.Model.API.DVHEstimationModelSummary _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncDVHEstimationModelSummary(VMS.TPS.Common.Model.API.DVHEstimationModelSummary inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Description = inner.Description;
            IsPublished = inner.IsPublished;
            IsTrained = inner.IsTrained;
            ModelDataVersion = inner.ModelDataVersion;
            ModelUID = inner.ModelUID;
            Name = inner.Name;
            Revision = inner.Revision;
            TreatmentSite = inner.TreatmentSite;
        }


        public string Description { get; }

        public bool IsPublished { get; }

        public bool IsTrained { get; }

        public string ModelDataVersion { get; }

        public System.Guid ModelUID { get; }

        public string Name { get; }

        public int Revision { get; }

        public string TreatmentSite { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.DVHEstimationModelSummary> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.DVHEstimationModelSummary, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.DVHEstimationModelSummary(AsyncDVHEstimationModelSummary wrapper) => wrapper._inner;
    }
}
