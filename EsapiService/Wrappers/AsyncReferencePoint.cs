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
    public class AsyncReferencePoint : AsyncApiDataObject, IReferencePoint
    {
        internal new readonly VMS.TPS.Common.Model.API.ReferencePoint _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncReferencePoint(VMS.TPS.Common.Model.API.ReferencePoint inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }


        public Task<bool> AddLocationAsync(IImage Image, double x, double y, double z, System.Text.StringBuilder errorHint) => _service.PostAsync(context => _inner.AddLocation(((AsyncImage)Image)._inner, x, y, z, errorHint));

        public Task<bool> ChangeLocationAsync(IImage Image, double x, double y, double z, System.Text.StringBuilder errorHint) => _service.PostAsync(context => _inner.ChangeLocation(((AsyncImage)Image)._inner, x, y, z, errorHint));

        public Task<bool> HasLocationAsync(IPlanSetup planSetup) => _service.PostAsync(context => _inner.HasLocation(((AsyncPlanSetup)planSetup)._inner));

        public Task<bool> RemoveLocationAsync(IImage Image, System.Text.StringBuilder errorHint) => _service.PostAsync(context => _inner.RemoveLocation(((AsyncImage)Image)._inner, errorHint));

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ReferencePoint> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ReferencePoint, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.ReferencePoint(AsyncReferencePoint wrapper) => wrapper._inner;
    }
}
