using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncTreatmentUnitOperatingLimit : ITreatmentUnitOperatingLimit
    {
        internal readonly VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimit _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncTreatmentUnitOperatingLimit(VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimit inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Label = inner.Label;
            MaxValue = inner.MaxValue;
            MinValue = inner.MinValue;
            UnitString = inner.UnitString;
        }

        public string Label { get; }
        public double MaxValue { get; }
        public double MinValue { get; }
        public async Task<IReadOnlyList<int>> GetPrecisionAsync()
        {
            return await _service.RunAsync(() => _inner.Precision?.ToList());
        }

        public string UnitString { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimit> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimit, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
