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
    public interface IExternalPlanSetup : IPlanSetup
    {

        // --- Accessors --- //
        Task<ITradeoffExplorationContext> GetTradeoffExplorationContextAsync();
        Task<IEvaluationDose> GetDoseAsEvaluationDoseAsync();

        // --- Methods --- //
        Task<ICalculationResult> CalculateDoseWithPresetValuesAsync(List<KeyValuePair<string, MetersetValue>> presetValues);
        Task<ICalculationResult> CalculateDoseAsync();
        Task<ICalculationResult> CalculatePlanUncertaintyDosesAsync();
        Task<ICalculationResult> CalculateLeafMotionsAndDoseAsync();
        Task<ICalculationResult> CalculateLeafMotionsAsync();
        Task<ICalculationResult> CalculateLeafMotionsAsync(LMCVOptions options);
        Task<ICalculationResult> CalculateLeafMotionsAsync(SmartLMCOptions options);
        Task<ICalculationResult> CalculateLeafMotionsAsync(LMCMSSOptions options);
        Task<IReadOnlyList<string>> GetModelsForCalculationTypeAsync(CalculationType calculationType);
        Task<IOptimizerResult> OptimizeAsync(int maxIterations);
        Task<IOptimizerResult> OptimizeAsync(int maxIterations, OptimizationOption optimizationOption);
        Task<IOptimizerResult> OptimizeAsync(int maxIterations, OptimizationOption optimizationOption, string mlcId);
        Task<IOptimizerResult> OptimizeAsync();
        Task<IOptimizerResult> OptimizeAsync(OptimizationOptionsIMRT options);
        Task<IOptimizerResult> OptimizeVMATAsync(string mlcId);
        Task<IOptimizerResult> OptimizeVMATAsync();
        Task<IOptimizerResult> OptimizeVMATAsync(OptimizationOptionsVMAT options);
        Task<ICalculationResult> CalculateDVHEstimatesAsync(string modelId, Dictionary<string, DoseValue> targetDoseLevels, Dictionary<string, string> structureMatches);
        Task<IBeam> AddArcBeamAsync(ExternalBeamMachineParameters machineParameters, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double gantryStop, GantryDirection gantryDirection, double patientSupportAngle, VVector isocenter);
        Task<IBeam> AddConformalArcBeamAsync(ExternalBeamMachineParameters machineParameters, double collimatorAngle, int controlPointCount, double gantryAngle, double gantryStop, GantryDirection gantryDirection, double patientSupportAngle, VVector isocenter);
        Task<IBeam> AddFixedSequenceBeamAsync(ExternalBeamMachineParameters machineParameters, double collimatorAngle, double gantryAngle, VVector isocenter);
        Task<bool> AddImagingSetupAsync(ExternalBeamMachineParameters machineParameters, ImagingBeamSetupParameters setupParameters, IStructure targetStructure);
        Task<IBeam> AddMLCArcBeamAsync(ExternalBeamMachineParameters machineParameters, float[,] leafPositions, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double gantryStop, GantryDirection gantryDirection, double patientSupportAngle, VVector isocenter);
        Task<IBeam> AddMLCBeamAsync(ExternalBeamMachineParameters machineParameters, float[,] leafPositions, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter);
        Task<IBeam> AddMLCSetupBeamAsync(ExternalBeamMachineParameters machineParameters, float[,] leafPositions, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter);
        Task<IBeam> AddMultipleStaticSegmentBeamAsync(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter);
        Task<IBeam> AddSetupBeamAsync(ExternalBeamMachineParameters machineParameters, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter);
        Task<IBeam> AddSlidingWindowBeamAsync(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter);
        Task<IBeam> AddSlidingWindowBeamForFixedJawsAsync(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter);
        Task<IBeam> AddStaticBeamAsync(ExternalBeamMachineParameters machineParameters, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter);
        Task<IBeam> AddVMATBeamAsync(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double gantryStop, GantryDirection gantryDirection, double patientSupportAngle, VVector isocenter);
        Task<IBeam> AddVMATBeamForFixedJawsAsync(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryStartAngle, double gantryStopAngle, GantryDirection gantryDir, double patientSupportAngle, VVector isocenter);
        Task<IEvaluationDose> CopyEvaluationDoseAsync(IDose existing);
        Task<IEvaluationDose> CreateEvaluationDoseAsync();
        Task RemoveBeamAsync(IBeam beam);

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ExternalPlanSetup object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ExternalPlanSetup> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ExternalPlanSetup object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ExternalPlanSetup, T> func);
    }
}
