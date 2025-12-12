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
    public class AsyncApplicationScript : AsyncApiDataObject, IApplicationScript, IEsapiWrapper<VMS.TPS.Common.Model.API.ApplicationScript>
    {
        internal new readonly VMS.TPS.Common.Model.API.ApplicationScript _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncApplicationScript(VMS.TPS.Common.Model.API.ApplicationScript inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            ApprovalStatusDisplayText = inner.ApprovalStatusDisplayText;
            AssemblyName = inner.AssemblyName;
            ExpirationDate = inner.ExpirationDate;
            IsReadOnlyScript = inner.IsReadOnlyScript;
            IsWriteableScript = inner.IsWriteableScript;
            PublisherName = inner.PublisherName;
            StatusDate = inner.StatusDate;
        }

        public string ApprovalStatusDisplayText { get; }

        public System.Reflection.AssemblyName AssemblyName { get; }

        public DateTime? ExpirationDate { get; }

        public bool IsReadOnlyScript { get; }

        public bool IsWriteableScript { get; }

        public string PublisherName { get; }

        public DateTime? StatusDate { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ApplicationScript> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ApplicationScript, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.ApplicationScript(AsyncApplicationScript wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ApplicationScript IEsapiWrapper<VMS.TPS.Common.Model.API.ApplicationScript>.Inner => _inner;
    }
}
