using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
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

            Id = inner.Id;
            Name = inner.Name;
            Comment = inner.Comment;
            PlanNormalizationValue = inner.PlanNormalizationValue;
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
            UseGating = inner.UseGating;
        }


        public async Task<(IReadOnlyList<IProtocolPhasePrescription> prescriptions, IReadOnlyList<IProtocolPhaseMeasure> measures)> GetProtocolPrescriptionsAndMeasuresAsync(IReadOnlyList<IProtocolPhasePrescription> prescriptions, IReadOnlyList<IProtocolPhaseMeasure> measures)
        {
            List<ProtocolPhasePrescription> prescriptions_temp = prescriptions._inner;
            List<ProtocolPhaseMeasure> measures_temp = measures._inner;
            await _service.RunAsync(() => _inner.GetProtocolPrescriptionsAndMeasures(ref prescriptions_temp, ref measures_temp));
            return (prescriptions_temp is null ? null : new IReadOnlyList<AsyncProtocolPhasePrescription>(prescriptions_temp, _service), measures_temp is null ? null : new IReadOnlyList<AsyncProtocolPhaseMeasure>(measures_temp, _service));
        }

        public async Task<IReferencePoint> AddReferencePointAsync(bool target, Nullable<VVector> location, string id)
        {
            return await _service.RunAsync(() => 
                _inner.AddReferencePoint(target, location, id) is var result && result is null ? null : new AsyncReferencePoint(result, _service));
        }


        public async Task<(bool Result, List<PlanValidationResultEsapiDetail> validationResults)> IsValidForPlanApprovalAsync()
        {
            List<PlanValidationResultEsapiDetail> validationResults_temp;
            var result = await _service.RunAsync(() => _inner.IsValidForPlanApproval(out validationResults_temp));
            return (result, validationResults_temp);
        }

        public async Task<IPlanUncertainty> AddPlanUncertaintyWithParametersAsync(PlanUncertaintyType uncertaintyType, bool planSpecificUncertainty, double HUConversionError, VVector isocenterShift)
        {
            return await _service.RunAsync(() => 
                _inner.AddPlanUncertaintyWithParameters(uncertaintyType, planSpecificUncertainty, HUConversionError, isocenterShift) is var result && result is null ? null : new AsyncPlanUncertainty(result, _service));
        }


        public Task SetTreatmentOrderAsync(IReadOnlyList<IBeam> orderedBeams) => _service.RunAsync(() => _inner.SetTreatmentOrder(orderedBeams));

        public Task AddReferencePointAsync(IReferencePoint refPoint) => _service.RunAsync(() => _inner.AddReferencePoint(refPoint));

        public Task ClearCalculationModelAsync(CalculationType calculationType) => _service.RunAsync(() => _inner.ClearCalculationModel(calculationType));

        public Task<string> GetCalculationModelAsync(CalculationType calculationType) => _service.RunAsync(() => _inner.GetCalculationModel(calculationType));

        public async Task<(bool Result, string optionValue)> GetCalculationOptionAsync(string calculationModel, string optionName)
        {
            string optionValue_temp;
            var result = await _service.RunAsync(() => _inner.GetCalculationOption(calculationModel, optionName, out optionValue_temp));
            return (result, optionValue_temp);
        }

        public Task<Dictionary<string, string>> GetCalculationOptionsAsync(string calculationModel) => _service.RunAsync(() => _inner.GetCalculationOptions(calculationModel));

        public Task<string> GetDvhEstimationModelNameAsync() => _service.RunAsync(() => _inner.GetDvhEstimationModelName());

        public Task<bool> IsEntireBodyAndBolusesCoveredByCalculationAreaAsync() => _service.RunAsync(() => _inner.IsEntireBodyAndBolusesCoveredByCalculationArea());

        public Task MoveToCourseAsync(ICourse destinationCourse) => _service.RunAsync(() => _inner.MoveToCourse(destinationCourse));

        public Task RemoveReferencePointAsync(IReferencePoint refPoint) => _service.RunAsync(() => _inner.RemoveReferencePoint(refPoint));

        public Task SetCalculationModelAsync(CalculationType calculationType, string model) => _service.RunAsync(() => _inner.SetCalculationModel(calculationType, model));

        public Task<bool> SetCalculationOptionAsync(string calculationModel, string optionName, string optionValue) => _service.RunAsync(() => _inner.SetCalculationOption(calculationModel, optionName, optionValue));

        public Task SetPrescriptionAsync(int numberOfFractions, DoseValue dosePerFraction, double treatmentPercentage) => _service.RunAsync(() => _inner.SetPrescription(numberOfFractions, dosePerFraction, treatmentPercentage));

        public Task<bool> SetTargetStructureIfNoDoseAsync(IStructure newTargetStructure, Text.StringBuilder errorHint) => _service.RunAsync(() => _inner.SetTargetStructureIfNoDose(newTargetStructure, errorHint));

        public string Id { get; private set; }
        public async Task SetIdAsync(string value)
        {
            Id = await _service.RunAsync(() =>
            {
                _inner.Id = value;
                return _inner.Id;
            });
        }

        public string Name { get; private set; }
        public async Task SetNameAsync(string value)
        {
            Name = await _service.RunAsync(() =>
            {
                _inner.Name = value;
                return _inner.Name;
            });
        }

        public string Comment { get; private set; }
        public async Task SetCommentAsync(string value)
        {
            Comment = await _service.RunAsync(() =>
            {
                _inner.Comment = value;
                return _inner.Comment;
            });
        }

        public double PlanNormalizationValue { get; private set; }
        public async Task SetPlanNormalizationValueAsync(double value)
        {
            PlanNormalizationValue = await _service.RunAsync(() =>
            {
                _inner.PlanNormalizationValue = value;
                return _inner.PlanNormalizationValue;
            });
        }

        public async Task<IReadOnlyList<IPlanUncertainty>> GetPlanUncertaintiesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.PlanUncertainties?.Select(x => new AsyncPlanUncertainty(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<string>> GetPlanObjectiveStructuresAsync()
        {
            return await _service.RunAsync(() => _inner.PlanObjectiveStructures?.ToList());
        }


        public async Task<IReadOnlyList<ApprovalHistoryEntry>> GetApprovalHistoryAsync()
        {
            return await _service.RunAsync(() => _inner.ApprovalHistory?.ToList());
        }


        public async Task<IReadOnlyList<ApprovalHistoryEntry>> GetApprovalHistoryLocalizedAsync()
        {
            return await _service.RunAsync(() => _inner.ApprovalHistoryLocalized?.ToList());
        }


        public DoseValue DosePerFractionInPrimaryRefPoint { get; }

        public DoseValue PrescribedDosePerFraction { get; }

        public double PrescribedPercentage { get; }

        public DoseValue TotalPrescribedDose { get; }

        public async Task<IReadOnlyList<IApplicationScriptLog>> GetApplicationScriptLogsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.ApplicationScriptLogs?.Select(x => new AsyncApplicationScriptLog(x, _service)).ToList());
        }


        public PlanSetupApprovalStatus ApprovalStatus { get; }

        public string ApprovalStatusAsString { get; }

        public async Task<IPlanningItem> GetBaseDosePlanningItemAsync()
        {
            return await _service.RunAsync(() => 
                _inner.BaseDosePlanningItem is null ? null : new AsyncPlanningItem(_inner.BaseDosePlanningItem, _service));
        }

        public async Task SetBaseDosePlanningItemAsync(IPlanningItem value)
        {
            // Handle null assignment
            if (value is null)
            {
                await _service.RunAsync(() => _inner.BaseDosePlanningItem = null);
                return;
            }
            // Unwrap the interface to get the Varian object
            if (value is AsyncPlanningItem wrapper)
            {
                 await _service.RunAsync(() => _inner.BaseDosePlanningItem = wrapper._inner);
                 return;
            }
            throw new System.ArgumentException("Value must be of type AsyncPlanningItem");
        }

        public async Task<IReadOnlyList<IBeam>> GetBeamsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Beams?.Select(x => new AsyncBeam(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IBeam>> GetBeamsInTreatmentOrderAsync()
        {
            return await _service.RunAsync(() => 
                _inner.BeamsInTreatmentOrder?.Select(x => new AsyncBeam(x, _service)).ToList());
        }


        public string CreationUserName { get; }

        public string DBKey { get; }

        public DoseValue DosePerFraction { get; }

        public async Task<IReadOnlyList<IEstimatedDVH>> GetDVHEstimatesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.DVHEstimates?.Select(x => new AsyncEstimatedDVH(x, _service)).ToList());
        }


        public string ElectronCalculationModel { get; }

        public Dictionary<string, string> ElectronCalculationOptions { get; }

        public string IntegrityHash { get; }

        public bool IsDoseValid { get; }

        public bool IsTreated { get; }

        public async Task<IReadOnlyList<int>> GetNumberOfFractionsAsync()
        {
            return await _service.RunAsync(() => _inner.NumberOfFractions?.ToList());
        }


        public async Task<IOptimizationSetup> GetOptimizationSetupAsync()
        {
            return await _service.RunAsync(() => 
                _inner.OptimizationSetup is null ? null : new AsyncOptimizationSetup(_inner.OptimizationSetup, _service));
        }

        public async Task<IPatientSupportDevice> GetPatientSupportDeviceAsync()
        {
            return await _service.RunAsync(() => 
                _inner.PatientSupportDevice is null ? null : new AsyncPatientSupportDevice(_inner.PatientSupportDevice, _service));
        }

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

        public async Task<IPlanSetup> GetPredecessorPlanAsync()
        {
            return await _service.RunAsync(() => 
                _inner.PredecessorPlan is null ? null : new AsyncPlanSetup(_inner.PredecessorPlan, _service));
        }

        public string PredecessorPlanUID { get; }

        public async Task<IReferencePoint> GetPrimaryReferencePointAsync()
        {
            return await _service.RunAsync(() => 
                _inner.PrimaryReferencePoint is null ? null : new AsyncReferencePoint(_inner.PrimaryReferencePoint, _service));
        }

        public string ProtocolID { get; }

        public string ProtocolPhaseID { get; }

        public string ProtonCalculationModel { get; }

        public Dictionary<string, string> ProtonCalculationOptions { get; }

        public async Task<IReadOnlyList<IReferencePoint>> GetReferencePointsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.ReferencePoints?.Select(x => new AsyncReferencePoint(x, _service)).ToList());
        }


        public async Task<IRTPrescription> GetRTPrescriptionAsync()
        {
            return await _service.RunAsync(() => 
                _inner.RTPrescription is null ? null : new AsyncRTPrescription(_inner.RTPrescription, _service));
        }

        public async Task<ISeries> GetSeriesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Series is null ? null : new AsyncSeries(_inner.Series, _service));
        }

        public string SeriesUID { get; }

        public string TargetVolumeID { get; }

        public DoseValue TotalDose { get; }

        public string TreatmentApprovalDate { get; }

        public string TreatmentApprover { get; }

        public string TreatmentApproverDisplayName { get; }

        public PatientOrientation TreatmentOrientation { get; }

        public string TreatmentOrientationAsString { get; }

        public double TreatmentPercentage { get; }

        public async Task<IReadOnlyList<IPlanTreatmentSession>> GetTreatmentSessionsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.TreatmentSessions?.Select(x => new AsyncPlanTreatmentSession(x, _service)).ToList());
        }


        public string UID { get; }

        public bool UseGating { get; private set; }
        public async Task SetUseGatingAsync(bool value)
        {
            UseGating = await _service.RunAsync(() =>
            {
                _inner.UseGating = value;
                return _inner.UseGating;
            });
        }

        public async Task<IPlanSetup> GetVerifiedPlanAsync()
        {
            return await _service.RunAsync(() => 
                _inner.VerifiedPlan is null ? null : new AsyncPlanSetup(_inner.VerifiedPlan, _service));
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanSetup> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanSetup, T> func) => _service.RunAsync(() => func(_inner));
    }
}
