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
    public class AsyncApiDataObject : AsyncSerializableObject, IApiDataObject, IEsapiWrapper<VMS.TPS.Common.Model.API.ApiDataObject>
    {
        internal new readonly VMS.TPS.Common.Model.API.ApiDataObject _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncApiDataObject(VMS.TPS.Common.Model.API.ApiDataObject inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public string Id =>
            _inner.Id;


        public string Name =>
            _inner.Name;


        public string Comment =>
            _inner.Comment;


        public string HistoryUserName =>
            _inner.HistoryUserName;


        public string HistoryUserDisplayName =>
            _inner.HistoryUserDisplayName;


        public DateTime HistoryDateTime =>
            _inner.HistoryDateTime;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ApiDataObject> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ApiDataObject, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.ApiDataObject(AsyncApiDataObject wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ApiDataObject IEsapiWrapper<VMS.TPS.Common.Model.API.ApiDataObject>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.ApiDataObject>.Service => _service;

        /* --- Skipped Members (Not generated) ---
           - op_Equality: Static members are not supported
           - op_Inequality: Static members are not supported
        */
    }
}
