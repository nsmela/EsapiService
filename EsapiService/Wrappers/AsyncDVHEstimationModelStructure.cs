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
    public class AsyncDVHEstimationModelStructure : AsyncSerializableObject, IDVHEstimationModelStructure
    {
        internal readonly VMS.TPS.Common.Model.API.DVHEstimationModelStructure _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncDVHEstimationModelStructure(VMS.TPS.Common.Model.API.DVHEstimationModelStructure inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Id = inner.Id;
            IsValid = inner.IsValid;
            ModelStructureGuid = inner.ModelStructureGuid;
            StructureType = inner.StructureType;
        }


        public string Id { get; }

        public bool IsValid { get; }

        public System.Guid ModelStructureGuid { get; }

        public async Task<IReadOnlyList<IStructureCode>> GetStructureCodesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.StructureCodes?.Select(x => new AsyncStructureCode(x, _service)).ToList());
        }


        public DVHEstimationStructureType StructureType { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.DVHEstimationModelStructure> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.DVHEstimationModelStructure, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
