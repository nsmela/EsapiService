using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncPlanSetup : AsyncPlanningItem, IPlanSetup, IEsapiWrapper<VMS.TPS.Common.Model.API.PlanSetup>
    {
        internal new readonly VMS.TPS.Common.Model.API.PlanSetup _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncPlanSetup(VMS.TPS.Common.Model.API.PlanSetup inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            PlanNormalizationValue = inner.PlanNormalizationValue;
            ApprovalStatus = inner.ApprovalStatus;
            ApprovalStatusAsString = inner.ApprovalStatusAsString;
            CreationUserName = inner.CreationUserName;
            DosePerFraction = inner.DosePerFraction;
            ElectronCalculationModel = inner.ElectronCalculationModel;
            ElectronCalculationOptions = inner.ElectronCalculationOptions;
            IntegrityHash = inner.IntegrityHash;
            IsDoseValid = inner.IsDoseValid;
            IsTreated = inner.IsTreated;
            NumberOfFractions = inner.NumberOfFractions;
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
            var postResult = await _service.PostAsync(context => {
                List<ProtocolPhasePrescription> prescriptions_temp = prescriptions?.Select(x => ((IEsapiWrapper<ProtocolPhasePrescription>)x).Inner).ToList();
                List<ProtocolPhaseMeasure> measures_temp = measures?.Select(x => ((IEsapiWrapper<ProtocolPhaseMeasure>)x).Inner).ToList();
                _inner.GetProtocolPrescriptionsAndMeasures(ref prescriptions_temp, ref measures_temp);
                return (prescriptions_temp, measures_temp);
            });
            return (postResult.Item1?.Select(x => new AsyncProtocolPhasePrescription(x, _service)).ToList(),
                    postResult.Item2?.Select(x => new AsyncProtocolPhaseMeasure(x, _service)).ToList());
        }


        public async Task<IReferencePoint> AddReferencePointAsync(bool target, VVector? location, string id)
        {
            return await _service.PostAsync(context => 
                _inner.AddReferencePoint(target, location, id) is var result && result is null ? null : new AsyncReferencePoint(result, _service));
        }


        public async Task<(bool result, List<PlanValidationResultEsapiDetail> validationResults)> IsValidForPlanApprovalAsync()
        {
            var postResult = await _service.PostAsync(context => {
                List<PlanValidationResultEsapiDetail> validationResults_temp = default(List<PlanValidationResultEsapiDetail>);
                var result = _inner.IsValidForPlanApproval(out validationResults_temp);
                return (result, validationResults_temp);
            });
            return (postResult.Item1,
                    postResult.Item2);
        }


        public async Task<IPlanUncertainty> AddPlanUncertaintyWithParametersAsync(PlanUncertaintyType uncertaintyType, bool planSpecificUncertainty, double HUConversionError, VVector isocenterShift)
        {
            return await _service.PostAsync(context => 
                _inner.AddPlanUncertaintyWithParameters(uncertaintyType, planSpecificUncertainty, HUConversionError, isocenterShift) is var result && result is null ? null : new AsyncPlanUncertainty(result, _service));
        }


        // Simple Void Method
        public Task SetTreatmentOrderAsync(IReadOnlyList<IBeam> orderedBeams) =>
            _service.PostAsync(context => _inner.SetTreatmentOrder((orderedBeams.Select(x => ((AsyncBeam)x)._inner))));

        // Simple Void Method
        public Task AddReferencePointAsync(IReferencePoint refPoint) =>
            _service.PostAsync(context => _inner.AddReferencePoint(((AsyncReferencePoint)refPoint)._inner));

        // Simple Void Method
        public Task ClearCalculationModelAsync(CalculationType calculationType) =>
            _service.PostAsync(context => _inner.ClearCalculationModel(calculationType));

        // Simple Method
        public Task<string> GetCalculationModelAsync(CalculationType calculationType) => 
            _service.PostAsync(context => _inner.GetCalculationModel(calculationType));

        public async Task<(bool result, string optionValue)> GetCalculationOptionAsync(string calculationModel, string optionName)
        {
            var postResult = await _service.PostAsync(context => {
                string optionValue_temp = default(string);
                var result = _inner.GetCalculationOption(calculationModel, optionName, out optionValue_temp);
                return (result, optionValue_temp);
            });
            return (postResult.Item1,
                    postResult.Item2);
        }


        // Simple Method
        public Task<Dictionary<string, string>> GetCalculationOptionsAsync(string calculationModel) => 
            _service.PostAsync(context => _inner.GetCalculationOptions(calculationModel));

        // Simple Method
        public Task<string> GetDvhEstimationModelNameAsync() => 
            _service.PostAsync(context => _inner.GetDvhEstimationModelName());

        // Simple Method
        public Task<bool> IsEntireBodyAndBolusesCoveredByCalculationAreaAsync() => 
            _service.PostAsync(context => _inner.IsEntireBodyAndBolusesCoveredByCalculationArea());

        // Simple Void Method
        public Task MoveToCourseAsync(ICourse destinationCourse) =>
            _service.PostAsync(context => _inner.MoveToCourse(((AsyncCourse)destinationCourse)._inner));

        // Simple Void Method
        public Task RemoveReferencePointAsync(IReferencePoint refPoint) =>
            _service.PostAsync(context => _inner.RemoveReferencePoint(((AsyncReferencePoint)refPoint)._inner));

        // Simple Void Method
        public Task SetCalculationModelAsync(CalculationType calculationType, string model) =>
            _service.PostAsync(context => _inner.SetCalculationModel(calculationType, model));

        // Simple Method
        public Task<bool> SetCalculationOptionAsync(string calculationModel, string optionName, string optionValue) => 
            _service.PostAsync(context => _inner.SetCalculationOption(calculationModel, optionName, optionValue));

        // Simple Void Method
        public Task SetPrescriptionAsync(int numberOfFractions, DoseValue dosePerFraction, double treatmentPercentage) =>
            _service.PostAsync(context => _inner.SetPrescription(numberOfFractions, dosePerFraction, treatmentPercentage));

        // Simple Method
        public Task<bool> SetTargetStructureIfNoDoseAsync(IStructure newTargetStructure, System.Text.StringBuilder errorHint) => 
            _service.PostAsync(context => _inner.SetTargetStructureIfNoDose(((AsyncStructure)newTargetStructure)._inner, errorHint));

        public double PlanNormalizationValue { get; private set; }
        public async Task SetPlanNormalizationValueAsync(double value)
        {
            PlanNormalizationValue = await _service.PostAsync(context => 
            {
                _inner.PlanNormalizationValue = value;
                return _inner.PlanNormalizationValue;
            });
        }

        public async Task<IReadOnlyList<IPlanUncertainty>> GetPlanUncertaintiesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.PlanUncertainties?.Select(x => new AsyncPlanUncertainty(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IApplicationScriptLog>> GetApplicationScriptLogsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.ApplicationScriptLogs?.Select(x => new AsyncApplicationScriptLog(x, _service)).ToList());
        }


        public PlanSetupApprovalStatus ApprovalStatus { get; }

        public string ApprovalStatusAsString { get; }

        public async Task<IPlanningItem> GetBaseDosePlanningItemAsync()
        {
            return await _service.PostAsync(context => 
                _inner.BaseDosePlanningItem is null ? null : new AsyncPlanningItem(_inner.BaseDosePlanningItem, _service));
        }

        public async Task SetBaseDosePlanningItemAsync(IPlanningItem value)
        {
            if (value is null)
            {
                await _service.PostAsync(context => _inner.BaseDosePlanningItem = null);
                return;
            }
            if (value is IEsapiWrapper<PlanningItem> wrapper)
            {
                 await _service.PostAsync(context => _inner.BaseDosePlanningItem = wrapper.Inner);
                 return;
            }
            throw new System.ArgumentException("Value must be of type AsyncPlanningItem");
        }

        public async Task<IReadOnlyList<IBeam>> GetBeamsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Beams?.Select(x => new AsyncBeam(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IBeam>> GetBeamsInTreatmentOrderAsync()
        {
            return await _service.PostAsync(context => 
                _inner.BeamsInTreatmentOrder?.Select(x => new AsyncBeam(x, _service)).ToList());
        }


        public string CreationUserName { get; }

        public DoseValue DosePerFraction { get; }

        public async Task<IReadOnlyList<IEstimatedDVH>> GetDVHEstimatesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.DVHEstimates?.Select(x => new AsyncEstimatedDVH(x, _service)).ToList());
        }


        public string ElectronCalculationModel { get; }

        public Dictionary<string, string> ElectronCalculationOptions { get; }

        public string IntegrityHash { get; }

        public bool IsDoseValid { get; }

        public bool IsTreated { get; }

        public int? NumberOfFractions { get; }

        public async Task<IOptimizationSetup> GetOptimizationSetupAsync()
        {
            return await _service.PostAsync(context => 
                _inner.OptimizationSetup is null ? null : new AsyncOptimizationSetup(_inner.OptimizationSetup, _service));
        }

        public async Task<IPatientSupportDevice> GetPatientSupportDeviceAsync()
        {
            return await _service.PostAsync(context => 
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
            return await _service.PostAsync(context => 
                _inner.PredecessorPlan is null ? null : new AsyncPlanSetup(_inner.PredecessorPlan, _service));
        }

        public string PredecessorPlanUID { get; }

        public async Task<IReferencePoint> GetPrimaryReferencePointAsync()
        {
            return await _service.PostAsync(context => 
                _inner.PrimaryReferencePoint is null ? null : new AsyncReferencePoint(_inner.PrimaryReferencePoint, _service));
        }

        public string ProtocolID { get; }

        public string ProtocolPhaseID { get; }

        public string ProtonCalculationModel { get; }

        public Dictionary<string, string> ProtonCalculationOptions { get; }

        public async Task<IReadOnlyList<IReferencePoint>> GetReferencePointsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.ReferencePoints?.Select(x => new AsyncReferencePoint(x, _service)).ToList());
        }


        public async Task<IRTPrescription> GetRTPrescriptionAsync()
        {
            return await _service.PostAsync(context => 
                _inner.RTPrescription is null ? null : new AsyncRTPrescription(_inner.RTPrescription, _service));
        }

        public async Task<ISeries> GetSeriesAsync()
        {
            return await _service.PostAsync(context => 
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
            return await _service.PostAsync(context => 
                _inner.TreatmentSessions?.Select(x => new AsyncPlanTreatmentSession(x, _service)).ToList());
        }


        public string UID { get; }

        public bool UseGating { get; private set; }
        public async Task SetUseGatingAsync(bool value)
        {
            UseGating = await _service.PostAsync(context => 
            {
                _inner.UseGating = value;
                return _inner.UseGating;
            });
        }

        public async Task<IPlanSetup> GetVerifiedPlanAsync()
        {
            return await _service.PostAsync(context => 
                _inner.VerifiedPlan is null ? null : new AsyncPlanSetup(_inner.VerifiedPlan, _service));
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanSetup> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanSetup, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.PlanSetup(AsyncPlanSetup wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.PlanSetup IEsapiWrapper<VMS.TPS.Common.Model.API.PlanSetup>.Inner => _inner;

        /* --- Skipped Members (Not generated) ---
           - Id: Shadows member in wrapped base class
           - Name: Shadows member in wrapped base class
           - Comment: Shadows member in wrapped base class
           - PlanObjectiveStructures: No matching factory found (Not Implemented)
           - ApprovalHistory: No matching factory found (Not Implemented)
        */
    }
}
