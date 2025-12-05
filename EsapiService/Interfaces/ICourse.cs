using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;

namespace Esapi.Interfaces
{
    public interface ICourse : IApiDataObject
    {
        Task<IPlanSum> CreatePlanSumAsync(System.Collections.Generic.IEnumerable<VMS.TPS.Common.Model.API.PlanningItem> planningItems, VMS.TPS.Common.Model.API.Image image);
        Task<IExternalPlanSetup> AddExternalPlanSetupAsync(VMS.TPS.Common.Model.API.StructureSet structureSet, VMS.TPS.Common.Model.API.Structure targetStructure, VMS.TPS.Common.Model.API.ReferencePoint primaryReferencePoint, System.Collections.Generic.IEnumerable<VMS.TPS.Common.Model.API.ReferencePoint> additionalReferencePoints);
        Task<IBrachyPlanSetup> AddBrachyPlanSetupAsync(VMS.TPS.Common.Model.API.StructureSet structureSet, VMS.TPS.Common.Model.API.Structure targetStructure, VMS.TPS.Common.Model.API.ReferencePoint primaryReferencePoint, VMS.TPS.Common.Model.Types.DoseValue dosePerFraction, VMS.TPS.Common.Model.Types.BrachyTreatmentTechniqueType brachyTreatmentTechnique, System.Collections.Generic.IEnumerable<VMS.TPS.Common.Model.API.ReferencePoint> additionalReferencePoints);
        Task<IIonPlanSetup> AddIonPlanSetupAsync(VMS.TPS.Common.Model.API.StructureSet structureSet, VMS.TPS.Common.Model.API.Structure targetStructure, VMS.TPS.Common.Model.API.ReferencePoint primaryReferencePoint, string patientSupportDeviceId, System.Collections.Generic.IEnumerable<VMS.TPS.Common.Model.API.ReferencePoint> additionalReferencePoints);
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task<IBrachyPlanSetup> AddBrachyPlanSetupAsync(VMS.TPS.Common.Model.API.StructureSet structureSet, VMS.TPS.Common.Model.Types.DoseValue dosePerFraction, VMS.TPS.Common.Model.Types.BrachyTreatmentTechniqueType brachyTreatmentTechnique);
        Task<IExternalPlanSetup> AddExternalPlanSetupAsync(VMS.TPS.Common.Model.API.StructureSet structureSet);
        Task<IExternalPlanSetup> AddExternalPlanSetupAsVerificationPlanAsync(VMS.TPS.Common.Model.API.StructureSet structureSet, VMS.TPS.Common.Model.API.ExternalPlanSetup verifiedPlan);
        Task<IIonPlanSetup> AddIonPlanSetupAsync(VMS.TPS.Common.Model.API.StructureSet structureSet, string patientSupportDeviceId);
        Task<IIonPlanSetup> AddIonPlanSetupAsVerificationPlanAsync(VMS.TPS.Common.Model.API.StructureSet structureSet, string patientSupportDeviceId, VMS.TPS.Common.Model.API.IonPlanSetup verifiedPlan);
        Task<bool> CanAddPlanSetupAsync(VMS.TPS.Common.Model.API.StructureSet structureSet);
        Task<bool> CanRemovePlanSetupAsync(VMS.TPS.Common.Model.API.PlanSetup planSetup);
        Task<IBrachyPlanSetup> CopyBrachyPlanSetupAsync(VMS.TPS.Common.Model.API.BrachyPlanSetup sourcePlan, System.Text.StringBuilder outputDiagnostics);
        Task<IBrachyPlanSetup> CopyBrachyPlanSetupAsync(VMS.TPS.Common.Model.API.BrachyPlanSetup sourcePlan, VMS.TPS.Common.Model.API.StructureSet structureset, System.Text.StringBuilder outputDiagnostics);
        Task<IPlanSetup> CopyPlanSetupAsync(VMS.TPS.Common.Model.API.PlanSetup sourcePlan);
        Task<IPlanSetup> CopyPlanSetupAsync(VMS.TPS.Common.Model.API.PlanSetup sourcePlan, VMS.TPS.Common.Model.API.Image targetImage, System.Text.StringBuilder outputDiagnostics);
        Task<IPlanSetup> CopyPlanSetupAsync(VMS.TPS.Common.Model.API.PlanSetup sourcePlan, VMS.TPS.Common.Model.API.Image targetImage, VMS.TPS.Common.Model.API.Registration registration, System.Text.StringBuilder outputDiagnostics);
        Task<IPlanSetup> CopyPlanSetupAsync(VMS.TPS.Common.Model.API.PlanSetup sourcePlan, VMS.TPS.Common.Model.API.StructureSet structureset, System.Text.StringBuilder outputDiagnostics);
        Task<bool> IsCompletedAsync();
        Task RemovePlanSetupAsync(VMS.TPS.Common.Model.API.PlanSetup planSetup);
        Task RemovePlanSumAsync(VMS.TPS.Common.Model.API.PlanSum planSum);
        string Id { get; }
        Task SetIdAsync(string value);
        string Comment { get; }
        Task SetCommentAsync(string value);
        System.Collections.Generic.IReadOnlyList<IExternalPlanSetup> ExternalPlanSetups { get; }
        System.Collections.Generic.IReadOnlyList<IBrachyPlanSetup> BrachyPlanSetups { get; }
        System.Collections.Generic.IReadOnlyList<IIonPlanSetup> IonPlanSetups { get; }
        VMS.TPS.Common.Model.Types.CourseClinicalStatus ClinicalStatus { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> CompletedDateTime { get; }
        System.Collections.Generic.IReadOnlyList<IDiagnosis> Diagnoses { get; }
        string Intent { get; }
        Task<IPatient> GetPatientAsync();
        System.Collections.Generic.IReadOnlyList<IPlanSetup> PlanSetups { get; }
        System.Collections.Generic.IReadOnlyList<IPlanSum> PlanSums { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> StartDateTime { get; }
        System.Collections.Generic.IReadOnlyList<ITreatmentPhase> TreatmentPhases { get; }
        System.Collections.Generic.IReadOnlyList<ITreatmentSession> TreatmentSessions { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Course object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Course> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Course object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Course, T> func);
    }
}
