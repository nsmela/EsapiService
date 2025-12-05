    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
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

        }

        public IIonPlanSetup CreateDectVerificationPlan(IImage rhoImage, IImage zImage) => _inner.CreateDectVerificationPlan(rhoImage, zImage) is var result && result is null ? null : new AsyncIonPlanSetup(result, _service);
        public ICalculationResult CalculateBeamLine() => _inner.CalculateBeamLine() is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public ICalculationResult CalculateDose() => _inner.CalculateDose() is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public ICalculationResult CalculatePlanUncertaintyDoses() => _inner.CalculatePlanUncertaintyDoses() is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public IOptimizerResult OptimizeIMPT(OptimizationOptionsIMPT options) => _inner.OptimizeIMPT(options) is var result && result is null ? null : new AsyncOptimizerResult(result, _service);
        public ICalculationResult PostProcessAndCalculateDose() => _inner.PostProcessAndCalculateDose() is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public ICalculationResult CalculateDoseWithoutPostProcessing() => _inner.CalculateDoseWithoutPostProcessing() is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public ICalculationResult CalculateBeamDeliveryDynamics() => _inner.CalculateBeamDeliveryDynamics() is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public IReadOnlyList<string> GetModelsForCalculationType(CalculationType calculationType) => _inner.GetModelsForCalculationType(calculationType)?.ToList();
        public ICalculationResult CalculateDVHEstimates(string modelId, Dictionary<string, DoseValue> targetDoseLevels, Dictionary<string, string> structureMatches) => _inner.CalculateDVHEstimates(modelId, targetDoseLevels, structureMatches) is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public IBeam AddModulatedScanningBeam(ProtonBeamMachineParameters machineParameters, string snoutId, double snoutPosition, double gantryAngle, double patientSupportAngle, VVector isocenter) => _inner.AddModulatedScanningBeam(machineParameters, snoutId, snoutPosition, gantryAngle, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service);
        public IEvaluationDose CopyEvaluationDose(IDose existing) => _inner.CopyEvaluationDose(existing) is var result && result is null ? null : new AsyncEvaluationDose(result, _service);
        public IEvaluationDose CreateEvaluationDose() => _inner.CreateEvaluationDose() is var result && result is null ? null : new AsyncEvaluationDose(result, _service);
        public IonPlanOptimizationMode GetOptimizationMode() => _inner.GetOptimizationMode();
        public void SetNormalization(IonPlanNormalizationParameters normalizationParameters) => _inner.SetNormalization(normalizationParameters);
        public void SetOptimizationMode(IonPlanOptimizationMode mode) => _inner.SetOptimizationMode(mode);
        public bool IsPostProcessingNeeded => _inner.IsPostProcessingNeeded;
        public async Task SetIsPostProcessingNeededAsync(bool value) => _service.RunAsync(() => _inner.IsPostProcessingNeeded = value);
        public IEvaluationDose DoseAsEvaluationDose => _inner.DoseAsEvaluationDose is null ? null : new AsyncEvaluationDose(_inner.DoseAsEvaluationDose, _service);

        public IReadOnlyList<IIonBeam> IonBeams => _inner.IonBeams?.Select(x => new AsyncIonBeam(x, _service)).ToList();

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonPlanSetup> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonPlanSetup, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
