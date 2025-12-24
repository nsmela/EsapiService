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

            ApprovalStatus = inner.ApprovalStatus;
            Description = inner.Description;
            ExpirationDate = inner.ExpirationDate;
            PackageId = inner.PackageId;
            PackageName = inner.PackageName;
            PackageVersion = inner.PackageVersion;
            PublisherData = inner.PublisherData;
            PublisherName = inner.PublisherName;
        }


        public ApplicationScriptApprovalStatus ApprovalStatus { get; private set; }


        public string Description { get; private set; }


        public DateTime? ExpirationDate { get; private set; }


        public string PackageId { get; private set; }


        public string PackageName { get; private set; }


        public string PackageVersion { get; private set; }


        public string PublisherData { get; private set; }


        public string PublisherName { get; private set; }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ApplicationPackage> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ApplicationPackage, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            ApprovalStatus = _inner.ApprovalStatus;
            Description = _inner.Description;
            ExpirationDate = _inner.ExpirationDate;
            PackageId = _inner.PackageId;
            PackageName = _inner.PackageName;
            PackageVersion = _inner.PackageVersion;
            PublisherData = _inner.PublisherData;
            PublisherName = _inner.PublisherName;
        }

        public static implicit operator VMS.TPS.Common.Model.API.ApplicationPackage(AsyncApplicationPackage wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ApplicationPackage IEsapiWrapper<VMS.TPS.Common.Model.API.ApplicationPackage>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.ApplicationPackage>.Service => _service;
    }
}
