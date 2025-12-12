using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncRTPrescription : AsyncApiDataObject, IRTPrescription, IEsapiWrapper<VMS.TPS.Common.Model.API.RTPrescription>
    {
        internal new readonly VMS.TPS.Common.Model.API.RTPrescription _inner;

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
            Energies = inner.Energies;
            EnergyModes = inner.EnergyModes;
            Gating = inner.Gating;
            Notes = inner.Notes;
            NumberOfFractions = inner.NumberOfFractions;
            OrgansAtRisk = inner.OrgansAtRisk;
            PhaseType = inner.PhaseType;
            RevisionNumber = inner.RevisionNumber;
            SimulationNeeded = inner.SimulationNeeded;
            Site = inner.Site;
            Status = inner.Status;
            TargetConstraintsWithoutTargetLevel = inner.TargetConstraintsWithoutTargetLevel;
            Targets = inner.Targets;
            Technique = inner.Technique;
        }

        public string BolusFrequency { get; }

        public string BolusThickness { get; }

        public IEnumerable<string> Energies { get; }

        public IEnumerable<string> EnergyModes { get; }

        public string Gating { get; }

        public async Task<IRTPrescription> GetLatestRevisionAsync()
        {
            return await _service.PostAsync(context => 
                _inner.LatestRevision is null ? null : new AsyncRTPrescription(_inner.LatestRevision, _service));
        }

        public string Notes { get; }

        public int? NumberOfFractions { get; }

        public IEnumerable<RTPrescriptionOrganAtRisk> OrgansAtRisk { get; }

        public string PhaseType { get; }

        public async Task<IRTPrescription> GetPredecessorPrescriptionAsync()
        {
            return await _service.PostAsync(context => 
                _inner.PredecessorPrescription is null ? null : new AsyncRTPrescription(_inner.PredecessorPrescription, _service));
        }

        public int RevisionNumber { get; }

        public bool? SimulationNeeded { get; }

        public string Site { get; }

        public string Status { get; }

        public IEnumerable<RTPrescriptionTargetConstraints> TargetConstraintsWithoutTargetLevel { get; }

        public IEnumerable<RTPrescriptionTarget> Targets { get; }

        public string Technique { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RTPrescription> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RTPrescription, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.RTPrescription(AsyncRTPrescription wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.RTPrescription IEsapiWrapper<VMS.TPS.Common.Model.API.RTPrescription>.Inner => _inner;
    }
}
