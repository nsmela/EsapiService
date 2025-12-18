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
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            ClinicalStatus = inner.ClinicalStatus;
            CompletedDateTime = inner.CompletedDateTime;
            Intent = inner.Intent;
            StartDateTime = inner.StartDateTime;
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


        public async Task<IBrachyPlanSetup> AddBrachyPlanSetupAsync(IStructureSet structureSet, IStructure targetStructure, IReferencePoint primaryReferencePoint, DoseValue dosePerFraction, BrachyTreatmentTechniqueType brachyTreatmentTechnique, IReadOnlyList<IReferencePoint> additionalReferencePoints)
        {
            return await _service.PostAsync(context => 
                _inner.AddBrachyPlanSetup(((AsyncStructureSet)structureSet)._inner, ((AsyncStructure)targetStructure)._inner, ((AsyncReferencePoint)primaryReferencePoint)._inner, dosePerFraction, brachyTreatmentTechnique, (additionalReferencePoints.Select(x => ((AsyncReferencePoint)x)._inner))) is var result && result is null ? null : new AsyncBrachyPlanSetup(result, _service));
        }


        public async Task<IIonPlanSetup> AddIonPlanSetupAsync(IStructureSet structureSet, IStructure targetStructure, IReferencePoint primaryReferencePoint, string patientSupportDeviceId, IReadOnlyList<IReferencePoint> additionalReferencePoints)
        {
            return await _service.PostAsync(context => 
                _inner.AddIonPlanSetup(((AsyncStructureSet)structureSet)._inner, ((AsyncStructure)targetStructure)._inner, ((AsyncReferencePoint)primaryReferencePoint)._inner, patientSupportDeviceId, (additionalReferencePoints.Select(x => ((AsyncReferencePoint)x)._inner))) is var result && result is null ? null : new AsyncIonPlanSetup(result, _service));
        }


        public async Task<IBrachyPlanSetup> AddBrachyPlanSetupAsync(IStructureSet structureSet, DoseValue dosePerFraction, BrachyTreatmentTechniqueType brachyTreatmentTechnique)
        {
            return await _service.PostAsync(context => 
                _inner.AddBrachyPlanSetup(((AsyncStructureSet)structureSet)._inner, dosePerFraction, brachyTreatmentTechnique) is var result && result is null ? null : new AsyncBrachyPlanSetup(result, _service));
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
        public Task<bool> CanAddPlanSetupAsync(IStructureSet structureSet) => 
            _service.PostAsync(context => _inner.CanAddPlanSetup(((AsyncStructureSet)structureSet)._inner));

        // Simple Method
        public Task<bool> CanRemovePlanSetupAsync(IPlanSetup planSetup) => 
            _service.PostAsync(context => _inner.CanRemovePlanSetup(((AsyncPlanSetup)planSetup)._inner));

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
        public Task<bool> IsCompletedAsync() => 
            _service.PostAsync(context => _inner.IsCompleted());

        // Simple Void Method
        public Task RemovePlanSetupAsync(IPlanSetup planSetup) =>
            _service.PostAsync(context => _inner.RemovePlanSetup(((AsyncPlanSetup)planSetup)._inner));

        // Simple Void Method
        public Task RemovePlanSumAsync(IPlanSum planSum) =>
            _service.PostAsync(context => _inner.RemovePlanSum(((AsyncPlanSum)planSum)._inner));

        public async Task<IReadOnlyList<IExternalPlanSetup>> GetExternalPlanSetupsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.ExternalPlanSetups?.Select(x => new AsyncExternalPlanSetup(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IBrachyPlanSetup>> GetBrachyPlanSetupsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.BrachyPlanSetups?.Select(x => new AsyncBrachyPlanSetup(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IIonPlanSetup>> GetIonPlanSetupsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.IonPlanSetups?.Select(x => new AsyncIonPlanSetup(x, _service)).ToList());
        }


        public CourseClinicalStatus ClinicalStatus { get; }

        public DateTime? CompletedDateTime { get; }

        public async Task<IReadOnlyList<IDiagnosis>> GetDiagnosesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Diagnoses?.Select(x => new AsyncDiagnosis(x, _service)).ToList());
        }


        public string Intent { get; }

        public async Task<IPatient> GetPatientAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Patient is null ? null : new AsyncPatient(_inner.Patient, _service);
                return innerResult;
            });
        }

        public async Task<IReadOnlyList<IPlanSetup>> GetPlanSetupsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.PlanSetups?.Select(x => new AsyncPlanSetup(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IPlanSum>> GetPlanSumsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.PlanSums?.Select(x => new AsyncPlanSum(x, _service)).ToList());
        }


        public DateTime? StartDateTime { get; }

        public async Task<IReadOnlyList<ITreatmentPhase>> GetTreatmentPhasesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.TreatmentPhases?.Select(x => new AsyncTreatmentPhase(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<ITreatmentSession>> GetTreatmentSessionsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.TreatmentSessions?.Select(x => new AsyncTreatmentSession(x, _service)).ToList());
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Course> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Course, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.Course(AsyncCourse wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Course IEsapiWrapper<VMS.TPS.Common.Model.API.Course>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Course>.Service => _service;

        /* --- Skipped Members (Not generated) ---
           - Id: Shadows base member in wrapped base class
           - Comment: Shadows base member in wrapped base class
        */
    }
}
