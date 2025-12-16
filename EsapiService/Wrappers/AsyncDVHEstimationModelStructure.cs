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
    public class AsyncDVHEstimationModelStructure : AsyncSerializableObject, IDVHEstimationModelStructure, IEsapiWrapper<VMS.TPS.Common.Model.API.DVHEstimationModelStructure>
    {
        internal new readonly VMS.TPS.Common.Model.API.DVHEstimationModelStructure _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncDVHEstimationModelStructure(VMS.TPS.Common.Model.API.DVHEstimationModelStructure inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            Id = inner.Id;
            IsValid = inner.IsValid;
            ModelStructureGuid = inner.ModelStructureGuid;
            StructureCodes = inner.StructureCodes;
            StructureType = inner.StructureType;
        }

        public string Id { get; }

        public bool IsValid { get; }

        public System.Guid ModelStructureGuid { get; }

        public IReadOnlyList<StructureCode> StructureCodes { get; }

        public DVHEstimationStructureType StructureType { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.DVHEstimationModelStructure> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.DVHEstimationModelStructure, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.DVHEstimationModelStructure(AsyncDVHEstimationModelStructure wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.DVHEstimationModelStructure IEsapiWrapper<VMS.TPS.Common.Model.API.DVHEstimationModelStructure>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.DVHEstimationModelStructure>.Service => _service;

        /* --- Skipped Members (Not generated) ---
           - .ctor: Explicitly ignored by name
        */
    }
}
