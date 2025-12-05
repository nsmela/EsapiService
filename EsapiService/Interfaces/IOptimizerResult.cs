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
        System.Collections.Generic.IReadOnlyList<IOptimizerDVH> StructureDVHs { get; }
        System.Collections.Generic.IReadOnlyList<IOptimizerObjectiveValue> StructureObjectiveValues { get; }
        double MinMUObjectiveValue { get; }
        double TotalObjectiveFunctionValue { get; }
        int NumberOfIMRTOptimizerIterations { get; }

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
