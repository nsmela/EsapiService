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
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            ApprovalStatus = inner.ApprovalStatus;
            ApprovalStatusDisplayText = inner.ApprovalStatusDisplayText;
            AssemblyName = inner.AssemblyName;
            ExpirationDate = inner.ExpirationDate;
            IsReadOnlyScript = inner.IsReadOnlyScript;
            IsWriteableScript = inner.IsWriteableScript;
            PublisherName = inner.PublisherName;
            ScriptType = inner.ScriptType;
            StatusDate = inner.StatusDate;
            StatusUserIdentity = inner.StatusUserIdentity;
        }


        public ApplicationScriptApprovalStatus ApprovalStatus { get; private set; }

        public string ApprovalStatusDisplayText { get; private set; }

        public System.Reflection.AssemblyName AssemblyName { get; private set; }

        public DateTime? ExpirationDate { get; private set; }

        public bool IsReadOnlyScript { get; private set; }

        public bool IsWriteableScript { get; private set; }

        public string PublisherName { get; private set; }

        public ApplicationScriptType ScriptType { get; private set; }

        public DateTime? StatusDate { get; private set; }

        public UserIdentity StatusUserIdentity { get; private set; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ApplicationScript> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ApplicationScript, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            ApprovalStatus = _inner.ApprovalStatus;
            ApprovalStatusDisplayText = _inner.ApprovalStatusDisplayText;
            AssemblyName = _inner.AssemblyName;
            ExpirationDate = _inner.ExpirationDate;
            IsReadOnlyScript = _inner.IsReadOnlyScript;
            IsWriteableScript = _inner.IsWriteableScript;
            PublisherName = _inner.PublisherName;
            ScriptType = _inner.ScriptType;
            StatusDate = _inner.StatusDate;
            StatusUserIdentity = _inner.StatusUserIdentity;
        }

        public static implicit operator VMS.TPS.Common.Model.API.ApplicationScript(AsyncApplicationScript wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ApplicationScript IEsapiWrapper<VMS.TPS.Common.Model.API.ApplicationScript>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.ApplicationScript>.Service => _service;
    }
}
