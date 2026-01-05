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
    public partial interface IOptimizationSetup : ISerializableObject
    {
        // --- Simple Properties --- //
        bool UseJawTracking { get; set; } // simple property

        // --- Collections --- //
        Task<IReadOnlyList<IOptimizationObjective>> GetObjectivesAsync(); // collection property context
        Task<IReadOnlyList<IOptimizationParameter>> GetParametersAsync(); // collection property context

        // --- Methods --- //
        Task<IOptimizationNormalTissueParameter> AddAutomaticNormalTissueObjectiveAsync(double priority); // complex method
        Task<IOptimizationNormalTissueParameter> AddAutomaticSbrtNormalTissueObjectiveAsync(double priority); // complex method
        Task<IOptimizationIMRTBeamParameter> AddBeamSpecificParameterAsync(IBeam beam, double smoothX, double smoothY, bool fixedJaws); // complex method
        Task<IOptimizationEUDObjective> AddEUDObjectiveAsync(IStructure structure, OptimizationObjectiveOperator objectiveOperator, DoseValue dose, double parameterA, double priority); // complex method
        Task<IOptimizationMeanDoseObjective> AddMeanDoseObjectiveAsync(IStructure structure, DoseValue dose, double priority); // complex method
        Task<IOptimizationNormalTissueParameter> AddNormalTissueObjectiveAsync(double priority, double distanceFromTargetBorderInMM, double startDosePercentage, double endDosePercentage, double fallOff); // complex method
        Task<IOptimizationPointObjective> AddPointObjectiveAsync(IStructure structure, OptimizationObjectiveOperator objectiveOperator, DoseValue dose, double volume, double priority); // complex method
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
