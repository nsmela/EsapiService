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
    public class AsyncTradeoffExplorationContext : ITradeoffExplorationContext, IEsapiWrapper<VMS.TPS.Common.Model.API.TradeoffExplorationContext>
    {
        internal readonly VMS.TPS.Common.Model.API.TradeoffExplorationContext _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncTradeoffExplorationContext(VMS.TPS.Common.Model.API.TradeoffExplorationContext inner, IEsapiService service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        // Simple Method
        public Task<bool> LoadSavedPlanCollectionAsync() => 
            _service.PostAsync(context => _inner.LoadSavedPlanCollection());

        // Simple Method
        public Task<bool> CreatePlanCollectionAsync(bool continueOptimization, TradeoffPlanGenerationIntermediateDoseMode intermediateDoseMode, bool useHybridOptimizationForVmat) => 
            _service.PostAsync(context => _inner.CreatePlanCollection(continueOptimization, intermediateDoseMode, useHybridOptimizationForVmat));

        // Simple Method
        public Task<double> GetObjectiveCostAsync(ITradeoffObjective objective) => 
            _service.PostAsync(context => _inner.GetObjectiveCost(((AsyncTradeoffObjective)objective)._inner));

        // Simple Method
        public Task<double> GetObjectiveLowerLimitAsync(ITradeoffObjective objective) => 
            _service.PostAsync(context => _inner.GetObjectiveLowerLimit(((AsyncTradeoffObjective)objective)._inner));

        // Simple Method
        public Task<double> GetObjectiveUpperLimitAsync(ITradeoffObjective objective) => 
            _service.PostAsync(context => _inner.GetObjectiveUpperLimit(((AsyncTradeoffObjective)objective)._inner));

        // Simple Method
        public Task<double> GetObjectiveUpperRestrictorAsync(ITradeoffObjective objective) => 
            _service.PostAsync(context => _inner.GetObjectiveUpperRestrictor(((AsyncTradeoffObjective)objective)._inner));

        // Simple Void Method
        public Task SetObjectiveCostAsync(ITradeoffObjective tradeoffObjective, double cost) 
        {
            _service.PostAsync(context => _inner.SetObjectiveCost(((AsyncTradeoffObjective)tradeoffObjective)._inner, cost));
            return Task.CompletedTask;
        }

        // Simple Void Method
        public Task SetObjectiveUpperRestrictorAsync(ITradeoffObjective tradeoffObjective, double restrictorValue) 
        {
            _service.PostAsync(context => _inner.SetObjectiveUpperRestrictor(((AsyncTradeoffObjective)tradeoffObjective)._inner, restrictorValue));
            return Task.CompletedTask;
        }

        // Simple Void Method
        public Task ResetToBalancedPlanAsync() 
        {
            _service.PostAsync(context => _inner.ResetToBalancedPlan());
            return Task.CompletedTask;
        }

        public async Task<IDVHData> GetStructureDvhAsync(IStructure structure)
        {
            return await _service.PostAsync(context => 
                _inner.GetStructureDvh(((AsyncStructure)structure)._inner) is var result && result is null ? null : new AsyncDVHData(result, _service));
        }

        // Simple Method
        public Task<bool> AddTargetHomogeneityObjectiveAsync(IStructure targetStructure) => 
            _service.PostAsync(context => _inner.AddTargetHomogeneityObjective(((AsyncStructure)targetStructure)._inner));

        // Simple Method
        public Task<bool> AddTradeoffObjectiveAsync(IStructure structure) => 
            _service.PostAsync(context => _inner.AddTradeoffObjective(((AsyncStructure)structure)._inner));

        // Simple Method
        public Task<bool> AddTradeoffObjectiveAsync(IOptimizationObjective objective) => 
            _service.PostAsync(context => _inner.AddTradeoffObjective(((AsyncOptimizationObjective)objective)._inner));

        // Simple Method
        public Task<bool> RemoveTradeoffObjectiveAsync(ITradeoffObjective tradeoffObjective) => 
            _service.PostAsync(context => _inner.RemoveTradeoffObjective(((AsyncTradeoffObjective)tradeoffObjective)._inner));

        // Simple Method
        public Task<bool> RemoveTargetHomogeneityObjectiveAsync(IStructure targetStructure) => 
            _service.PostAsync(context => _inner.RemoveTargetHomogeneityObjective(((AsyncStructure)targetStructure)._inner));

        // Simple Method
        public Task<bool> RemoveTradeoffObjectiveAsync(IStructure structure) => 
            _service.PostAsync(context => _inner.RemoveTradeoffObjective(((AsyncStructure)structure)._inner));

        // Simple Void Method
        public Task RemovePlanCollectionAsync() 
        {
            _service.PostAsync(context => _inner.RemovePlanCollection());
            return Task.CompletedTask;
        }

        // Simple Void Method
        public Task RemoveAllTradeoffObjectivesAsync() 
        {
            _service.PostAsync(context => _inner.RemoveAllTradeoffObjectives());
            return Task.CompletedTask;
        }

        // Simple Void Method
        public Task ApplyTradeoffExplorationResultAsync() 
        {
            _service.PostAsync(context => _inner.ApplyTradeoffExplorationResult());
            return Task.CompletedTask;
        }

        // Simple Method
        public Task<bool> CreateDeliverableVmatPlanAsync(bool useIntermediateDose) => 
            _service.PostAsync(context => _inner.CreateDeliverableVmatPlan(useIntermediateDose));

        public bool HasPlanCollection =>
            _inner.HasPlanCollection;


        public bool CanLoadSavedPlanCollection =>
            _inner.CanLoadSavedPlanCollection;


        public bool CanCreatePlanCollection =>
            _inner.CanCreatePlanCollection;


        public bool CanUsePlanDoseAsIntermediateDose =>
            _inner.CanUsePlanDoseAsIntermediateDose;


        public bool CanUseHybridOptimizationInPlanGeneration =>
            _inner.CanUseHybridOptimizationInPlanGeneration;


        public IReadOnlyList<OptimizationObjective> TradeoffObjectiveCandidates =>
            _inner.TradeoffObjectiveCandidates;


        public IReadOnlyCollection<TradeoffObjective> TradeoffObjectives =>
            _inner.TradeoffObjectives;


        public IReadOnlyList<Structure> TradeoffStructureCandidates =>
            _inner.TradeoffStructureCandidates;


        public IReadOnlyList<Structure> TargetStructures =>
            _inner.TargetStructures;


        public async Task<IDose> GetCurrentDoseAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.CurrentDose is null ? null : new AsyncDose(_inner.CurrentDose, _service);
                return innerResult;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.TradeoffExplorationContext> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.TradeoffExplorationContext, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.TradeoffExplorationContext(AsyncTradeoffExplorationContext wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.TradeoffExplorationContext IEsapiWrapper<VMS.TPS.Common.Model.API.TradeoffExplorationContext>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.TradeoffExplorationContext>.Service => _service;
    }
}
