using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncEstimatedDVH : AsyncApiDataObject, IEstimatedDVH
    {
        internal readonly VMS.TPS.Common.Model.API.EstimatedDVH _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncEstimatedDVH(VMS.TPS.Common.Model.API.EstimatedDVH inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            CurveData = inner.CurveData;
            PlanSetupId = inner.PlanSetupId;
            StructureId = inner.StructureId;
            TargetDoseLevel = inner.TargetDoseLevel;
            Type = inner.Type;
        }


        public DVHPoint[] CurveData { get; }

        public async Task<IPlanSetup> GetPlanSetupAsync()
        {
            return await _service.RunAsync(() => 
                _inner.PlanSetup is null ? null : new AsyncPlanSetup(_inner.PlanSetup, _service));
        }

        public string PlanSetupId { get; }

        public async Task<IStructure> GetStructureAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Structure is null ? null : new AsyncStructure(_inner.Structure, _service));
        }

        public string StructureId { get; }

        public DoseValue TargetDoseLevel { get; }

        public DVHEstimateType Type { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.EstimatedDVH> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.EstimatedDVH, T> func) => _service.RunAsync(() => func(_inner));
    }
}
