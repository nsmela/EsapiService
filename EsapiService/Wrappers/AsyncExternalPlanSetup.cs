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


        public async Task<ICalculationResult> CalculateLeafMotionsAsync(LMCVOptions options)
        {
            return await _service.PostAsync(context => 
                _inner.CalculateLeafMotions(options) is var result && result is null ? null : new AsyncCalculationResult(result, _service));
        }


        public async Task<ICalculationResult> CalculateLeafMotionsAsync(SmartLMCOptions options)
        {
            return await _service.PostAsync(context => 
                _inner.CalculateLeafMotions(options) is var result && result is null ? null : new AsyncCalculationResult(result, _service));
        }


        public async Task<ICalculationResult> CalculateLeafMotionsAsync(LMCMSSOptions options)
        {
            return await _service.PostAsync(context => 
                _inner.CalculateLeafMotions(options) is var result && result is null ? null : new AsyncCalculationResult(result, _service));
        }


        // Simple Collection Method
        public async Task<IReadOnlyList<string>> GetModelsForCalculationTypeAsync(CalculationType calculationType) => 
            await _service.PostAsync(context => _inner.GetModelsForCalculationType(calculationType)?.ToList());

        public async Task<IOptimizerResult> OptimizeAsync(int maxIterations)
        {
            return await _service.PostAsync(context => 
                _inner.Optimize(maxIterations) is var result && result is null ? null : new AsyncOptimizerResult(result, _service));
        }


        public async Task<IOptimizerResult> OptimizeAsync(int maxIterations, OptimizationOption optimizationOption)
        {
            return await _service.PostAsync(context => 
                _inner.Optimize(maxIterations, optimizationOption) is var result && result is null ? null : new AsyncOptimizerResult(result, _service));
        }


        public async Task<IOptimizerResult> OptimizeAsync(int maxIterations, OptimizationOption optimizationOption, string mlcId)
        {
            return await _service.PostAsync(context => 
                _inner.Optimize(maxIterations, optimizationOption, mlcId) is var result && result is null ? null : new AsyncOptimizerResult(result, _service));
        }


        public async Task<IOptimizerResult> OptimizeAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Optimize() is var result && result is null ? null : new AsyncOptimizerResult(result, _service));
        }


        public async Task<IOptimizerResult> OptimizeAsync(OptimizationOptionsIMRT options)
        {
            return await _service.PostAsync(context => 
                _inner.Optimize(options) is var result && result is null ? null : new AsyncOptimizerResult(result, _service));
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


        public async Task<IOptimizerResult> OptimizeVMATAsync(OptimizationOptionsVMAT options)
        {
            return await _service.PostAsync(context => 
                _inner.OptimizeVMAT(options) is var result && result is null ? null : new AsyncOptimizerResult(result, _service));
        }


        public async Task<ICalculationResult> CalculateDVHEstimatesAsync(string modelId, Dictionary<string, DoseValue> targetDoseLevels, Dictionary<string, string> structureMatches)
        {
            return await _service.PostAsync(context => 
                _inner.CalculateDVHEstimates(modelId, targetDoseLevels, structureMatches) is var result && result is null ? null : new AsyncCalculationResult(result, _service));
        }


        public async Task<IBeam> AddArcBeamAsync(ExternalBeamMachineParameters machineParameters, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double gantryStop, GantryDirection gantryDirection, double patientSupportAngle, VVector isocenter)
        {
            return await _service.PostAsync(context => 
                _inner.AddArcBeam(machineParameters, jawPositions, collimatorAngle, gantryAngle, gantryStop, gantryDirection, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service));
        }


        public async Task<IBeam> AddConformalArcBeamAsync(ExternalBeamMachineParameters machineParameters, double collimatorAngle, int controlPointCount, double gantryAngle, double gantryStop, GantryDirection gantryDirection, double patientSupportAngle, VVector isocenter)
        {
            return await _service.PostAsync(context => 
                _inner.AddConformalArcBeam(machineParameters, collimatorAngle, controlPointCount, gantryAngle, gantryStop, gantryDirection, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service));
        }


        public async Task<IBeam> AddFixedSequenceBeamAsync(ExternalBeamMachineParameters machineParameters, double collimatorAngle, double gantryAngle, VVector isocenter)
        {
            return await _service.PostAsync(context => 
                _inner.AddFixedSequenceBeam(machineParameters, collimatorAngle, gantryAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service));
        }


        // Simple Method
        public Task<bool> AddImagingSetupAsync(ExternalBeamMachineParameters machineParameters, ImagingBeamSetupParameters setupParameters, IStructure targetStructure) => 
            _service.PostAsync(context => _inner.AddImagingSetup(machineParameters, setupParameters, ((AsyncStructure)targetStructure)._inner));

        public async Task<IBeam> AddMLCArcBeamAsync(ExternalBeamMachineParameters machineParameters, float[,] leafPositions, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double gantryStop, GantryDirection gantryDirection, double patientSupportAngle, VVector isocenter)
        {
            return await _service.PostAsync(context => 
                _inner.AddMLCArcBeam(machineParameters, leafPositions, jawPositions, collimatorAngle, gantryAngle, gantryStop, gantryDirection, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service));
        }


        public async Task<IBeam> AddMLCBeamAsync(ExternalBeamMachineParameters machineParameters, float[,] leafPositions, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter)
        {
            return await _service.PostAsync(context => 
                _inner.AddMLCBeam(machineParameters, leafPositions, jawPositions, collimatorAngle, gantryAngle, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service));
        }


        public async Task<IBeam> AddMLCSetupBeamAsync(ExternalBeamMachineParameters machineParameters, float[,] leafPositions, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter)
        {
            return await _service.PostAsync(context => 
                _inner.AddMLCSetupBeam(machineParameters, leafPositions, jawPositions, collimatorAngle, gantryAngle, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service));
        }


        public async Task<IBeam> AddMultipleStaticSegmentBeamAsync(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter)
        {
            return await _service.PostAsync(context => 
                _inner.AddMultipleStaticSegmentBeam(machineParameters, metersetWeights, collimatorAngle, gantryAngle, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service));
        }


        public async Task<IBeam> AddSetupBeamAsync(ExternalBeamMachineParameters machineParameters, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter)
        {
            return await _service.PostAsync(context => 
                _inner.AddSetupBeam(machineParameters, jawPositions, collimatorAngle, gantryAngle, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service));
        }


        public async Task<IBeam> AddSlidingWindowBeamAsync(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter)
        {
            return await _service.PostAsync(context => 
                _inner.AddSlidingWindowBeam(machineParameters, metersetWeights, collimatorAngle, gantryAngle, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service));
        }


        public async Task<IBeam> AddSlidingWindowBeamForFixedJawsAsync(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter)
        {
            return await _service.PostAsync(context => 
                _inner.AddSlidingWindowBeamForFixedJaws(machineParameters, metersetWeights, collimatorAngle, gantryAngle, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service));
        }


        public async Task<IBeam> AddStaticBeamAsync(ExternalBeamMachineParameters machineParameters, VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VVector isocenter)
        {
            return await _service.PostAsync(context => 
                _inner.AddStaticBeam(machineParameters, jawPositions, collimatorAngle, gantryAngle, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service));
        }


        public async Task<IBeam> AddVMATBeamAsync(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double gantryStop, GantryDirection gantryDirection, double patientSupportAngle, VVector isocenter)
        {
            return await _service.PostAsync(context => 
                _inner.AddVMATBeam(machineParameters, metersetWeights, collimatorAngle, gantryAngle, gantryStop, gantryDirection, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service));
        }


        public async Task<IBeam> AddVMATBeamForFixedJawsAsync(ExternalBeamMachineParameters machineParameters, IEnumerable<double> metersetWeights, double collimatorAngle, double gantryStartAngle, double gantryStopAngle, GantryDirection gantryDir, double patientSupportAngle, VVector isocenter)
        {
            return await _service.PostAsync(context => 
                _inner.AddVMATBeamForFixedJaws(machineParameters, metersetWeights, collimatorAngle, gantryStartAngle, gantryStopAngle, gantryDir, patientSupportAngle, isocenter) is var result && result is null ? null : new AsyncBeam(result, _service));
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

        public static implicit operator VMS.TPS.Common.Model.API.ExternalPlanSetup(AsyncExternalPlanSetup wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ExternalPlanSetup IEsapiWrapper<VMS.TPS.Common.Model.API.ExternalPlanSetup>.Inner => _inner;
    }
}
