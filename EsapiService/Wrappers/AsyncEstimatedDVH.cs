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
    public class AsyncEstimatedDVH : AsyncApiDataObject, IEstimatedDVH
    {
        internal new readonly VMS.TPS.Common.Model.API.EstimatedDVH _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncEstimatedDVH(VMS.TPS.Common.Model.API.EstimatedDVH inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            PlanSetupId = inner.PlanSetupId;
            StructureId = inner.StructureId;
        }


        public async Task<IPlanSetup> GetPlanSetupAsync()
        {
            return await _service.PostAsync(context => 
                _inner.PlanSetup is null ? null : new AsyncPlanSetup(_inner.PlanSetup, _service));
        }

        public string PlanSetupId { get; }

        public async Task<IStructure> GetStructureAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Structure is null ? null : new AsyncStructure(_inner.Structure, _service));
        }

        public string StructureId { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.EstimatedDVH> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.EstimatedDVH, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.EstimatedDVH(AsyncEstimatedDVH wrapper) => wrapper._inner;
    }
}
