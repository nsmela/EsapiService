namespace EsapiService.Wrappers
{
    public class AsyncRegistration : IRegistration
    {
        internal readonly VMS.TPS.Common.Model.API.Registration _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncRegistration(VMS.TPS.Common.Model.API.Registration inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            RegisteredFOR = inner.RegisteredFOR;
            SourceFOR = inner.SourceFOR;
            Status = inner.Status;
            StatusUserDisplayName = inner.StatusUserDisplayName;
            StatusUserName = inner.StatusUserName;
            TransformationMatrix = inner.TransformationMatrix;
            UID = inner.UID;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public VMS.TPS.Common.Model.Types.VVector InverseTransformPoint(VMS.TPS.Common.Model.Types.VVector pt) => _inner.InverseTransformPoint(pt);
        public VMS.TPS.Common.Model.Types.VVector TransformPoint(VMS.TPS.Common.Model.Types.VVector pt) => _inner.TransformPoint(pt);
        public System.Collections.Generic.IReadOnlyList<System.DateTime> CreationDateTime => _inner.CreationDateTime?.ToList();
        public string RegisteredFOR { get; }
        public string SourceFOR { get; }
        public VMS.TPS.Common.Model.Types.RegistrationApprovalStatus Status { get; }
        public System.Collections.Generic.IReadOnlyList<System.DateTime> StatusDateTime => _inner.StatusDateTime?.ToList();
        public string StatusUserDisplayName { get; }
        public string StatusUserName { get; }
        public double[,] TransformationMatrix { get; }
        public string UID { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Registration> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Registration, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
