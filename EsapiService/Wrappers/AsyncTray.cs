namespace EsapiService.Wrappers
{
    public class AsyncTray : ITray
    {
        internal readonly VMS.TPS.Common.Model.API.Tray _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncTray(VMS.TPS.Common.Model.API.Tray inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Tray> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Tray, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
