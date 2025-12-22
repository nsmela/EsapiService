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
    public class AsyncCalculationResult : ICalculationResult, IEsapiWrapper<VMS.TPS.Common.Model.API.CalculationResult>
    {
        internal readonly VMS.TPS.Common.Model.API.CalculationResult _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncCalculationResult(VMS.TPS.Common.Model.API.CalculationResult inner, IEsapiService service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            Success = inner.Success;
        }


        public bool Success { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.CalculationResult> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.CalculationResult, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.CalculationResult(AsyncCalculationResult wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.CalculationResult IEsapiWrapper<VMS.TPS.Common.Model.API.CalculationResult>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.CalculationResult>.Service => _service;
    }
}
