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
    public partial interface IExternalPlanSetup : IPlanSetup
    {

        // --- Accessors --- //
        Task<ITradeoffExplorationContext> GetTradeoffExplorationContextAsync(); // read complex property
        Task<IEvaluationDose> GetDoseAsEvaluationDoseAsync(); // read complex property

        // --- Methods --- //
        Task<ICalculationResult> CalculateDoseWithPresetValuesAsync(List<KeyValuePair<string, MetersetValue>> presetValues); // complex method
        Task<ICalculationResult> CalculateDoseAsync(); // complex method
        Task<ICalculationResult> CalculatePlanUncertaintyDosesAsync(); // complex method
        Task<ICalculationResult> CalculateLeafMotionsAndDoseAsync(); // complex method
        Task<ICalculationResult> CalculateLeafMotionsAsync(); // complex method
        Task<ICalculationResult> CalculateLeafMotionsAsync(LMCVOptions options); // complex method
        Task<ICalculationResult> CalculateLeafMotionsAsync(SmartLMCOptions options); // complex method
        Task<ICalculationResult> CalculateLeafMotionsAsync(LMCMSSOptions options); // complex method
        Task<IReadOnlyList<string>> GetModelsForCalculationTypeAsync(CalculationType calculationType); // simple collection method 
        Task<IOptimizerResult> OptimizeAsync(int maxIterations); // complex method
        Task<IOptimizerResult> OptimizeAsync(int maxIterations, OptimizationOption optimizationOption); // complex method
        Task<IOptimizerResult> OptimizeAsync(int maxIterations, OptimizationOption optimizationOption, string mlcId); // complex method
        Task<IOptimizerResult> OptimizeAsync(); // complex method
        Task<IOptimizerResult> OptimizeAsync(OptimizationOptionsIMRT options); // complex method
        Task<IOptimizerResult> OptimizeVMATAsync(string mlcId); // complex method
        Task<IOptimizerResult> OptimizeVMATAsync(); // complex method
        Task<IOptimizerResult> OptimizeVMATAsync(OptimizationOptionsVMAT options); // complex method
        Task<ICalculationResult> CalculateDVHEstimatesAsync(string modelId, Dictionary<string, DoseValue> targetDoseLevels, Dictionary<string, string> structureMatches); // complex method
        Task<IBeam> AddArcBeamAsync(ExternalBeamMachineParameters machineParameters, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double gantryStop, GantryDirection gantryDirection, double patientSupportAngle, VVector isocenter); // complex method
        Task<IBeam> AddConformalArcBeamAsync(ExternalBeamMachineParameters machineParameters, double collimatorAngle, int controlPointCount, double gantryAngle, double gantryStop, GantryDirection gantryDirection, double patientSupportAngle, VVector isocenter); // complex method
        Task<IBeam> AddFixedSequenceBeamAsync(ExternalBeamMachineParameters machineParameters, double collimatorAngle, double gantryAngle, VVector isocenter); // complex method
        Task<bool> AddImagingSetupAsync(ExternalBeamMachineParameters machineParameters, ImagingBeamSetupParameters setupParameters, IStructure targetStructure); // simple method
        Task<IBeam> AddMLCArcBeamAsync(ExternalBeamMachineParameters machineParameters, float[,] leafPositions, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double gantryStop, GantryDirection gantryDirection, double patientSupportAngle, VVector isocenter); // complex method
        Task<IBeam> AddMLCBeamAsync(ExternalBeamMachineParameters machineParameters, float[,] leafPositions, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter); // complex method
        Task<IBeam> AddMLCSetupBeamAsync(ExternalBeamMachineParameters machineParameters, float[,] leafPositions, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter); // complex method
        Task<IBeam> AddMultipleStaticSegmentBeamAsync(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter); // complex method
        Task<IBeam> AddSetupBeamAsync(ExternalBeamMachineParameters machineParameters, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter); // complex method
        Task<IBeam> AddSlidingWindowBeamAsync(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter); // complex method
        Task<IBeam> AddSlidingWindowBeamForFixedJawsAsync(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter); // complex method
        Task<IBeam> AddStaticBeamAsync(ExternalBeamMachineParameters machineParameters, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter); // complex method
        Task<IBeam> AddVMATBeamAsync(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double gantryStop, GantryDirection gantryDirection, double patientSupportAngle, VVector isocenter); // complex method
        Task<IBeam> AddVMATBeamForFixedJawsAsync(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryStartAngle, double gantryStopAngle, GantryDirection gantryDir, double patientSupportAngle, VVector isocenter); // complex method
        Task<IEvaluationDose> CopyEvaluationDoseAsync(IDose existing); // complex method
        Task<IEvaluationDose> CreateEvaluationDoseAsync(); // complex method
        Task RemoveBeamAsync(IBeam beam); // void method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ExternalPlanSetup object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ExternalPlanSetup> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ExternalPlanSetup object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ExternalPlanSetup, T> func);

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
