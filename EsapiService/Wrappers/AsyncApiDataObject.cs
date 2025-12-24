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

            Id = inner.Id;
            Name = inner.Name;
            Comment = inner.Comment;
            HistoryUserName = inner.HistoryUserName;
            HistoryUserDisplayName = inner.HistoryUserDisplayName;
            HistoryDateTime = inner.HistoryDateTime;
        }


        public string Id { get; private set; }


        public string Name { get; private set; }


        public string Comment { get; private set; }


        public string HistoryUserName { get; private set; }


        public string HistoryUserDisplayName { get; private set; }


        public DateTime HistoryDateTime { get; private set; }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ApiDataObject> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ApiDataObject, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            Id = _inner.Id;
            Name = _inner.Name;
            Comment = _inner.Comment;
            HistoryUserName = _inner.HistoryUserName;
            HistoryUserDisplayName = _inner.HistoryUserDisplayName;
            HistoryDateTime = _inner.HistoryDateTime;
        }

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
