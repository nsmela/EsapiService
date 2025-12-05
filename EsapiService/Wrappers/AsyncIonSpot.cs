namespace EsapiService.Wrappers
{
    public class AsyncIonSpot : IIonSpot
    {
        internal readonly VMS.TPS.Common.Model.API.IonSpot _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncIonSpot(VMS.TPS.Common.Model.API.IonSpot inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Position = inner.Position;
            Weight = inner.Weight;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public VMS.TPS.Common.Model.Types.VVector Position { get; }
        public float Weight { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonSpot> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonSpot, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
