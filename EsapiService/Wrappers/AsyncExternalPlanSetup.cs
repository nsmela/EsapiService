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
    public class AsyncExternalPlanSetup : AsyncPlanSetup, IExternalPlanSetup, IEsapiWrapper<VMS.TPS.Common.Model.API.ExternalPlanSetup>
    {
        internal new readonly VMS.TPS.Common.Model.API.ExternalPlanSetup _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncExternalPlanSetup(VMS.TPS.Common.Model.API.ExternalPlanSetup inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

        }

        public async Task<ICalculationResult> CalculateDoseWithPresetValuesAsync(List<KeyValuePair<string, MetersetValue>> presetValues)
        {
            return await _service.PostAsync(context => 
                _inner.CalculateDoseWithPresetValues(presetValues) is var result && result is null ? null : new AsyncCalculationResult(result, _service));
        }


        public async Task<ICalculationResult> CalculateDoseAsync()
        {
            return await _service.PostAsync(context => 
                _inner.CalculateDose() is var result && result is null ? null : new AsyncCalculationResult(result, _service));
        }


        public async Task<ICalculationResult> CalculatePlanUncertaintyDosesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.CalculatePlanUncertaintyDoses() is var result && result is null ? null : new AsyncCalculationResult(result, _service));
        }


        public async Task<ICalculationResult> CalculateLeafMotionsAndDoseAsync()
        {
            return await _service.PostAsync(context => 
                _inner.CalculateLeafMotionsAndDose() is var result && result is null ? null : new AsyncCalculationResult(result, _service));
        }


        public async Task<ICalculationResult> CalculateLeafMotionsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.CalculateLeafMotions() is var result && result is null ? null : new AsyncCalculationResult(result, _service));
        }


        public async Task<IOptimizerResult> OptimizeAsync(int maxIterations)
        {
            return await _service.PostAsync(context => 
                _inner.Optimize(maxIterations) is var result && result is null ? null : new AsyncOptimizerResult(result, _service));
        }


        public async Task<IOptimizerResult> OptimizeAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Optimize() is var result && result is null ? null : new AsyncOptimizerResult(result, _service));
        }


        public async Task<IOptimizerResult> OptimizeVMATAsync(string mlcId)
        {
            return await _service.PostAsync(context => 
                _inner.OptimizeVMAT(mlcId) is var result && result is null ? null : new AsyncOptimizerResult(result, _service));
        }


        public async Task<IOptimizerResult> OptimizeVMATAsync()
        {
            return await _service.PostAsync(context => 
                _inner.OptimizeVMAT() is var result && result is null ? null : new AsyncOptimizerResult(result, _service));
        }


        public async Task<ICalculationResult> CalculateDVHEstimatesAsync(string modelId, Dictionary<string, DoseValue> targetDoseLevels, Dictionary<string, string> structureMatches)
        {
            return await _service.PostAsync(context => 
                _inner.CalculateDVHEstimates(modelId, targetDoseLevels, structureMatches) is var result && result is null ? null : new AsyncCalculationResult(result, _service));
        }


        public async Task<IEvaluationDose> CopyEvaluationDoseAsync(IDose existing)
        {
            return await _service.PostAsync(context => 
                _inner.CopyEvaluationDose(((AsyncDose)existing)._inner) is var result && result is null ? null : new AsyncEvaluationDose(result, _service));
        }


        public async Task<IEvaluationDose> CreateEvaluationDoseAsync()
        {
            return await _service.PostAsync(context => 
                _inner.CreateEvaluationDose() is var result && result is null ? null : new AsyncEvaluationDose(result, _service));
        }


        // Simple Void Method
        public Task RemoveBeamAsync(IBeam beam) =>
            _service.PostAsync(context => _inner.RemoveBeam(((AsyncBeam)beam)._inner));

        public async Task<ITradeoffExplorationContext> GetTradeoffExplorationContextAsync()
        {
            return await _service.PostAsync(context => 
                _inner.TradeoffExplorationContext is null ? null : new AsyncTradeoffExplorationContext(_inner.TradeoffExplorationContext, _service));
        }

        public async Task<IEvaluationDose> GetDoseAsEvaluationDoseAsync()
        {
            return await _service.PostAsync(context => 
                _inner.DoseAsEvaluationDose is null ? null : new AsyncEvaluationDose(_inner.DoseAsEvaluationDose, _service));
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ExternalPlanSetup> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ExternalPlanSetup, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.ExternalPlanSetup(AsyncExternalPlanSetup wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ExternalPlanSetup IEsapiWrapper<VMS.TPS.Common.Model.API.ExternalPlanSetup>.Inner => _inner;
    }
}
