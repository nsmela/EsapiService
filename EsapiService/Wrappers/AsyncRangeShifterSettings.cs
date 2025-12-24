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
    public class AsyncRangeShifterSettings : AsyncSerializableObject, IRangeShifterSettings, IEsapiWrapper<VMS.TPS.Common.Model.API.RangeShifterSettings>
    {
        internal new readonly VMS.TPS.Common.Model.API.RangeShifterSettings _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncRangeShifterSettings(VMS.TPS.Common.Model.API.RangeShifterSettings inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public double IsocenterToRangeShifterDistance =>
            _inner.IsocenterToRangeShifterDistance;


        public string RangeShifterSetting =>
            _inner.RangeShifterSetting;


        public double RangeShifterWaterEquivalentThickness =>
            _inner.RangeShifterWaterEquivalentThickness;


        public async Task<IRangeShifter> GetReferencedRangeShifterAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.ReferencedRangeShifter is null ? null : new AsyncRangeShifter(_inner.ReferencedRangeShifter, _service);
                return innerResult;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RangeShifterSettings> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RangeShifterSettings, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.RangeShifterSettings(AsyncRangeShifterSettings wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.RangeShifterSettings IEsapiWrapper<VMS.TPS.Common.Model.API.RangeShifterSettings>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.RangeShifterSettings>.Service => _service;
    }
}
