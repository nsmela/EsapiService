namespace EsapiService.Wrappers
{
    public class AsyncLateralSpreadingDevice : ILateralSpreadingDevice
    {
        internal readonly VMS.TPS.Common.Model.API.LateralSpreadingDevice _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncLateralSpreadingDevice(VMS.TPS.Common.Model.API.LateralSpreadingDevice inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Type = inner.Type;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public VMS.TPS.Common.Model.Types.LateralSpreadingDeviceType Type { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.LateralSpreadingDevice> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.LateralSpreadingDevice, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
