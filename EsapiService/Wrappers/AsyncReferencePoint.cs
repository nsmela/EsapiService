using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

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

            DailyDoseLimit = inner.DailyDoseLimit;
            SessionDoseLimit = inner.SessionDoseLimit;
            TotalDoseLimit = inner.TotalDoseLimit;
        }


        public Task<bool> AddLocationAsync(IImage Image, double x, double y, double z, Text.StringBuilder errorHint) => _service.RunAsync(() => _inner.AddLocation(Image, x, y, z, errorHint));

        public Task<bool> ChangeLocationAsync(IImage Image, double x, double y, double z, Text.StringBuilder errorHint) => _service.RunAsync(() => _inner.ChangeLocation(Image, x, y, z, errorHint));

        public Task<VVector> GetReferencePointLocationAsync(IImage Image) => _service.RunAsync(() => _inner.GetReferencePointLocation(Image));

        public Task<VVector> GetReferencePointLocationAsync(IPlanSetup planSetup) => _service.RunAsync(() => _inner.GetReferencePointLocation(planSetup));

        public Task<bool> HasLocationAsync(IPlanSetup planSetup) => _service.RunAsync(() => _inner.HasLocation(planSetup));

        public Task<bool> RemoveLocationAsync(IImage Image, Text.StringBuilder errorHint) => _service.RunAsync(() => _inner.RemoveLocation(Image, errorHint));

        public DoseValue DailyDoseLimit { get; private set; }
        public async Task SetDailyDoseLimitAsync(DoseValue value)
        {
            DailyDoseLimit = await _service.RunAsync(() =>
            {
                _inner.DailyDoseLimit = value;
                return _inner.DailyDoseLimit;
            });
        }

        public DoseValue SessionDoseLimit { get; private set; }
        public async Task SetSessionDoseLimitAsync(DoseValue value)
        {
            SessionDoseLimit = await _service.RunAsync(() =>
            {
                _inner.SessionDoseLimit = value;
                return _inner.SessionDoseLimit;
            });
        }

        public DoseValue TotalDoseLimit { get; private set; }
        public async Task SetTotalDoseLimitAsync(DoseValue value)
        {
            TotalDoseLimit = await _service.RunAsync(() =>
            {
                _inner.TotalDoseLimit = value;
                return _inner.TotalDoseLimit;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ReferencePoint> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ReferencePoint, T> func) => _service.RunAsync(() => func(_inner));
    }
}
