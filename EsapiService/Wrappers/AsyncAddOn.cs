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
    public class AsyncAddOn : AsyncApiDataObject, IAddOn, IEsapiWrapper<VMS.TPS.Common.Model.API.AddOn>
    {
        internal new readonly VMS.TPS.Common.Model.API.AddOn _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncAddOn(VMS.TPS.Common.Model.API.AddOn inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            CreationDateTime = inner.CreationDateTime;
        }

        public DateTime? CreationDateTime { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.AddOn> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.AddOn, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.AddOn(AsyncAddOn wrapper) => wrapper._inner;
        // Internal Explicit Implementation to expose _inner safely
        VMS.TPS.Common.Model.API.AddOn IEsapiWrapper<VMS.TPS.Common.Model.API.AddOn>.Inner => _inner;
    }
}
