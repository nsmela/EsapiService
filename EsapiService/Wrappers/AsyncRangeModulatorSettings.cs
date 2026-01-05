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
    public partial class AsyncRangeModulatorSettings : AsyncSerializableObject, IRangeModulatorSettings, IEsapiWrapper<VMS.TPS.Common.Model.API.RangeModulatorSettings>
    {
        internal new readonly VMS.TPS.Common.Model.API.RangeModulatorSettings _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncRangeModulatorSettings(VMS.TPS.Common.Model.API.RangeModulatorSettings inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public double IsocenterToRangeModulatorDistance =>
            _inner.IsocenterToRangeModulatorDistance;


        public double RangeModulatorGatingStartValue =>
            _inner.RangeModulatorGatingStartValue;


        public double RangeModulatorGatingStarWaterEquivalentThickness =>
            _inner.RangeModulatorGatingStarWaterEquivalentThickness;


        public double RangeModulatorGatingStopValue =>
            _inner.RangeModulatorGatingStopValue;


        public double RangeModulatorGatingStopWaterEquivalentThickness =>
            _inner.RangeModulatorGatingStopWaterEquivalentThickness;


        public async Task<IRangeModulator> GetReferencedRangeModulatorAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.ReferencedRangeModulator is null ? null : new AsyncRangeModulator(_inner.ReferencedRangeModulator, _service);
                return innerResult;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RangeModulatorSettings> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RangeModulatorSettings, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.RangeModulatorSettings(AsyncRangeModulatorSettings wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.RangeModulatorSettings IEsapiWrapper<VMS.TPS.Common.Model.API.RangeModulatorSettings>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.RangeModulatorSettings>.Service => _service;
    }
}
