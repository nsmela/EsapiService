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
    public class AsyncPlanSum : AsyncPlanningItem, IPlanSum, IEsapiWrapper<VMS.TPS.Common.Model.API.PlanSum>
    {
        internal new readonly VMS.TPS.Common.Model.API.PlanSum _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncPlanSum(VMS.TPS.Common.Model.API.PlanSum inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

        }


        // Simple Method
        public Task<PlanSumOperation> GetPlanSumOperationAsync(IPlanSetup planSetupInPlanSum) => 
            _service.PostAsync(context => _inner.GetPlanSumOperation(((AsyncPlanSetup)planSetupInPlanSum)._inner));

        // Simple Method
        public Task<double> GetPlanWeightAsync(IPlanSetup planSetupInPlanSum) => 
            _service.PostAsync(context => _inner.GetPlanWeight(((AsyncPlanSetup)planSetupInPlanSum)._inner));

        public async Task<ICourse> GetCourseAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Course is null ? null : new AsyncCourse(_inner.Course, _service);
                return innerResult;
            });
        }

        public async Task<IReadOnlyList<IPlanSumComponent>> GetPlanSumComponentsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.PlanSumComponents?.Select(x => new AsyncPlanSumComponent(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IPlanSetup>> GetPlanSetupsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.PlanSetups?.Select(x => new AsyncPlanSetup(x, _service)).ToList());
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanSum> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanSum, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.PlanSum(AsyncPlanSum wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.PlanSum IEsapiWrapper<VMS.TPS.Common.Model.API.PlanSum>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.PlanSum>.Service => _service;
    }
}
