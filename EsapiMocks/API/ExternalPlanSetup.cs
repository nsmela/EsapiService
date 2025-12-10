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
        public OptimizerResult Optimize(int maxIterations) => default;
        public OptimizerResult Optimize() => default;
        public OptimizerResult OptimizeVMAT(string mlcId) => default;
        public OptimizerResult OptimizeVMAT() => default;
        public CalculationResult CalculateDVHEstimates(string modelId, Dictionary<string, DoseValue> targetDoseLevels, Dictionary<string, string> structureMatches) => default;
        public EvaluationDose CopyEvaluationDose(Dose existing) => default;
        public EvaluationDose CreateEvaluationDose() => default;
        public void RemoveBeam(Beam beam) { }
        public TradeoffExplorationContext TradeoffExplorationContext { get; set; }
        public EvaluationDose DoseAsEvaluationDose { get; set; }
    }
}
