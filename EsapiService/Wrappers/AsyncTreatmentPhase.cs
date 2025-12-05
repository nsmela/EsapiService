namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
    public class AsyncTreatmentPhase : ITreatmentPhase
    {
        internal readonly VMS.TPS.Common.Model.API.TreatmentPhase _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncTreatmentPhase(VMS.TPS.Common.Model.API.TreatmentPhase inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            OtherInfo = inner.OtherInfo;
            PhaseGapNumberOfDays = inner.PhaseGapNumberOfDays;
            TimeGapType = inner.TimeGapType;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public string OtherInfo { get; }
        public int PhaseGapNumberOfDays { get; }
        public System.Collections.Generic.IReadOnlyList<IRTPrescription> Prescriptions => _inner.Prescriptions?.Select(x => new AsyncRTPrescription(x, _service)).ToList();
        public string TimeGapType { get; }
    }
}
