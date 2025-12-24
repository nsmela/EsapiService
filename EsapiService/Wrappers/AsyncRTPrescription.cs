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
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public string BolusFrequency =>
            _inner.BolusFrequency;


        public string BolusThickness =>
            _inner.BolusThickness;


        public string Gating =>
            _inner.Gating;


        public async Task<IRTPrescription> GetLatestRevisionAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.LatestRevision is null ? null : new AsyncRTPrescription(_inner.LatestRevision, _service);
                return innerResult;
            });
        }

        public string Notes =>
            _inner.Notes;


        public int? NumberOfFractions =>
            _inner.NumberOfFractions;


        public async Task<IReadOnlyList<IRTPrescriptionOrganAtRisk>> GetOrgansAtRiskAsync()
        {
            return await _service.PostAsync(context => 
                _inner.OrgansAtRisk?.Select(x => new AsyncRTPrescriptionOrganAtRisk(x, _service)).ToList());
        }


        public string PhaseType =>
            _inner.PhaseType;


        public async Task<IRTPrescription> GetPredecessorPrescriptionAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.PredecessorPrescription is null ? null : new AsyncRTPrescription(_inner.PredecessorPrescription, _service);
                return innerResult;
            });
        }

        public int RevisionNumber =>
            _inner.RevisionNumber;


        public bool? SimulationNeeded =>
            _inner.SimulationNeeded;


        public string Site =>
            _inner.Site;


        public string Status =>
            _inner.Status;


        public async Task<IReadOnlyList<IRTPrescriptionTargetConstraints>> GetTargetConstraintsWithoutTargetLevelAsync()
        {
            return await _service.PostAsync(context => 
                _inner.TargetConstraintsWithoutTargetLevel?.Select(x => new AsyncRTPrescriptionTargetConstraints(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IRTPrescriptionTarget>> GetTargetsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Targets?.Select(x => new AsyncRTPrescriptionTarget(x, _service)).ToList());
        }


        public string Technique =>
            _inner.Technique;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RTPrescription> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RTPrescription, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.RTPrescription(AsyncRTPrescription wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.RTPrescription IEsapiWrapper<VMS.TPS.Common.Model.API.RTPrescription>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.RTPrescription>.Service => _service;

        /* --- Skipped Members (Not generated) ---
           - Energies: No matching factory found (Not Implemented)
           - EnergyModes: No matching factory found (Not Implemented)
        */
    }
}
