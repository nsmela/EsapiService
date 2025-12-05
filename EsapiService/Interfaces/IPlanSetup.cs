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
    public interface IPlanSetup : IPlanningItem
    {
        // --- Simple Properties --- //
        string Id { get; }
        Task SetIdAsync(string value);
        string Name { get; }
        Task SetNameAsync(string value);
        string Comment { get; }
        Task SetCommentAsync(string value);
        double PlanNormalizationValue { get; }
        Task SetPlanNormalizationValueAsync(double value);
        DoseValue DosePerFractionInPrimaryRefPoint { get; }
        DoseValue PrescribedDosePerFraction { get; }
        double PrescribedPercentage { get; }
        DoseValue TotalPrescribedDose { get; }
        PlanSetupApprovalStatus ApprovalStatus { get; }
        string ApprovalStatusAsString { get; }
        string CreationUserName { get; }
        string DBKey { get; }
        DoseValue DosePerFraction { get; }
        string ElectronCalculationModel { get; }
        Dictionary<string, string> ElectronCalculationOptions { get; }
        string IntegrityHash { get; }
        bool IsDoseValid { get; }
        bool IsTreated { get; }
        string PhotonCalculationModel { get; }
        Dictionary<string, string> PhotonCalculationOptions { get; }
        string PlanIntent { get; }
        bool PlanIsInTreatment { get; }
        DoseValue PlannedDosePerFraction { get; }
        string PlanningApprovalDate { get; }
        string PlanningApprover { get; }
        string PlanningApproverDisplayName { get; }
        string PlanNormalizationMethod { get; }
        VVector PlanNormalizationPoint { get; }
        PlanType PlanType { get; }
        string PredecessorPlanUID { get; }
        string ProtocolID { get; }
        string ProtocolPhaseID { get; }
        string ProtonCalculationModel { get; }
        Dictionary<string, string> ProtonCalculationOptions { get; }
        string SeriesUID { get; }
        string TargetVolumeID { get; }
        DoseValue TotalDose { get; }
        string TreatmentApprovalDate { get; }
        string TreatmentApprover { get; }
        string TreatmentApproverDisplayName { get; }
        PatientOrientation TreatmentOrientation { get; }
        string TreatmentOrientationAsString { get; }
        double TreatmentPercentage { get; }
        string UID { get; }
        bool UseGating { get; }
        Task SetUseGatingAsync(bool value);

        // --- Accessors --- //
        Task<IPlanningItem> GetBaseDosePlanningItemAsync();
        Task SetBaseDosePlanningItemAsync(IPlanningItem value);
        Task<IOptimizationSetup> GetOptimizationSetupAsync();
        Task<IPatientSupportDevice> GetPatientSupportDeviceAsync();
        Task<IPlanSetup> GetPredecessorPlanAsync();
        Task<IReferencePoint> GetPrimaryReferencePointAsync();
        Task<IRTPrescription> GetRTPrescriptionAsync();
        Task<ISeries> GetSeriesAsync();
        Task<IPlanSetup> GetVerifiedPlanAsync();

        // --- Collections --- //
        Task<IReadOnlyList<IPlanUncertainty>> GetPlanUncertaintiesAsync();
        IReadOnlyList<string> PlanObjectiveStructures { get; }
        IReadOnlyList<ApprovalHistoryEntry> ApprovalHistory { get; }
        IReadOnlyList<ApprovalHistoryEntry> ApprovalHistoryLocalized { get; }
        Task<IReadOnlyList<IApplicationScriptLog>> GetApplicationScriptLogsAsync();
        Task<IReadOnlyList<IBeam>> GetBeamsAsync();
        Task<IReadOnlyList<IBeam>> GetBeamsInTreatmentOrderAsync();
        Task<IReadOnlyList<IEstimatedDVH>> GetDVHEstimatesAsync();
        IReadOnlyList<int> NumberOfFractions { get; }
        Task<IReadOnlyList<IReferencePoint>> GetReferencePointsAsync();
        Task<IReadOnlyList<IPlanTreatmentSession>> GetTreatmentSessionsAsync();

        // --- Methods --- //
        Task<(IReadOnlyList<IProtocolPhasePrescription> prescriptions, IReadOnlyList<IProtocolPhaseMeasure> measures)> GetProtocolPrescriptionsAndMeasuresAsync(IReadOnlyList<IProtocolPhasePrescription> prescriptions, IReadOnlyList<IProtocolPhaseMeasure> measures);
        Task<IReferencePoint> AddReferencePointAsync(bool target, Nullable<VVector> location, string id);
        Task<(bool Result, List<PlanValidationResultEsapiDetail> validationResults)> IsValidForPlanApprovalAsync();
        Task<IPlanUncertainty> AddPlanUncertaintyWithParametersAsync(PlanUncertaintyType uncertaintyType, bool planSpecificUncertainty, double HUConversionError, VVector isocenterShift);
        Task SetTreatmentOrderAsync(IReadOnlyList<IBeam> orderedBeams);
        Task AddReferencePointAsync(IReferencePoint refPoint);
        Task ClearCalculationModelAsync(CalculationType calculationType);
        Task<string> GetCalculationModelAsync(CalculationType calculationType);
        Task<(bool Result, string optionValue)> GetCalculationOptionAsync(string calculationModel, string optionName);
        Task<Dictionary<string, string>> GetCalculationOptionsAsync(string calculationModel);
        Task<string> GetDvhEstimationModelNameAsync();
        Task<bool> IsEntireBodyAndBolusesCoveredByCalculationAreaAsync();
        Task MoveToCourseAsync(ICourse destinationCourse);
        Task RemoveReferencePointAsync(IReferencePoint refPoint);
        Task SetCalculationModelAsync(CalculationType calculationType, string model);
        Task<bool> SetCalculationOptionAsync(string calculationModel, string optionName, string optionValue);
        Task SetPrescriptionAsync(int numberOfFractions, DoseValue dosePerFraction, double treatmentPercentage);
        Task<bool> SetTargetStructureIfNoDoseAsync(IStructure newTargetStructure, Text.StringBuilder errorHint);

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.PlanSetup object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanSetup> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.PlanSetup object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanSetup, T> func);
    }
}
