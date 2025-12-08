using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;

namespace Esapi.Wrappers
{
    public class AsyncIonPlanSetup : IIonPlanSetup
    {
        internal readonly VMS.TPS.Common.Model.API.IonPlanSetup _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncIonPlanSetup(VMS.TPS.Common.Model.API.IonPlanSetup inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            IsPostProcessingNeeded = inner.IsPostProcessingNeeded;
        }


        public async Task<IIonPlanSetup> CreateDectVerificationPlanAsync(IImage rhoImage, IImage zImage)
        {
            return await _service.RunAsync(() => 
                _inner.CreateDectVerificationPlan(rhoImage, zImage) is var result && result is null ? null : new AsyncIonPlanSetup(result, _service));
        }


        public async Task<ICalculationResult> CalculateBeamLineAsync()
        {
            return await _service.RunAsync(() => 
                _inner.CalculateBeamLine() is var result && result is null ? null : new AsyncCalculationResult(result, _service));
        }


        public async Task<ICalculationResult> CalculateDoseAsync()
        {
            return await _service.RunAsync(() => 
                _inner.CalculateDose() is var result && result is null ? null : new AsyncCalculationResult(result, _service));
        }


        public async Task<ICalculationResult> CalculatePlanUncertaintyDosesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.CalculatePlanUncertaintyDoses() is var result && result is null ? null : new AsyncCalculationResult(result, _service));
        }


        public async Task<IOptimizerResult> OptimizeIMPTAsync(OptimizationOptionsIMPT options)
        {
            return await _service.RunAsync(() => 
                _inner.OptimizeIMPT(options) is var result && result is null ? null : new AsyncOptimizerResult(result, _service));
        }


        public async Task<ICalculationResult> PostProcessAndCalculateDoseAsync()
        {
            return await _service.RunAsync(() => 
                _inner.PostProcessAndCalculateDose() is var result && result is null ? null : new AsyncCalculationResult(result, _service));
        }


        public async Task<ICalculationResult> CalculateDoseWithoutPostProcessingAsync()
        {
            return await _service.RunAsync(() => 
                _inner.CalculateDoseWithoutPostProcessing() is var result && result is null ? null : new AsyncCalculationResult(result, _service));
        }


        public async Task<ICalculationResult> CalculateBeamDeliveryDynamicsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.CalculateBeamDeliveryDynamics() is var result && result is null ? null : new AsyncCalculationResult(result, _service));
        }


        public Task<IReadOnlyList<string>> GetModelsForCalculationTypeAsync(CalculationType calculationType) => _service.RunAsync(() => _inner.GetModelsForCalculationType(calculationType)?.ToList());

        public async Task<ICalculationResult> CalculateDVHEstimatesAsync(string modelId, Dictionary<string, DoseValue> targetDoseLevels, Dictionary<string, string> structureMatches)
        {
            return await _service.RunAsync(() => 
                _inner.CalculateDVHEstimates(modelId, targetDoseLevels, structureMatches) is var result && result is null ? null : new AsyncCalculationResult(result, _service));
        }


        public async Task<IBeam> AddModulatedScanningBeamAsync(ProtonBeamMachineParameters machineParameters, string snoutId, double snoutPosition, double gantryAngle, double patientSupportAngle, VVector isocenter)
        {
            return await _service.RunAsync(() => 
                _inner.AddModulatedScanningBeam(machineParameters, snoutId, snoutPosition, gantryAngle, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service));
        }


        public async Task<IEvaluationDose> CopyEvaluationDoseAsync(IDose existing)
        {
            return await _service.RunAsync(() => 
                _inner.CopyEvaluationDose(existing) is var result && result is null ? null : new AsyncEvaluationDose(result, _service));
        }


        public async Task<IEvaluationDose> CreateEvaluationDoseAsync()
        {
            return await _service.RunAsync(() => 
                _inner.CreateEvaluationDose() is var result && result is null ? null : new AsyncEvaluationDose(result, _service));
        }


        public Task<IonPlanOptimizationMode> GetOptimizationModeAsync() => _service.RunAsync(() => _inner.GetOptimizationMode());

        public Task SetNormalizationAsync(IonPlanNormalizationParameters normalizationParameters) => _service.RunAsync(() => _inner.SetNormalization(normalizationParameters));

        public Task SetOptimizationModeAsync(IonPlanOptimizationMode mode) => _service.RunAsync(() => _inner.SetOptimizationMode(mode));

        public bool IsPostProcessingNeeded { get; private set; }
        public async Task SetIsPostProcessingNeededAsync(bool value)
        {
            IsPostProcessingNeeded = await _service.RunAsync(() =>
            {
                _inner.IsPostProcessingNeeded = value;
                return _inner.IsPostProcessingNeeded;
            });
        }

        public async Task<IEvaluationDose> GetDoseAsEvaluationDoseAsync()
        {
            return await _service.RunAsync(() => 
                _inner.DoseAsEvaluationDose is null ? null : new AsyncEvaluationDose(_inner.DoseAsEvaluationDose, _service));
        }

        public async Task<IReadOnlyList<IIonBeam>> GetIonBeamsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.IonBeams?.Select(x => new AsyncIonBeam(x, _service)).ToList());
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonPlanSetup> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonPlanSetup, T> func) => _service.RunAsync(() => func(_inner));
    }
}
