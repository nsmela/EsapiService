namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
    public class AsyncRTPrescription : IRTPrescription
    {
        internal readonly VMS.TPS.Common.Model.API.RTPrescription _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncRTPrescription(VMS.TPS.Common.Model.API.RTPrescription inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            BolusFrequency = inner.BolusFrequency;
            BolusThickness = inner.BolusThickness;
            Gating = inner.Gating;
            Notes = inner.Notes;
            PhaseType = inner.PhaseType;
            RevisionNumber = inner.RevisionNumber;
            Site = inner.Site;
            Status = inner.Status;
            Technique = inner.Technique;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public string BolusFrequency { get; }
        public string BolusThickness { get; }
        public System.Collections.Generic.IReadOnlyList<string> Energies => _inner.Energies?.ToList();
        public System.Collections.Generic.IReadOnlyList<string> EnergyModes => _inner.EnergyModes?.ToList();
        public string Gating { get; }
        public IRTPrescription LatestRevision => _inner.LatestRevision is null ? null : new AsyncRTPrescription(_inner.LatestRevision, _service);

        public string Notes { get; }
        public System.Collections.Generic.IReadOnlyList<int> NumberOfFractions => _inner.NumberOfFractions?.ToList();
        public System.Collections.Generic.IReadOnlyList<IRTPrescriptionOrganAtRisk> OrgansAtRisk => _inner.OrgansAtRisk?.Select(x => new AsyncRTPrescriptionOrganAtRisk(x, _service)).ToList();
        public string PhaseType { get; }
        public IRTPrescription PredecessorPrescription => _inner.PredecessorPrescription is null ? null : new AsyncRTPrescription(_inner.PredecessorPrescription, _service);

        public int RevisionNumber { get; }
        public System.Collections.Generic.IReadOnlyList<bool> SimulationNeeded => _inner.SimulationNeeded?.ToList();
        public string Site { get; }
        public string Status { get; }
        public System.Collections.Generic.IReadOnlyList<IRTPrescriptionTargetConstraints> TargetConstraintsWithoutTargetLevel => _inner.TargetConstraintsWithoutTargetLevel?.Select(x => new AsyncRTPrescriptionTargetConstraints(x, _service)).ToList();
        public System.Collections.Generic.IReadOnlyList<IRTPrescriptionTarget> Targets => _inner.Targets?.Select(x => new AsyncRTPrescriptionTarget(x, _service)).ToList();
        public string Technique { get; }
    }
}
