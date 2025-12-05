namespace EsapiService.Wrappers
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

        public IReadOnlyList<Calculation.Algorithm> GetInstalledAlgorithms() => _inner.GetInstalledAlgorithms()?.ToList();
        public IReadOnlyList<Calculation.CalculationModel> GetCalculationModels() => _inner.GetCalculationModels()?.ToList();
        public IReadOnlyList<IDVHEstimationModelStructure> GetDvhEstimationModelStructures(Guid modelId) => _inner.GetDvhEstimationModelStructures(modelId)?.Select(x => new AsyncDVHEstimationModelStructure(x, _service)).ToList();
        public IReadOnlyList<IDVHEstimationModelSummary> GetDvhEstimationModelSummaries() => _inner.GetDvhEstimationModelSummaries()?.Select(x => new AsyncDVHEstimationModelSummary(x, _service)).ToList();
        public string AlgorithmsRootPath { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Calculation> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Calculation, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
