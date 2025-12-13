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
    public interface ICalculation
    {
        // --- Simple Properties --- //
        string AlgorithmsRootPath { get; }

        // --- Methods --- //
        Task<IReadOnlyList<IDVHEstimationModelStructure>> GetDvhEstimationModelStructuresAsync(System.Guid modelId); // complex collection method
        Task<IReadOnlyList<IDVHEstimationModelSummary>> GetDvhEstimationModelSummariesAsync(); // complex collection method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Calculation object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Calculation> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Calculation object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Calculation, T> func);

        /* --- Skipped Members (Not generated) ---
           - .ctor: Explicitly ignored by name
           - GetInstalledAlgorithms: References non-wrapped Varian API type
           - GetCalculationModels: References non-wrapped Varian API type
           - Algorithm: No matching factory found (Not Implemented)
           - CalculationModel: No matching factory found (Not Implemented)
        */
    }
}
