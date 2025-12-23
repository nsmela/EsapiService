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
    public class AsyncUser : AsyncSerializableObject, IUser, IEsapiWrapper<VMS.TPS.Common.Model.API.User>
    {
        internal new readonly VMS.TPS.Common.Model.API.User _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncUser(VMS.TPS.Common.Model.API.User inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            Id = inner.Id;
            Language = inner.Language;
            Name = inner.Name;
        }


        public string Id { get; private set; }

        public string Language { get; private set; }

        public string Name { get; private set; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.User> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.User, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            Id = _inner.Id;
            Language = _inner.Language;
            Name = _inner.Name;
        }

        public static implicit operator VMS.TPS.Common.Model.API.User(AsyncUser wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.User IEsapiWrapper<VMS.TPS.Common.Model.API.User>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.User>.Service => _service;

        /* --- Skipped Members (Not generated) ---
           - op_Equality: Static members are not supported
           - op_Inequality: Static members are not supported
        */
    }
}
