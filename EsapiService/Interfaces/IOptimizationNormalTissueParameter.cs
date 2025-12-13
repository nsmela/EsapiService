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
        double DistanceFromTargetBorderInMM { get; }
        double EndDosePercentage { get; }
        double FallOff { get; }
        bool IsAutomatic { get; }
        bool IsAutomaticSbrt { get; }
        bool IsAutomaticSrs { get; }
        double Priority { get; }
        double StartDosePercentage { get; }

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
