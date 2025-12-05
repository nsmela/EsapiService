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

        public string BolusFrequency { get; }
        public string BolusThickness { get; }
        public IReadOnlyList<string> Energies => _inner.Energies?.ToList();
        public IReadOnlyList<string> EnergyModes => _inner.EnergyModes?.ToList();
        public string Gating { get; }
        public IRTPrescription LatestRevision => _inner.LatestRevision is null ? null : new AsyncRTPrescription(_inner.LatestRevision, _service);

        public string Notes { get; }
        public IReadOnlyList<int> NumberOfFractions => _inner.NumberOfFractions?.ToList();
        public IReadOnlyList<IRTPrescriptionOrganAtRisk> OrgansAtRisk => _inner.OrgansAtRisk?.Select(x => new AsyncRTPrescriptionOrganAtRisk(x, _service)).ToList();
        public string PhaseType { get; }
        public IRTPrescription PredecessorPrescription => _inner.PredecessorPrescription is null ? null : new AsyncRTPrescription(_inner.PredecessorPrescription, _service);

        public int RevisionNumber { get; }
        public IReadOnlyList<bool> SimulationNeeded => _inner.SimulationNeeded?.ToList();
        public string Site { get; }
        public string Status { get; }
        public IReadOnlyList<IRTPrescriptionTargetConstraints> TargetConstraintsWithoutTargetLevel => _inner.TargetConstraintsWithoutTargetLevel?.Select(x => new AsyncRTPrescriptionTargetConstraints(x, _service)).ToList();
        public IReadOnlyList<IRTPrescriptionTarget> Targets => _inner.Targets?.Select(x => new AsyncRTPrescriptionTarget(x, _service)).ToList();
        public string Technique { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RTPrescription> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RTPrescription, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
