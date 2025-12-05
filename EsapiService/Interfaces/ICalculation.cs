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
        Task<System.Collections.Generic.IReadOnlyList<VMS.TPS.Common.Model.API.Calculation.Algorithm>> GetInstalledAlgorithmsAsync();
        Task<System.Collections.Generic.IReadOnlyList<VMS.TPS.Common.Model.API.Calculation.CalculationModel>> GetCalculationModelsAsync();
        Task<System.Collections.Generic.IReadOnlyList<IDVHEstimationModelStructure>> GetDvhEstimationModelStructuresAsync(System.Guid modelId);
        Task<System.Collections.Generic.IReadOnlyList<IDVHEstimationModelSummary>> GetDvhEstimationModelSummariesAsync();
        string AlgorithmsRootPath { get; }

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
