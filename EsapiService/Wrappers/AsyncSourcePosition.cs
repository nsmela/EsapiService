    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    public class AsyncSourcePosition : ISourcePosition
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
            Transform = inner.Transform;
            Translation = inner.Translation;
        }

        public double DwellTime { get; }
        public IReadOnlyList<bool> DwellTimeLock => _inner.DwellTimeLock?.ToList();
        public double NominalDwellTime => _inner.NominalDwellTime;
        public async Task SetNominalDwellTimeAsync(double value) => _service.RunAsync(() => _inner.NominalDwellTime = value);
        public IRadioactiveSource RadioactiveSource => _inner.RadioactiveSource is null ? null : new AsyncRadioactiveSource(_inner.RadioactiveSource, _service);

        public double[,] Transform { get; }
        public VVector Translation { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.SourcePosition> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.SourcePosition, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
