    using System.Threading.Tasks;
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

        public IPlanSum CreatePlanSum(System.Collections.Generic.IEnumerable<VMS.TPS.Common.Model.API.PlanningItem> planningItems, VMS.TPS.Common.Model.API.Image image) => _inner.CreatePlanSum(planningItems, image) is var result && result is null ? null : new AsyncPlanSum(result, _service);
        public IExternalPlanSetup AddExternalPlanSetup(VMS.TPS.Common.Model.API.StructureSet structureSet, VMS.TPS.Common.Model.API.Structure targetStructure, VMS.TPS.Common.Model.API.ReferencePoint primaryReferencePoint, System.Collections.Generic.IEnumerable<VMS.TPS.Common.Model.API.ReferencePoint> additionalReferencePoints) => _inner.AddExternalPlanSetup(structureSet, targetStructure, primaryReferencePoint, additionalReferencePoints) is var result && result is null ? null : new AsyncExternalPlanSetup(result, _service);
        public IBrachyPlanSetup AddBrachyPlanSetup(VMS.TPS.Common.Model.API.StructureSet structureSet, VMS.TPS.Common.Model.API.Structure targetStructure, VMS.TPS.Common.Model.API.ReferencePoint primaryReferencePoint, VMS.TPS.Common.Model.Types.DoseValue dosePerFraction, VMS.TPS.Common.Model.Types.BrachyTreatmentTechniqueType brachyTreatmentTechnique, System.Collections.Generic.IEnumerable<VMS.TPS.Common.Model.API.ReferencePoint> additionalReferencePoints) => _inner.AddBrachyPlanSetup(structureSet, targetStructure, primaryReferencePoint, dosePerFraction, brachyTreatmentTechnique, additionalReferencePoints) is var result && result is null ? null : new AsyncBrachyPlanSetup(result, _service);
        public IIonPlanSetup AddIonPlanSetup(VMS.TPS.Common.Model.API.StructureSet structureSet, VMS.TPS.Common.Model.API.Structure targetStructure, VMS.TPS.Common.Model.API.ReferencePoint primaryReferencePoint, string patientSupportDeviceId, System.Collections.Generic.IEnumerable<VMS.TPS.Common.Model.API.ReferencePoint> additionalReferencePoints) => _inner.AddIonPlanSetup(structureSet, targetStructure, primaryReferencePoint, patientSupportDeviceId, additionalReferencePoints) is var result && result is null ? null : new AsyncIonPlanSetup(result, _service);
        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public IBrachyPlanSetup AddBrachyPlanSetup(VMS.TPS.Common.Model.API.StructureSet structureSet, VMS.TPS.Common.Model.Types.DoseValue dosePerFraction, VMS.TPS.Common.Model.Types.BrachyTreatmentTechniqueType brachyTreatmentTechnique) => _inner.AddBrachyPlanSetup(structureSet, dosePerFraction, brachyTreatmentTechnique) is var result && result is null ? null : new AsyncBrachyPlanSetup(result, _service);
        public IExternalPlanSetup AddExternalPlanSetup(VMS.TPS.Common.Model.API.StructureSet structureSet) => _inner.AddExternalPlanSetup(structureSet) is var result && result is null ? null : new AsyncExternalPlanSetup(result, _service);
        public IExternalPlanSetup AddExternalPlanSetupAsVerificationPlan(VMS.TPS.Common.Model.API.StructureSet structureSet, VMS.TPS.Common.Model.API.ExternalPlanSetup verifiedPlan) => _inner.AddExternalPlanSetupAsVerificationPlan(structureSet, verifiedPlan) is var result && result is null ? null : new AsyncExternalPlanSetup(result, _service);
        public IIonPlanSetup AddIonPlanSetup(VMS.TPS.Common.Model.API.StructureSet structureSet, string patientSupportDeviceId) => _inner.AddIonPlanSetup(structureSet, patientSupportDeviceId) is var result && result is null ? null : new AsyncIonPlanSetup(result, _service);
        public IIonPlanSetup AddIonPlanSetupAsVerificationPlan(VMS.TPS.Common.Model.API.StructureSet structureSet, string patientSupportDeviceId, VMS.TPS.Common.Model.API.IonPlanSetup verifiedPlan) => _inner.AddIonPlanSetupAsVerificationPlan(structureSet, patientSupportDeviceId, verifiedPlan) is var result && result is null ? null : new AsyncIonPlanSetup(result, _service);
        public bool CanAddPlanSetup(VMS.TPS.Common.Model.API.StructureSet structureSet) => _inner.CanAddPlanSetup(structureSet);
        public bool CanRemovePlanSetup(VMS.TPS.Common.Model.API.PlanSetup planSetup) => _inner.CanRemovePlanSetup(planSetup);
        public IBrachyPlanSetup CopyBrachyPlanSetup(VMS.TPS.Common.Model.API.BrachyPlanSetup sourcePlan, System.Text.StringBuilder outputDiagnostics) => _inner.CopyBrachyPlanSetup(sourcePlan, outputDiagnostics) is var result && result is null ? null : new AsyncBrachyPlanSetup(result, _service);
        public IBrachyPlanSetup CopyBrachyPlanSetup(VMS.TPS.Common.Model.API.BrachyPlanSetup sourcePlan, VMS.TPS.Common.Model.API.StructureSet structureset, System.Text.StringBuilder outputDiagnostics) => _inner.CopyBrachyPlanSetup(sourcePlan, structureset, outputDiagnostics) is var result && result is null ? null : new AsyncBrachyPlanSetup(result, _service);
        public IPlanSetup CopyPlanSetup(VMS.TPS.Common.Model.API.PlanSetup sourcePlan) => _inner.CopyPlanSetup(sourcePlan) is var result && result is null ? null : new AsyncPlanSetup(result, _service);
        public IPlanSetup CopyPlanSetup(VMS.TPS.Common.Model.API.PlanSetup sourcePlan, VMS.TPS.Common.Model.API.Image targetImage, System.Text.StringBuilder outputDiagnostics) => _inner.CopyPlanSetup(sourcePlan, targetImage, outputDiagnostics) is var result && result is null ? null : new AsyncPlanSetup(result, _service);
        public IPlanSetup CopyPlanSetup(VMS.TPS.Common.Model.API.PlanSetup sourcePlan, VMS.TPS.Common.Model.API.Image targetImage, VMS.TPS.Common.Model.API.Registration registration, System.Text.StringBuilder outputDiagnostics) => _inner.CopyPlanSetup(sourcePlan, targetImage, registration, outputDiagnostics) is var result && result is null ? null : new AsyncPlanSetup(result, _service);
        public IPlanSetup CopyPlanSetup(VMS.TPS.Common.Model.API.PlanSetup sourcePlan, VMS.TPS.Common.Model.API.StructureSet structureset, System.Text.StringBuilder outputDiagnostics) => _inner.CopyPlanSetup(sourcePlan, structureset, outputDiagnostics) is var result && result is null ? null : new AsyncPlanSetup(result, _service);
        public bool IsCompleted() => _inner.IsCompleted();
        public void RemovePlanSetup(VMS.TPS.Common.Model.API.PlanSetup planSetup) => _inner.RemovePlanSetup(planSetup);
        public void RemovePlanSum(VMS.TPS.Common.Model.API.PlanSum planSum) => _inner.RemovePlanSum(planSum);
        public string Id => _inner.Id;
        public async Task SetIdAsync(string value) => _service.RunAsync(() => _inner.Id = value);
        public string Comment => _inner.Comment;
        public async Task SetCommentAsync(string value) => _service.RunAsync(() => _inner.Comment = value);
        public System.Collections.Generic.IReadOnlyList<IExternalPlanSetup> ExternalPlanSetups => _inner.ExternalPlanSetups?.Select(x => new AsyncExternalPlanSetup(x, _service)).ToList();
        public System.Collections.Generic.IReadOnlyList<IBrachyPlanSetup> BrachyPlanSetups => _inner.BrachyPlanSetups?.Select(x => new AsyncBrachyPlanSetup(x, _service)).ToList();
        public System.Collections.Generic.IReadOnlyList<IIonPlanSetup> IonPlanSetups => _inner.IonPlanSetups?.Select(x => new AsyncIonPlanSetup(x, _service)).ToList();
        public VMS.TPS.Common.Model.Types.CourseClinicalStatus ClinicalStatus { get; }
        public System.Collections.Generic.IReadOnlyList<System.DateTime> CompletedDateTime => _inner.CompletedDateTime?.ToList();
        public System.Collections.Generic.IReadOnlyList<IDiagnosis> Diagnoses => _inner.Diagnoses?.Select(x => new AsyncDiagnosis(x, _service)).ToList();
        public string Intent { get; }
        public IPatient Patient => _inner.Patient is null ? null : new AsyncPatient(_inner.Patient, _service);

        public System.Collections.Generic.IReadOnlyList<IPlanSetup> PlanSetups => _inner.PlanSetups?.Select(x => new AsyncPlanSetup(x, _service)).ToList();
        public System.Collections.Generic.IReadOnlyList<IPlanSum> PlanSums => _inner.PlanSums?.Select(x => new AsyncPlanSum(x, _service)).ToList();
        public System.Collections.Generic.IReadOnlyList<System.DateTime> StartDateTime => _inner.StartDateTime?.ToList();
        public System.Collections.Generic.IReadOnlyList<ITreatmentPhase> TreatmentPhases => _inner.TreatmentPhases?.Select(x => new AsyncTreatmentPhase(x, _service)).ToList();
        public System.Collections.Generic.IReadOnlyList<ITreatmentSession> TreatmentSessions => _inner.TreatmentSessions?.Select(x => new AsyncTreatmentSession(x, _service)).ToList();

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Course> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Course, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
