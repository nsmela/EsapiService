namespace EsapiService.Wrappers
{
    public class AsyncApplicationPackage : IApplicationPackage
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
            PackageId = inner.PackageId;
            PackageName = inner.PackageName;
            PackageVersion = inner.PackageVersion;
            PublisherData = inner.PublisherData;
            PublisherName = inner.PublisherName;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public VMS.TPS.Common.Model.Types.ApplicationScriptApprovalStatus ApprovalStatus { get; }
        public string Description { get; }
        public System.Collections.Generic.IReadOnlyList<System.DateTime> ExpirationDate => _inner.ExpirationDate?.ToList();
        public string PackageId { get; }
        public string PackageName { get; }
        public string PackageVersion { get; }
        public string PublisherData { get; }
        public string PublisherName { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ApplicationPackage> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ApplicationPackage, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
