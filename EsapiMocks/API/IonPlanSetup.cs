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
        public CalculationResult PostProcessAndCalculateDose() => default;
        public CalculationResult CalculateDoseWithoutPostProcessing() => default;
        public CalculationResult CalculateBeamDeliveryDynamics() => default;
        public CalculationResult CalculateDVHEstimates(string modelId, Dictionary<string, DoseValue> targetDoseLevels, Dictionary<string, string> structureMatches) => default;
        public EvaluationDose CopyEvaluationDose(Dose existing) => default;
        public EvaluationDose CreateEvaluationDose() => default;
        public bool IsPostProcessingNeeded { get; set; }
        public EvaluationDose DoseAsEvaluationDose { get; set; }
        public IEnumerable<IonBeam> IonBeams { get; set; }
    }
}
