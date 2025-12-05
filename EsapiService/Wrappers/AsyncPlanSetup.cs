    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
    public class AsyncPlanSetup : IPlanSetup
    {
        internal readonly VMS.TPS.Common.Model.API.PlanSetup _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncPlanSetup(VMS.TPS.Common.Model.API.PlanSetup inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            DosePerFractionInPrimaryRefPoint = inner.DosePerFractionInPrimaryRefPoint;
            PrescribedDosePerFraction = inner.PrescribedDosePerFraction;
            PrescribedPercentage = inner.PrescribedPercentage;
            TotalPrescribedDose = inner.TotalPrescribedDose;
            ApprovalStatus = inner.ApprovalStatus;
            ApprovalStatusAsString = inner.ApprovalStatusAsString;
            CreationUserName = inner.CreationUserName;
            DBKey = inner.DBKey;
            DosePerFraction = inner.DosePerFraction;
            ElectronCalculationModel = inner.ElectronCalculationModel;
            ElectronCalculationOptions = inner.ElectronCalculationOptions;
            IntegrityHash = inner.IntegrityHash;
            IsDoseValid = inner.IsDoseValid;
            IsTreated = inner.IsTreated;
            PhotonCalculationModel = inner.PhotonCalculationModel;
            PhotonCalculationOptions = inner.PhotonCalculationOptions;
            PlanIntent = inner.PlanIntent;
            PlanIsInTreatment = inner.PlanIsInTreatment;
            PlannedDosePerFraction = inner.PlannedDosePerFraction;
            PlanningApprovalDate = inner.PlanningApprovalDate;
            PlanningApprover = inner.PlanningApprover;
            PlanningApproverDisplayName = inner.PlanningApproverDisplayName;
            PlanNormalizationMethod = inner.PlanNormalizationMethod;
            PlanNormalizationPoint = inner.PlanNormalizationPoint;
            PlanType = inner.PlanType;
            PredecessorPlanUID = inner.PredecessorPlanUID;
            ProtocolID = inner.ProtocolID;
            ProtocolPhaseID = inner.ProtocolPhaseID;
            ProtonCalculationModel = inner.ProtonCalculationModel;
            ProtonCalculationOptions = inner.ProtonCalculationOptions;
            SeriesUID = inner.SeriesUID;
            TargetVolumeID = inner.TargetVolumeID;
            TotalDose = inner.TotalDose;
            TreatmentApprovalDate = inner.TreatmentApprovalDate;
            TreatmentApprover = inner.TreatmentApprover;
            TreatmentApproverDisplayName = inner.TreatmentApproverDisplayName;
            TreatmentOrientation = inner.TreatmentOrientation;
            TreatmentOrientationAsString = inner.TreatmentOrientationAsString;
            TreatmentPercentage = inner.TreatmentPercentage;
            UID = inner.UID;
        }

        public async System.Threading.Tasks.Task<(IReadOnlyList<IProtocolPhasePrescription> prescriptions, IReadOnlyList<IProtocolPhaseMeasure> measures)> GetProtocolPrescriptionsAndMeasuresAsync(IReadOnlyList<IProtocolPhasePrescription> prescriptions, IReadOnlyList<IProtocolPhaseMeasure> measures)
        {
            List<ProtocolPhasePrescription> prescriptions_temp = prescriptions._inner;
            List<ProtocolPhaseMeasure> measures_temp = measures._inner;
            var result = await _service.RunAsync(() => _inner.GetProtocolPrescriptionsAndMeasures(ref prescriptions_temp, ref measures_temp));
            return (prescriptions_temp is null ? null : new IReadOnlyList<AsyncProtocolPhasePrescription>(prescriptions_temp, _service), measures_temp is null ? null : new IReadOnlyList<AsyncProtocolPhaseMeasure>(measures_temp, _service));
        }
        public IReferencePoint AddReferencePoint(bool target, Nullable<VVector> location, string id) => _inner.AddReferencePoint(target, location, id) is var result && result is null ? null : new AsyncReferencePoint(result, _service);
        public async System.Threading.Tasks.Task<(bool Result, List<PlanValidationResultEsapiDetail> validationResults)> IsValidForPlanApprovalAsync()
        {
            List<PlanValidationResultEsapiDetail> validationResults_temp;
            var result = await _service.RunAsync(() => _inner.IsValidForPlanApproval(out validationResults_temp));
            return (result, validationResults_temp);
        }
        public IPlanUncertainty AddPlanUncertaintyWithParameters(PlanUncertaintyType uncertaintyType, bool planSpecificUncertainty, double HUConversionError, VVector isocenterShift) => _inner.AddPlanUncertaintyWithParameters(uncertaintyType, planSpecificUncertainty, HUConversionError, isocenterShift) is var result && result is null ? null : new AsyncPlanUncertainty(result, _service);
        public void SetTreatmentOrder(IReadOnlyList<IBeam> orderedBeams) => _inner.SetTreatmentOrder(orderedBeams);
        public void AddReferencePoint(IReferencePoint refPoint) => _inner.AddReferencePoint(refPoint);
        public void ClearCalculationModel(CalculationType calculationType) => _inner.ClearCalculationModel(calculationType);
        public string GetCalculationModel(CalculationType calculationType) => _inner.GetCalculationModel(calculationType);
        public async System.Threading.Tasks.Task<(bool Result, string optionValue)> GetCalculationOptionAsync(string calculationModel, string optionName)
        {
            string optionValue_temp;
            var result = await _service.RunAsync(() => _inner.GetCalculationOption(calculationModel, optionName, out optionValue_temp));
            return (result, optionValue_temp);
        }
        public Dictionary<string, string> GetCalculationOptions(string calculationModel) => _inner.GetCalculationOptions(calculationModel);
        public string GetDvhEstimationModelName() => _inner.GetDvhEstimationModelName();
        public bool IsEntireBodyAndBolusesCoveredByCalculationArea() => _inner.IsEntireBodyAndBolusesCoveredByCalculationArea();
        public void MoveToCourse(ICourse destinationCourse) => _inner.MoveToCourse(destinationCourse);
        public void RemoveReferencePoint(IReferencePoint refPoint) => _inner.RemoveReferencePoint(refPoint);
        public void SetCalculationModel(CalculationType calculationType, string model) => _inner.SetCalculationModel(calculationType, model);
        public bool SetCalculationOption(string calculationModel, string optionName, string optionValue) => _inner.SetCalculationOption(calculationModel, optionName, optionValue);
        public void SetPrescription(int numberOfFractions, DoseValue dosePerFraction, double treatmentPercentage) => _inner.SetPrescription(numberOfFractions, dosePerFraction, treatmentPercentage);
        public bool SetTargetStructureIfNoDose(IStructure newTargetStructure, Text.StringBuilder errorHint) => _inner.SetTargetStructureIfNoDose(newTargetStructure, errorHint);
        public string Id => _inner.Id;
        public async Task SetIdAsync(string value) => _service.RunAsync(() => _inner.Id = value);
        public string Name => _inner.Name;
        public async Task SetNameAsync(string value) => _service.RunAsync(() => _inner.Name = value);
        public string Comment => _inner.Comment;
        public async Task SetCommentAsync(string value) => _service.RunAsync(() => _inner.Comment = value);
        public double PlanNormalizationValue => _inner.PlanNormalizationValue;
        public async Task SetPlanNormalizationValueAsync(double value) => _service.RunAsync(() => _inner.PlanNormalizationValue = value);
        public IReadOnlyList<IPlanUncertainty> PlanUncertainties => _inner.PlanUncertainties?.Select(x => new AsyncPlanUncertainty(x, _service)).ToList();
        public IReadOnlyList<string> PlanObjectiveStructures => _inner.PlanObjectiveStructures?.ToList();
        public IReadOnlyList<ApprovalHistoryEntry> ApprovalHistory => _inner.ApprovalHistory?.ToList();
        public IReadOnlyList<ApprovalHistoryEntry> ApprovalHistoryLocalized => _inner.ApprovalHistoryLocalized?.ToList();
        public DoseValue DosePerFractionInPrimaryRefPoint { get; }
        public DoseValue PrescribedDosePerFraction { get; }
        public double PrescribedPercentage { get; }
        public DoseValue TotalPrescribedDose { get; }
        public IReadOnlyList<IApplicationScriptLog> ApplicationScriptLogs => _inner.ApplicationScriptLogs?.Select(x => new AsyncApplicationScriptLog(x, _service)).ToList();
        public PlanSetupApprovalStatus ApprovalStatus { get; }
        public string ApprovalStatusAsString { get; }
        public IPlanningItem BaseDosePlanningItem => _inner.BaseDosePlanningItem is null ? null : new AsyncPlanningItem(_inner.BaseDosePlanningItem, _service);
        public System.Threading.Tasks.Task SetBaseDosePlanningItemAsync(IPlanningItem value)
        {
            // Unwrap the interface to get the Varian object
            if (value is AsyncPlanningItem wrapper)
            {
                 return _service.RunAsync(() => _inner.BaseDosePlanningItem = wrapper._inner);
            }
            throw new System.ArgumentException("Value must be of type AsyncPlanningItem");
        }

        public IReadOnlyList<IBeam> Beams => _inner.Beams?.Select(x => new AsyncBeam(x, _service)).ToList();
        public IReadOnlyList<IBeam> BeamsInTreatmentOrder => _inner.BeamsInTreatmentOrder?.Select(x => new AsyncBeam(x, _service)).ToList();
        public string CreationUserName { get; }
        public string DBKey { get; }
        public DoseValue DosePerFraction { get; }
        public IReadOnlyList<IEstimatedDVH> DVHEstimates => _inner.DVHEstimates?.Select(x => new AsyncEstimatedDVH(x, _service)).ToList();
        public string ElectronCalculationModel { get; }
        public Dictionary<string, string> ElectronCalculationOptions { get; }
        public string IntegrityHash { get; }
        public bool IsDoseValid { get; }
        public bool IsTreated { get; }
        public IReadOnlyList<int> NumberOfFractions => _inner.NumberOfFractions?.ToList();
        public IOptimizationSetup OptimizationSetup => _inner.OptimizationSetup is null ? null : new AsyncOptimizationSetup(_inner.OptimizationSetup, _service);

        public IPatientSupportDevice PatientSupportDevice => _inner.PatientSupportDevice is null ? null : new AsyncPatientSupportDevice(_inner.PatientSupportDevice, _service);

        public string PhotonCalculationModel { get; }
        public Dictionary<string, string> PhotonCalculationOptions { get; }
        public string PlanIntent { get; }
        public bool PlanIsInTreatment { get; }
        public DoseValue PlannedDosePerFraction { get; }
        public string PlanningApprovalDate { get; }
        public string PlanningApprover { get; }
        public string PlanningApproverDisplayName { get; }
        public string PlanNormalizationMethod { get; }
        public VVector PlanNormalizationPoint { get; }
        public PlanType PlanType { get; }
        public IPlanSetup PredecessorPlan => _inner.PredecessorPlan is null ? null : new AsyncPlanSetup(_inner.PredecessorPlan, _service);

        public string PredecessorPlanUID { get; }
        public IReferencePoint PrimaryReferencePoint => _inner.PrimaryReferencePoint is null ? null : new AsyncReferencePoint(_inner.PrimaryReferencePoint, _service);

        public string ProtocolID { get; }
        public string ProtocolPhaseID { get; }
        public string ProtonCalculationModel { get; }
        public Dictionary<string, string> ProtonCalculationOptions { get; }
        public IReadOnlyList<IReferencePoint> ReferencePoints => _inner.ReferencePoints?.Select(x => new AsyncReferencePoint(x, _service)).ToList();
        public IRTPrescription RTPrescription => _inner.RTPrescription is null ? null : new AsyncRTPrescription(_inner.RTPrescription, _service);

        public ISeries Series => _inner.Series is null ? null : new AsyncSeries(_inner.Series, _service);

        public string SeriesUID { get; }
        public string TargetVolumeID { get; }
        public DoseValue TotalDose { get; }
        public string TreatmentApprovalDate { get; }
        public string TreatmentApprover { get; }
        public string TreatmentApproverDisplayName { get; }
        public PatientOrientation TreatmentOrientation { get; }
        public string TreatmentOrientationAsString { get; }
        public double TreatmentPercentage { get; }
        public IReadOnlyList<IPlanTreatmentSession> TreatmentSessions => _inner.TreatmentSessions?.Select(x => new AsyncPlanTreatmentSession(x, _service)).ToList();
        public string UID { get; }
        public bool UseGating => _inner.UseGating;
        public async Task SetUseGatingAsync(bool value) => _service.RunAsync(() => _inner.UseGating = value);
        public IPlanSetup VerifiedPlan => _inner.VerifiedPlan is null ? null : new AsyncPlanSetup(_inner.VerifiedPlan, _service);


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanSetup> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanSetup, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
