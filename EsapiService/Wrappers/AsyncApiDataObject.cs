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
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            Id = inner.Id;
            Name = inner.Name;
            Comment = inner.Comment;
            HistoryUserName = inner.HistoryUserName;
            HistoryUserDisplayName = inner.HistoryUserDisplayName;
            HistoryDateTime = inner.HistoryDateTime;
        }

        public string Id { get; }

        public string Name { get; }

        public string Comment { get; }

        public string HistoryUserName { get; }

        public string HistoryUserDisplayName { get; }

        public DateTime HistoryDateTime { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ApiDataObject> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ApiDataObject, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.ApiDataObject(AsyncApiDataObject wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ApiDataObject IEsapiWrapper<VMS.TPS.Common.Model.API.ApiDataObject>.Inner => _inner;
    }
}
