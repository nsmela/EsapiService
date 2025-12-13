using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class ExternalPlanSetup : PlanSetup
    {
        public ExternalPlanSetup()
        {
        }

        public CalculationResult CalculateDoseWithPresetValues(List<KeyValuePair<string, MetersetValue>> presetValues) => default;
        public CalculationResult CalculateDose() => default;
        public CalculationResult CalculatePlanUncertaintyDoses() => default;
        public CalculationResult CalculateLeafMotionsAndDose() => default;
        public CalculationResult CalculateLeafMotions() => default;
        public CalculationResult CalculateLeafMotions(LMCVOptions options) => default;
        public CalculationResult CalculateLeafMotions(SmartLMCOptions options) => default;
        public CalculationResult CalculateLeafMotions(LMCMSSOptions options) => default;
        public IEnumerable<string> GetModelsForCalculationType(CalculationType calculationType) => new List<string>();
        public OptimizerResult Optimize(int maxIterations) => default;
        public OptimizerResult Optimize(int maxIterations, OptimizationOption optimizationOption) => default;
        public OptimizerResult Optimize(int maxIterations, OptimizationOption optimizationOption, string mlcId) => default;
        public OptimizerResult Optimize() => default;
        public OptimizerResult Optimize(OptimizationOptionsIMRT options) => default;
        public OptimizerResult OptimizeVMAT(string mlcId) => default;
        public OptimizerResult OptimizeVMAT() => default;
        public OptimizerResult OptimizeVMAT(OptimizationOptionsVMAT options) => default;
        public CalculationResult CalculateDVHEstimates(string modelId, Dictionary<string, DoseValue> targetDoseLevels, Dictionary<string, string> structureMatches) => default;
        public Beam AddArcBeam(ExternalBeamMachineParameters machineParameters, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double gantryStop, GantryDirection gantryDirection, double patientSupportAngle, VVector isocenter) => default;
        public Beam AddConformalArcBeam(ExternalBeamMachineParameters machineParameters, double collimatorAngle, int controlPointCount, double gantryAngle, double gantryStop, GantryDirection gantryDirection, double patientSupportAngle, VVector isocenter) => default;
        public Beam AddFixedSequenceBeam(ExternalBeamMachineParameters machineParameters, double collimatorAngle, double gantryAngle, VVector isocenter) => default;
        public bool AddImagingSetup(ExternalBeamMachineParameters machineParameters, ImagingBeamSetupParameters setupParameters, Structure targetStructure) => default;
        public Beam AddMLCArcBeam(ExternalBeamMachineParameters machineParameters, float[,] leafPositions, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double gantryStop, GantryDirection gantryDirection, double patientSupportAngle, VVector isocenter) => default;
        public Beam AddMLCBeam(ExternalBeamMachineParameters machineParameters, float[,] leafPositions, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter) => default;
        public Beam AddMLCSetupBeam(ExternalBeamMachineParameters machineParameters, float[,] leafPositions, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter) => default;
        public Beam AddMultipleStaticSegmentBeam(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter) => default;
        public Beam AddSetupBeam(ExternalBeamMachineParameters machineParameters, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter) => default;
        public Beam AddSlidingWindowBeam(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter) => default;
        public Beam AddSlidingWindowBeamForFixedJaws(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter) => default;
        public Beam AddStaticBeam(ExternalBeamMachineParameters machineParameters, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter) => default;
        public Beam AddVMATBeam(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double gantryStop, GantryDirection gantryDirection, double patientSupportAngle, VVector isocenter) => default;
        public Beam AddVMATBeamForFixedJaws(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryStartAngle, double gantryStopAngle, GantryDirection gantryDir, double patientSupportAngle, VVector isocenter) => default;
        public EvaluationDose CopyEvaluationDose(Dose existing) => default;
        public EvaluationDose CreateEvaluationDose() => default;
        public void RemoveBeam(Beam beam) { }
        public TradeoffExplorationContext TradeoffExplorationContext { get; set; }
        public EvaluationDose DoseAsEvaluationDose { get; set; }
    }
}
