using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;

namespace Esapi.Wrappers
{
    public class AsyncCalculation : ICalculation
    {
        internal readonly VMS.TPS.Common.Model.API.Calculation _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncCalculation(VMS.TPS.Common.Model.API.Calculation inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

            AlgorithmsRootPath = inner.AlgorithmsRootPath;
        }


        public Task<IReadOnlyList<Calculation.Algorithm>> GetInstalledAlgorithmsAsync() => _service.RunAsync(() => _inner.GetInstalledAlgorithms()?.ToList());

        public Task<IReadOnlyList<Calculation.CalculationModel>> GetCalculationModelsAsync() => _service.RunAsync(() => _inner.GetCalculationModels()?.ToList());

        public async Task<IReadOnlyList<IDVHEstimationModelStructure>> GetDvhEstimationModelStructuresAsync(Guid modelId)
        {
            return await _service.RunAsync(() => 
                _inner.GetDvhEstimationModelStructures(modelId)?.Select(x => new AsyncDVHEstimationModelStructure(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IDVHEstimationModelSummary>> GetDvhEstimationModelSummariesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.GetDvhEstimationModelSummaries()?.Select(x => new AsyncDVHEstimationModelSummary(x, _service)).ToList());
        }


        public string AlgorithmsRootPath { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Calculation> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Calculation, T> func) => _service.RunAsync(() => func(_inner));
    }
}
