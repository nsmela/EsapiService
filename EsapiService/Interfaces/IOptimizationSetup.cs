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
    public interface IOptimizationSetup : ISerializableObject
    {
        // --- Simple Properties --- //
        bool UseJawTracking { get; }
        Task SetUseJawTrackingAsync(bool value);
        IEnumerable<OptimizationObjective> Objectives { get; }
        IEnumerable<OptimizationParameter> Parameters { get; }

        // --- Methods --- //
        Task<IOptimizationNormalTissueParameter> AddAutomaticNormalTissueObjectiveAsync(double priority); // complex method
        Task<IOptimizationNormalTissueParameter> AddAutomaticSbrtNormalTissueObjectiveAsync(double priority); // complex method
        Task<IOptimizationIMRTBeamParameter> AddBeamSpecificParameterAsync(IBeam beam, double smoothX, double smoothY, bool fixedJaws); // complex method
        Task<IOptimizationNormalTissueParameter> AddNormalTissueObjectiveAsync(double priority, double distanceFromTargetBorderInMM, double startDosePercentage, double endDosePercentage, double fallOff); // complex method
        Task<IOptimizationNormalTissueParameter> AddProtonNormalTissueObjectiveAsync(double priority, double distanceFromTargetBorderInMM, double startDosePercentage, double endDosePercentage); // complex method
        Task RemoveObjectiveAsync(IOptimizationObjective objective); // void method
        Task RemoveParameterAsync(IOptimizationParameter parameter); // void method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.OptimizationSetup object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationSetup> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.OptimizationSetup object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationSetup, T> func);
    }
}
