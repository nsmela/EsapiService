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
        Task<IIonPlanSetup> CreateDectVerificationPlanAsync(VMS.TPS.Common.Model.API.Image rhoImage, VMS.TPS.Common.Model.API.Image zImage);
        Task<ICalculationResult> CalculateBeamLineAsync();
        Task<ICalculationResult> CalculateDoseAsync();
        Task<ICalculationResult> CalculatePlanUncertaintyDosesAsync();
        Task<IOptimizerResult> OptimizeIMPTAsync(VMS.TPS.Common.Model.Types.OptimizationOptionsIMPT options);
        Task<ICalculationResult> PostProcessAndCalculateDoseAsync();
        Task<ICalculationResult> CalculateDoseWithoutPostProcessingAsync();
        Task<ICalculationResult> CalculateBeamDeliveryDynamicsAsync();
        Task<System.Collections.Generic.IReadOnlyList<string>> GetModelsForCalculationTypeAsync(VMS.TPS.Common.Model.Types.CalculationType calculationType);
        Task<ICalculationResult> CalculateDVHEstimatesAsync(string modelId, System.Collections.Generic.Dictionary<string, VMS.TPS.Common.Model.Types.DoseValue> targetDoseLevels, System.Collections.Generic.Dictionary<string, string> structureMatches);
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task<IBeam> AddModulatedScanningBeamAsync(VMS.TPS.Common.Model.Types.ProtonBeamMachineParameters machineParameters, string snoutId, double snoutPosition, double gantryAngle, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        Task<IEvaluationDose> CopyEvaluationDoseAsync(VMS.TPS.Common.Model.API.Dose existing);
        Task<IEvaluationDose> CreateEvaluationDoseAsync();
        Task<VMS.TPS.Common.Model.Types.IonPlanOptimizationMode> GetOptimizationModeAsync();
        Task SetNormalizationAsync(VMS.TPS.Common.Model.Types.IonPlanNormalizationParameters normalizationParameters);
        Task SetOptimizationModeAsync(VMS.TPS.Common.Model.Types.IonPlanOptimizationMode mode);
        bool IsPostProcessingNeeded { get; }
        Task SetIsPostProcessingNeededAsync(bool value);
        Task<IEvaluationDose> GetDoseAsEvaluationDoseAsync();
        System.Collections.Generic.IReadOnlyList<IIonBeam> IonBeams { get; }

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
