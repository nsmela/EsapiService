    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
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

            SnoutId = inner.SnoutId;
            SnoutPosition = inner.SnoutPosition;
        }

        public System.Collections.Generic.IReadOnlyList<IIonControlPointParameters> ControlPoints => _inner.ControlPoints?.Select(x => new AsyncIonControlPointParameters(x, _service)).ToList();
        public string PreSelectedRangeShifter1Id => _inner.PreSelectedRangeShifter1Id;
        public async Task SetPreSelectedRangeShifter1IdAsync(string value) => _service.RunAsync(() => _inner.PreSelectedRangeShifter1Id = value);
        public string PreSelectedRangeShifter1Setting => _inner.PreSelectedRangeShifter1Setting;
        public async Task SetPreSelectedRangeShifter1SettingAsync(string value) => _service.RunAsync(() => _inner.PreSelectedRangeShifter1Setting = value);
        public string PreSelectedRangeShifter2Id => _inner.PreSelectedRangeShifter2Id;
        public async Task SetPreSelectedRangeShifter2IdAsync(string value) => _service.RunAsync(() => _inner.PreSelectedRangeShifter2Id = value);
        public string PreSelectedRangeShifter2Setting => _inner.PreSelectedRangeShifter2Setting;
        public async Task SetPreSelectedRangeShifter2SettingAsync(string value) => _service.RunAsync(() => _inner.PreSelectedRangeShifter2Setting = value);
        public IIonControlPointPairCollection IonControlPointPairs => _inner.IonControlPointPairs is null ? null : new AsyncIonControlPointPairCollection(_inner.IonControlPointPairs, _service);

        public string SnoutId { get; }
        public double SnoutPosition { get; }
        public IStructure TargetStructure => _inner.TargetStructure is null ? null : new AsyncStructure(_inner.TargetStructure, _service);
        public System.Threading.Tasks.Task SetTargetStructureAsync(IStructure value)
        {
            // Unwrap the interface to get the Varian object
            if (value is AsyncStructure wrapper)
            {
                 return _service.RunAsync(() => _inner.TargetStructure = wrapper._inner);
            }
            throw new System.ArgumentException("Value must be of type AsyncStructure");
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonBeamParameters> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonBeamParameters, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
