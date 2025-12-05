namespace VMS.TPS.Common.Model.API
{
    public interface ITradeoffExplorationContext
    {
        bool LoadSavedPlanCollection();
        bool CreatePlanCollection(bool continueOptimization, VMS.TPS.Common.Model.API.TradeoffPlanGenerationIntermediateDoseMode intermediateDoseMode, bool useHybridOptimizationForVmat);
        double GetObjectiveCost(VMS.TPS.Common.Model.API.TradeoffObjective objective);
        double GetObjectiveLowerLimit(VMS.TPS.Common.Model.API.TradeoffObjective objective);
        double GetObjectiveUpperLimit(VMS.TPS.Common.Model.API.TradeoffObjective objective);
        double GetObjectiveUpperRestrictor(VMS.TPS.Common.Model.API.TradeoffObjective objective);
        void SetObjectiveCost(VMS.TPS.Common.Model.API.TradeoffObjective tradeoffObjective, double cost);
        void SetObjectiveUpperRestrictor(VMS.TPS.Common.Model.API.TradeoffObjective tradeoffObjective, double restrictorValue);
        void ResetToBalancedPlan();
        IDVHData GetStructureDvh(VMS.TPS.Common.Model.API.Structure structure);
        bool AddTargetHomogeneityObjective(VMS.TPS.Common.Model.API.Structure targetStructure);
        bool AddTradeoffObjective(VMS.TPS.Common.Model.API.Structure structure);
        bool AddTradeoffObjective(VMS.TPS.Common.Model.API.OptimizationObjective objective);
        bool RemoveTradeoffObjective(VMS.TPS.Common.Model.API.TradeoffObjective tradeoffObjective);
        bool RemoveTargetHomogeneityObjective(VMS.TPS.Common.Model.API.Structure targetStructure);
        bool RemoveTradeoffObjective(VMS.TPS.Common.Model.API.Structure structure);
        void RemovePlanCollection();
        void RemoveAllTradeoffObjectives();
        void ApplyTradeoffExplorationResult();
        bool CreateDeliverableVmatPlan(bool useIntermediateDose);
        bool HasPlanCollection { get; }
        bool CanLoadSavedPlanCollection { get; }
        bool CanCreatePlanCollection { get; }
        bool CanUsePlanDoseAsIntermediateDose { get; }
        bool CanUseHybridOptimizationInPlanGeneration { get; }
        System.Collections.Generic.IReadOnlyList<IOptimizationObjective> TradeoffObjectiveCandidates { get; }
        System.Collections.Generic.IReadOnlyList<ITradeoffObjective> TradeoffObjectives { get; }
        System.Collections.Generic.IReadOnlyList<IStructure> TradeoffStructureCandidates { get; }
        System.Collections.Generic.IReadOnlyList<IStructure> TargetStructures { get; }
        IDose CurrentDose { get; }
    }
}
