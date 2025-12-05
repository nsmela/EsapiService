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
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task AddItemAsync(VMS.TPS.Common.Model.API.PlanningItem pi);
        Task AddItemAsync(VMS.TPS.Common.Model.API.PlanningItem pi, VMS.TPS.Common.Model.Types.PlanSumOperation operation, double planWeight);
        Task<VMS.TPS.Common.Model.Types.PlanSumOperation> GetPlanSumOperationAsync(VMS.TPS.Common.Model.API.PlanSetup planSetupInPlanSum);
        Task<double> GetPlanWeightAsync(VMS.TPS.Common.Model.API.PlanSetup planSetupInPlanSum);
        Task RemoveItemAsync(VMS.TPS.Common.Model.API.PlanningItem pi);
        Task SetPlanSumOperationAsync(VMS.TPS.Common.Model.API.PlanSetup planSetupInPlanSum, VMS.TPS.Common.Model.Types.PlanSumOperation operation);
        Task SetPlanWeightAsync(VMS.TPS.Common.Model.API.PlanSetup planSetupInPlanSum, double weight);
        System.Collections.Generic.IReadOnlyList<IPlanSumComponent> PlanSumComponents { get; }
        string Id { get; }
        Task SetIdAsync(string value);
        string Name { get; }
        Task SetNameAsync(string value);
        System.Collections.Generic.IReadOnlyList<IPlanSetup> PlanSetups { get; }

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
