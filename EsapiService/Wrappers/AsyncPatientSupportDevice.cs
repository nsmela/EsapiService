namespace EsapiService.Wrappers
{
    public class AsyncPatientSupportDevice : IPatientSupportDevice
    {
        internal readonly VMS.TPS.Common.Model.API.PatientSupportDevice _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncPatientSupportDevice(VMS.TPS.Common.Model.API.PatientSupportDevice inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            PatientSupportAccessoryCode = inner.PatientSupportAccessoryCode;
            PatientSupportDeviceType = inner.PatientSupportDeviceType;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public string PatientSupportAccessoryCode { get; }
        public string PatientSupportDeviceType { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PatientSupportDevice> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PatientSupportDevice, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
