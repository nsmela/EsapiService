using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;
using Esapi.Interfaces;

namespace Esapi.Interfaces
{
    public interface ICalculation
    {
        // --- Simple Properties --- //
        string AlgorithmsRootPath { get; }

        // --- Methods --- //
        Task<IReadOnlyList<Calculation.Algorithm>> GetInstalledAlgorithmsAsync();
        Task<IReadOnlyList<Calculation.CalculationModel>> GetCalculationModelsAsync();
        Task<IReadOnlyList<IDVHEstimationModelStructure>> GetDvhEstimationModelStructuresAsync(Guid modelId);
        Task<IReadOnlyList<IDVHEstimationModelSummary>> GetDvhEstimationModelSummariesAsync();

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Calculation object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Calculation> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Calculation object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Calculation, T> func);
    }
}
