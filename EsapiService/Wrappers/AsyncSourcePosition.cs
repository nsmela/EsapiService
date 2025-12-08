using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncSourcePosition : AsyncApiDataObject, ISourcePosition
    {
        internal readonly VMS.TPS.Common.Model.API.SourcePosition _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncSourcePosition(VMS.TPS.Common.Model.API.SourcePosition inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            DwellTime = inner.DwellTime;
            DwellTimeLock = inner.DwellTimeLock;
            NominalDwellTime = inner.NominalDwellTime;
            Transform = inner.Transform;
            Translation = inner.Translation;
        }


        public double DwellTime { get; }

        public bool? DwellTimeLock { get; private set; }
        public async Task SetDwellTimeLockAsync(bool? value)
        {
            DwellTimeLock = await _service.RunAsync(() =>
            {
                _inner.DwellTimeLock = value;
                return _inner.DwellTimeLock;
            });
        }

        public double NominalDwellTime { get; private set; }
        public async Task SetNominalDwellTimeAsync(double value)
        {
            NominalDwellTime = await _service.RunAsync(() =>
            {
                _inner.NominalDwellTime = value;
                return _inner.NominalDwellTime;
            });
        }

        public async Task<IRadioactiveSource> GetRadioactiveSourceAsync()
        {
            return await _service.RunAsync(() => 
                _inner.RadioactiveSource is null ? null : new AsyncRadioactiveSource(_inner.RadioactiveSource, _service));
        }

        public double[,] Transform { get; }

        public VVector Translation { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.SourcePosition> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.SourcePosition, T> func) => _service.RunAsync(() => func(_inner));
    }
}
