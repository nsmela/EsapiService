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
    public interface IEstimatedDVH : IApiDataObject
    {
        // --- Simple Properties --- //
        DVHPoint[] CurveData { get; } // simple property
        string PlanSetupId { get; } // simple property
        string StructureId { get; } // simple property
        DoseValue TargetDoseLevel { get; } // simple property
        DVHEstimateType Type { get; } // simple property

        // --- Accessors --- //
        Task<IPlanSetup> GetPlanSetupAsync(); // read complex property
        Task<IStructure> GetStructureAsync(); // read complex property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.EstimatedDVH object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.EstimatedDVH> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.EstimatedDVH object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.EstimatedDVH, T> func);

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        new bool IsValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        new bool IsNotValid();
    }
}
