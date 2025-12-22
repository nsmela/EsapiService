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
    public class AsyncESAPIScriptAttribute : IESAPIScriptAttribute, IEsapiWrapper<VMS.TPS.Common.Model.API.ESAPIScriptAttribute>
    {
        internal readonly VMS.TPS.Common.Model.API.ESAPIScriptAttribute _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncESAPIScriptAttribute(VMS.TPS.Common.Model.API.ESAPIScriptAttribute inner, IEsapiService service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            IsWriteable = inner.IsWriteable;
        }


        public bool IsWriteable { get; private set; }
        public async Task SetIsWriteableAsync(bool value)
        {
            IsWriteable = await _service.PostAsync(context => 
            {
                _inner.IsWriteable = value;
                return _inner.IsWriteable;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ESAPIScriptAttribute> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ESAPIScriptAttribute, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.ESAPIScriptAttribute(AsyncESAPIScriptAttribute wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ESAPIScriptAttribute IEsapiWrapper<VMS.TPS.Common.Model.API.ESAPIScriptAttribute>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.ESAPIScriptAttribute>.Service => _service;

        /* --- Skipped Members (Not generated) ---
           - .ctor: Explicitly ignored by name
        */
    }
}
