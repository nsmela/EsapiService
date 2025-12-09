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
    public class AsyncTradeoffExplorationContext : ITradeoffExplorationContext
    {
        internal readonly VMS.TPS.Common.Model.API.TradeoffExplorationContext _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncTradeoffExplorationContext(VMS.TPS.Common.Model.API.TradeoffExplorationContext inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

            HasPlanCollection = inner.HasPlanCollection;
            CanLoadSavedPlanCollection = inner.CanLoadSavedPlanCollection;
            CanCreatePlanCollection = inner.CanCreatePlanCollection;
            CanUsePlanDoseAsIntermediateDose = inner.CanUsePlanDoseAsIntermediateDose;
            CanUseHybridOptimizationInPlanGeneration = inner.CanUseHybridOptimizationInPlanGeneration;
        }


        public Task<bool> LoadSavedPlanCollectionAsync() => _service.PostAsync(context => _inner.LoadSavedPlanCollection());

        public Task<bool> CreatePlanCollectionAsync(bool continueOptimization, ITradeoffPlanGenerationIntermediateDoseMode intermediateDoseMode, bool useHybridOptimizationForVmat) => _service.PostAsync(context => _inner.CreatePlanCollection(continueOptimization, ((AsyncTradeoffPlanGenerationIntermediateDoseMode)intermediateDoseMode)._inner, useHybridOptimizationForVmat));

        public Task<double> GetObjectiveCostAsync(ITradeoffObjective objective) => _service.PostAsync(context => _inner.GetObjectiveCost(((AsyncTradeoffObjective)objective)._inner));

        public Task<double> GetObjectiveLowerLimitAsync(ITradeoffObjective objective) => _service.PostAsync(context => _inner.GetObjectiveLowerLimit(((AsyncTradeoffObjective)objective)._inner));

        public Task<double> GetObjectiveUpperLimitAsync(ITradeoffObjective objective) => _service.PostAsync(context => _inner.GetObjectiveUpperLimit(((AsyncTradeoffObjective)objective)._inner));

        public Task<double> GetObjectiveUpperRestrictorAsync(ITradeoffObjective objective) => _service.PostAsync(context => _inner.GetObjectiveUpperRestrictor(((AsyncTradeoffObjective)objective)._inner));

        public Task SetObjectiveCostAsync(ITradeoffObjective tradeoffObjective, double cost) => _service.PostAsync(context => _inner.SetObjectiveCost(((AsyncTradeoffObjective)tradeoffObjective)._inner, cost));

        public Task SetObjectiveUpperRestrictorAsync(ITradeoffObjective tradeoffObjective, double restrictorValue) => _service.PostAsync(context => _inner.SetObjectiveUpperRestrictor(((AsyncTradeoffObjective)tradeoffObjective)._inner, restrictorValue));

        public Task ResetToBalancedPlanAsync() => _service.PostAsync(context => _inner.ResetToBalancedPlan());

        public async Task<IDVHData> GetStructureDvhAsync(IStructure structure)
        {
            return await _service.PostAsync(context => 
                _inner.GetStructureDvh(((AsyncStructure)structure)._inner) is var result && result is null ? null : new AsyncDVHData(result, _service));
        }


        public Task<bool> AddTargetHomogeneityObjectiveAsync(IStructure targetStructure) => _service.PostAsync(context => _inner.AddTargetHomogeneityObjective(((AsyncStructure)targetStructure)._inner));

        public Task<bool> AddTradeoffObjectiveAsync(IStructure structure) => _service.PostAsync(context => _inner.AddTradeoffObjective(((AsyncStructure)structure)._inner));

        public Task<bool> AddTradeoffObjectiveAsync(IOptimizationObjective objective) => _service.PostAsync(context => _inner.AddTradeoffObjective(((AsyncOptimizationObjective)objective)._inner));

        public Task<bool> RemoveTradeoffObjectiveAsync(ITradeoffObjective tradeoffObjective) => _service.PostAsync(context => _inner.RemoveTradeoffObjective(((AsyncTradeoffObjective)tradeoffObjective)._inner));

        public Task<bool> RemoveTargetHomogeneityObjectiveAsync(IStructure targetStructure) => _service.PostAsync(context => _inner.RemoveTargetHomogeneityObjective(((AsyncStructure)targetStructure)._inner));

        public Task<bool> RemoveTradeoffObjectiveAsync(IStructure structure) => _service.PostAsync(context => _inner.RemoveTradeoffObjective(((AsyncStructure)structure)._inner));

        public Task RemovePlanCollectionAsync() => _service.PostAsync(context => _inner.RemovePlanCollection());

        public Task RemoveAllTradeoffObjectivesAsync() => _service.PostAsync(context => _inner.RemoveAllTradeoffObjectives());

        public Task ApplyTradeoffExplorationResultAsync() => _service.PostAsync(context => _inner.ApplyTradeoffExplorationResult());

        public Task<bool> CreateDeliverableVmatPlanAsync(bool useIntermediateDose) => _service.PostAsync(context => _inner.CreateDeliverableVmatPlan(useIntermediateDose));

        public bool HasPlanCollection { get; }

        public bool CanLoadSavedPlanCollection { get; }

        public bool CanCreatePlanCollection { get; }

        public bool CanUsePlanDoseAsIntermediateDose { get; }

        public bool CanUseHybridOptimizationInPlanGeneration { get; }

        public async Task<IReadOnlyList<IOptimizationObjective>> GetTradeoffObjectiveCandidatesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.TradeoffObjectiveCandidates?.Select(x => new AsyncOptimizationObjective(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<ITradeoffObjective>> GetTradeoffObjectivesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.TradeoffObjectives?.Select(x => new AsyncTradeoffObjective(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IStructure>> GetTradeoffStructureCandidatesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.TradeoffStructureCandidates?.Select(x => new AsyncStructure(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IStructure>> GetTargetStructuresAsync()
        {
            return await _service.PostAsync(context => 
                _inner.TargetStructures?.Select(x => new AsyncStructure(x, _service)).ToList());
        }


        public async Task<IDose> GetCurrentDoseAsync()
        {
            return await _service.PostAsync(context => 
                _inner.CurrentDose is null ? null : new AsyncDose(_inner.CurrentDose, _service));
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.TradeoffExplorationContext> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.TradeoffExplorationContext, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
