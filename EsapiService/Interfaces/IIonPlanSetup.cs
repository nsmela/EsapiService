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
    public interface IIonPlanSetup : IPlanSetup
    {
        // --- Simple Properties --- //
        bool IsPostProcessingNeeded { get; }
        Task SetIsPostProcessingNeededAsync(bool value);

        // --- Accessors --- //
        Task<IEvaluationDose> GetDoseAsEvaluationDoseAsync();

        // --- Collections --- //
        Task<IReadOnlyList<IIonBeam>> GetIonBeamsAsync();

        // --- Methods --- //
        Task<IIonPlanSetup> CreateDectVerificationPlanAsync(IImage rhoImage, IImage zImage);
        Task<ICalculationResult> CalculateBeamLineAsync();
        Task<ICalculationResult> CalculateDoseAsync();
        Task<ICalculationResult> CalculatePlanUncertaintyDosesAsync();
        Task<IOptimizerResult> OptimizeIMPTAsync(OptimizationOptionsIMPT options);
        Task<ICalculationResult> PostProcessAndCalculateDoseAsync();
        Task<ICalculationResult> CalculateDoseWithoutPostProcessingAsync();
        Task<ICalculationResult> CalculateBeamDeliveryDynamicsAsync();
        Task<IReadOnlyList<string>> GetModelsForCalculationTypeAsync(CalculationType calculationType);
        Task<ICalculationResult> CalculateDVHEstimatesAsync(string modelId, Dictionary<string, DoseValue> targetDoseLevels, Dictionary<string, string> structureMatches);
        Task<IBeam> AddModulatedScanningBeamAsync(ProtonBeamMachineParameters machineParameters, string snoutId, double snoutPosition, double gantryAngle, double patientSupportAngle, VVector isocenter);
        Task<IEvaluationDose> CopyEvaluationDoseAsync(IDose existing);
        Task<IEvaluationDose> CreateEvaluationDoseAsync();
        Task<IonPlanOptimizationMode> GetOptimizationModeAsync();
        Task SetNormalizationAsync(IonPlanNormalizationParameters normalizationParameters);
        Task SetOptimizationModeAsync(IonPlanOptimizationMode mode);

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.IonPlanSetup object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.IonPlanSetup> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.IonPlanSetup object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonPlanSetup, T> func);
    }
}
