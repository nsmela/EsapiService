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
    public class AsyncOptimizerDVH : IOptimizerDVH, IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizerDVH>
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizerDVH _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncOptimizerDVH(VMS.TPS.Common.Model.API.OptimizerDVH inner, IEsapiService service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public DVHPoint[] CurveData =>
            _inner.CurveData;


        public async Task<IStructure> GetStructureAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Structure is null ? null : new AsyncStructure(_inner.Structure, _service);
                return innerResult;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizerDVH> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizerDVH, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.OptimizerDVH(AsyncOptimizerDVH wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.OptimizerDVH IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizerDVH>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizerDVH>.Service => _service;
    }
}
