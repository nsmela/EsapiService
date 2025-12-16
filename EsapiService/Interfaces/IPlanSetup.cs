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
    public interface IPlanSetup : IPlanningItem
    {
        // --- Simple Properties --- //
        double PlanNormalizationValue { get; } // simple property
        Task SetPlanNormalizationValueAsync(double value);
        PlanSetupApprovalStatus ApprovalStatus { get; } // simple property
        string ApprovalStatusAsString { get; } // simple property
        string CreationUserName { get; } // simple property
        string DBKey { get; } // simple property
        DoseValue DosePerFraction { get; } // simple property
        string ElectronCalculationModel { get; } // simple property
        Dictionary<string, string> ElectronCalculationOptions { get; } // simple property
        string IntegrityHash { get; } // simple property
        bool IsDoseValid { get; } // simple property
        bool IsTreated { get; } // simple property
        int? NumberOfFractions { get; } // simple property
        string PhotonCalculationModel { get; } // simple property
        Dictionary<string, string> PhotonCalculationOptions { get; } // simple property
        string PlanIntent { get; } // simple property
        bool PlanIsInTreatment { get; } // simple property
        DoseValue PlannedDosePerFraction { get; } // simple property
        string PlanningApprovalDate { get; } // simple property
        string PlanningApprover { get; } // simple property
        string PlanningApproverDisplayName { get; } // simple property
        string PlanNormalizationMethod { get; } // simple property
        VVector PlanNormalizationPoint { get; } // simple property
        PlanType PlanType { get; } // simple property
        string PredecessorPlanUID { get; } // simple property
        string ProtocolID { get; } // simple property
        string ProtocolPhaseID { get; } // simple property
        string ProtonCalculationModel { get; } // simple property
        Dictionary<string, string> ProtonCalculationOptions { get; } // simple property
        string SeriesUID { get; } // simple property
        string TargetVolumeID { get; } // simple property
        DoseValue TotalDose { get; } // simple property
        string TreatmentApprovalDate { get; } // simple property
        string TreatmentApprover { get; } // simple property
        string TreatmentApproverDisplayName { get; } // simple property
        PatientOrientation TreatmentOrientation { get; } // simple property
        string TreatmentOrientationAsString { get; } // simple property
        double TreatmentPercentage { get; } // simple property
        string UID { get; } // simple property
        bool UseGating { get; } // simple property
        Task SetUseGatingAsync(bool value);

        // --- Accessors --- //
        Task<IPlanningItem> GetBaseDosePlanningItemAsync(); // read complex property
        Task SetBaseDosePlanningItemAsync(IPlanningItem value); // write complex property
        Task<IOptimizationSetup> GetOptimizationSetupAsync(); // read complex property
        Task<IPatientSupportDevice> GetPatientSupportDeviceAsync(); // read complex property
        Task<IPlanSetup> GetPredecessorPlanAsync(); // read complex property
        Task<IReferencePoint> GetPrimaryReferencePointAsync(); // read complex property
        Task<IRTPrescription> GetRTPrescriptionAsync(); // read complex property
        Task<ISeries> GetSeriesAsync(); // read complex property
        Task<IPlanSetup> GetVerifiedPlanAsync(); // read complex property

        // --- Collections --- //
        Task<IReadOnlyList<IPlanUncertainty>> GetPlanUncertaintiesAsync(); // collection proeprty context
        Task<IReadOnlyList<IApplicationScriptLog>> GetApplicationScriptLogsAsync(); // collection proeprty context
        Task<IReadOnlyList<IBeam>> GetBeamsAsync(); // collection proeprty context
        Task<IReadOnlyList<IBeam>> GetBeamsInTreatmentOrderAsync(); // collection proeprty context
        Task<IReadOnlyList<IEstimatedDVH>> GetDVHEstimatesAsync(); // collection proeprty context
        Task<IReadOnlyList<IReferencePoint>> GetReferencePointsAsync(); // collection proeprty context
        Task<IReadOnlyList<IPlanTreatmentSession>> GetTreatmentSessionsAsync(); // collection proeprty context

        // --- Methods --- //
        Task<(IReadOnlyList<IProtocolPhasePrescription> prescriptions, IReadOnlyList<IProtocolPhaseMeasure> measures)> GetProtocolPrescriptionsAndMeasuresAsync(IReadOnlyList<IProtocolPhasePrescription> prescriptions, IReadOnlyList<IProtocolPhaseMeasure> measures); // out/ref parameter method
        Task<IReferencePoint> AddReferencePointAsync(bool target, VVector? location, string id); // complex method
        Task<(bool result, List<PlanValidationResultEsapiDetail> validationResults)> IsValidForPlanApprovalAsync(); // out/ref parameter method
        Task<IPlanUncertainty> AddPlanUncertaintyWithParametersAsync(PlanUncertaintyType uncertaintyType, bool planSpecificUncertainty, double HUConversionError, VVector isocenterShift); // complex method
        Task SetTreatmentOrderAsync(IReadOnlyList<IBeam> orderedBeams); // void method
        Task AddReferencePointAsync(IReferencePoint refPoint); // void method
        Task ClearCalculationModelAsync(CalculationType calculationType); // void method
        Task<string> GetCalculationModelAsync(CalculationType calculationType); // simple method
        Task<(bool result, string optionValue)> GetCalculationOptionAsync(string calculationModel, string optionName); // out/ref parameter method
        Task<Dictionary<string, string>> GetCalculationOptionsAsync(string calculationModel); // simple method
        Task<string> GetDvhEstimationModelNameAsync(); // simple method
        Task<bool> IsEntireBodyAndBolusesCoveredByCalculationAreaAsync(); // simple method
        Task MoveToCourseAsync(ICourse destinationCourse); // void method
        Task RemoveReferencePointAsync(IReferencePoint refPoint); // void method
        Task SetCalculationModelAsync(CalculationType calculationType, string model); // void method
        Task<bool> SetCalculationOptionAsync(string calculationModel, string optionName, string optionValue); // simple method
        Task SetPrescriptionAsync(int numberOfFractions, DoseValue dosePerFraction, double treatmentPercentage); // void method
        Task<bool> SetTargetStructureIfNoDoseAsync(IStructure newTargetStructure, System.Text.StringBuilder errorHint); // simple method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.PlanSetup object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanSetup> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.PlanSetup object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanSetup, T> func);

        /* --- Skipped Members (Not generated) ---
           - Id: Shadows member in wrapped base class
           - Name: Shadows member in wrapped base class
           - Comment: Shadows member in wrapped base class
           - PlanObjectiveStructures: No matching factory found (Not Implemented)
           - ApprovalHistory: No matching factory found (Not Implemented)
           - ApprovalHistoryLocalized: No matching factory found (Not Implemented)
        */
    }
}
