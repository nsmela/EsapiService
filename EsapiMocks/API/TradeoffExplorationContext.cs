using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class TradeoffExplorationContext
    {
        public TradeoffExplorationContext()
        {
            TradeoffObjectiveCandidates = new List<OptimizationObjective>();
            TradeoffObjectives = new List<TradeoffObjective>();
            TradeoffStructureCandidates = new List<Structure>();
            TargetStructures = new List<Structure>();
        }

        public bool LoadSavedPlanCollection() => default;
        public bool CreatePlanCollection(bool continueOptimization, TradeoffPlanGenerationIntermediateDoseMode intermediateDoseMode, bool useHybridOptimizationForVmat) => default;
        public double GetObjectiveCost(TradeoffObjective objective) => default;
        public double GetObjectiveLowerLimit(TradeoffObjective objective) => default;
        public double GetObjectiveUpperLimit(TradeoffObjective objective) => default;
        public double GetObjectiveUpperRestrictor(TradeoffObjective objective) => default;
        public void SetObjectiveCost(TradeoffObjective tradeoffObjective, double cost) { }
        public void SetObjectiveUpperRestrictor(TradeoffObjective tradeoffObjective, double restrictorValue) { }
        public void ResetToBalancedPlan() { }
        public DVHData GetStructureDvh(Structure structure) => default;
        public bool AddTargetHomogeneityObjective(Structure targetStructure) => default;
        public bool AddTradeoffObjective(Structure structure) => default;
        public bool AddTradeoffObjective(OptimizationObjective objective) => default;
        public bool RemoveTradeoffObjective(TradeoffObjective tradeoffObjective) => default;
        public bool RemoveTargetHomogeneityObjective(Structure targetStructure) => default;
        public bool RemoveTradeoffObjective(Structure structure) => default;
        public void RemovePlanCollection() { }
        public void RemoveAllTradeoffObjectives() { }
        public void ApplyTradeoffExplorationResult() { }
        public bool CreateDeliverableVmatPlan(bool useIntermediateDose) => default;
        public bool HasPlanCollection { get; set; }
        public bool CanLoadSavedPlanCollection { get; set; }
        public bool CanCreatePlanCollection { get; set; }
        public bool CanUsePlanDoseAsIntermediateDose { get; set; }
        public bool CanUseHybridOptimizationInPlanGeneration { get; set; }
        public IReadOnlyList<OptimizationObjective> TradeoffObjectiveCandidates { get; set; }
        public IReadOnlyCollection<TradeoffObjective> TradeoffObjectives { get; set; }
        public IReadOnlyList<Structure> TradeoffStructureCandidates { get; set; }
        public IReadOnlyList<Structure> TargetStructures { get; set; }
        public Dose CurrentDose { get; set; }
    }
}
