using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
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


        public Task<bool> LoadSavedPlanCollectionAsync() => _service.RunAsync(() => _inner.LoadSavedPlanCollection());

        public Task<bool> CreatePlanCollectionAsync(bool continueOptimization, ITradeoffPlanGenerationIntermediateDoseMode intermediateDoseMode, bool useHybridOptimizationForVmat) => _service.RunAsync(() => _inner.CreatePlanCollection(continueOptimization, intermediateDoseMode, useHybridOptimizationForVmat));

        public Task<double> GetObjectiveCostAsync(ITradeoffObjective objective) => _service.RunAsync(() => _inner.GetObjectiveCost(objective));

        public Task<double> GetObjectiveLowerLimitAsync(ITradeoffObjective objective) => _service.RunAsync(() => _inner.GetObjectiveLowerLimit(objective));

        public Task<double> GetObjectiveUpperLimitAsync(ITradeoffObjective objective) => _service.RunAsync(() => _inner.GetObjectiveUpperLimit(objective));

        public Task<double> GetObjectiveUpperRestrictorAsync(ITradeoffObjective objective) => _service.RunAsync(() => _inner.GetObjectiveUpperRestrictor(objective));

        public Task SetObjectiveCostAsync(ITradeoffObjective tradeoffObjective, double cost) => _service.RunAsync(() => _inner.SetObjectiveCost(tradeoffObjective, cost));

        public Task SetObjectiveUpperRestrictorAsync(ITradeoffObjective tradeoffObjective, double restrictorValue) => _service.RunAsync(() => _inner.SetObjectiveUpperRestrictor(tradeoffObjective, restrictorValue));

        public Task ResetToBalancedPlanAsync() => _service.RunAsync(() => _inner.ResetToBalancedPlan());

        public async Task<IDVHData> GetStructureDvhAsync(IStructure structure)
        {
            return await _service.RunAsync(() => 
                _inner.GetStructureDvh(structure) is var result && result is null ? null : new AsyncDVHData(result, _service));
        }


        public Task<bool> AddTargetHomogeneityObjectiveAsync(IStructure targetStructure) => _service.RunAsync(() => _inner.AddTargetHomogeneityObjective(targetStructure));

        public Task<bool> AddTradeoffObjectiveAsync(IStructure structure) => _service.RunAsync(() => _inner.AddTradeoffObjective(structure));

        public Task<bool> AddTradeoffObjectiveAsync(IOptimizationObjective objective) => _service.RunAsync(() => _inner.AddTradeoffObjective(objective));

        public Task<bool> RemoveTradeoffObjectiveAsync(ITradeoffObjective tradeoffObjective) => _service.RunAsync(() => _inner.RemoveTradeoffObjective(tradeoffObjective));

        public Task<bool> RemoveTargetHomogeneityObjectiveAsync(IStructure targetStructure) => _service.RunAsync(() => _inner.RemoveTargetHomogeneityObjective(targetStructure));

        public Task<bool> RemoveTradeoffObjectiveAsync(IStructure structure) => _service.RunAsync(() => _inner.RemoveTradeoffObjective(structure));

        public Task RemovePlanCollectionAsync() => _service.RunAsync(() => _inner.RemovePlanCollection());

        public Task RemoveAllTradeoffObjectivesAsync() => _service.RunAsync(() => _inner.RemoveAllTradeoffObjectives());

        public Task ApplyTradeoffExplorationResultAsync() => _service.RunAsync(() => _inner.ApplyTradeoffExplorationResult());

        public Task<bool> CreateDeliverableVmatPlanAsync(bool useIntermediateDose) => _service.RunAsync(() => _inner.CreateDeliverableVmatPlan(useIntermediateDose));

        public bool HasPlanCollection { get; }

        public bool CanLoadSavedPlanCollection { get; }

        public bool CanCreatePlanCollection { get; }

        public bool CanUsePlanDoseAsIntermediateDose { get; }

        public bool CanUseHybridOptimizationInPlanGeneration { get; }

        public async Task<IReadOnlyList<IOptimizationObjective>> GetTradeoffObjectiveCandidatesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.TradeoffObjectiveCandidates?.Select(x => new AsyncOptimizationObjective(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<ITradeoffObjective>> GetTradeoffObjectivesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.TradeoffObjectives?.Select(x => new AsyncTradeoffObjective(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IStructure>> GetTradeoffStructureCandidatesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.TradeoffStructureCandidates?.Select(x => new AsyncStructure(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IStructure>> GetTargetStructuresAsync()
        {
            return await _service.RunAsync(() => 
                _inner.TargetStructures?.Select(x => new AsyncStructure(x, _service)).ToList());
        }


        public async Task<IDose> GetCurrentDoseAsync()
        {
            return await _service.RunAsync(() => 
                _inner.CurrentDose is null ? null : new AsyncDose(_inner.CurrentDose, _service));
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.TradeoffExplorationContext> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.TradeoffExplorationContext, T> func) => _service.RunAsync(() => func(_inner));
    }
}
