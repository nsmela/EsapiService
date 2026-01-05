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
    public partial class AsyncPlanSum : AsyncPlanningItem, IPlanSum, IEsapiWrapper<VMS.TPS.Common.Model.API.PlanSum>
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


        // Simple Void Method
        public Task AddItemAsync(IPlanningItem pi) 
        {
            _service.PostAsync(context => _inner.AddItem(((AsyncPlanningItem)pi)._inner));
            return Task.CompletedTask;
        }

        // Simple Void Method
        public Task AddItemAsync(IPlanningItem pi, PlanSumOperation operation, double planWeight) 
        {
            _service.PostAsync(context => _inner.AddItem(((AsyncPlanningItem)pi)._inner, operation, planWeight));
            return Task.CompletedTask;
        }

        // Simple Method
        public Task<PlanSumOperation> GetPlanSumOperationAsync(IPlanSetup planSetupInPlanSum) => 
            _service.PostAsync(context => _inner.GetPlanSumOperation(((AsyncPlanSetup)planSetupInPlanSum)._inner));

        // Simple Method
        public Task<double> GetPlanWeightAsync(IPlanSetup planSetupInPlanSum) => 
            _service.PostAsync(context => _inner.GetPlanWeight(((AsyncPlanSetup)planSetupInPlanSum)._inner));

        // Simple Void Method
        public Task RemoveItemAsync(IPlanningItem pi) 
        {
            _service.PostAsync(context => _inner.RemoveItem(((AsyncPlanningItem)pi)._inner));
            return Task.CompletedTask;
        }

        // Simple Void Method
        public Task SetPlanSumOperationAsync(IPlanSetup planSetupInPlanSum, PlanSumOperation operation) 
        {
            _service.PostAsync(context => _inner.SetPlanSumOperation(((AsyncPlanSetup)planSetupInPlanSum)._inner, operation));
            return Task.CompletedTask;
        }

        // Simple Void Method
        public Task SetPlanWeightAsync(IPlanSetup planSetupInPlanSum, double weight) 
        {
            _service.PostAsync(context => _inner.SetPlanWeight(((AsyncPlanSetup)planSetupInPlanSum)._inner, weight));
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<IPlanSumComponent>> GetPlanSumComponentsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.PlanSumComponents?.Select(x => new AsyncPlanSumComponent(x, _service)).ToList());
        }


        public new string Id
        {
            get => _inner.Id;
            set => _inner.Id = value;
        }


        public new string Name
        {
            get => _inner.Name;
            set => _inner.Name = value;
        }


        public async Task<IReadOnlyList<IPlanSetup>> GetPlanSetupsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.PlanSetups?.Select(x => new AsyncPlanSetup(x, _service)).ToList());
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanSum> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanSum, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.PlanSum(AsyncPlanSum wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.PlanSum IEsapiWrapper<VMS.TPS.Common.Model.API.PlanSum>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.PlanSum>.Service => _service;
    }
}
