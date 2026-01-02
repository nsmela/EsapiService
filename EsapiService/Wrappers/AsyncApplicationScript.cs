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
        }


        public ApplicationScriptApprovalStatus ApprovalStatus =>
            _inner.ApprovalStatus;


        public string ApprovalStatusDisplayText =>
            _inner.ApprovalStatusDisplayText;


        public System.Reflection.AssemblyName AssemblyName =>
            _inner.AssemblyName;


        public DateTime? ExpirationDate =>
            _inner.ExpirationDate;


        public bool IsReadOnlyScript =>
            _inner.IsReadOnlyScript;


        public bool IsWriteableScript =>
            _inner.IsWriteableScript;


        public string PublisherName =>
            _inner.PublisherName;


        public ApplicationScriptType ScriptType =>
            _inner.ScriptType;


        public DateTime? StatusDate =>
            _inner.StatusDate;


        public UserIdentity StatusUserIdentity =>
            _inner.StatusUserIdentity;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ApplicationScript> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ApplicationScript, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.ApplicationScript(AsyncApplicationScript wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ApplicationScript IEsapiWrapper<VMS.TPS.Common.Model.API.ApplicationScript>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.ApplicationScript>.Service => _service;
    }
}
