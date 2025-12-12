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
    public class AsyncIonPlanSetup : AsyncPlanSetup, IIonPlanSetup, IEsapiWrapper<VMS.TPS.Common.Model.API.IonPlanSetup>
    {
        internal new readonly VMS.TPS.Common.Model.API.IonPlanSetup _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncIonPlanSetup(VMS.TPS.Common.Model.API.IonPlanSetup inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            IsPostProcessingNeeded = inner.IsPostProcessingNeeded;
            IonBeams = inner.IonBeams;
        }

        public async Task<IIonPlanSetup> CreateDectVerificationPlanAsync(IImage rhoImage, IImage zImage)
        {
            return await _service.PostAsync(context => 
                _inner.CreateDectVerificationPlan(((AsyncImage)rhoImage)._inner, ((AsyncImage)zImage)._inner) is var result && result is null ? null : new AsyncIonPlanSetup(result, _service));
        }


        public async Task<ICalculationResult> CalculateBeamLineAsync()
        {
            return await _service.PostAsync(context => 
                _inner.CalculateBeamLine() is var result && result is null ? null : new AsyncCalculationResult(result, _service));
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


        public async Task<ICalculationResult> PostProcessAndCalculateDoseAsync()
        {
            return await _service.PostAsync(context => 
                _inner.PostProcessAndCalculateDose() is var result && result is null ? null : new AsyncCalculationResult(result, _service));
        }


        public async Task<ICalculationResult> CalculateDoseWithoutPostProcessingAsync()
        {
            return await _service.PostAsync(context => 
                _inner.CalculateDoseWithoutPostProcessing() is var result && result is null ? null : new AsyncCalculationResult(result, _service));
        }


        public async Task<ICalculationResult> CalculateBeamDeliveryDynamicsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.CalculateBeamDeliveryDynamics() is var result && result is null ? null : new AsyncCalculationResult(result, _service));
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


        public bool IsPostProcessingNeeded { get; private set; }
        public async Task SetIsPostProcessingNeededAsync(bool value)
        {
            IsPostProcessingNeeded = await _service.PostAsync(context => 
            {
                _inner.IsPostProcessingNeeded = value;
                return _inner.IsPostProcessingNeeded;
            });
        }

        public async Task<IEvaluationDose> GetDoseAsEvaluationDoseAsync()
        {
            return await _service.PostAsync(context => 
                _inner.DoseAsEvaluationDose is null ? null : new AsyncEvaluationDose(_inner.DoseAsEvaluationDose, _service));
        }

        public IEnumerable<IonBeam> IonBeams { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonPlanSetup> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonPlanSetup, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.IonPlanSetup(AsyncIonPlanSetup wrapper) => wrapper._inner;
        // Internal Explicit Implementation to expose _inner safely
        VMS.TPS.Common.Model.API.IonPlanSetup IEsapiWrapper<VMS.TPS.Common.Model.API.IonPlanSetup>.Inner => _inner;
    }
}
