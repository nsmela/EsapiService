using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class IonPlanSetup : PlanSetup
    {
        public IonPlanSetup()
        {
            IonBeams = new List<IonBeam>();
        }

        public IonPlanSetup CreateDectVerificationPlan(Image rhoImage, Image zImage) => default;
        public CalculationResult CalculateBeamLine() => default;
        public CalculationResult CalculateDose() => default;
        public CalculationResult CalculatePlanUncertaintyDoses() => default;
        public OptimizerResult OptimizeIMPT(OptimizationOptionsIMPT options) => default;
        public CalculationResult PostProcessAndCalculateDose() => default;
        public CalculationResult CalculateDoseWithoutPostProcessing() => default;
        public CalculationResult CalculateBeamDeliveryDynamics() => default;
        public IEnumerable<string> GetModelsForCalculationType(CalculationType calculationType) => new List<string>();
        public CalculationResult CalculateDVHEstimates(string modelId, Dictionary<string, DoseValue> targetDoseLevels, Dictionary<string, string> structureMatches) => default;
        public Beam AddModulatedScanningBeam(ProtonBeamMachineParameters machineParameters, string snoutId, double snoutPosition, double gantryAngle, double patientSupportAngle, VVector isocenter) => default;
        public EvaluationDose CopyEvaluationDose(Dose existing) => default;
        public EvaluationDose CreateEvaluationDose() => default;
        public IonPlanOptimizationMode GetOptimizationMode() => default;
        public void SetNormalization(IonPlanNormalizationParameters normalizationParameters) { }
        public void SetOptimizationMode(IonPlanOptimizationMode mode) { }
        public bool IsPostProcessingNeeded { get; set; }
        public EvaluationDose DoseAsEvaluationDose { get; set; }
        public IEnumerable<IonBeam> IonBeams { get; set; }
    }
}
