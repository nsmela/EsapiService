using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;

namespace Esapi.Interfaces
{
    public interface IExternalPlanSetup : IPlanSetup
    {

        // --- Accessors --- //
        Task<ITradeoffExplorationContext> GetTradeoffExplorationContextAsync(); // read complex property
        Task<IEvaluationDose> GetDoseAsEvaluationDoseAsync(); // read complex property

        // --- Methods --- //
        Task<ICalculationResult> CalculateDoseWithPresetValuesAsync(List<KeyValuePair<string, MetersetValue>> presetValues); // complex method
        Task<ICalculationResult> CalculateDoseAsync(); // complex method
        Task<ICalculationResult> CalculatePlanUncertaintyDosesAsync(); // complex method
        Task<ICalculationResult> CalculateLeafMotionsAndDoseAsync(); // complex method
        Task<ICalculationResult> CalculateLeafMotionsAsync(); // complex method
        Task<IOptimizerResult> OptimizeAsync(int maxIterations); // complex method
        Task<IOptimizerResult> OptimizeAsync(); // complex method
        Task<IOptimizerResult> OptimizeVMATAsync(string mlcId); // complex method
        Task<IOptimizerResult> OptimizeVMATAsync(); // complex method
        Task<ICalculationResult> CalculateDVHEstimatesAsync(string modelId, Dictionary<string, DoseValue> targetDoseLevels, Dictionary<string, string> structureMatches); // complex method
        Task<IEvaluationDose> CopyEvaluationDoseAsync(IDose existing); // complex method
        Task<IEvaluationDose> CreateEvaluationDoseAsync(); // complex method
        Task RemoveBeamAsync(IBeam beam); // void method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ExternalPlanSetup object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ExternalPlanSetup> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ExternalPlanSetup object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ExternalPlanSetup, T> func);
    }
}
