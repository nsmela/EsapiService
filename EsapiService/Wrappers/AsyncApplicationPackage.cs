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
    public class AsyncApplicationPackage : AsyncApiDataObject, IApplicationPackage, IEsapiWrapper<VMS.TPS.Common.Model.API.ApplicationPackage>
    {
        internal new readonly VMS.TPS.Common.Model.API.ApplicationPackage _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncApplicationPackage(VMS.TPS.Common.Model.API.ApplicationPackage inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public ApplicationScriptApprovalStatus ApprovalStatus =>
            _inner.ApprovalStatus;


        public string Description =>
            _inner.Description;


        public DateTime? ExpirationDate =>
            _inner.ExpirationDate;


        public string PackageId =>
            _inner.PackageId;


        public string PackageName =>
            _inner.PackageName;


        public string PackageVersion =>
            _inner.PackageVersion;


        public string PublisherData =>
            _inner.PublisherData;


        public string PublisherName =>
            _inner.PublisherName;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ApplicationPackage> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ApplicationPackage, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.ApplicationPackage(AsyncApplicationPackage wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ApplicationPackage IEsapiWrapper<VMS.TPS.Common.Model.API.ApplicationPackage>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.ApplicationPackage>.Service => _service;
    }
}
