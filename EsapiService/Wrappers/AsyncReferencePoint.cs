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
    public class AsyncReferencePoint : AsyncApiDataObject, IReferencePoint, IEsapiWrapper<VMS.TPS.Common.Model.API.ReferencePoint>
    {
        internal new readonly VMS.TPS.Common.Model.API.ReferencePoint _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncReferencePoint(VMS.TPS.Common.Model.API.ReferencePoint inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            DailyDoseLimit = inner.DailyDoseLimit;
            SessionDoseLimit = inner.SessionDoseLimit;
            TotalDoseLimit = inner.TotalDoseLimit;
        }

        // Simple Method
        public Task<bool> AddLocationAsync(IImage Image, double x, double y, double z, System.Text.StringBuilder errorHint) => 
            _service.PostAsync(context => _inner.AddLocation(((AsyncImage)Image)._inner, x, y, z, errorHint));

        // Simple Method
        public Task<bool> ChangeLocationAsync(IImage Image, double x, double y, double z, System.Text.StringBuilder errorHint) => 
            _service.PostAsync(context => _inner.ChangeLocation(((AsyncImage)Image)._inner, x, y, z, errorHint));

        // Simple Method
        public Task<VVector> GetReferencePointLocationAsync(IImage Image) => 
            _service.PostAsync(context => _inner.GetReferencePointLocation(((AsyncImage)Image)._inner));

        // Simple Method
        public Task<VVector> GetReferencePointLocationAsync(IPlanSetup planSetup) => 
            _service.PostAsync(context => _inner.GetReferencePointLocation(((AsyncPlanSetup)planSetup)._inner));

        // Simple Method
        public Task<bool> HasLocationAsync(IPlanSetup planSetup) => 
            _service.PostAsync(context => _inner.HasLocation(((AsyncPlanSetup)planSetup)._inner));

        // Simple Method
        public Task<bool> RemoveLocationAsync(IImage Image, System.Text.StringBuilder errorHint) => 
            _service.PostAsync(context => _inner.RemoveLocation(((AsyncImage)Image)._inner, errorHint));

        public DoseValue DailyDoseLimit { get; private set; }
        public async Task SetDailyDoseLimitAsync(DoseValue value)
        {
            DailyDoseLimit = await _service.PostAsync(context => 
            {
                _inner.DailyDoseLimit = value;
                return _inner.DailyDoseLimit;
            });
        }

        public DoseValue SessionDoseLimit { get; private set; }
        public async Task SetSessionDoseLimitAsync(DoseValue value)
        {
            SessionDoseLimit = await _service.PostAsync(context => 
            {
                _inner.SessionDoseLimit = value;
                return _inner.SessionDoseLimit;
            });
        }

        public DoseValue TotalDoseLimit { get; private set; }
        public async Task SetTotalDoseLimitAsync(DoseValue value)
        {
            TotalDoseLimit = await _service.PostAsync(context => 
            {
                _inner.TotalDoseLimit = value;
                return _inner.TotalDoseLimit;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ReferencePoint> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ReferencePoint, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.ReferencePoint(AsyncReferencePoint wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ReferencePoint IEsapiWrapper<VMS.TPS.Common.Model.API.ReferencePoint>.Inner => _inner;

        /* --- Skipped Members (Not generated) ---
           - Id: Shadows member in wrapped base class
        */
    }
}
