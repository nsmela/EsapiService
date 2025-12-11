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
    public class AsyncGlobals : IGlobals, IEsapiWrapper<VMS.TPS.Common.Model.API.Globals>
    {
        internal readonly VMS.TPS.Common.Model.API.Globals _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

public AsyncGlobals(VMS.TPS.Common.Model.API.Globals inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Globals> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Globals, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.Globals(AsyncGlobals wrapper) => wrapper._inner;
        // Internal Explicit Implementation to expose _inner safely
        VMS.TPS.Common.Model.API.Globals IEsapiWrapper<VMS.TPS.Common.Model.API.Globals>.Inner => _inner;
    }
}
