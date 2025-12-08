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
    public interface IOptimizationSetup : ISerializableObject
    {
        // --- Simple Properties --- //
        bool UseJawTracking { get; }
        Task SetUseJawTrackingAsync(bool value);

        // --- Collections --- //
        Task<IReadOnlyList<IOptimizationObjective>> GetObjectivesAsync();
        Task<IReadOnlyList<IOptimizationParameter>> GetParametersAsync();

        // --- Methods --- //
        Task<IOptimizationNormalTissueParameter> AddAutomaticNormalTissueObjectiveAsync(double priority);
        Task<IOptimizationNormalTissueParameter> AddAutomaticSbrtNormalTissueObjectiveAsync(double priority);
        Task<IOptimizationIMRTBeamParameter> AddBeamSpecificParameterAsync(IBeam beam, double smoothX, double smoothY, bool fixedJaws);
        Task<IOptimizationEUDObjective> AddEUDObjectiveAsync(IStructure structure, OptimizationObjectiveOperator objectiveOperator, DoseValue dose, double parameterA, double priority);
        Task<IOptimizationEUDObjective> AddEUDObjectiveAsync(IStructure structure, OptimizationObjectiveOperator objectiveOperator, DoseValue dose, double parameterA, double priority, bool isRobustObjective);
        Task<IOptimizationMeanDoseObjective> AddMeanDoseObjectiveAsync(IStructure structure, DoseValue dose, double priority);
        Task<IOptimizationMeanDoseObjective> AddMeanDoseObjectiveAsync(IStructure structure, DoseValue dose, double priority, bool isRobustObjective);
        Task<IOptimizationNormalTissueParameter> AddNormalTissueObjectiveAsync(double priority, double distanceFromTargetBorderInMM, double startDosePercentage, double endDosePercentage, double fallOff);
        Task<IOptimizationPointObjective> AddPointObjectiveAsync(IStructure structure, OptimizationObjectiveOperator objectiveOperator, DoseValue dose, double volume, double priority);
        Task<IOptimizationPointObjective> AddPointObjectiveAsync(IStructure structure, OptimizationObjectiveOperator objectiveOperator, DoseValue dose, double volume, double priority, bool isRobustObjective);
        Task<IOptimizationNormalTissueParameter> AddProtonNormalTissueObjectiveAsync(double priority, double distanceFromTargetBorderInMM, double startDosePercentage, double endDosePercentage);
        Task RemoveObjectiveAsync(IOptimizationObjective objective);
        Task RemoveParameterAsync(IOptimizationParameter parameter);

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
