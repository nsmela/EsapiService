    using System.Threading.Tasks;
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
            TransmissionFactor = inner.TransmissionFactor;
            TrayTransmissionFactor = inner.TrayTransmissionFactor;
            Type = inner.Type;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public IAddOnMaterial AddOnMaterial => _inner.AddOnMaterial is null ? null : new AsyncAddOnMaterial(_inner.AddOnMaterial, _service);

        public bool IsDiverging { get; }
        public System.Windows.Point[][] Outline => _inner.Outline;
        public async Task SetOutlineAsync(System.Windows.Point[][] value) => _service.RunAsync(() => _inner.Outline = value);
        public double TransmissionFactor { get; }
        public ITray Tray => _inner.Tray is null ? null : new AsyncTray(_inner.Tray, _service);

        public double TrayTransmissionFactor { get; }
        public VMS.TPS.Common.Model.Types.BlockType Type { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Block> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Block, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
