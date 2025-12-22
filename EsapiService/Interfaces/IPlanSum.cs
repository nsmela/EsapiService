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

        // --- Accessors --- //
        Task<ICourse> GetCourseAsync(); // read complex property

        // --- Collections --- //
        Task<IReadOnlyList<IPlanSumComponent>> GetPlanSumComponentsAsync(); // collection proeprty context
        Task<IReadOnlyList<IPlanSetup>> GetPlanSetupsAsync(); // collection proeprty context

        // --- Methods --- //
        Task<PlanSumOperation> GetPlanSumOperationAsync(IPlanSetup planSetupInPlanSum); // simple method
        Task<double> GetPlanWeightAsync(IPlanSetup planSetupInPlanSum); // simple method

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
