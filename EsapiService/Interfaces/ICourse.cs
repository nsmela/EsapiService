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
        // --- Simple Properties --- //
        IEnumerable<ExternalPlanSetup> ExternalPlanSetups { get; }
        IEnumerable<BrachyPlanSetup> BrachyPlanSetups { get; }
        IEnumerable<IonPlanSetup> IonPlanSetups { get; }
        CourseClinicalStatus ClinicalStatus { get; }
        DateTime? CompletedDateTime { get; }
        IEnumerable<Diagnosis> Diagnoses { get; }
        string Intent { get; }
        IEnumerable<PlanSetup> PlanSetups { get; }
        IEnumerable<PlanSum> PlanSums { get; }
        DateTime? StartDateTime { get; }
        IEnumerable<TreatmentPhase> TreatmentPhases { get; }
        IEnumerable<TreatmentSession> TreatmentSessions { get; }

        // --- Accessors --- //
        Task<IPatient> GetPatientAsync(); // read complex property

        // --- Methods --- //
        Task<IPlanSum> CreatePlanSumAsync(IReadOnlyList<IPlanningItem> planningItems, IImage image); // complex method
        Task<IExternalPlanSetup> AddExternalPlanSetupAsync(IStructureSet structureSet, IStructure targetStructure, IReferencePoint primaryReferencePoint, IReadOnlyList<IReferencePoint> additionalReferencePoints); // complex method
        Task<IBrachyPlanSetup> AddBrachyPlanSetupAsync(IStructureSet structureSet, IStructure targetStructure, IReferencePoint primaryReferencePoint, DoseValue dosePerFraction, BrachyTreatmentTechniqueType brachyTreatmentTechnique, IReadOnlyList<IReferencePoint> additionalReferencePoints); // complex method
        Task<IIonPlanSetup> AddIonPlanSetupAsync(IStructureSet structureSet, IStructure targetStructure, IReferencePoint primaryReferencePoint, string patientSupportDeviceId, IReadOnlyList<IReferencePoint> additionalReferencePoints); // complex method
        Task<IBrachyPlanSetup> AddBrachyPlanSetupAsync(IStructureSet structureSet, DoseValue dosePerFraction, BrachyTreatmentTechniqueType brachyTreatmentTechnique); // complex method
        Task<IExternalPlanSetup> AddExternalPlanSetupAsync(IStructureSet structureSet); // complex method
        Task<IExternalPlanSetup> AddExternalPlanSetupAsVerificationPlanAsync(IStructureSet structureSet, IExternalPlanSetup verifiedPlan); // complex method
        Task<IIonPlanSetup> AddIonPlanSetupAsync(IStructureSet structureSet, string patientSupportDeviceId); // complex method
        Task<IIonPlanSetup> AddIonPlanSetupAsVerificationPlanAsync(IStructureSet structureSet, string patientSupportDeviceId, IIonPlanSetup verifiedPlan); // complex method
        Task<bool> CanAddPlanSetupAsync(IStructureSet structureSet); // simple method
        Task<bool> CanRemovePlanSetupAsync(IPlanSetup planSetup); // simple method
        Task<IBrachyPlanSetup> CopyBrachyPlanSetupAsync(IBrachyPlanSetup sourcePlan, System.Text.StringBuilder outputDiagnostics); // complex method
        Task<IBrachyPlanSetup> CopyBrachyPlanSetupAsync(IBrachyPlanSetup sourcePlan, IStructureSet structureset, System.Text.StringBuilder outputDiagnostics); // complex method
        Task<IPlanSetup> CopyPlanSetupAsync(IPlanSetup sourcePlan); // complex method
        Task<IPlanSetup> CopyPlanSetupAsync(IPlanSetup sourcePlan, IImage targetImage, System.Text.StringBuilder outputDiagnostics); // complex method
        Task<IPlanSetup> CopyPlanSetupAsync(IPlanSetup sourcePlan, IImage targetImage, IRegistration registration, System.Text.StringBuilder outputDiagnostics); // complex method
        Task<IPlanSetup> CopyPlanSetupAsync(IPlanSetup sourcePlan, IStructureSet structureset, System.Text.StringBuilder outputDiagnostics); // complex method
        Task<bool> IsCompletedAsync(); // simple method
        Task RemovePlanSetupAsync(IPlanSetup planSetup); // void method
        Task RemovePlanSumAsync(IPlanSum planSum); // void method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Course object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Course> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Course object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Course, T> func);

        /* --- Skipped Members (Not generated) ---
           - Id: Shadows member in wrapped base class
           - Comment: Shadows member in wrapped base class
        */
    }
}
