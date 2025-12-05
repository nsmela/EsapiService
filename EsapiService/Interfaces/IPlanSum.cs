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
    public interface IPlanSum : IPlanningItem
    {
        // --- Simple Properties --- //
        string Id { get; }
        Task SetIdAsync(string value);
        string Name { get; }
        Task SetNameAsync(string value);

        // --- Collections --- //
        Task<IReadOnlyList<IPlanSumComponent>> GetPlanSumComponentsAsync();
        Task<IReadOnlyList<IPlanSetup>> GetPlanSetupsAsync();

        // --- Methods --- //
        Task AddItemAsync(IPlanningItem pi);
        Task AddItemAsync(IPlanningItem pi, PlanSumOperation operation, double planWeight);
        Task<PlanSumOperation> GetPlanSumOperationAsync(IPlanSetup planSetupInPlanSum);
        Task<double> GetPlanWeightAsync(IPlanSetup planSetupInPlanSum);
        Task RemoveItemAsync(IPlanningItem pi);
        Task SetPlanSumOperationAsync(IPlanSetup planSetupInPlanSum, PlanSumOperation operation);
        Task SetPlanWeightAsync(IPlanSetup planSetupInPlanSum, double weight);

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.PlanSum object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanSum> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.PlanSum object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanSum, T> func);
    }
}
