using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;
using Esapi.Interfaces;

namespace Esapi.Interfaces
{
    public interface ICourse : IApiDataObject
    {
        // --- Simple Properties --- //
        CourseClinicalStatus ClinicalStatus { get; }
        DateTime? CompletedDateTime { get; }
        string Intent { get; }
        DateTime? StartDateTime { get; }

        // --- Accessors --- //
        Task<IPatient> GetPatientAsync();

        // --- Collections --- //
        Task<IReadOnlyList<IExternalPlanSetup>> GetExternalPlanSetupsAsync();
        Task<IReadOnlyList<IBrachyPlanSetup>> GetBrachyPlanSetupsAsync();
        Task<IReadOnlyList<IIonPlanSetup>> GetIonPlanSetupsAsync();
        Task<IReadOnlyList<IDiagnosis>> GetDiagnosesAsync();
        Task<IReadOnlyList<IPlanSetup>> GetPlanSetupsAsync();
        Task<IReadOnlyList<IPlanSum>> GetPlanSumsAsync();
        Task<IReadOnlyList<ITreatmentPhase>> GetTreatmentPhasesAsync();
        Task<IReadOnlyList<ITreatmentSession>> GetTreatmentSessionsAsync();

        // --- Methods --- //
        Task<IPlanSum> CreatePlanSumAsync(IReadOnlyList<IPlanningItem> planningItems, IImage image);
        Task<IExternalPlanSetup> AddExternalPlanSetupAsync(IStructureSet structureSet, IStructure targetStructure, IReferencePoint primaryReferencePoint, IReadOnlyList<IReferencePoint> additionalReferencePoints);
        Task<IBrachyPlanSetup> AddBrachyPlanSetupAsync(IStructureSet structureSet, IStructure targetStructure, IReferencePoint primaryReferencePoint, DoseValue dosePerFraction, BrachyTreatmentTechniqueType brachyTreatmentTechnique, IReadOnlyList<IReferencePoint> additionalReferencePoints);
        Task<IIonPlanSetup> AddIonPlanSetupAsync(IStructureSet structureSet, IStructure targetStructure, IReferencePoint primaryReferencePoint, string patientSupportDeviceId, IReadOnlyList<IReferencePoint> additionalReferencePoints);
        Task<IBrachyPlanSetup> AddBrachyPlanSetupAsync(IStructureSet structureSet, DoseValue dosePerFraction, BrachyTreatmentTechniqueType brachyTreatmentTechnique);
        Task<IExternalPlanSetup> AddExternalPlanSetupAsync(IStructureSet structureSet);
        Task<IExternalPlanSetup> AddExternalPlanSetupAsVerificationPlanAsync(IStructureSet structureSet, IExternalPlanSetup verifiedPlan);
        Task<IIonPlanSetup> AddIonPlanSetupAsync(IStructureSet structureSet, string patientSupportDeviceId);
        Task<IIonPlanSetup> AddIonPlanSetupAsVerificationPlanAsync(IStructureSet structureSet, string patientSupportDeviceId, IIonPlanSetup verifiedPlan);
        Task<bool> CanAddPlanSetupAsync(IStructureSet structureSet);
        Task<bool> CanRemovePlanSetupAsync(IPlanSetup planSetup);
        Task<IBrachyPlanSetup> CopyBrachyPlanSetupAsync(IBrachyPlanSetup sourcePlan, System.Text.StringBuilder outputDiagnostics);
        Task<IBrachyPlanSetup> CopyBrachyPlanSetupAsync(IBrachyPlanSetup sourcePlan, IStructureSet structureset, System.Text.StringBuilder outputDiagnostics);
        Task<IPlanSetup> CopyPlanSetupAsync(IPlanSetup sourcePlan);
        Task<IPlanSetup> CopyPlanSetupAsync(IPlanSetup sourcePlan, IImage targetImage, System.Text.StringBuilder outputDiagnostics);
        Task<IPlanSetup> CopyPlanSetupAsync(IPlanSetup sourcePlan, IImage targetImage, IRegistration registration, System.Text.StringBuilder outputDiagnostics);
        Task<IPlanSetup> CopyPlanSetupAsync(IPlanSetup sourcePlan, IStructureSet structureset, System.Text.StringBuilder outputDiagnostics);
        Task<bool> IsCompletedAsync();
        Task RemovePlanSetupAsync(IPlanSetup planSetup);
        Task RemovePlanSumAsync(IPlanSum planSum);

        // --- RunAsync --- //
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
