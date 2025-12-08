using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncRTPrescription : AsyncApiDataObject, IRTPrescription
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
            NumberOfFractions = inner.NumberOfFractions;
            PhaseType = inner.PhaseType;
            RevisionNumber = inner.RevisionNumber;
            SimulationNeeded = inner.SimulationNeeded;
            Site = inner.Site;
            Status = inner.Status;
            Technique = inner.Technique;
        }


        public string BolusFrequency { get; }

        public string BolusThickness { get; }

        public async Task<IReadOnlyList<string>> GetEnergiesAsync()
        {
            return await _service.RunAsync(() => _inner.Energies?.ToList());
        }


        public async Task<IReadOnlyList<string>> GetEnergyModesAsync()
        {
            return await _service.RunAsync(() => _inner.EnergyModes?.ToList());
        }


        public string Gating { get; }

        public async Task<IRTPrescription> GetLatestRevisionAsync()
        {
            return await _service.RunAsync(() => 
                _inner.LatestRevision is null ? null : new AsyncRTPrescription(_inner.LatestRevision, _service));
        }

        public string Notes { get; }

        public int? NumberOfFractions { get; }

        public async Task<IReadOnlyList<IRTPrescriptionOrganAtRisk>> GetOrgansAtRiskAsync()
        {
            return await _service.RunAsync(() => 
                _inner.OrgansAtRisk?.Select(x => new AsyncRTPrescriptionOrganAtRisk(x, _service)).ToList());
        }


        public string PhaseType { get; }

        public async Task<IRTPrescription> GetPredecessorPrescriptionAsync()
        {
            return await _service.RunAsync(() => 
                _inner.PredecessorPrescription is null ? null : new AsyncRTPrescription(_inner.PredecessorPrescription, _service));
        }

        public int RevisionNumber { get; }

        public bool? SimulationNeeded { get; }

        public string Site { get; }

        public string Status { get; }

        public async Task<IReadOnlyList<IRTPrescriptionTargetConstraints>> GetTargetConstraintsWithoutTargetLevelAsync()
        {
            return await _service.RunAsync(() => 
                _inner.TargetConstraintsWithoutTargetLevel?.Select(x => new AsyncRTPrescriptionTargetConstraints(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IRTPrescriptionTarget>> GetTargetsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Targets?.Select(x => new AsyncRTPrescriptionTarget(x, _service)).ToList());
        }


        public string Technique { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RTPrescription> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RTPrescription, T> func) => _service.RunAsync(() => func(_inner));
    }
}
