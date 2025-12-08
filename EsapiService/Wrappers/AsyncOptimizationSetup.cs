using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
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

            UseJawTracking = inner.UseJawTracking;
        }

        public async Task<IOptimizationNormalTissueParameter> AddAutomaticNormalTissueObjectiveAsync(double priority)
        {
            return await _service.RunAsync(() => 
                _inner.AddAutomaticNormalTissueObjective(priority) is var result && result is null ? null : new AsyncOptimizationNormalTissueParameter(result, _service));
        }

        public async Task<IOptimizationNormalTissueParameter> AddAutomaticSbrtNormalTissueObjectiveAsync(double priority)
        {
            return await _service.RunAsync(() => 
                _inner.AddAutomaticSbrtNormalTissueObjective(priority) is var result && result is null ? null : new AsyncOptimizationNormalTissueParameter(result, _service));
        }

        public async Task<IOptimizationIMRTBeamParameter> AddBeamSpecificParameterAsync(IBeam beam, double smoothX, double smoothY, bool fixedJaws)
        {
            return await _service.RunAsync(() => 
                _inner.AddBeamSpecificParameter(beam, smoothX, smoothY, fixedJaws) is var result && result is null ? null : new AsyncOptimizationIMRTBeamParameter(result, _service));
        }

        public async Task<IOptimizationEUDObjective> AddEUDObjectiveAsync(IStructure structure, OptimizationObjectiveOperator objectiveOperator, DoseValue dose, double parameterA, double priority)
        {
            return await _service.RunAsync(() => 
                _inner.AddEUDObjective(structure, objectiveOperator, dose, parameterA, priority) is var result && result is null ? null : new AsyncOptimizationEUDObjective(result, _service));
        }

        public async Task<IOptimizationEUDObjective> AddEUDObjectiveAsync(IStructure structure, OptimizationObjectiveOperator objectiveOperator, DoseValue dose, double parameterA, double priority, bool isRobustObjective)
        {
            return await _service.RunAsync(() => 
                _inner.AddEUDObjective(structure, objectiveOperator, dose, parameterA, priority, isRobustObjective) is var result && result is null ? null : new AsyncOptimizationEUDObjective(result, _service));
        }

        public async Task<IOptimizationMeanDoseObjective> AddMeanDoseObjectiveAsync(IStructure structure, DoseValue dose, double priority)
        {
            return await _service.RunAsync(() => 
                _inner.AddMeanDoseObjective(structure, dose, priority) is var result && result is null ? null : new AsyncOptimizationMeanDoseObjective(result, _service));
        }

        public async Task<IOptimizationMeanDoseObjective> AddMeanDoseObjectiveAsync(IStructure structure, DoseValue dose, double priority, bool isRobustObjective)
        {
            return await _service.RunAsync(() => 
                _inner.AddMeanDoseObjective(structure, dose, priority, isRobustObjective) is var result && result is null ? null : new AsyncOptimizationMeanDoseObjective(result, _service));
        }

        public async Task<IOptimizationNormalTissueParameter> AddNormalTissueObjectiveAsync(double priority, double distanceFromTargetBorderInMM, double startDosePercentage, double endDosePercentage, double fallOff)
        {
            return await _service.RunAsync(() => 
                _inner.AddNormalTissueObjective(priority, distanceFromTargetBorderInMM, startDosePercentage, endDosePercentage, fallOff) is var result && result is null ? null : new AsyncOptimizationNormalTissueParameter(result, _service));
        }

        public async Task<IOptimizationPointObjective> AddPointObjectiveAsync(IStructure structure, OptimizationObjectiveOperator objectiveOperator, DoseValue dose, double volume, double priority)
        {
            return await _service.RunAsync(() => 
                _inner.AddPointObjective(structure, objectiveOperator, dose, volume, priority) is var result && result is null ? null : new AsyncOptimizationPointObjective(result, _service));
        }

        public async Task<IOptimizationPointObjective> AddPointObjectiveAsync(IStructure structure, OptimizationObjectiveOperator objectiveOperator, DoseValue dose, double volume, double priority, bool isRobustObjective)
        {
            return await _service.RunAsync(() => 
                _inner.AddPointObjective(structure, objectiveOperator, dose, volume, priority, isRobustObjective) is var result && result is null ? null : new AsyncOptimizationPointObjective(result, _service));
        }

        public async Task<IOptimizationNormalTissueParameter> AddProtonNormalTissueObjectiveAsync(double priority, double distanceFromTargetBorderInMM, double startDosePercentage, double endDosePercentage)
        {
            return await _service.RunAsync(() => 
                _inner.AddProtonNormalTissueObjective(priority, distanceFromTargetBorderInMM, startDosePercentage, endDosePercentage) is var result && result is null ? null : new AsyncOptimizationNormalTissueParameter(result, _service));
        }

        public Task RemoveObjectiveAsync(IOptimizationObjective objective) => _service.RunAsync(() => _inner.RemoveObjective(objective));
        public Task RemoveParameterAsync(IOptimizationParameter parameter) => _service.RunAsync(() => _inner.RemoveParameter(parameter));
        public bool UseJawTracking { get; private set; }
        public async Task SetUseJawTrackingAsync(bool value)
        {
            UseJawTracking = await _service.RunAsync(() =>
            {
                _inner.UseJawTracking = value;
                return _inner.UseJawTracking;
            });
        }
        public async Task<IReadOnlyList<IOptimizationObjective>> GetObjectivesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Objectives?.Select(x => new AsyncOptimizationObjective(x, _service)).ToList());
        }

        public async Task<IReadOnlyList<IOptimizationParameter>> GetParametersAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Parameters?.Select(x => new AsyncOptimizationParameter(x, _service)).ToList());
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationSetup> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationSetup, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
