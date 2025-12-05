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

        public VVector InverseTransformPoint(VVector pt) => _inner.InverseTransformPoint(pt);
        public VVector TransformPoint(VVector pt) => _inner.TransformPoint(pt);
        public IReadOnlyList<DateTime> CreationDateTime => _inner.CreationDateTime?.ToList();
        public string RegisteredFOR { get; }
        public string SourceFOR { get; }
        public RegistrationApprovalStatus Status { get; }
        public IReadOnlyList<DateTime> StatusDateTime => _inner.StatusDateTime?.ToList();
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
