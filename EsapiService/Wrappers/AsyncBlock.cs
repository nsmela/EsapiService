using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncBlock : IBlock
    {
        internal readonly VMS.TPS.Common.Model.API.Block _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncBlock(VMS.TPS.Common.Model.API.Block inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            IsDiverging = inner.IsDiverging;
            Outline = inner.Outline;
            TransmissionFactor = inner.TransmissionFactor;
            TrayTransmissionFactor = inner.TrayTransmissionFactor;
            Type = inner.Type;
        }

        public async Task<IAddOnMaterial> GetAddOnMaterialAsync()
        {
            return await _service.RunAsync(() => 
                _inner.AddOnMaterial is null ? null : new AsyncAddOnMaterial(_inner.AddOnMaterial, _service));
        }
        public bool IsDiverging { get; }
        public Windows.Point[][] Outline { get; private set; }
        public async Task SetOutlineAsync(Windows.Point[][] value)
        {
            Outline = await _service.RunAsync(() =>
            {
                _inner.Outline = value;
                return _inner.Outline;
            });
        }
        public double TransmissionFactor { get; }
        public async Task<ITray> GetTrayAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Tray is null ? null : new AsyncTray(_inner.Tray, _service));
        }
        public double TrayTransmissionFactor { get; }
        public BlockType Type { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Block> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Block, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
