    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    public class AsyncReferencePoint : IReferencePoint
    {
        internal readonly VMS.TPS.Common.Model.API.ReferencePoint _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncReferencePoint(VMS.TPS.Common.Model.API.ReferencePoint inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }

        public bool AddLocation(IImage Image, double x, double y, double z, Text.StringBuilder errorHint) => _inner.AddLocation(Image, x, y, z, errorHint);
        public bool ChangeLocation(IImage Image, double x, double y, double z, Text.StringBuilder errorHint) => _inner.ChangeLocation(Image, x, y, z, errorHint);
        public VVector GetReferencePointLocation(IImage Image) => _inner.GetReferencePointLocation(Image);
        public VVector GetReferencePointLocation(IPlanSetup planSetup) => _inner.GetReferencePointLocation(planSetup);
        public bool HasLocation(IPlanSetup planSetup) => _inner.HasLocation(planSetup);
        public bool RemoveLocation(IImage Image, Text.StringBuilder errorHint) => _inner.RemoveLocation(Image, errorHint);
        public DoseValue DailyDoseLimit => _inner.DailyDoseLimit;
        public async Task SetDailyDoseLimitAsync(DoseValue value) => _service.RunAsync(() => _inner.DailyDoseLimit = value);
        public DoseValue SessionDoseLimit => _inner.SessionDoseLimit;
        public async Task SetSessionDoseLimitAsync(DoseValue value) => _service.RunAsync(() => _inner.SessionDoseLimit = value);
        public DoseValue TotalDoseLimit => _inner.TotalDoseLimit;
        public async Task SetTotalDoseLimitAsync(DoseValue value) => _service.RunAsync(() => _inner.TotalDoseLimit = value);

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ReferencePoint> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ReferencePoint, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
