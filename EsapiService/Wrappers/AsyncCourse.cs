namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
    public class AsyncCourse : ICourse
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
            Intent = inner.Intent;
        }

        public IPlanSum CreatePlanSum(IReadOnlyList<IPlanningItem> planningItems, IImage image) => _inner.CreatePlanSum(planningItems, image) is var result && result is null ? null : new AsyncPlanSum(result, _service);
        public IExternalPlanSetup AddExternalPlanSetup(IStructureSet structureSet, IStructure targetStructure, IReferencePoint primaryReferencePoint, IReadOnlyList<IReferencePoint> additionalReferencePoints) => _inner.AddExternalPlanSetup(structureSet, targetStructure, primaryReferencePoint, additionalReferencePoints) is var result && result is null ? null : new AsyncExternalPlanSetup(result, _service);
        public IBrachyPlanSetup AddBrachyPlanSetup(IStructureSet structureSet, IStructure targetStructure, IReferencePoint primaryReferencePoint, DoseValue dosePerFraction, BrachyTreatmentTechniqueType brachyTreatmentTechnique, IReadOnlyList<IReferencePoint> additionalReferencePoints) => _inner.AddBrachyPlanSetup(structureSet, targetStructure, primaryReferencePoint, dosePerFraction, brachyTreatmentTechnique, additionalReferencePoints) is var result && result is null ? null : new AsyncBrachyPlanSetup(result, _service);
        public IIonPlanSetup AddIonPlanSetup(IStructureSet structureSet, IStructure targetStructure, IReferencePoint primaryReferencePoint, string patientSupportDeviceId, IReadOnlyList<IReferencePoint> additionalReferencePoints) => _inner.AddIonPlanSetup(structureSet, targetStructure, primaryReferencePoint, patientSupportDeviceId, additionalReferencePoints) is var result && result is null ? null : new AsyncIonPlanSetup(result, _service);
        public IBrachyPlanSetup AddBrachyPlanSetup(IStructureSet structureSet, DoseValue dosePerFraction, BrachyTreatmentTechniqueType brachyTreatmentTechnique) => _inner.AddBrachyPlanSetup(structureSet, dosePerFraction, brachyTreatmentTechnique) is var result && result is null ? null : new AsyncBrachyPlanSetup(result, _service);
        public IExternalPlanSetup AddExternalPlanSetup(IStructureSet structureSet) => _inner.AddExternalPlanSetup(structureSet) is var result && result is null ? null : new AsyncExternalPlanSetup(result, _service);
        public IExternalPlanSetup AddExternalPlanSetupAsVerificationPlan(IStructureSet structureSet, IExternalPlanSetup verifiedPlan) => _inner.AddExternalPlanSetupAsVerificationPlan(structureSet, verifiedPlan) is var result && result is null ? null : new AsyncExternalPlanSetup(result, _service);
        public IIonPlanSetup AddIonPlanSetup(IStructureSet structureSet, string patientSupportDeviceId) => _inner.AddIonPlanSetup(structureSet, patientSupportDeviceId) is var result && result is null ? null : new AsyncIonPlanSetup(result, _service);
        public IIonPlanSetup AddIonPlanSetupAsVerificationPlan(IStructureSet structureSet, string patientSupportDeviceId, IIonPlanSetup verifiedPlan) => _inner.AddIonPlanSetupAsVerificationPlan(structureSet, patientSupportDeviceId, verifiedPlan) is var result && result is null ? null : new AsyncIonPlanSetup(result, _service);
        public bool CanAddPlanSetup(IStructureSet structureSet) => _inner.CanAddPlanSetup(structureSet);
        public bool CanRemovePlanSetup(IPlanSetup planSetup) => _inner.CanRemovePlanSetup(planSetup);
        public IBrachyPlanSetup CopyBrachyPlanSetup(IBrachyPlanSetup sourcePlan, Text.StringBuilder outputDiagnostics) => _inner.CopyBrachyPlanSetup(sourcePlan, outputDiagnostics) is var result && result is null ? null : new AsyncBrachyPlanSetup(result, _service);
        public IBrachyPlanSetup CopyBrachyPlanSetup(IBrachyPlanSetup sourcePlan, IStructureSet structureset, Text.StringBuilder outputDiagnostics) => _inner.CopyBrachyPlanSetup(sourcePlan, structureset, outputDiagnostics) is var result && result is null ? null : new AsyncBrachyPlanSetup(result, _service);
        public IPlanSetup CopyPlanSetup(IPlanSetup sourcePlan) => _inner.CopyPlanSetup(sourcePlan) is var result && result is null ? null : new AsyncPlanSetup(result, _service);
        public IPlanSetup CopyPlanSetup(IPlanSetup sourcePlan, IImage targetImage, Text.StringBuilder outputDiagnostics) => _inner.CopyPlanSetup(sourcePlan, targetImage, outputDiagnostics) is var result && result is null ? null : new AsyncPlanSetup(result, _service);
        public IPlanSetup CopyPlanSetup(IPlanSetup sourcePlan, IImage targetImage, IRegistration registration, Text.StringBuilder outputDiagnostics) => _inner.CopyPlanSetup(sourcePlan, targetImage, registration, outputDiagnostics) is var result && result is null ? null : new AsyncPlanSetup(result, _service);
        public IPlanSetup CopyPlanSetup(IPlanSetup sourcePlan, IStructureSet structureset, Text.StringBuilder outputDiagnostics) => _inner.CopyPlanSetup(sourcePlan, structureset, outputDiagnostics) is var result && result is null ? null : new AsyncPlanSetup(result, _service);
        public bool IsCompleted() => _inner.IsCompleted();
        public void RemovePlanSetup(IPlanSetup planSetup) => _inner.RemovePlanSetup(planSetup);
        public void RemovePlanSum(IPlanSum planSum) => _inner.RemovePlanSum(planSum);
        public IReadOnlyList<IExternalPlanSetup> ExternalPlanSetups => _inner.ExternalPlanSetups?.Select(x => new AsyncExternalPlanSetup(x, _service)).ToList();
        public IReadOnlyList<IBrachyPlanSetup> BrachyPlanSetups => _inner.BrachyPlanSetups?.Select(x => new AsyncBrachyPlanSetup(x, _service)).ToList();
        public IReadOnlyList<IIonPlanSetup> IonPlanSetups => _inner.IonPlanSetups?.Select(x => new AsyncIonPlanSetup(x, _service)).ToList();
        public CourseClinicalStatus ClinicalStatus { get; }
        public IReadOnlyList<DateTime> CompletedDateTime => _inner.CompletedDateTime?.ToList();
        public IReadOnlyList<IDiagnosis> Diagnoses => _inner.Diagnoses?.Select(x => new AsyncDiagnosis(x, _service)).ToList();
        public string Intent { get; }
        public IPatient Patient => _inner.Patient is null ? null : new AsyncPatient(_inner.Patient, _service);

        public IReadOnlyList<IPlanSetup> PlanSetups => _inner.PlanSetups?.Select(x => new AsyncPlanSetup(x, _service)).ToList();
        public IReadOnlyList<IPlanSum> PlanSums => _inner.PlanSums?.Select(x => new AsyncPlanSum(x, _service)).ToList();
        public IReadOnlyList<DateTime> StartDateTime => _inner.StartDateTime?.ToList();
        public IReadOnlyList<ITreatmentPhase> TreatmentPhases => _inner.TreatmentPhases?.Select(x => new AsyncTreatmentPhase(x, _service)).ToList();
        public IReadOnlyList<ITreatmentSession> TreatmentSessions => _inner.TreatmentSessions?.Select(x => new AsyncTreatmentSession(x, _service)).ToList();

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Course> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Course, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
