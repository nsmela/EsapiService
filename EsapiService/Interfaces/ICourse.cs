namespace VMS.TPS.Common.Model.API
{
    public interface ICourse : IApiDataObject
    {
        IPlanSum CreatePlanSum(System.Collections.Generic.IEnumerable<VMS.TPS.Common.Model.API.PlanningItem> planningItems, VMS.TPS.Common.Model.API.Image image);
        IExternalPlanSetup AddExternalPlanSetup(VMS.TPS.Common.Model.API.StructureSet structureSet, VMS.TPS.Common.Model.API.Structure targetStructure, VMS.TPS.Common.Model.API.ReferencePoint primaryReferencePoint, System.Collections.Generic.IEnumerable<VMS.TPS.Common.Model.API.ReferencePoint> additionalReferencePoints);
        IBrachyPlanSetup AddBrachyPlanSetup(VMS.TPS.Common.Model.API.StructureSet structureSet, VMS.TPS.Common.Model.API.Structure targetStructure, VMS.TPS.Common.Model.API.ReferencePoint primaryReferencePoint, VMS.TPS.Common.Model.Types.DoseValue dosePerFraction, VMS.TPS.Common.Model.Types.BrachyTreatmentTechniqueType brachyTreatmentTechnique, System.Collections.Generic.IEnumerable<VMS.TPS.Common.Model.API.ReferencePoint> additionalReferencePoints);
        IIonPlanSetup AddIonPlanSetup(VMS.TPS.Common.Model.API.StructureSet structureSet, VMS.TPS.Common.Model.API.Structure targetStructure, VMS.TPS.Common.Model.API.ReferencePoint primaryReferencePoint, string patientSupportDeviceId, System.Collections.Generic.IEnumerable<VMS.TPS.Common.Model.API.ReferencePoint> additionalReferencePoints);
        void WriteXml(System.Xml.XmlWriter writer);
        IBrachyPlanSetup AddBrachyPlanSetup(VMS.TPS.Common.Model.API.StructureSet structureSet, VMS.TPS.Common.Model.Types.DoseValue dosePerFraction, VMS.TPS.Common.Model.Types.BrachyTreatmentTechniqueType brachyTreatmentTechnique);
        IExternalPlanSetup AddExternalPlanSetup(VMS.TPS.Common.Model.API.StructureSet structureSet);
        IExternalPlanSetup AddExternalPlanSetupAsVerificationPlan(VMS.TPS.Common.Model.API.StructureSet structureSet, VMS.TPS.Common.Model.API.ExternalPlanSetup verifiedPlan);
        IIonPlanSetup AddIonPlanSetup(VMS.TPS.Common.Model.API.StructureSet structureSet, string patientSupportDeviceId);
        IIonPlanSetup AddIonPlanSetupAsVerificationPlan(VMS.TPS.Common.Model.API.StructureSet structureSet, string patientSupportDeviceId, VMS.TPS.Common.Model.API.IonPlanSetup verifiedPlan);
        bool CanAddPlanSetup(VMS.TPS.Common.Model.API.StructureSet structureSet);
        bool CanRemovePlanSetup(VMS.TPS.Common.Model.API.PlanSetup planSetup);
        IBrachyPlanSetup CopyBrachyPlanSetup(VMS.TPS.Common.Model.API.BrachyPlanSetup sourcePlan, System.Text.StringBuilder outputDiagnostics);
        IBrachyPlanSetup CopyBrachyPlanSetup(VMS.TPS.Common.Model.API.BrachyPlanSetup sourcePlan, VMS.TPS.Common.Model.API.StructureSet structureset, System.Text.StringBuilder outputDiagnostics);
        IPlanSetup CopyPlanSetup(VMS.TPS.Common.Model.API.PlanSetup sourcePlan);
        IPlanSetup CopyPlanSetup(VMS.TPS.Common.Model.API.PlanSetup sourcePlan, VMS.TPS.Common.Model.API.Image targetImage, System.Text.StringBuilder outputDiagnostics);
        IPlanSetup CopyPlanSetup(VMS.TPS.Common.Model.API.PlanSetup sourcePlan, VMS.TPS.Common.Model.API.Image targetImage, VMS.TPS.Common.Model.API.Registration registration, System.Text.StringBuilder outputDiagnostics);
        IPlanSetup CopyPlanSetup(VMS.TPS.Common.Model.API.PlanSetup sourcePlan, VMS.TPS.Common.Model.API.StructureSet structureset, System.Text.StringBuilder outputDiagnostics);
        bool IsCompleted();
        void RemovePlanSetup(VMS.TPS.Common.Model.API.PlanSetup planSetup);
        void RemovePlanSum(VMS.TPS.Common.Model.API.PlanSum planSum);
        string Id { get; }
        System.Threading.Tasks.Task SetIdAsync(string value);
        string Comment { get; }
        System.Threading.Tasks.Task SetCommentAsync(string value);
        System.Collections.Generic.IReadOnlyList<IExternalPlanSetup> ExternalPlanSetups { get; }
        System.Collections.Generic.IReadOnlyList<IBrachyPlanSetup> BrachyPlanSetups { get; }
        System.Collections.Generic.IReadOnlyList<IIonPlanSetup> IonPlanSetups { get; }
        VMS.TPS.Common.Model.Types.CourseClinicalStatus ClinicalStatus { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> CompletedDateTime { get; }
        System.Collections.Generic.IReadOnlyList<IDiagnosis> Diagnoses { get; }
        string Intent { get; }
        IPatient Patient { get; }
        System.Collections.Generic.IReadOnlyList<IPlanSetup> PlanSetups { get; }
        System.Collections.Generic.IReadOnlyList<IPlanSum> PlanSums { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> StartDateTime { get; }
        System.Collections.Generic.IReadOnlyList<ITreatmentPhase> TreatmentPhases { get; }
        System.Collections.Generic.IReadOnlyList<ITreatmentSession> TreatmentSessions { get; }
    }
}
