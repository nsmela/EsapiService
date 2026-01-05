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
    public partial class AsyncRegistration : AsyncApiDataObject, IRegistration, IEsapiWrapper<VMS.TPS.Common.Model.API.Registration>
    {
        internal new readonly VMS.TPS.Common.Model.API.Registration _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncRegistration(VMS.TPS.Common.Model.API.Registration inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        // Simple Method
        public Task<VVector> InverseTransformPointAsync(VVector pt) => 
            _service.PostAsync(context => _inner.InverseTransformPoint(pt));

        // Simple Method
        public Task<VVector> TransformPointAsync(VVector pt) => 
            _service.PostAsync(context => _inner.TransformPoint(pt));

        public DateTime? CreationDateTime =>
            _inner.CreationDateTime;


        public string RegisteredFOR =>
            _inner.RegisteredFOR;


        public string SourceFOR =>
            _inner.SourceFOR;


        public RegistrationApprovalStatus Status =>
            _inner.Status;


        public DateTime? StatusDateTime =>
            _inner.StatusDateTime;


        public string StatusUserDisplayName =>
            _inner.StatusUserDisplayName;


        public string StatusUserName =>
            _inner.StatusUserName;


        public double[,] TransformationMatrix =>
            _inner.TransformationMatrix;


        public string UID =>
            _inner.UID;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Registration> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Registration, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.Registration(AsyncRegistration wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Registration IEsapiWrapper<VMS.TPS.Common.Model.API.Registration>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Registration>.Service => _service;
    }
}
