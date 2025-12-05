namespace EsapiService.Wrappers
{
    public class AsyncExternalBeamTreatmentUnit : IExternalBeamTreatmentUnit
    {
        internal readonly VMS.TPS.Common.Model.API.ExternalBeamTreatmentUnit _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncExternalBeamTreatmentUnit(VMS.TPS.Common.Model.API.ExternalBeamTreatmentUnit inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            MachineDepartmentName = inner.MachineDepartmentName;
            MachineModel = inner.MachineModel;
            MachineModelName = inner.MachineModelName;
            MachineScaleDisplayName = inner.MachineScaleDisplayName;
            SourceAxisDistance = inner.SourceAxisDistance;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public string MachineDepartmentName { get; }
        public string MachineModel { get; }
        public string MachineModelName { get; }
        public string MachineScaleDisplayName { get; }
        public ITreatmentUnitOperatingLimits OperatingLimits => _inner.OperatingLimits is null ? null : new AsyncTreatmentUnitOperatingLimits(_inner.OperatingLimits, _service);

        public double SourceAxisDistance { get; }
    }
}
