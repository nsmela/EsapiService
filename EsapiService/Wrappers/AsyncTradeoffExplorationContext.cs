namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
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

        public bool LoadSavedPlanCollection() => _inner.LoadSavedPlanCollection();
        public bool CreatePlanCollection(bool continueOptimization, ITradeoffPlanGenerationIntermediateDoseMode intermediateDoseMode, bool useHybridOptimizationForVmat) => _inner.CreatePlanCollection(continueOptimization, intermediateDoseMode, useHybridOptimizationForVmat);
        public double GetObjectiveCost(ITradeoffObjective objective) => _inner.GetObjectiveCost(objective);
        public double GetObjectiveLowerLimit(ITradeoffObjective objective) => _inner.GetObjectiveLowerLimit(objective);
        public double GetObjectiveUpperLimit(ITradeoffObjective objective) => _inner.GetObjectiveUpperLimit(objective);
        public double GetObjectiveUpperRestrictor(ITradeoffObjective objective) => _inner.GetObjectiveUpperRestrictor(objective);
        public void SetObjectiveCost(ITradeoffObjective tradeoffObjective, double cost) => _inner.SetObjectiveCost(tradeoffObjective, cost);
        public void SetObjectiveUpperRestrictor(ITradeoffObjective tradeoffObjective, double restrictorValue) => _inner.SetObjectiveUpperRestrictor(tradeoffObjective, restrictorValue);
        public void ResetToBalancedPlan() => _inner.ResetToBalancedPlan();
        public IDVHData GetStructureDvh(IStructure structure) => _inner.GetStructureDvh(structure) is var result && result is null ? null : new AsyncDVHData(result, _service);
        public bool AddTargetHomogeneityObjective(IStructure targetStructure) => _inner.AddTargetHomogeneityObjective(targetStructure);
        public bool AddTradeoffObjective(IStructure structure) => _inner.AddTradeoffObjective(structure);
        public bool AddTradeoffObjective(IOptimizationObjective objective) => _inner.AddTradeoffObjective(objective);
        public bool RemoveTradeoffObjective(ITradeoffObjective tradeoffObjective) => _inner.RemoveTradeoffObjective(tradeoffObjective);
        public bool RemoveTargetHomogeneityObjective(IStructure targetStructure) => _inner.RemoveTargetHomogeneityObjective(targetStructure);
        public bool RemoveTradeoffObjective(IStructure structure) => _inner.RemoveTradeoffObjective(structure);
        public void RemovePlanCollection() => _inner.RemovePlanCollection();
        public void RemoveAllTradeoffObjectives() => _inner.RemoveAllTradeoffObjectives();
        public void ApplyTradeoffExplorationResult() => _inner.ApplyTradeoffExplorationResult();
        public bool CreateDeliverableVmatPlan(bool useIntermediateDose) => _inner.CreateDeliverableVmatPlan(useIntermediateDose);
        public bool HasPlanCollection { get; }
        public bool CanLoadSavedPlanCollection { get; }
        public bool CanCreatePlanCollection { get; }
        public bool CanUsePlanDoseAsIntermediateDose { get; }
        public bool CanUseHybridOptimizationInPlanGeneration { get; }
        public IReadOnlyList<IOptimizationObjective> TradeoffObjectiveCandidates => _inner.TradeoffObjectiveCandidates?.Select(x => new AsyncOptimizationObjective(x, _service)).ToList();
        public IReadOnlyList<ITradeoffObjective> TradeoffObjectives => _inner.TradeoffObjectives?.Select(x => new AsyncTradeoffObjective(x, _service)).ToList();
        public IReadOnlyList<IStructure> TradeoffStructureCandidates => _inner.TradeoffStructureCandidates?.Select(x => new AsyncStructure(x, _service)).ToList();
        public IReadOnlyList<IStructure> TargetStructures => _inner.TargetStructures?.Select(x => new AsyncStructure(x, _service)).ToList();
        public IDose CurrentDose => _inner.CurrentDose is null ? null : new AsyncDose(_inner.CurrentDose, _service);


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.TradeoffExplorationContext> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.TradeoffExplorationContext, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
