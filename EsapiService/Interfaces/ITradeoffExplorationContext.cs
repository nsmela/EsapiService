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
        // --- Simple Properties --- //
        bool HasPlanCollection { get; }
        bool CanLoadSavedPlanCollection { get; }
        bool CanCreatePlanCollection { get; }
        bool CanUsePlanDoseAsIntermediateDose { get; }
        bool CanUseHybridOptimizationInPlanGeneration { get; }
        IReadOnlyList<OptimizationObjective> TradeoffObjectiveCandidates { get; }
        IReadOnlyCollection<TradeoffObjective> TradeoffObjectives { get; }
        IReadOnlyList<Structure> TradeoffStructureCandidates { get; }
        IReadOnlyList<Structure> TargetStructures { get; }

        // --- Accessors --- //
        Task<IDose> GetCurrentDoseAsync(); // read complex property

        // --- Methods --- //
        Task<bool> LoadSavedPlanCollectionAsync(); // simple method
        Task<bool> CreatePlanCollectionAsync(bool continueOptimization, ITradeoffPlanGenerationIntermediateDoseMode intermediateDoseMode, bool useHybridOptimizationForVmat); // simple method
        Task<double> GetObjectiveCostAsync(ITradeoffObjective objective); // simple method
        Task<double> GetObjectiveLowerLimitAsync(ITradeoffObjective objective); // simple method
        Task<double> GetObjectiveUpperLimitAsync(ITradeoffObjective objective); // simple method
        Task<double> GetObjectiveUpperRestrictorAsync(ITradeoffObjective objective); // simple method
        Task SetObjectiveCostAsync(ITradeoffObjective tradeoffObjective, double cost); // void method
        Task SetObjectiveUpperRestrictorAsync(ITradeoffObjective tradeoffObjective, double restrictorValue); // void method
        Task ResetToBalancedPlanAsync(); // void method
        Task<IDVHData> GetStructureDvhAsync(IStructure structure); // complex method
        Task<bool> AddTargetHomogeneityObjectiveAsync(IStructure targetStructure); // simple method
        Task<bool> AddTradeoffObjectiveAsync(IStructure structure); // simple method
        Task<bool> AddTradeoffObjectiveAsync(IOptimizationObjective objective); // simple method
        Task<bool> RemoveTradeoffObjectiveAsync(ITradeoffObjective tradeoffObjective); // simple method
        Task<bool> RemoveTargetHomogeneityObjectiveAsync(IStructure targetStructure); // simple method
        Task<bool> RemoveTradeoffObjectiveAsync(IStructure structure); // simple method
        Task RemovePlanCollectionAsync(); // void method
        Task RemoveAllTradeoffObjectivesAsync(); // void method
        Task ApplyTradeoffExplorationResultAsync(); // void method
        Task<bool> CreateDeliverableVmatPlanAsync(bool useIntermediateDose); // simple method

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
