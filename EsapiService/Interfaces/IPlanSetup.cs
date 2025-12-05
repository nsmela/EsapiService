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
        Task GetProtocolPrescriptionsAndMeasuresAsync(System.Collections.Generic.List<VMS.TPS.Common.Model.API.ProtocolPhasePrescription> prescriptions, System.Collections.Generic.List<VMS.TPS.Common.Model.API.ProtocolPhaseMeasure> measures);
        Task<IReferencePoint> AddReferencePointAsync(bool target, System.Nullable<VMS.TPS.Common.Model.Types.VVector> location, string id);
        Task<(bool Result, System.Collections.Generic.List<VMS.TPS.Common.Model.Types.PlanValidationResultEsapiDetail> validationResults)> IsValidForPlanApprovalAsync();
        Task<IPlanUncertainty> AddPlanUncertaintyWithParametersAsync(VMS.TPS.Common.Model.Types.PlanUncertaintyType uncertaintyType, bool planSpecificUncertainty, double HUConversionError, VMS.TPS.Common.Model.Types.VVector isocenterShift);
        Task SetTreatmentOrderAsync(System.Collections.Generic.IEnumerable<VMS.TPS.Common.Model.API.Beam> orderedBeams);
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task AddReferencePointAsync(VMS.TPS.Common.Model.API.ReferencePoint refPoint);
        Task ClearCalculationModelAsync(VMS.TPS.Common.Model.Types.CalculationType calculationType);
        Task<string> GetCalculationModelAsync(VMS.TPS.Common.Model.Types.CalculationType calculationType);
        Task<(bool Result, string optionValue)> GetCalculationOptionAsync(string calculationModel, string optionName);
        Task<System.Collections.Generic.Dictionary<string, string>> GetCalculationOptionsAsync(string calculationModel);
        Task<string> GetDvhEstimationModelNameAsync();
        Task<bool> IsEntireBodyAndBolusesCoveredByCalculationAreaAsync();
        Task MoveToCourseAsync(VMS.TPS.Common.Model.API.Course destinationCourse);
        Task RemoveReferencePointAsync(VMS.TPS.Common.Model.API.ReferencePoint refPoint);
        Task SetCalculationModelAsync(VMS.TPS.Common.Model.Types.CalculationType calculationType, string model);
        Task<bool> SetCalculationOptionAsync(string calculationModel, string optionName, string optionValue);
        Task SetPrescriptionAsync(int numberOfFractions, VMS.TPS.Common.Model.Types.DoseValue dosePerFraction, double treatmentPercentage);
        Task<bool> SetTargetStructureIfNoDoseAsync(VMS.TPS.Common.Model.API.Structure newTargetStructure, System.Text.StringBuilder errorHint);
        string Id { get; }
        Task SetIdAsync(string value);
        string Name { get; }
        Task SetNameAsync(string value);
        string Comment { get; }
        Task SetCommentAsync(string value);
        double PlanNormalizationValue { get; }
        Task SetPlanNormalizationValueAsync(double value);
        System.Collections.Generic.IReadOnlyList<IPlanUncertainty> PlanUncertainties { get; }
        System.Collections.Generic.IReadOnlyList<string> PlanObjectiveStructures { get; }
        System.Collections.Generic.IReadOnlyList<VMS.TPS.Common.Model.Types.ApprovalHistoryEntry> ApprovalHistory { get; }
        System.Collections.Generic.IReadOnlyList<VMS.TPS.Common.Model.Types.ApprovalHistoryEntry> ApprovalHistoryLocalized { get; }
        VMS.TPS.Common.Model.Types.DoseValue DosePerFractionInPrimaryRefPoint { get; }
        VMS.TPS.Common.Model.Types.DoseValue PrescribedDosePerFraction { get; }
        double PrescribedPercentage { get; }
        VMS.TPS.Common.Model.Types.DoseValue TotalPrescribedDose { get; }
        System.Collections.Generic.IReadOnlyList<IApplicationScriptLog> ApplicationScriptLogs { get; }
        VMS.TPS.Common.Model.Types.PlanSetupApprovalStatus ApprovalStatus { get; }
        string ApprovalStatusAsString { get; }
        Task<IPlanningItem> GetBaseDosePlanningItemAsync();
        Task SetBaseDosePlanningItemAsync(IPlanningItem value);
        System.Collections.Generic.IReadOnlyList<IBeam> Beams { get; }
        System.Collections.Generic.IReadOnlyList<IBeam> BeamsInTreatmentOrder { get; }
        string CreationUserName { get; }
        string DBKey { get; }
        VMS.TPS.Common.Model.Types.DoseValue DosePerFraction { get; }
        System.Collections.Generic.IReadOnlyList<IEstimatedDVH> DVHEstimates { get; }
        string ElectronCalculationModel { get; }
        System.Collections.Generic.Dictionary<string, string> ElectronCalculationOptions { get; }
        string IntegrityHash { get; }
        bool IsDoseValid { get; }
        bool IsTreated { get; }
        System.Collections.Generic.IReadOnlyList<int> NumberOfFractions { get; }
        Task<IOptimizationSetup> GetOptimizationSetupAsync();
        Task<IPatientSupportDevice> GetPatientSupportDeviceAsync();
        string PhotonCalculationModel { get; }
        System.Collections.Generic.Dictionary<string, string> PhotonCalculationOptions { get; }
        string PlanIntent { get; }
        bool PlanIsInTreatment { get; }
        VMS.TPS.Common.Model.Types.DoseValue PlannedDosePerFraction { get; }
        string PlanningApprovalDate { get; }
        string PlanningApprover { get; }
        string PlanningApproverDisplayName { get; }
        string PlanNormalizationMethod { get; }
        VMS.TPS.Common.Model.Types.VVector PlanNormalizationPoint { get; }
        VMS.TPS.Common.Model.Types.PlanType PlanType { get; }
        Task<IPlanSetup> GetPredecessorPlanAsync();
        string PredecessorPlanUID { get; }
        Task<IReferencePoint> GetPrimaryReferencePointAsync();
        string ProtocolID { get; }
        string ProtocolPhaseID { get; }
        string ProtonCalculationModel { get; }
        System.Collections.Generic.Dictionary<string, string> ProtonCalculationOptions { get; }
        System.Collections.Generic.IReadOnlyList<IReferencePoint> ReferencePoints { get; }
        Task<IRTPrescription> GetRTPrescriptionAsync();
        Task<ISeries> GetSeriesAsync();
        string SeriesUID { get; }
        string TargetVolumeID { get; }
        VMS.TPS.Common.Model.Types.DoseValue TotalDose { get; }
        string TreatmentApprovalDate { get; }
        string TreatmentApprover { get; }
        string TreatmentApproverDisplayName { get; }
        VMS.TPS.Common.Model.Types.PatientOrientation TreatmentOrientation { get; }
        string TreatmentOrientationAsString { get; }
        double TreatmentPercentage { get; }
        System.Collections.Generic.IReadOnlyList<IPlanTreatmentSession> TreatmentSessions { get; }
        string UID { get; }
        bool UseGating { get; }
        Task SetUseGatingAsync(bool value);
        Task<IPlanSetup> GetVerifiedPlanAsync();

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
