using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncCourse : AsyncApiDataObject, ICourse
    {
        internal readonly VMS.TPS.Common.Model.API.Course _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncCourse(VMS.TPS.Common.Model.API.Course inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            ClinicalStatus = inner.ClinicalStatus;
            CompletedDateTime = inner.CompletedDateTime;
            Intent = inner.Intent;
            StartDateTime = inner.StartDateTime;
        }


        public async Task<IPlanSum> CreatePlanSumAsync(IReadOnlyList<IPlanningItem> planningItems, IImage image)
        {
            return await _service.RunAsync(() => 
                _inner.CreatePlanSum(planningItems, image) is var result && result is null ? null : new AsyncPlanSum(result, _service));
        }


        public async Task<IExternalPlanSetup> AddExternalPlanSetupAsync(IStructureSet structureSet, IStructure targetStructure, IReferencePoint primaryReferencePoint, IReadOnlyList<IReferencePoint> additionalReferencePoints)
        {
            return await _service.RunAsync(() => 
                _inner.AddExternalPlanSetup(structureSet, targetStructure, primaryReferencePoint, additionalReferencePoints) is var result && result is null ? null : new AsyncExternalPlanSetup(result, _service));
        }


        public async Task<IBrachyPlanSetup> AddBrachyPlanSetupAsync(IStructureSet structureSet, IStructure targetStructure, IReferencePoint primaryReferencePoint, DoseValue dosePerFraction, BrachyTreatmentTechniqueType brachyTreatmentTechnique, IReadOnlyList<IReferencePoint> additionalReferencePoints)
        {
            return await _service.RunAsync(() => 
                _inner.AddBrachyPlanSetup(structureSet, targetStructure, primaryReferencePoint, dosePerFraction, brachyTreatmentTechnique, additionalReferencePoints) is var result && result is null ? null : new AsyncBrachyPlanSetup(result, _service));
        }


        public async Task<IIonPlanSetup> AddIonPlanSetupAsync(IStructureSet structureSet, IStructure targetStructure, IReferencePoint primaryReferencePoint, string patientSupportDeviceId, IReadOnlyList<IReferencePoint> additionalReferencePoints)
        {
            return await _service.RunAsync(() => 
                _inner.AddIonPlanSetup(structureSet, targetStructure, primaryReferencePoint, patientSupportDeviceId, additionalReferencePoints) is var result && result is null ? null : new AsyncIonPlanSetup(result, _service));
        }


        public async Task<IBrachyPlanSetup> AddBrachyPlanSetupAsync(IStructureSet structureSet, DoseValue dosePerFraction, BrachyTreatmentTechniqueType brachyTreatmentTechnique)
        {
            return await _service.RunAsync(() => 
                _inner.AddBrachyPlanSetup(structureSet, dosePerFraction, brachyTreatmentTechnique) is var result && result is null ? null : new AsyncBrachyPlanSetup(result, _service));
        }


        public async Task<IExternalPlanSetup> AddExternalPlanSetupAsync(IStructureSet structureSet)
        {
            return await _service.RunAsync(() => 
                _inner.AddExternalPlanSetup(structureSet) is var result && result is null ? null : new AsyncExternalPlanSetup(result, _service));
        }


        public async Task<IExternalPlanSetup> AddExternalPlanSetupAsVerificationPlanAsync(IStructureSet structureSet, IExternalPlanSetup verifiedPlan)
        {
            return await _service.RunAsync(() => 
                _inner.AddExternalPlanSetupAsVerificationPlan(structureSet, verifiedPlan) is var result && result is null ? null : new AsyncExternalPlanSetup(result, _service));
        }


        public async Task<IIonPlanSetup> AddIonPlanSetupAsync(IStructureSet structureSet, string patientSupportDeviceId)
        {
            return await _service.RunAsync(() => 
                _inner.AddIonPlanSetup(structureSet, patientSupportDeviceId) is var result && result is null ? null : new AsyncIonPlanSetup(result, _service));
        }


        public async Task<IIonPlanSetup> AddIonPlanSetupAsVerificationPlanAsync(IStructureSet structureSet, string patientSupportDeviceId, IIonPlanSetup verifiedPlan)
        {
            return await _service.RunAsync(() => 
                _inner.AddIonPlanSetupAsVerificationPlan(structureSet, patientSupportDeviceId, verifiedPlan) is var result && result is null ? null : new AsyncIonPlanSetup(result, _service));
        }


        public Task<bool> CanAddPlanSetupAsync(IStructureSet structureSet) => _service.RunAsync(() => _inner.CanAddPlanSetup(structureSet));

        public Task<bool> CanRemovePlanSetupAsync(IPlanSetup planSetup) => _service.RunAsync(() => _inner.CanRemovePlanSetup(planSetup));

        public async Task<IBrachyPlanSetup> CopyBrachyPlanSetupAsync(IBrachyPlanSetup sourcePlan, System.Text.StringBuilder outputDiagnostics)
        {
            return await _service.RunAsync(() => 
                _inner.CopyBrachyPlanSetup(sourcePlan, outputDiagnostics) is var result && result is null ? null : new AsyncBrachyPlanSetup(result, _service));
        }


        public async Task<IBrachyPlanSetup> CopyBrachyPlanSetupAsync(IBrachyPlanSetup sourcePlan, IStructureSet structureset, System.Text.StringBuilder outputDiagnostics)
        {
            return await _service.RunAsync(() => 
                _inner.CopyBrachyPlanSetup(sourcePlan, structureset, outputDiagnostics) is var result && result is null ? null : new AsyncBrachyPlanSetup(result, _service));
        }


        public async Task<IPlanSetup> CopyPlanSetupAsync(IPlanSetup sourcePlan)
        {
            return await _service.RunAsync(() => 
                _inner.CopyPlanSetup(sourcePlan) is var result && result is null ? null : new AsyncPlanSetup(result, _service));
        }


        public async Task<IPlanSetup> CopyPlanSetupAsync(IPlanSetup sourcePlan, IImage targetImage, System.Text.StringBuilder outputDiagnostics)
        {
            return await _service.RunAsync(() => 
                _inner.CopyPlanSetup(sourcePlan, targetImage, outputDiagnostics) is var result && result is null ? null : new AsyncPlanSetup(result, _service));
        }


        public async Task<IPlanSetup> CopyPlanSetupAsync(IPlanSetup sourcePlan, IImage targetImage, IRegistration registration, System.Text.StringBuilder outputDiagnostics)
        {
            return await _service.RunAsync(() => 
                _inner.CopyPlanSetup(sourcePlan, targetImage, registration, outputDiagnostics) is var result && result is null ? null : new AsyncPlanSetup(result, _service));
        }


        public async Task<IPlanSetup> CopyPlanSetupAsync(IPlanSetup sourcePlan, IStructureSet structureset, System.Text.StringBuilder outputDiagnostics)
        {
            return await _service.RunAsync(() => 
                _inner.CopyPlanSetup(sourcePlan, structureset, outputDiagnostics) is var result && result is null ? null : new AsyncPlanSetup(result, _service));
        }


        public Task<bool> IsCompletedAsync() => _service.RunAsync(() => _inner.IsCompleted());

        public Task RemovePlanSetupAsync(IPlanSetup planSetup) => _service.RunAsync(() => _inner.RemovePlanSetup(planSetup));

        public Task RemovePlanSumAsync(IPlanSum planSum) => _service.RunAsync(() => _inner.RemovePlanSum(planSum));

        public async Task<IReadOnlyList<IExternalPlanSetup>> GetExternalPlanSetupsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.ExternalPlanSetups?.Select(x => new AsyncExternalPlanSetup(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IBrachyPlanSetup>> GetBrachyPlanSetupsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.BrachyPlanSetups?.Select(x => new AsyncBrachyPlanSetup(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IIonPlanSetup>> GetIonPlanSetupsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.IonPlanSetups?.Select(x => new AsyncIonPlanSetup(x, _service)).ToList());
        }


        public CourseClinicalStatus ClinicalStatus { get; }

        public DateTime? CompletedDateTime { get; }

        public async Task<IReadOnlyList<IDiagnosis>> GetDiagnosesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Diagnoses?.Select(x => new AsyncDiagnosis(x, _service)).ToList());
        }


        public string Intent { get; }

        public async Task<IPatient> GetPatientAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Patient is null ? null : new AsyncPatient(_inner.Patient, _service));
        }

        public async Task<IReadOnlyList<IPlanSetup>> GetPlanSetupsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.PlanSetups?.Select(x => new AsyncPlanSetup(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IPlanSum>> GetPlanSumsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.PlanSums?.Select(x => new AsyncPlanSum(x, _service)).ToList());
        }


        public DateTime? StartDateTime { get; }

        public async Task<IReadOnlyList<ITreatmentPhase>> GetTreatmentPhasesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.TreatmentPhases?.Select(x => new AsyncTreatmentPhase(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<ITreatmentSession>> GetTreatmentSessionsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.TreatmentSessions?.Select(x => new AsyncTreatmentSession(x, _service)).ToList());
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Course> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Course, T> func) => _service.RunAsync(() => func(_inner));
    }
}
