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
    public class AsyncOptimizationSetup : AsyncSerializableObject, IOptimizationSetup
    {
        internal new readonly VMS.TPS.Common.Model.API.OptimizationSetup _inner;

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


        public async Task<IOptimizationNormalTissueParameter> AddNormalTissueObjectiveAsync(double priority, double distanceFromTargetBorderInMM, double startDosePercentage, double endDosePercentage, double fallOff)
        {
            return await _service.PostAsync(context => 
                _inner.AddNormalTissueObjective(priority, distanceFromTargetBorderInMM, startDosePercentage, endDosePercentage, fallOff) is var result && result is null ? null : new AsyncOptimizationNormalTissueParameter(result, _service));
        }


        public async Task<IOptimizationNormalTissueParameter> AddProtonNormalTissueObjectiveAsync(double priority, double distanceFromTargetBorderInMM, double startDosePercentage, double endDosePercentage)
        {
            return await _service.PostAsync(context => 
                _inner.AddProtonNormalTissueObjective(priority, distanceFromTargetBorderInMM, startDosePercentage, endDosePercentage) is var result && result is null ? null : new AsyncOptimizationNormalTissueParameter(result, _service));
        }


        public Task RemoveObjectiveAsync(IOptimizationObjective objective) => _service.PostAsync(context => _inner.RemoveObjective(((AsyncOptimizationObjective)objective)._inner));

        public Task RemoveParameterAsync(IOptimizationParameter parameter) => _service.PostAsync(context => _inner.RemoveParameter(((AsyncOptimizationParameter)parameter)._inner));

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
    }
}
