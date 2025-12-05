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
    public interface IOptimizationSetup : ISerializableObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task<IOptimizationNormalTissueParameter> AddAutomaticNormalTissueObjectiveAsync(double priority);
        Task<IOptimizationNormalTissueParameter> AddAutomaticSbrtNormalTissueObjectiveAsync(double priority);
        Task<IOptimizationIMRTBeamParameter> AddBeamSpecificParameterAsync(VMS.TPS.Common.Model.API.Beam beam, double smoothX, double smoothY, bool fixedJaws);
        Task<IOptimizationEUDObjective> AddEUDObjectiveAsync(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.OptimizationObjectiveOperator objectiveOperator, VMS.TPS.Common.Model.Types.DoseValue dose, double parameterA, double priority);
        Task<IOptimizationEUDObjective> AddEUDObjectiveAsync(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.OptimizationObjectiveOperator objectiveOperator, VMS.TPS.Common.Model.Types.DoseValue dose, double parameterA, double priority, bool isRobustObjective);
        Task<IOptimizationMeanDoseObjective> AddMeanDoseObjectiveAsync(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.DoseValue dose, double priority);
        Task<IOptimizationMeanDoseObjective> AddMeanDoseObjectiveAsync(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.DoseValue dose, double priority, bool isRobustObjective);
        Task<IOptimizationNormalTissueParameter> AddNormalTissueObjectiveAsync(double priority, double distanceFromTargetBorderInMM, double startDosePercentage, double endDosePercentage, double fallOff);
        Task<IOptimizationPointObjective> AddPointObjectiveAsync(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.OptimizationObjectiveOperator objectiveOperator, VMS.TPS.Common.Model.Types.DoseValue dose, double volume, double priority);
        Task<IOptimizationPointObjective> AddPointObjectiveAsync(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.OptimizationObjectiveOperator objectiveOperator, VMS.TPS.Common.Model.Types.DoseValue dose, double volume, double priority, bool isRobustObjective);
        Task<IOptimizationNormalTissueParameter> AddProtonNormalTissueObjectiveAsync(double priority, double distanceFromTargetBorderInMM, double startDosePercentage, double endDosePercentage);
        Task RemoveObjectiveAsync(VMS.TPS.Common.Model.API.OptimizationObjective objective);
        Task RemoveParameterAsync(VMS.TPS.Common.Model.API.OptimizationParameter parameter);
        bool UseJawTracking { get; }
        Task SetUseJawTrackingAsync(bool value);
        System.Collections.Generic.IReadOnlyList<IOptimizationObjective> Objectives { get; }
        System.Collections.Generic.IReadOnlyList<IOptimizationParameter> Parameters { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.OptimizationSetup object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationSetup> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.OptimizationSetup object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationSetup, T> func);
    }
}
