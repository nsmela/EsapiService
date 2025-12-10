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
    public interface IOptimizerResult : ICalculationResult
    {
        // --- Simple Properties --- //
        double MinMUObjectiveValue { get; }
        double TotalObjectiveFunctionValue { get; }
        int NumberOfIMRTOptimizerIterations { get; }

        // --- Collections --- //
        Task<IReadOnlyList<IOptimizerDVH>> GetStructureDVHsAsync(); // collection proeprty context
        Task<IReadOnlyList<IOptimizerObjectiveValue>> GetStructureObjectiveValuesAsync(); // collection proeprty context

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.OptimizerResult object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizerResult> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.OptimizerResult object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizerResult, T> func);
    }
}
