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
    public interface IOptimizationNormalTissueParameter : IOptimizationParameter
    {
        // --- Simple Properties --- //
        double DistanceFromTargetBorderInMM { get; } // simple property
        double EndDosePercentage { get; } // simple property
        double FallOff { get; } // simple property
        bool IsAutomatic { get; } // simple property
        bool IsAutomaticSbrt { get; } // simple property
        bool IsAutomaticSrs { get; } // simple property
        double Priority { get; } // simple property
        double StartDosePercentage { get; } // simple property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter, T> func);
    }
}
