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
    public class AsyncESAPIActionPackAttribute : IESAPIActionPackAttribute, IEsapiWrapper<VMS.TPS.Common.Model.API.ESAPIActionPackAttribute>
    {
        internal readonly VMS.TPS.Common.Model.API.ESAPIActionPackAttribute _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncESAPIActionPackAttribute(VMS.TPS.Common.Model.API.ESAPIActionPackAttribute inner, IEsapiService service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public bool IsWriteable
        {
            get => _inner.IsWriteable;
            set => _inner.IsWriteable = value;
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ESAPIActionPackAttribute> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ESAPIActionPackAttribute, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.ESAPIActionPackAttribute(AsyncESAPIActionPackAttribute wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ESAPIActionPackAttribute IEsapiWrapper<VMS.TPS.Common.Model.API.ESAPIActionPackAttribute>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.ESAPIActionPackAttribute>.Service => _service;

        /* --- Skipped Members (Not generated) ---
           - .ctor: Explicitly ignored by name
        */
    }
}
