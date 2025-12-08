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
    public interface IEstimatedDVH : IApiDataObject
    {
        // --- Simple Properties --- //
        DVHPoint[] CurveData { get; }
        string PlanSetupId { get; }
        string StructureId { get; }
        DoseValue TargetDoseLevel { get; }
        DVHEstimateType Type { get; }

        // --- Accessors --- //
        Task<IPlanSetup> GetPlanSetupAsync();
        Task<IStructure> GetStructureAsync();

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.EstimatedDVH object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.EstimatedDVH> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.EstimatedDVH object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.EstimatedDVH, T> func);
    }
}
