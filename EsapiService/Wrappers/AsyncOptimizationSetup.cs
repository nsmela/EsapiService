    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
    public class AsyncOptimizationSetup : IOptimizationSetup
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizationSetup _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationSetup(VMS.TPS.Common.Model.API.OptimizationSetup inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public IOptimizationNormalTissueParameter AddAutomaticNormalTissueObjective(double priority) => _inner.AddAutomaticNormalTissueObjective(priority) is var result && result is null ? null : new AsyncOptimizationNormalTissueParameter(result, _service);
        public IOptimizationNormalTissueParameter AddAutomaticSbrtNormalTissueObjective(double priority) => _inner.AddAutomaticSbrtNormalTissueObjective(priority) is var result && result is null ? null : new AsyncOptimizationNormalTissueParameter(result, _service);
        public IOptimizationIMRTBeamParameter AddBeamSpecificParameter(VMS.TPS.Common.Model.API.Beam beam, double smoothX, double smoothY, bool fixedJaws) => _inner.AddBeamSpecificParameter(beam, smoothX, smoothY, fixedJaws) is var result && result is null ? null : new AsyncOptimizationIMRTBeamParameter(result, _service);
        public IOptimizationEUDObjective AddEUDObjective(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.OptimizationObjectiveOperator objectiveOperator, VMS.TPS.Common.Model.Types.DoseValue dose, double parameterA, double priority) => _inner.AddEUDObjective(structure, objectiveOperator, dose, parameterA, priority) is var result && result is null ? null : new AsyncOptimizationEUDObjective(result, _service);
        public IOptimizationEUDObjective AddEUDObjective(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.OptimizationObjectiveOperator objectiveOperator, VMS.TPS.Common.Model.Types.DoseValue dose, double parameterA, double priority, bool isRobustObjective) => _inner.AddEUDObjective(structure, objectiveOperator, dose, parameterA, priority, isRobustObjective) is var result && result is null ? null : new AsyncOptimizationEUDObjective(result, _service);
        public IOptimizationMeanDoseObjective AddMeanDoseObjective(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.DoseValue dose, double priority) => _inner.AddMeanDoseObjective(structure, dose, priority) is var result && result is null ? null : new AsyncOptimizationMeanDoseObjective(result, _service);
        public IOptimizationMeanDoseObjective AddMeanDoseObjective(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.DoseValue dose, double priority, bool isRobustObjective) => _inner.AddMeanDoseObjective(structure, dose, priority, isRobustObjective) is var result && result is null ? null : new AsyncOptimizationMeanDoseObjective(result, _service);
        public IOptimizationNormalTissueParameter AddNormalTissueObjective(double priority, double distanceFromTargetBorderInMM, double startDosePercentage, double endDosePercentage, double fallOff) => _inner.AddNormalTissueObjective(priority, distanceFromTargetBorderInMM, startDosePercentage, endDosePercentage, fallOff) is var result && result is null ? null : new AsyncOptimizationNormalTissueParameter(result, _service);
        public IOptimizationPointObjective AddPointObjective(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.OptimizationObjectiveOperator objectiveOperator, VMS.TPS.Common.Model.Types.DoseValue dose, double volume, double priority) => _inner.AddPointObjective(structure, objectiveOperator, dose, volume, priority) is var result && result is null ? null : new AsyncOptimizationPointObjective(result, _service);
        public IOptimizationPointObjective AddPointObjective(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.OptimizationObjectiveOperator objectiveOperator, VMS.TPS.Common.Model.Types.DoseValue dose, double volume, double priority, bool isRobustObjective) => _inner.AddPointObjective(structure, objectiveOperator, dose, volume, priority, isRobustObjective) is var result && result is null ? null : new AsyncOptimizationPointObjective(result, _service);
        public IOptimizationNormalTissueParameter AddProtonNormalTissueObjective(double priority, double distanceFromTargetBorderInMM, double startDosePercentage, double endDosePercentage) => _inner.AddProtonNormalTissueObjective(priority, distanceFromTargetBorderInMM, startDosePercentage, endDosePercentage) is var result && result is null ? null : new AsyncOptimizationNormalTissueParameter(result, _service);
        public void RemoveObjective(VMS.TPS.Common.Model.API.OptimizationObjective objective) => _inner.RemoveObjective(objective);
        public void RemoveParameter(VMS.TPS.Common.Model.API.OptimizationParameter parameter) => _inner.RemoveParameter(parameter);
        public bool UseJawTracking => _inner.UseJawTracking;
        public async Task SetUseJawTrackingAsync(bool value) => _service.RunAsync(() => _inner.UseJawTracking = value);
        public System.Collections.Generic.IReadOnlyList<IOptimizationObjective> Objectives => _inner.Objectives?.Select(x => new AsyncOptimizationObjective(x, _service)).ToList();
        public System.Collections.Generic.IReadOnlyList<IOptimizationParameter> Parameters => _inner.Parameters?.Select(x => new AsyncOptimizationParameter(x, _service)).ToList();
    }
}
