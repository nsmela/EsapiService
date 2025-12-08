using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncIonBeamParameters : IIonBeamParameters
    {
        internal readonly VMS.TPS.Common.Model.API.IonBeamParameters _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncIonBeamParameters(VMS.TPS.Common.Model.API.IonBeamParameters inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            PreSelectedRangeShifter1Id = inner.PreSelectedRangeShifter1Id;
            PreSelectedRangeShifter1Setting = inner.PreSelectedRangeShifter1Setting;
            PreSelectedRangeShifter2Id = inner.PreSelectedRangeShifter2Id;
            PreSelectedRangeShifter2Setting = inner.PreSelectedRangeShifter2Setting;
            SnoutId = inner.SnoutId;
            SnoutPosition = inner.SnoutPosition;
        }

        public string PreSelectedRangeShifter1Id { get; private set; }
        public async Task SetPreSelectedRangeShifter1IdAsync(string value)
        {
            PreSelectedRangeShifter1Id = await _service.RunAsync(() =>
            {
                _inner.PreSelectedRangeShifter1Id = value;
                return _inner.PreSelectedRangeShifter1Id;
            });
        }
        public string PreSelectedRangeShifter1Setting { get; private set; }
        public async Task SetPreSelectedRangeShifter1SettingAsync(string value)
        {
            PreSelectedRangeShifter1Setting = await _service.RunAsync(() =>
            {
                _inner.PreSelectedRangeShifter1Setting = value;
                return _inner.PreSelectedRangeShifter1Setting;
            });
        }
        public string PreSelectedRangeShifter2Id { get; private set; }
        public async Task SetPreSelectedRangeShifter2IdAsync(string value)
        {
            PreSelectedRangeShifter2Id = await _service.RunAsync(() =>
            {
                _inner.PreSelectedRangeShifter2Id = value;
                return _inner.PreSelectedRangeShifter2Id;
            });
        }
        public string PreSelectedRangeShifter2Setting { get; private set; }
        public async Task SetPreSelectedRangeShifter2SettingAsync(string value)
        {
            PreSelectedRangeShifter2Setting = await _service.RunAsync(() =>
            {
                _inner.PreSelectedRangeShifter2Setting = value;
                return _inner.PreSelectedRangeShifter2Setting;
            });
        }
        public async Task<IIonControlPointPairCollection> GetIonControlPointPairsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.IonControlPointPairs is null ? null : new AsyncIonControlPointPairCollection(_inner.IonControlPointPairs, _service));
        }
        public string SnoutId { get; }
        public double SnoutPosition { get; }
        public async Task<IStructure> GetTargetStructureAsync()
        {
            return await _service.RunAsync(() => 
                _inner.TargetStructure is null ? null : new AsyncStructure(_inner.TargetStructure, _service));
        }
        public async Task SetTargetStructureAsync(IStructure value)
        {
            // Handle null assignment
            if (value is null)
            {
                await _service.RunAsync(() => _inner.TargetStructure = null);
                return;
            }
            // Unwrap the interface to get the Varian object
            if (value is AsyncStructure wrapper)
            {
                 await _service.RunAsync(() => _inner.TargetStructure = wrapper._inner);
                 return;
            }
            throw new System.ArgumentException("Value must be of type AsyncStructure");
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonBeamParameters> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonBeamParameters, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
