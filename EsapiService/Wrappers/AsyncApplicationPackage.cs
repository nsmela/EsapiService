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
    public class AsyncApplicationPackage : AsyncApiDataObject, IApplicationPackage
    {
        internal readonly VMS.TPS.Common.Model.API.ApplicationPackage _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncApplicationPackage(VMS.TPS.Common.Model.API.ApplicationPackage inner, IEsapiService service) : base(inner, service)
        {
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


        public ApplicationScriptApprovalStatus ApprovalStatus { get; }

        public string Description { get; }

        public DateTime? ExpirationDate { get; }

        public string PackageId { get; }

        public string PackageName { get; }

        public string PackageVersion { get; }

        public string PublisherData { get; }

        public string PublisherName { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ApplicationPackage> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ApplicationPackage, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
