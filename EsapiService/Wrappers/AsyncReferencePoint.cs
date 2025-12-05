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

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public bool AddLocation(VMS.TPS.Common.Model.API.Image Image, double x, double y, double z, System.Text.StringBuilder errorHint) => _inner.AddLocation(Image, x, y, z, errorHint);
        public bool ChangeLocation(VMS.TPS.Common.Model.API.Image Image, double x, double y, double z, System.Text.StringBuilder errorHint) => _inner.ChangeLocation(Image, x, y, z, errorHint);
        public VMS.TPS.Common.Model.Types.VVector GetReferencePointLocation(VMS.TPS.Common.Model.API.Image Image) => _inner.GetReferencePointLocation(Image);
        public VMS.TPS.Common.Model.Types.VVector GetReferencePointLocation(VMS.TPS.Common.Model.API.PlanSetup planSetup) => _inner.GetReferencePointLocation(planSetup);
        public bool HasLocation(VMS.TPS.Common.Model.API.PlanSetup planSetup) => _inner.HasLocation(planSetup);
        public bool RemoveLocation(VMS.TPS.Common.Model.API.Image Image, System.Text.StringBuilder errorHint) => _inner.RemoveLocation(Image, errorHint);
        public string Id => _inner.Id;
        public async Task SetIdAsync(string value) => _service.RunAsync(() => _inner.Id = value);
        public VMS.TPS.Common.Model.Types.DoseValue DailyDoseLimit => _inner.DailyDoseLimit;
        public async Task SetDailyDoseLimitAsync(VMS.TPS.Common.Model.Types.DoseValue value) => _service.RunAsync(() => _inner.DailyDoseLimit = value);
        public VMS.TPS.Common.Model.Types.DoseValue SessionDoseLimit => _inner.SessionDoseLimit;
        public async Task SetSessionDoseLimitAsync(VMS.TPS.Common.Model.Types.DoseValue value) => _service.RunAsync(() => _inner.SessionDoseLimit = value);
        public VMS.TPS.Common.Model.Types.DoseValue TotalDoseLimit => _inner.TotalDoseLimit;
        public async Task SetTotalDoseLimitAsync(VMS.TPS.Common.Model.Types.DoseValue value) => _service.RunAsync(() => _inner.TotalDoseLimit = value);

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ReferencePoint> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ReferencePoint, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
