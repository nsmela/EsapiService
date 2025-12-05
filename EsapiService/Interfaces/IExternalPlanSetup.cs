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
    public interface IExternalPlanSetup : IPlanSetup
    {
        Task<ICalculationResult> CalculateDoseWithPresetValuesAsync(System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, VMS.TPS.Common.Model.Types.MetersetValue>> presetValues);
        Task<ICalculationResult> CalculateDoseAsync();
        Task<ICalculationResult> CalculatePlanUncertaintyDosesAsync();
        Task<ICalculationResult> CalculateLeafMotionsAndDoseAsync();
        Task<ICalculationResult> CalculateLeafMotionsAsync();
        Task<ICalculationResult> CalculateLeafMotionsAsync(VMS.TPS.Common.Model.Types.LMCVOptions options);
        Task<ICalculationResult> CalculateLeafMotionsAsync(VMS.TPS.Common.Model.Types.SmartLMCOptions options);
        Task<ICalculationResult> CalculateLeafMotionsAsync(VMS.TPS.Common.Model.Types.LMCMSSOptions options);
        Task<System.Collections.Generic.IReadOnlyList<string>> GetModelsForCalculationTypeAsync(VMS.TPS.Common.Model.Types.CalculationType calculationType);
        Task<IOptimizerResult> OptimizeAsync(int maxIterations);
        Task<IOptimizerResult> OptimizeAsync(int maxIterations, VMS.TPS.Common.Model.Types.OptimizationOption optimizationOption);
        Task<IOptimizerResult> OptimizeAsync(int maxIterations, VMS.TPS.Common.Model.Types.OptimizationOption optimizationOption, string mlcId);
        Task<IOptimizerResult> OptimizeAsync();
        Task<IOptimizerResult> OptimizeAsync(VMS.TPS.Common.Model.Types.OptimizationOptionsIMRT options);
        Task<IOptimizerResult> OptimizeVMATAsync(string mlcId);
        Task<IOptimizerResult> OptimizeVMATAsync();
        Task<IOptimizerResult> OptimizeVMATAsync(VMS.TPS.Common.Model.Types.OptimizationOptionsVMAT options);
        Task<ICalculationResult> CalculateDVHEstimatesAsync(string modelId, System.Collections.Generic.Dictionary<string, VMS.TPS.Common.Model.Types.DoseValue> targetDoseLevels, System.Collections.Generic.Dictionary<string, string> structureMatches);
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task<IBeam> AddArcBeamAsync(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, VMS.TPS.Common.Model.Types.VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double gantryStop, VMS.TPS.Common.Model.Types.GantryDirection gantryDirection, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        Task<IBeam> AddConformalArcBeamAsync(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, double collimatorAngle, int controlPointCount, double gantryAngle, double gantryStop, VMS.TPS.Common.Model.Types.GantryDirection gantryDirection, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        Task<IBeam> AddFixedSequenceBeamAsync(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, double collimatorAngle, double gantryAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        Task<bool> AddImagingSetupAsync(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, VMS.TPS.Common.Model.Types.ImagingBeamSetupParameters setupParameters, VMS.TPS.Common.Model.API.Structure targetStructure);
        Task<IBeam> AddMLCArcBeamAsync(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, float[,] leafPositions, VMS.TPS.Common.Model.Types.VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double gantryStop, VMS.TPS.Common.Model.Types.GantryDirection gantryDirection, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        Task<IBeam> AddMLCBeamAsync(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, float[,] leafPositions, VMS.TPS.Common.Model.Types.VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        Task<IBeam> AddMLCSetupBeamAsync(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, float[,] leafPositions, VMS.TPS.Common.Model.Types.VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        Task<IBeam> AddMultipleStaticSegmentBeamAsync(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, System.Collections.Generic.IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        Task<IBeam> AddSetupBeamAsync(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, VMS.TPS.Common.Model.Types.VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        Task<IBeam> AddSlidingWindowBeamAsync(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, System.Collections.Generic.IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        Task<IBeam> AddSlidingWindowBeamForFixedJawsAsync(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, System.Collections.Generic.IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        Task<IBeam> AddStaticBeamAsync(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, VMS.TPS.Common.Model.Types.VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        Task<IBeam> AddVMATBeamAsync(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, System.Collections.Generic.IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double gantryStop, VMS.TPS.Common.Model.Types.GantryDirection gantryDirection, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        Task<IBeam> AddVMATBeamForFixedJawsAsync(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, System.Collections.Generic.IEnumerable<double> metersetWeights, double collimatorAngle, double gantryStartAngle, double gantryStopAngle, VMS.TPS.Common.Model.Types.GantryDirection gantryDir, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        Task<IEvaluationDose> CopyEvaluationDoseAsync(VMS.TPS.Common.Model.API.Dose existing);
        Task<IEvaluationDose> CreateEvaluationDoseAsync();
        Task RemoveBeamAsync(VMS.TPS.Common.Model.API.Beam beam);
        Task<ITradeoffExplorationContext> GetTradeoffExplorationContextAsync();
        Task<IEvaluationDose> GetDoseAsEvaluationDoseAsync();

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
