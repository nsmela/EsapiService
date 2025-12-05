namespace EsapiService.Wrappers
{
    public class AsyncExternalPlanSetup : IExternalPlanSetup
    {
        internal readonly VMS.TPS.Common.Model.API.ExternalPlanSetup _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncExternalPlanSetup(VMS.TPS.Common.Model.API.ExternalPlanSetup inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }

        public ICalculationResult CalculateDoseWithPresetValues(List<KeyValuePair<string, MetersetValue>> presetValues) => _inner.CalculateDoseWithPresetValues(presetValues) is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public ICalculationResult CalculateDose() => _inner.CalculateDose() is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public ICalculationResult CalculatePlanUncertaintyDoses() => _inner.CalculatePlanUncertaintyDoses() is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public ICalculationResult CalculateLeafMotionsAndDose() => _inner.CalculateLeafMotionsAndDose() is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public ICalculationResult CalculateLeafMotions() => _inner.CalculateLeafMotions() is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public ICalculationResult CalculateLeafMotions(LMCVOptions options) => _inner.CalculateLeafMotions(options) is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public ICalculationResult CalculateLeafMotions(SmartLMCOptions options) => _inner.CalculateLeafMotions(options) is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public ICalculationResult CalculateLeafMotions(LMCMSSOptions options) => _inner.CalculateLeafMotions(options) is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public IReadOnlyList<string> GetModelsForCalculationType(CalculationType calculationType) => _inner.GetModelsForCalculationType(calculationType)?.ToList();
        public IOptimizerResult Optimize(int maxIterations) => _inner.Optimize(maxIterations) is var result && result is null ? null : new AsyncOptimizerResult(result, _service);
        public IOptimizerResult Optimize(int maxIterations, OptimizationOption optimizationOption) => _inner.Optimize(maxIterations, optimizationOption) is var result && result is null ? null : new AsyncOptimizerResult(result, _service);
        public IOptimizerResult Optimize(int maxIterations, OptimizationOption optimizationOption, string mlcId) => _inner.Optimize(maxIterations, optimizationOption, mlcId) is var result && result is null ? null : new AsyncOptimizerResult(result, _service);
        public IOptimizerResult Optimize() => _inner.Optimize() is var result && result is null ? null : new AsyncOptimizerResult(result, _service);
        public IOptimizerResult Optimize(OptimizationOptionsIMRT options) => _inner.Optimize(options) is var result && result is null ? null : new AsyncOptimizerResult(result, _service);
        public IOptimizerResult OptimizeVMAT(string mlcId) => _inner.OptimizeVMAT(mlcId) is var result && result is null ? null : new AsyncOptimizerResult(result, _service);
        public IOptimizerResult OptimizeVMAT() => _inner.OptimizeVMAT() is var result && result is null ? null : new AsyncOptimizerResult(result, _service);
        public IOptimizerResult OptimizeVMAT(OptimizationOptionsVMAT options) => _inner.OptimizeVMAT(options) is var result && result is null ? null : new AsyncOptimizerResult(result, _service);
        public ICalculationResult CalculateDVHEstimates(string modelId, Dictionary<string, DoseValue> targetDoseLevels, Dictionary<string, string> structureMatches) => _inner.CalculateDVHEstimates(modelId, targetDoseLevels, structureMatches) is var result && result is null ? null : new AsyncCalculationResult(result, _service);
        public IBeam AddArcBeam(ExternalBeamMachineParameters machineParameters, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double gantryStop, GantryDirection gantryDirection, double patientSupportAngle, VVector isocenter) => _inner.AddArcBeam(machineParameters, jawPositions, collimatorAngle, gantryAngle, gantryStop, gantryDirection, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service);
        public IBeam AddConformalArcBeam(ExternalBeamMachineParameters machineParameters, double collimatorAngle, int controlPointCount, double gantryAngle, double gantryStop, GantryDirection gantryDirection, double patientSupportAngle, VVector isocenter) => _inner.AddConformalArcBeam(machineParameters, collimatorAngle, controlPointCount, gantryAngle, gantryStop, gantryDirection, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service);
        public IBeam AddFixedSequenceBeam(ExternalBeamMachineParameters machineParameters, double collimatorAngle, double gantryAngle, VVector isocenter) => _inner.AddFixedSequenceBeam(machineParameters, collimatorAngle, gantryAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service);
        public bool AddImagingSetup(ExternalBeamMachineParameters machineParameters, ImagingBeamSetupParameters setupParameters, IStructure targetStructure) => _inner.AddImagingSetup(machineParameters, setupParameters, targetStructure);
        public IBeam AddMLCArcBeam(ExternalBeamMachineParameters machineParameters, float[,] leafPositions, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double gantryStop, GantryDirection gantryDirection, double patientSupportAngle, VVector isocenter) => _inner.AddMLCArcBeam(machineParameters, leafPositions, jawPositions, collimatorAngle, gantryAngle, gantryStop, gantryDirection, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service);
        public IBeam AddMLCBeam(ExternalBeamMachineParameters machineParameters, float[,] leafPositions, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter) => _inner.AddMLCBeam(machineParameters, leafPositions, jawPositions, collimatorAngle, gantryAngle, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service);
        public IBeam AddMLCSetupBeam(ExternalBeamMachineParameters machineParameters, float[,] leafPositions, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter) => _inner.AddMLCSetupBeam(machineParameters, leafPositions, jawPositions, collimatorAngle, gantryAngle, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service);
        public IBeam AddMultipleStaticSegmentBeam(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter) => _inner.AddMultipleStaticSegmentBeam(machineParameters, metersetWeights, collimatorAngle, gantryAngle, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service);
        public IBeam AddSetupBeam(ExternalBeamMachineParameters machineParameters, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter) => _inner.AddSetupBeam(machineParameters, jawPositions, collimatorAngle, gantryAngle, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service);
        public IBeam AddSlidingWindowBeam(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter) => _inner.AddSlidingWindowBeam(machineParameters, metersetWeights, collimatorAngle, gantryAngle, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service);
        public IBeam AddSlidingWindowBeamForFixedJaws(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter) => _inner.AddSlidingWindowBeamForFixedJaws(machineParameters, metersetWeights, collimatorAngle, gantryAngle, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service);
        public IBeam AddStaticBeam(ExternalBeamMachineParameters machineParameters, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter) => _inner.AddStaticBeam(machineParameters, jawPositions, collimatorAngle, gantryAngle, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service);
        public IBeam AddVMATBeam(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double gantryStop, GantryDirection gantryDirection, double patientSupportAngle, VVector isocenter) => _inner.AddVMATBeam(machineParameters, metersetWeights, collimatorAngle, gantryAngle, gantryStop, gantryDirection, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service);
        public IBeam AddVMATBeamForFixedJaws(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryStartAngle, double gantryStopAngle, GantryDirection gantryDir, double patientSupportAngle, VVector isocenter) => _inner.AddVMATBeamForFixedJaws(machineParameters, metersetWeights, collimatorAngle, gantryStartAngle, gantryStopAngle, gantryDir, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service);
        public IEvaluationDose CopyEvaluationDose(IDose existing) => _inner.CopyEvaluationDose(existing) is var result && result is null ? null : new AsyncEvaluationDose(result, _service);
        public IEvaluationDose CreateEvaluationDose() => _inner.CreateEvaluationDose() is var result && result is null ? null : new AsyncEvaluationDose(result, _service);
        public void RemoveBeam(IBeam beam) => _inner.RemoveBeam(beam);
        public ITradeoffExplorationContext TradeoffExplorationContext => _inner.TradeoffExplorationContext is null ? null : new AsyncTradeoffExplorationContext(_inner.TradeoffExplorationContext, _service);

        public IEvaluationDose DoseAsEvaluationDose => _inner.DoseAsEvaluationDose is null ? null : new AsyncEvaluationDose(_inner.DoseAsEvaluationDose, _service);


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ExternalPlanSetup> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ExternalPlanSetup, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
