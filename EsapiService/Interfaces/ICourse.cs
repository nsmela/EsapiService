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
        DateTime? CompletedDateTime { get; } // simple property
        string Intent { get; } // simple property
        DateTime? StartDateTime { get; } // simple property

        // --- Accessors --- //
        Task<IPatient> GetPatientAsync(); // read complex property

        // --- Collections --- //
        Task<IReadOnlyList<IExternalPlanSetup>> GetExternalPlanSetupsAsync(); // collection proeprty context
        Task<IReadOnlyList<IBrachyPlanSetup>> GetBrachyPlanSetupsAsync(); // collection proeprty context
        Task<IReadOnlyList<IIonPlanSetup>> GetIonPlanSetupsAsync(); // collection proeprty context
        Task<IReadOnlyList<IDiagnosis>> GetDiagnosesAsync(); // collection proeprty context
        Task<IReadOnlyList<IPlanSetup>> GetPlanSetupsAsync(); // collection proeprty context
        Task<IReadOnlyList<IPlanSum>> GetPlanSumsAsync(); // collection proeprty context
        Task<IReadOnlyList<ITreatmentPhase>> GetTreatmentPhasesAsync(); // collection proeprty context
        Task<IReadOnlyList<ITreatmentSession>> GetTreatmentSessionsAsync(); // collection proeprty context

        // --- Methods --- //
        Task<IIonPlanSetup> AddIonPlanSetupAsVerificationPlanAsync(IStructureSet structureSet, string patientSupportDeviceId, IIonPlanSetup verifiedPlan); // complex method
        Task<IExternalPlanSetup> AddExternalPlanSetupAsync(IStructureSet structureSet); // complex method
        Task<IExternalPlanSetup> AddExternalPlanSetupAsVerificationPlanAsync(IStructureSet structureSet, IExternalPlanSetup verifiedPlan); // complex method
        Task<IIonPlanSetup> AddIonPlanSetupAsync(IStructureSet structureSet, string patientSupportDeviceId); // complex method
        Task<bool> CanAddPlanSetupAsync(IStructureSet structureSet); // simple method
        Task<bool> CanRemovePlanSetupAsync(IPlanSetup planSetup); // simple method
        Task<IPlanSetup> CopyPlanSetupAsync(IPlanSetup sourcePlan); // complex method
        Task<IPlanSetup> CopyPlanSetupAsync(IPlanSetup sourcePlan, IStructureSet structureset, System.Text.StringBuilder outputDiagnostics); // complex method
        Task RemovePlanSetupAsync(IPlanSetup planSetup); // void method

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
           - Id: Shadows base member in wrapped base class
        */
    }
}
