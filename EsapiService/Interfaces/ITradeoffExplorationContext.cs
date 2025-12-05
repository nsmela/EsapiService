using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;

namespace Esapi.Interfaces
{
    public interface ITradeoffExplorationContext
    {
        Task<bool> LoadSavedPlanCollectionAsync();
        Task<bool> CreatePlanCollectionAsync(bool continueOptimization, VMS.TPS.Common.Model.API.TradeoffPlanGenerationIntermediateDoseMode intermediateDoseMode, bool useHybridOptimizationForVmat);
        Task<double> GetObjectiveCostAsync(VMS.TPS.Common.Model.API.TradeoffObjective objective);
        Task<double> GetObjectiveLowerLimitAsync(VMS.TPS.Common.Model.API.TradeoffObjective objective);
        Task<double> GetObjectiveUpperLimitAsync(VMS.TPS.Common.Model.API.TradeoffObjective objective);
        Task<double> GetObjectiveUpperRestrictorAsync(VMS.TPS.Common.Model.API.TradeoffObjective objective);
        Task SetObjectiveCostAsync(VMS.TPS.Common.Model.API.TradeoffObjective tradeoffObjective, double cost);
        Task SetObjectiveUpperRestrictorAsync(VMS.TPS.Common.Model.API.TradeoffObjective tradeoffObjective, double restrictorValue);
        Task ResetToBalancedPlanAsync();
        Task<IDVHData> GetStructureDvhAsync(VMS.TPS.Common.Model.API.Structure structure);
        Task<bool> AddTargetHomogeneityObjectiveAsync(VMS.TPS.Common.Model.API.Structure targetStructure);
        Task<bool> AddTradeoffObjectiveAsync(VMS.TPS.Common.Model.API.Structure structure);
        Task<bool> AddTradeoffObjectiveAsync(VMS.TPS.Common.Model.API.OptimizationObjective objective);
        Task<bool> RemoveTradeoffObjectiveAsync(VMS.TPS.Common.Model.API.TradeoffObjective tradeoffObjective);
        Task<bool> RemoveTargetHomogeneityObjectiveAsync(VMS.TPS.Common.Model.API.Structure targetStructure);
        Task<bool> RemoveTradeoffObjectiveAsync(VMS.TPS.Common.Model.API.Structure structure);
        Task RemovePlanCollectionAsync();
        Task RemoveAllTradeoffObjectivesAsync();
        Task ApplyTradeoffExplorationResultAsync();
        Task<bool> CreateDeliverableVmatPlanAsync(bool useIntermediateDose);
        bool HasPlanCollection { get; }
        bool CanLoadSavedPlanCollection { get; }
        bool CanCreatePlanCollection { get; }
        bool CanUsePlanDoseAsIntermediateDose { get; }
        bool CanUseHybridOptimizationInPlanGeneration { get; }
        System.Collections.Generic.IReadOnlyList<IOptimizationObjective> TradeoffObjectiveCandidates { get; }
        System.Collections.Generic.IReadOnlyList<ITradeoffObjective> TradeoffObjectives { get; }
        System.Collections.Generic.IReadOnlyList<IStructure> TradeoffStructureCandidates { get; }
        System.Collections.Generic.IReadOnlyList<IStructure> TargetStructures { get; }
        Task<IDose> GetCurrentDoseAsync();

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.TradeoffExplorationContext object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.TradeoffExplorationContext> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.TradeoffExplorationContext object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.TradeoffExplorationContext, T> func);
    }
}
