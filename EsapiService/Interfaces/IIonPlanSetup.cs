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
    public interface IIonPlanSetup : IPlanSetup
    {
        // --- Simple Properties --- //
        bool IsPostProcessingNeeded { get; set; } // simple property

        // --- Accessors --- //
        Task<IEvaluationDose> GetDoseAsEvaluationDoseAsync(); // read complex property

        // --- Collections --- //
        Task<IReadOnlyList<IIonBeam>> GetIonBeamsAsync(); // collection proeprty context

        // --- Methods --- //
        Task<IIonPlanSetup> CreateDectVerificationPlanAsync(IImage rhoImage, IImage zImage); // complex method
        Task<ICalculationResult> CalculateBeamLineAsync(); // complex method
        Task<ICalculationResult> CalculateDoseAsync(); // complex method
        Task<ICalculationResult> CalculatePlanUncertaintyDosesAsync(); // complex method
        Task<IOptimizerResult> OptimizeIMPTAsync(OptimizationOptionsIMPT options); // complex method
        Task<ICalculationResult> PostProcessAndCalculateDoseAsync(); // complex method
        Task<ICalculationResult> CalculateDoseWithoutPostProcessingAsync(); // complex method
        Task<ICalculationResult> CalculateBeamDeliveryDynamicsAsync(); // complex method
        Task<IReadOnlyList<string>> GetModelsForCalculationTypeAsync(CalculationType calculationType); // simple collection method 
        Task<ICalculationResult> CalculateDVHEstimatesAsync(string modelId, Dictionary<string, DoseValue> targetDoseLevels, Dictionary<string, string> structureMatches); // complex method
        Task<IBeam> AddModulatedScanningBeamAsync(ProtonBeamMachineParameters machineParameters, string snoutId, double snoutPosition, double gantryAngle, double patientSupportAngle, VVector isocenter); // complex method
        Task<IEvaluationDose> CopyEvaluationDoseAsync(IDose existing); // complex method
        Task<IEvaluationDose> CreateEvaluationDoseAsync(); // complex method
        Task<IonPlanOptimizationMode> GetOptimizationModeAsync(); // simple method
        Task SetNormalizationAsync(IonPlanNormalizationParameters normalizationParameters); // void method
        Task SetOptimizationModeAsync(IonPlanOptimizationMode mode); // void method

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
