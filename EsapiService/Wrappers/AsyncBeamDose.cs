namespace EsapiService.Wrappers
{
    public class AsyncBeamDose : IBeamDose
    {
        internal readonly VMS.TPS.Common.Model.API.BeamDose _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncBeamDose(VMS.TPS.Common.Model.API.BeamDose inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public VMS.TPS.Common.Model.Types.DoseValue GetAbsoluteBeamDoseValue(VMS.TPS.Common.Model.Types.DoseValue relative) => _inner.GetAbsoluteBeamDoseValue(relative);

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BeamDose> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BeamDose, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
