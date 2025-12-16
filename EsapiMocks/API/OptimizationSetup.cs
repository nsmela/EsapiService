using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class OptimizationSetup : SerializableObject
    {
        public OptimizationSetup()
        {
            Objectives = new List<OptimizationObjective>();
            Parameters = new List<OptimizationParameter>();
        }

        public OptimizationNormalTissueParameter AddAutomaticNormalTissueObjective(double priority) => default;
        public OptimizationNormalTissueParameter AddAutomaticSbrtNormalTissueObjective(double priority) => default;
        public OptimizationIMRTBeamParameter AddBeamSpecificParameter(Beam beam, double smoothX, double smoothY, bool fixedJaws) => default;
        public OptimizationEUDObjective AddEUDObjective(Structure structure, OptimizationObjectiveOperator objectiveOperator, DoseValue dose, double parameterA, double priority) => default;
        public OptimizationEUDObjective AddEUDObjective(Structure structure, OptimizationObjectiveOperator objectiveOperator, DoseValue dose, double parameterA, double priority, bool isRobustObjective) => default;
        public OptimizationMeanDoseObjective AddMeanDoseObjective(Structure structure, DoseValue dose, double priority) => default;
        public OptimizationMeanDoseObjective AddMeanDoseObjective(Structure structure, DoseValue dose, double priority, bool isRobustObjective) => default;
        public OptimizationNormalTissueParameter AddNormalTissueObjective(double priority, double distanceFromTargetBorderInMM, double startDosePercentage, double endDosePercentage, double fallOff) => default;
        public OptimizationPointObjective AddPointObjective(Structure structure, OptimizationObjectiveOperator objectiveOperator, DoseValue dose, double volume, double priority) => default;
        public OptimizationPointObjective AddPointObjective(Structure structure, OptimizationObjectiveOperator objectiveOperator, DoseValue dose, double volume, double priority, bool isRobustObjective) => default;
        public OptimizationNormalTissueParameter AddProtonNormalTissueObjective(double priority, double distanceFromTargetBorderInMM, double startDosePercentage, double endDosePercentage) => default;
        public void RemoveObjective(OptimizationObjective objective) { }
        public void RemoveParameter(OptimizationParameter parameter) { }
        public bool UseJawTracking { get; set; }
        public IEnumerable<OptimizationObjective> Objectives { get; set; }
        public IEnumerable<OptimizationParameter> Parameters { get; set; }
    }
}
