using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;
using Esapi.Interfaces;

namespace Esapi.Interfaces
{
    public interface ITradeoffExplorationContext
    {
        // --- Simple Properties --- //
        bool HasPlanCollection { get; }
        bool CanLoadSavedPlanCollection { get; }
        bool CanCreatePlanCollection { get; }
        bool CanUsePlanDoseAsIntermediateDose { get; }
        bool CanUseHybridOptimizationInPlanGeneration { get; }

        // --- Accessors --- //
        Task<IDose> GetCurrentDoseAsync();

        // --- Collections --- //
        Task<IReadOnlyList<IOptimizationObjective>> GetTradeoffObjectiveCandidatesAsync();
        Task<IReadOnlyList<ITradeoffObjective>> GetTradeoffObjectivesAsync();
        Task<IReadOnlyList<IStructure>> GetTradeoffStructureCandidatesAsync();
        Task<IReadOnlyList<IStructure>> GetTargetStructuresAsync();

        // --- Methods --- //
        Task<bool> LoadSavedPlanCollectionAsync();
        Task<bool> CreatePlanCollectionAsync(bool continueOptimization, ITradeoffPlanGenerationIntermediateDoseMode intermediateDoseMode, bool useHybridOptimizationForVmat);
        Task<double> GetObjectiveCostAsync(ITradeoffObjective objective);
        Task<double> GetObjectiveLowerLimitAsync(ITradeoffObjective objective);
        Task<double> GetObjectiveUpperLimitAsync(ITradeoffObjective objective);
        Task<double> GetObjectiveUpperRestrictorAsync(ITradeoffObjective objective);
        Task SetObjectiveCostAsync(ITradeoffObjective tradeoffObjective, double cost);
        Task SetObjectiveUpperRestrictorAsync(ITradeoffObjective tradeoffObjective, double restrictorValue);
        Task ResetToBalancedPlanAsync();
        Task<IDVHData> GetStructureDvhAsync(IStructure structure);
        Task<bool> AddTargetHomogeneityObjectiveAsync(IStructure targetStructure);
        Task<bool> AddTradeoffObjectiveAsync(IStructure structure);
        Task<bool> AddTradeoffObjectiveAsync(IOptimizationObjective objective);
        Task<bool> RemoveTradeoffObjectiveAsync(ITradeoffObjective tradeoffObjective);
        Task<bool> RemoveTargetHomogeneityObjectiveAsync(IStructure targetStructure);
        Task<bool> RemoveTradeoffObjectiveAsync(IStructure structure);
        Task RemovePlanCollectionAsync();
        Task RemoveAllTradeoffObjectivesAsync();
        Task ApplyTradeoffExplorationResultAsync();
        Task<bool> CreateDeliverableVmatPlanAsync(bool useIntermediateDose);

        // --- RunAsync --- //
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
