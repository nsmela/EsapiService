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
    public class AsyncEstimatedDVH : AsyncApiDataObject, IEstimatedDVH, IEsapiWrapper<VMS.TPS.Common.Model.API.EstimatedDVH>
    {
        internal new readonly VMS.TPS.Common.Model.API.EstimatedDVH _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncEstimatedDVH(VMS.TPS.Common.Model.API.EstimatedDVH inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public DVHPoint[] CurveData =>
            _inner.CurveData;


        public async Task<IPlanSetup> GetPlanSetupAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.PlanSetup is null ? null : new AsyncPlanSetup(_inner.PlanSetup, _service);
                return innerResult;
            });
        }

        public string PlanSetupId =>
            _inner.PlanSetupId;


        public async Task<IStructure> GetStructureAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Structure is null ? null : new AsyncStructure(_inner.Structure, _service);
                return innerResult;
            });
        }

        public string StructureId =>
            _inner.StructureId;


        public DoseValue TargetDoseLevel =>
            _inner.TargetDoseLevel;


        public DVHEstimateType Type =>
            _inner.Type;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.EstimatedDVH> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.EstimatedDVH, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.EstimatedDVH(AsyncEstimatedDVH wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.EstimatedDVH IEsapiWrapper<VMS.TPS.Common.Model.API.EstimatedDVH>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.EstimatedDVH>.Service => _service;
    }
}
