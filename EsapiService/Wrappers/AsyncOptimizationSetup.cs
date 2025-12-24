using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncOptimizationSetup : AsyncSerializableObject, IOptimizationSetup, IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationSetup>
    {
        internal new readonly VMS.TPS.Common.Model.API.OptimizationSetup _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationSetup(VMS.TPS.Common.Model.API.OptimizationSetup inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            UseJawTracking = inner.UseJawTracking;
        }


        public async Task<IOptimizationNormalTissueParameter> AddAutomaticNormalTissueObjectiveAsync(double priority)
        {
            return await _service.PostAsync(context => 
                _inner.AddAutomaticNormalTissueObjective(priority) is var result && result is null ? null : new AsyncOptimizationNormalTissueParameter(result, _service));
        }

        public async Task<IOptimizationNormalTissueParameter> AddAutomaticSbrtNormalTissueObjectiveAsync(double priority)
        {
            return await _service.PostAsync(context => 
                _inner.AddAutomaticSbrtNormalTissueObjective(priority) is var result && result is null ? null : new AsyncOptimizationNormalTissueParameter(result, _service));
        }

        public async Task<IOptimizationIMRTBeamParameter> AddBeamSpecificParameterAsync(IBeam beam, double smoothX, double smoothY, bool fixedJaws)
        {
            return await _service.PostAsync(context => 
                _inner.AddBeamSpecificParameter(((AsyncBeam)beam)._inner, smoothX, smoothY, fixedJaws) is var result && result is null ? null : new AsyncOptimizationIMRTBeamParameter(result, _service));
        }

        public async Task<IOptimizationEUDObjective> AddEUDObjectiveAsync(IStructure structure, OptimizationObjectiveOperator objectiveOperator, DoseValue dose, double parameterA, double priority)
        {
            return await _service.PostAsync(context => 
                _inner.AddEUDObjective(((AsyncStructure)structure)._inner, objectiveOperator, dose, parameterA, priority) is var result && result is null ? null : new AsyncOptimizationEUDObjective(result, _service));
        }

        public async Task<IOptimizationEUDObjective> AddEUDObjectiveAsync(IStructure structure, OptimizationObjectiveOperator objectiveOperator, DoseValue dose, double parameterA, double priority, bool isRobustObjective)
        {
            return await _service.PostAsync(context => 
                _inner.AddEUDObjective(((AsyncStructure)structure)._inner, objectiveOperator, dose, parameterA, priority, isRobustObjective) is var result && result is null ? null : new AsyncOptimizationEUDObjective(result, _service));
        }

        public async Task<IOptimizationMeanDoseObjective> AddMeanDoseObjectiveAsync(IStructure structure, DoseValue dose, double priority)
        {
            return await _service.PostAsync(context => 
                _inner.AddMeanDoseObjective(((AsyncStructure)structure)._inner, dose, priority) is var result && result is null ? null : new AsyncOptimizationMeanDoseObjective(result, _service));
        }

        public async Task<IOptimizationMeanDoseObjective> AddMeanDoseObjectiveAsync(IStructure structure, DoseValue dose, double priority, bool isRobustObjective)
        {
            return await _service.PostAsync(context => 
                _inner.AddMeanDoseObjective(((AsyncStructure)structure)._inner, dose, priority, isRobustObjective) is var result && result is null ? null : new AsyncOptimizationMeanDoseObjective(result, _service));
        }

        public async Task<IOptimizationNormalTissueParameter> AddNormalTissueObjectiveAsync(double priority, double distanceFromTargetBorderInMM, double startDosePercentage, double endDosePercentage, double fallOff)
        {
            return await _service.PostAsync(context => 
                _inner.AddNormalTissueObjective(priority, distanceFromTargetBorderInMM, startDosePercentage, endDosePercentage, fallOff) is var result && result is null ? null : new AsyncOptimizationNormalTissueParameter(result, _service));
        }

        public async Task<IOptimizationPointObjective> AddPointObjectiveAsync(IStructure structure, OptimizationObjectiveOperator objectiveOperator, DoseValue dose, double volume, double priority)
        {
            return await _service.PostAsync(context => 
                _inner.AddPointObjective(((AsyncStructure)structure)._inner, objectiveOperator, dose, volume, priority) is var result && result is null ? null : new AsyncOptimizationPointObjective(result, _service));
        }

        public async Task<IOptimizationPointObjective> AddPointObjectiveAsync(IStructure structure, OptimizationObjectiveOperator objectiveOperator, DoseValue dose, double volume, double priority, bool isRobustObjective)
        {
            return await _service.PostAsync(context => 
                _inner.AddPointObjective(((AsyncStructure)structure)._inner, objectiveOperator, dose, volume, priority, isRobustObjective) is var result && result is null ? null : new AsyncOptimizationPointObjective(result, _service));
        }

        public async Task<IOptimizationNormalTissueParameter> AddProtonNormalTissueObjectiveAsync(double priority, double distanceFromTargetBorderInMM, double startDosePercentage, double endDosePercentage)
        {
            return await _service.PostAsync(context => 
                _inner.AddProtonNormalTissueObjective(priority, distanceFromTargetBorderInMM, startDosePercentage, endDosePercentage) is var result && result is null ? null : new AsyncOptimizationNormalTissueParameter(result, _service));
        }

        // Simple Void Method
        public Task RemoveObjectiveAsync(IOptimizationObjective objective) 
        {
            _service.PostAsync(context => _inner.RemoveObjective(((AsyncOptimizationObjective)objective)._inner));
            Refresh();
            return Task.CompletedTask;
        }

        // Simple Void Method
        public Task RemoveParameterAsync(IOptimizationParameter parameter) 
        {
            _service.PostAsync(context => _inner.RemoveParameter(((AsyncOptimizationParameter)parameter)._inner));
            Refresh();
            return Task.CompletedTask;
        }

        public bool UseJawTracking { get; private set; }
        public async Task SetUseJawTrackingAsync(bool value)
        {
            UseJawTracking = await _service.PostAsync(context => 
            {
                _inner.UseJawTracking = value;
                return _inner.UseJawTracking;
            });
        }


        public async Task<IReadOnlyList<IOptimizationObjective>> GetObjectivesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Objectives?.Select(x => new AsyncOptimizationObjective(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IOptimizationParameter>> GetParametersAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Parameters?.Select(x => new AsyncOptimizationParameter(x, _service)).ToList());
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationSetup> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationSetup, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            UseJawTracking = _inner.UseJawTracking;
        }

        public static implicit operator VMS.TPS.Common.Model.API.OptimizationSetup(AsyncOptimizationSetup wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.OptimizationSetup IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationSetup>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationSetup>.Service => _service;
    }
}
