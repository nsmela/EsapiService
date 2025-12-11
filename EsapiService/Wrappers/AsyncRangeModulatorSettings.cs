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
    public class AsyncRangeModulatorSettings : AsyncSerializableObject, IRangeModulatorSettings, IEsapiWrapper<VMS.TPS.Common.Model.API.RangeModulatorSettings>
    {
        internal new readonly VMS.TPS.Common.Model.API.RangeModulatorSettings _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncRangeModulatorSettings(VMS.TPS.Common.Model.API.RangeModulatorSettings inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            IsocenterToRangeModulatorDistance = inner.IsocenterToRangeModulatorDistance;
            RangeModulatorGatingStartValue = inner.RangeModulatorGatingStartValue;
            RangeModulatorGatingStarWaterEquivalentThickness = inner.RangeModulatorGatingStarWaterEquivalentThickness;
            RangeModulatorGatingStopValue = inner.RangeModulatorGatingStopValue;
            RangeModulatorGatingStopWaterEquivalentThickness = inner.RangeModulatorGatingStopWaterEquivalentThickness;
        }

        public double IsocenterToRangeModulatorDistance { get; }

        public double RangeModulatorGatingStartValue { get; }

        public double RangeModulatorGatingStarWaterEquivalentThickness { get; }

        public double RangeModulatorGatingStopValue { get; }

        public double RangeModulatorGatingStopWaterEquivalentThickness { get; }

        public async Task<IRangeModulator> GetReferencedRangeModulatorAsync()
        {
            return await _service.PostAsync(context => 
                _inner.ReferencedRangeModulator is null ? null : new AsyncRangeModulator(_inner.ReferencedRangeModulator, _service));
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RangeModulatorSettings> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RangeModulatorSettings, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.RangeModulatorSettings(AsyncRangeModulatorSettings wrapper) => wrapper._inner;
        // Internal Explicit Implementation to expose _inner safely
        VMS.TPS.Common.Model.API.RangeModulatorSettings IEsapiWrapper<VMS.TPS.Common.Model.API.RangeModulatorSettings>.Inner => _inner;
    }
}
