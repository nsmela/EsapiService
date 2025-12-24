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
    public class AsyncRegistration : AsyncApiDataObject, IRegistration, IEsapiWrapper<VMS.TPS.Common.Model.API.Registration>
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

            CreationDateTime = inner.CreationDateTime;
            RegisteredFOR = inner.RegisteredFOR;
            SourceFOR = inner.SourceFOR;
            Status = inner.Status;
            StatusDateTime = inner.StatusDateTime;
            StatusUserDisplayName = inner.StatusUserDisplayName;
            StatusUserName = inner.StatusUserName;
            TransformationMatrix = inner.TransformationMatrix;
            UID = inner.UID;
        }


        // Simple Method
        public Task<VVector> InverseTransformPointAsync(VVector pt) => 
            _service.PostAsync(context => _inner.InverseTransformPoint(pt));

        // Simple Method
        public Task<VVector> TransformPointAsync(VVector pt) => 
            _service.PostAsync(context => _inner.TransformPoint(pt));

        public DateTime? CreationDateTime { get; private set; }


        public string RegisteredFOR { get; private set; }


        public string SourceFOR { get; private set; }


        public RegistrationApprovalStatus Status { get; private set; }


        public DateTime? StatusDateTime { get; private set; }


        public string StatusUserDisplayName { get; private set; }


        public string StatusUserName { get; private set; }


        public double[,] TransformationMatrix { get; private set; }


        public string UID { get; private set; }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Registration> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Registration, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            CreationDateTime = _inner.CreationDateTime;
            RegisteredFOR = _inner.RegisteredFOR;
            SourceFOR = _inner.SourceFOR;
            Status = _inner.Status;
            StatusDateTime = _inner.StatusDateTime;
            StatusUserDisplayName = _inner.StatusUserDisplayName;
            StatusUserName = _inner.StatusUserName;
            TransformationMatrix = _inner.TransformationMatrix;
            UID = _inner.UID;
        }

        public static implicit operator VMS.TPS.Common.Model.API.Registration(AsyncRegistration wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Registration IEsapiWrapper<VMS.TPS.Common.Model.API.Registration>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Registration>.Service => _service;
    }
}
