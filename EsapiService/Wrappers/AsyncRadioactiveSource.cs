using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncRadioactiveSource : IRadioactiveSource
    {
        internal readonly VMS.TPS.Common.Model.API.RadioactiveSource _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncRadioactiveSource(VMS.TPS.Common.Model.API.RadioactiveSource inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            NominalActivity = inner.NominalActivity;
            SerialNumber = inner.SerialNumber;
            Strength = inner.Strength;
        }

        public async Task<IReadOnlyList<DateTime>> GetCalibrationDateAsync()
        {
            return await _service.RunAsync(() => _inner.CalibrationDate?.ToList());
        }

        public bool NominalActivity { get; }
        public async Task<IRadioactiveSourceModel> GetRadioactiveSourceModelAsync()
        {
            return await _service.RunAsync(() => 
                _inner.RadioactiveSourceModel is null ? null : new AsyncRadioactiveSourceModel(_inner.RadioactiveSourceModel, _service));
        }
        public string SerialNumber { get; }
        public double Strength { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RadioactiveSource> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RadioactiveSource, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
