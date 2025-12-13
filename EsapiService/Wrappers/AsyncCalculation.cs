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
    public class AsyncCalculation : ICalculation, IEsapiWrapper<VMS.TPS.Common.Model.API.Calculation>
    {
        internal readonly VMS.TPS.Common.Model.API.Calculation _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

public AsyncCalculation(VMS.TPS.Common.Model.API.Calculation inner, IEsapiService service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            AlgorithmsRootPath = inner.AlgorithmsRootPath;
        }

        public async Task<IReadOnlyList<IDVHEstimationModelStructure>> GetDvhEstimationModelStructuresAsync(System.Guid modelId)
        {
            return await _service.PostAsync(context => 
                _inner.GetDvhEstimationModelStructures(modelId)?.Select(x => new AsyncDVHEstimationModelStructure(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IDVHEstimationModelSummary>> GetDvhEstimationModelSummariesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.GetDvhEstimationModelSummaries()?.Select(x => new AsyncDVHEstimationModelSummary(x, _service)).ToList());
        }


        public string AlgorithmsRootPath { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Calculation> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Calculation, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.Calculation(AsyncCalculation wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Calculation IEsapiWrapper<VMS.TPS.Common.Model.API.Calculation>.Inner => _inner;

        /* --- Skipped Members (Not generated) ---
           - .ctor: Explicitly ignored by name
           - GetInstalledAlgorithms: References non-wrapped Varian API type
           - GetCalculationModels: References non-wrapped Varian API type
           - Algorithm: No matching factory found (Not Implemented)
           - CalculationModel: No matching factory found (Not Implemented)
        */
    }
}
