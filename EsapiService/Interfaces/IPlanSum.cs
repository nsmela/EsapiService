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
        IEnumerable<PlanSumComponent> PlanSumComponents { get; }
        IEnumerable<PlanSetup> PlanSetups { get; }

        // --- Methods --- //
        Task AddItemAsync(IPlanningItem pi); // void method
        Task<double> GetPlanWeightAsync(IPlanSetup planSetupInPlanSum); // simple method
        Task RemoveItemAsync(IPlanningItem pi); // void method
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
