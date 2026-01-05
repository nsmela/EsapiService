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
    public partial class AsyncGlobals : IGlobals, IEsapiWrapper<VMS.TPS.Common.Model.API.Globals>
    {
        internal readonly VMS.TPS.Common.Model.API.Globals _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncGlobals(VMS.TPS.Common.Model.API.Globals inner, IEsapiService service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Globals> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Globals, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.Globals(AsyncGlobals wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Globals IEsapiWrapper<VMS.TPS.Common.Model.API.Globals>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Globals>.Service => _service;

        /* --- Skipped Members (Not generated) ---
           - Initialize: References non-wrapped Varian API type
           - SetMaximumNumberOfLoggedApiCalls: Static members are not supported
           - GetLoggedApiCalls: Static members are not supported
           - EnableApiAccessTrace: Static members are not supported
           - DisableApiAccessTrace: Static members are not supported
           - AddCustomLogEntry: Static members are not supported
           - AbortNow: Static members are not supported
           - DefaultMaximumNumberOfLoggedApiCalls: Static members are not supported
        */
    }
}
