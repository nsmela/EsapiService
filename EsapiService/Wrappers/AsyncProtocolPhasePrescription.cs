namespace EsapiService.Wrappers
{
    public class AsyncProtocolPhasePrescription : IProtocolPhasePrescription
    {
        internal readonly VMS.TPS.Common.Model.API.ProtocolPhasePrescription _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncProtocolPhasePrescription(VMS.TPS.Common.Model.API.ProtocolPhasePrescription inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            TargetTotalDose = inner.TargetTotalDose;
            TargetFractionDose = inner.TargetFractionDose;
            ActualTotalDose = inner.ActualTotalDose;
            PrescModifier = inner.PrescModifier;
            PrescParameter = inner.PrescParameter;
            PrescType = inner.PrescType;
            StructureId = inner.StructureId;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public VMS.TPS.Common.Model.Types.DoseValue TargetTotalDose { get; }
        public VMS.TPS.Common.Model.Types.DoseValue TargetFractionDose { get; }
        public VMS.TPS.Common.Model.Types.DoseValue ActualTotalDose { get; }
        public System.Collections.Generic.IReadOnlyList<bool> TargetIsMet => _inner.TargetIsMet?.ToList();
        public VMS.TPS.Common.Model.Types.PrescriptionModifier PrescModifier { get; }
        public double PrescParameter { get; }
        public VMS.TPS.Common.Model.Types.PrescriptionType PrescType { get; }
        public string StructureId { get; }
    }
}
