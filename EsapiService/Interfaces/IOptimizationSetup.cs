namespace VMS.TPS.Common.Model.API
{
    public interface IOptimizationSetup : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        IOptimizationNormalTissueParameter AddAutomaticNormalTissueObjective(double priority);
        IOptimizationNormalTissueParameter AddAutomaticSbrtNormalTissueObjective(double priority);
        IOptimizationIMRTBeamParameter AddBeamSpecificParameter(VMS.TPS.Common.Model.API.Beam beam, double smoothX, double smoothY, bool fixedJaws);
        IOptimizationEUDObjective AddEUDObjective(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.OptimizationObjectiveOperator objectiveOperator, VMS.TPS.Common.Model.Types.DoseValue dose, double parameterA, double priority);
        IOptimizationEUDObjective AddEUDObjective(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.OptimizationObjectiveOperator objectiveOperator, VMS.TPS.Common.Model.Types.DoseValue dose, double parameterA, double priority, bool isRobustObjective);
        IOptimizationMeanDoseObjective AddMeanDoseObjective(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.DoseValue dose, double priority);
        IOptimizationMeanDoseObjective AddMeanDoseObjective(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.DoseValue dose, double priority, bool isRobustObjective);
        IOptimizationNormalTissueParameter AddNormalTissueObjective(double priority, double distanceFromTargetBorderInMM, double startDosePercentage, double endDosePercentage, double fallOff);
        IOptimizationPointObjective AddPointObjective(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.OptimizationObjectiveOperator objectiveOperator, VMS.TPS.Common.Model.Types.DoseValue dose, double volume, double priority);
        IOptimizationPointObjective AddPointObjective(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.OptimizationObjectiveOperator objectiveOperator, VMS.TPS.Common.Model.Types.DoseValue dose, double volume, double priority, bool isRobustObjective);
        IOptimizationNormalTissueParameter AddProtonNormalTissueObjective(double priority, double distanceFromTargetBorderInMM, double startDosePercentage, double endDosePercentage);
        void RemoveObjective(VMS.TPS.Common.Model.API.OptimizationObjective objective);
        void RemoveParameter(VMS.TPS.Common.Model.API.OptimizationParameter parameter);
        bool UseJawTracking { get; }
        System.Threading.Tasks.Task SetUseJawTrackingAsync(bool value);
        System.Collections.Generic.IReadOnlyList<IOptimizationObjective> Objectives { get; }
        System.Collections.Generic.IReadOnlyList<IOptimizationParameter> Parameters { get; }
    }
}
