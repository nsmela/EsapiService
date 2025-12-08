using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;

namespace Esapi.Wrappers
{
    public class AsyncRangeShifterSettings : IRangeShifterSettings
    {
        internal readonly VMS.TPS.Common.Model.API.RangeShifterSettings _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncRangeShifterSettings(VMS.TPS.Common.Model.API.RangeShifterSettings inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            IsocenterToRangeShifterDistance = inner.IsocenterToRangeShifterDistance;
            RangeShifterSetting = inner.RangeShifterSetting;
            RangeShifterWaterEquivalentThickness = inner.RangeShifterWaterEquivalentThickness;
        }


        public double IsocenterToRangeShifterDistance { get; }

        public string RangeShifterSetting { get; }

        public double RangeShifterWaterEquivalentThickness { get; }

        public async Task<IRangeShifter> GetReferencedRangeShifterAsync()
        {
            return await _service.RunAsync(() => 
                _inner.ReferencedRangeShifter is null ? null : new AsyncRangeShifter(_inner.ReferencedRangeShifter, _service));
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RangeShifterSettings> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RangeShifterSettings, T> func) => _service.RunAsync(() => func(_inner));
    }
}
