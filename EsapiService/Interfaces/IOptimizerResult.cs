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
        IEnumerable<OptimizerDVH> StructureDVHs { get; }
        IEnumerable<OptimizerObjectiveValue> StructureObjectiveValues { get; }
        double TotalObjectiveFunctionValue { get; }
        int NumberOfIMRTOptimizerIterations { get; }

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
