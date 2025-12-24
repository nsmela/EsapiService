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
    public interface IPlanSum : IPlanningItem
    {
        // --- Simple Properties --- //
        new string Id { get; set; } // simple property
        new string Name { get; set; } // simple property

        // --- Collections --- //
        Task<IReadOnlyList<IPlanSumComponent>> GetPlanSumComponentsAsync(); // collection proeprty context
        Task<IReadOnlyList<IPlanSetup>> GetPlanSetupsAsync(); // collection proeprty context

        // --- Methods --- //
        Task AddItemAsync(IPlanningItem pi); // void method
        Task AddItemAsync(IPlanningItem pi, PlanSumOperation operation, double planWeight); // void method
        Task<PlanSumOperation> GetPlanSumOperationAsync(IPlanSetup planSetupInPlanSum); // simple method
        Task<double> GetPlanWeightAsync(IPlanSetup planSetupInPlanSum); // simple method
        Task RemoveItemAsync(IPlanningItem pi); // void method
        Task SetPlanSumOperationAsync(IPlanSetup planSetupInPlanSum, PlanSumOperation operation); // void method
        Task SetPlanWeightAsync(IPlanSetup planSetupInPlanSum, double weight); // void method

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
