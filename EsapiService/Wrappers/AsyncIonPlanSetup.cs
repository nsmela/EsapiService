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

        public IIonPlanSetup CreateDectVerificationPlan(VMS.TPS.Common.Model.API.Image rhoImage, VMS.TPS.Common.Model.API.Image zImage) => _inner.CreateDectVerificationPlan(rhoImage, zImage) is var result && result is null ? null : new AsyncIonPlanSetup(result, _service);
        public ICalculationResult CalculateBeamLine() => _inner.CalculateBeamLine() is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public ICalculationResult CalculateDose() => _inner.CalculateDose() is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public ICalculationResult CalculatePlanUncertaintyDoses() => _inner.CalculatePlanUncertaintyDoses() is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public IOptimizerResult OptimizeIMPT(VMS.TPS.Common.Model.Types.OptimizationOptionsIMPT options) => _inner.OptimizeIMPT(options) is var result && result is null ? null : new AsyncOptimizerResult(result, _service);
        public ICalculationResult PostProcessAndCalculateDose() => _inner.PostProcessAndCalculateDose() is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public ICalculationResult CalculateDoseWithoutPostProcessing() => _inner.CalculateDoseWithoutPostProcessing() is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public ICalculationResult CalculateBeamDeliveryDynamics() => _inner.CalculateBeamDeliveryDynamics() is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public System.Collections.Generic.IReadOnlyList<string> GetModelsForCalculationType(VMS.TPS.Common.Model.Types.CalculationType calculationType) => _inner.GetModelsForCalculationType(calculationType)?.ToList();
        public ICalculationResult CalculateDVHEstimates(string modelId, System.Collections.Generic.Dictionary<string, VMS.TPS.Common.Model.Types.DoseValue> targetDoseLevels, System.Collections.Generic.Dictionary<string, string> structureMatches) => _inner.CalculateDVHEstimates(modelId, targetDoseLevels, structureMatches) is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public IBeam AddModulatedScanningBeam(VMS.TPS.Common.Model.Types.ProtonBeamMachineParameters machineParameters, string snoutId, double snoutPosition, double gantryAngle, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter) => _inner.AddModulatedScanningBeam(machineParameters, snoutId, snoutPosition, gantryAngle, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service);
        public IEvaluationDose CopyEvaluationDose(VMS.TPS.Common.Model.API.Dose existing) => _inner.CopyEvaluationDose(existing) is var result && result is null ? null : new AsyncEvaluationDose(result, _service);
        public IEvaluationDose CreateEvaluationDose() => _inner.CreateEvaluationDose() is var result && result is null ? null : new AsyncEvaluationDose(result, _service);
        public VMS.TPS.Common.Model.Types.IonPlanOptimizationMode GetOptimizationMode() => _inner.GetOptimizationMode();
        public void SetNormalization(VMS.TPS.Common.Model.Types.IonPlanNormalizationParameters normalizationParameters) => _inner.SetNormalization(normalizationParameters);
        public void SetOptimizationMode(VMS.TPS.Common.Model.Types.IonPlanOptimizationMode mode) => _inner.SetOptimizationMode(mode);
        public bool IsPostProcessingNeeded => _inner.IsPostProcessingNeeded;
        public async Task SetIsPostProcessingNeededAsync(bool value) => _service.RunAsync(() => _inner.IsPostProcessingNeeded = value);
        public IEvaluationDose DoseAsEvaluationDose => _inner.DoseAsEvaluationDose is null ? null : new AsyncEvaluationDose(_inner.DoseAsEvaluationDose, _service);

        public System.Collections.Generic.IReadOnlyList<IIonBeam> IonBeams => _inner.IonBeams?.Select(x => new AsyncIonBeam(x, _service)).ToList();

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonPlanSetup> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonPlanSetup, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
