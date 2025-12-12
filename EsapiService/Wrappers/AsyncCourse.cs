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
    public class AsyncCourse : AsyncApiDataObject, ICourse, IEsapiWrapper<VMS.TPS.Common.Model.API.Course>
    {
        internal new readonly VMS.TPS.Common.Model.API.Course _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncCourse(VMS.TPS.Common.Model.API.Course inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            ExternalPlanSetups = inner.ExternalPlanSetups;
            BrachyPlanSetups = inner.BrachyPlanSetups;
            IonPlanSetups = inner.IonPlanSetups;
            CompletedDateTime = inner.CompletedDateTime;
            Diagnoses = inner.Diagnoses;
            Intent = inner.Intent;
            PlanSetups = inner.PlanSetups;
            PlanSums = inner.PlanSums;
            StartDateTime = inner.StartDateTime;
            TreatmentPhases = inner.TreatmentPhases;
            TreatmentSessions = inner.TreatmentSessions;
        }

        public async Task<IPlanSum> CreatePlanSumAsync(IReadOnlyList<IPlanningItem> planningItems, IImage image)
        {
            return await _service.PostAsync(context => 
                _inner.CreatePlanSum((planningItems.Select(x => ((AsyncPlanningItem)x)._inner)), ((AsyncImage)image)._inner) is var result && result is null ? null : new AsyncPlanSum(result, _service));
        }


        public async Task<IExternalPlanSetup> AddExternalPlanSetupAsync(IStructureSet structureSet, IStructure targetStructure, IReferencePoint primaryReferencePoint, IReadOnlyList<IReferencePoint> additionalReferencePoints)
        {
            return await _service.PostAsync(context => 
                _inner.AddExternalPlanSetup(((AsyncStructureSet)structureSet)._inner, ((AsyncStructure)targetStructure)._inner, ((AsyncReferencePoint)primaryReferencePoint)._inner, (additionalReferencePoints.Select(x => ((AsyncReferencePoint)x)._inner))) is var result && result is null ? null : new AsyncExternalPlanSetup(result, _service));
        }


        public async Task<IIonPlanSetup> AddIonPlanSetupAsync(IStructureSet structureSet, IStructure targetStructure, IReferencePoint primaryReferencePoint, string patientSupportDeviceId, IReadOnlyList<IReferencePoint> additionalReferencePoints)
        {
            return await _service.PostAsync(context => 
                _inner.AddIonPlanSetup(((AsyncStructureSet)structureSet)._inner, ((AsyncStructure)targetStructure)._inner, ((AsyncReferencePoint)primaryReferencePoint)._inner, patientSupportDeviceId, (additionalReferencePoints.Select(x => ((AsyncReferencePoint)x)._inner))) is var result && result is null ? null : new AsyncIonPlanSetup(result, _service));
        }


        public async Task<IExternalPlanSetup> AddExternalPlanSetupAsync(IStructureSet structureSet)
        {
            return await _service.PostAsync(context => 
                _inner.AddExternalPlanSetup(((AsyncStructureSet)structureSet)._inner) is var result && result is null ? null : new AsyncExternalPlanSetup(result, _service));
        }


        public async Task<IExternalPlanSetup> AddExternalPlanSetupAsVerificationPlanAsync(IStructureSet structureSet, IExternalPlanSetup verifiedPlan)
        {
            return await _service.PostAsync(context => 
                _inner.AddExternalPlanSetupAsVerificationPlan(((AsyncStructureSet)structureSet)._inner, ((AsyncExternalPlanSetup)verifiedPlan)._inner) is var result && result is null ? null : new AsyncExternalPlanSetup(result, _service));
        }


        public async Task<IIonPlanSetup> AddIonPlanSetupAsync(IStructureSet structureSet, string patientSupportDeviceId)
        {
            return await _service.PostAsync(context => 
                _inner.AddIonPlanSetup(((AsyncStructureSet)structureSet)._inner, patientSupportDeviceId) is var result && result is null ? null : new AsyncIonPlanSetup(result, _service));
        }


        public async Task<IIonPlanSetup> AddIonPlanSetupAsVerificationPlanAsync(IStructureSet structureSet, string patientSupportDeviceId, IIonPlanSetup verifiedPlan)
        {
            return await _service.PostAsync(context => 
                _inner.AddIonPlanSetupAsVerificationPlan(((AsyncStructureSet)structureSet)._inner, patientSupportDeviceId, ((AsyncIonPlanSetup)verifiedPlan)._inner) is var result && result is null ? null : new AsyncIonPlanSetup(result, _service));
        }


        // Simple Method
        public Task<bool> CanAddPlanSetupAsync(IStructureSet structureSet) => _service.PostAsync(context => _inner.CanAddPlanSetup(((AsyncStructureSet)structureSet)._inner));

        // Simple Method
        public Task<bool> CanRemovePlanSetupAsync(IPlanSetup planSetup) => _service.PostAsync(context => _inner.CanRemovePlanSetup(((AsyncPlanSetup)planSetup)._inner));

        public async Task<IBrachyPlanSetup> CopyBrachyPlanSetupAsync(IBrachyPlanSetup sourcePlan, System.Text.StringBuilder outputDiagnostics)
        {
            return await _service.PostAsync(context => 
                _inner.CopyBrachyPlanSetup(((AsyncBrachyPlanSetup)sourcePlan)._inner, outputDiagnostics) is var result && result is null ? null : new AsyncBrachyPlanSetup(result, _service));
        }


        public async Task<IBrachyPlanSetup> CopyBrachyPlanSetupAsync(IBrachyPlanSetup sourcePlan, IStructureSet structureset, System.Text.StringBuilder outputDiagnostics)
        {
            return await _service.PostAsync(context => 
                _inner.CopyBrachyPlanSetup(((AsyncBrachyPlanSetup)sourcePlan)._inner, ((AsyncStructureSet)structureset)._inner, outputDiagnostics) is var result && result is null ? null : new AsyncBrachyPlanSetup(result, _service));
        }


        public async Task<IPlanSetup> CopyPlanSetupAsync(IPlanSetup sourcePlan)
        {
            return await _service.PostAsync(context => 
                _inner.CopyPlanSetup(((AsyncPlanSetup)sourcePlan)._inner) is var result && result is null ? null : new AsyncPlanSetup(result, _service));
        }


        public async Task<IPlanSetup> CopyPlanSetupAsync(IPlanSetup sourcePlan, IImage targetImage, System.Text.StringBuilder outputDiagnostics)
        {
            return await _service.PostAsync(context => 
                _inner.CopyPlanSetup(((AsyncPlanSetup)sourcePlan)._inner, ((AsyncImage)targetImage)._inner, outputDiagnostics) is var result && result is null ? null : new AsyncPlanSetup(result, _service));
        }


        public async Task<IPlanSetup> CopyPlanSetupAsync(IPlanSetup sourcePlan, IImage targetImage, IRegistration registration, System.Text.StringBuilder outputDiagnostics)
        {
            return await _service.PostAsync(context => 
                _inner.CopyPlanSetup(((AsyncPlanSetup)sourcePlan)._inner, ((AsyncImage)targetImage)._inner, ((AsyncRegistration)registration)._inner, outputDiagnostics) is var result && result is null ? null : new AsyncPlanSetup(result, _service));
        }


        public async Task<IPlanSetup> CopyPlanSetupAsync(IPlanSetup sourcePlan, IStructureSet structureset, System.Text.StringBuilder outputDiagnostics)
        {
            return await _service.PostAsync(context => 
                _inner.CopyPlanSetup(((AsyncPlanSetup)sourcePlan)._inner, ((AsyncStructureSet)structureset)._inner, outputDiagnostics) is var result && result is null ? null : new AsyncPlanSetup(result, _service));
        }


        // Simple Method
        public Task<bool> IsCompletedAsync() => _service.PostAsync(context => _inner.IsCompleted());

        // Simple Void Method
        public Task RemovePlanSetupAsync(IPlanSetup planSetup) => _service.PostAsync(context => _inner.RemovePlanSetup(((AsyncPlanSetup)planSetup)._inner));

        // Simple Void Method
        public Task RemovePlanSumAsync(IPlanSum planSum) => _service.PostAsync(context => _inner.RemovePlanSum(((AsyncPlanSum)planSum)._inner));

        public IEnumerable<ExternalPlanSetup> ExternalPlanSetups { get; }

        public IEnumerable<BrachyPlanSetup> BrachyPlanSetups { get; }

        public IEnumerable<IonPlanSetup> IonPlanSetups { get; }

        public DateTime? CompletedDateTime { get; }

        public IEnumerable<Diagnosis> Diagnoses { get; }

        public string Intent { get; }

        public async Task<IPatient> GetPatientAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Patient is null ? null : new AsyncPatient(_inner.Patient, _service));
        }

        public IEnumerable<PlanSetup> PlanSetups { get; }

        public IEnumerable<PlanSum> PlanSums { get; }

        public DateTime? StartDateTime { get; }

        public IEnumerable<TreatmentPhase> TreatmentPhases { get; }

        public IEnumerable<TreatmentSession> TreatmentSessions { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Course> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Course, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.Course(AsyncCourse wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Course IEsapiWrapper<VMS.TPS.Common.Model.API.Course>.Inner => _inner;
    }
}
