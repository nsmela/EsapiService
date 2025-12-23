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
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            Id = inner.Id;
            Name = inner.Name;
            Comment = inner.Comment;
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

        public new string Id { get; private set; }
        public async Task SetIdAsync(string value)
        {
            Id = await _service.PostAsync(context => 
            {
                _inner.Id = value;
                return _inner.Id;
            });
        }

        public new string Name { get; private set; }
        public async Task SetNameAsync(string value)
        {
            Name = await _service.PostAsync(context => 
            {
                _inner.Name = value;
                return _inner.Name;
            });
        }

        public new string Comment { get; private set; }
        public async Task SetCommentAsync(string value)
        {
            Comment = await _service.PostAsync(context => 
            {
                _inner.Comment = value;
                return _inner.Comment;
            });
        }

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


        public PlanSetupApprovalStatus ApprovalStatus { get; private set; }

        public string ApprovalStatusAsString { get; private set; }

        public async Task<IPlanningItem> GetBaseDosePlanningItemAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.BaseDosePlanningItem is null ? null : new AsyncPlanningItem(_inner.BaseDosePlanningItem, _service);
                return innerResult;
            });
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


        public string CreationUserName { get; private set; }

        public DoseValue DosePerFraction { get; private set; }

        public async Task<IReadOnlyList<IEstimatedDVH>> GetDVHEstimatesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.DVHEstimates?.Select(x => new AsyncEstimatedDVH(x, _service)).ToList());
        }


        public string ElectronCalculationModel { get; private set; }

        public Dictionary<string, string> ElectronCalculationOptions { get; private set; }

        public string IntegrityHash { get; private set; }

        public bool IsDoseValid { get; private set; }

        public bool IsTreated { get; private set; }

        public int? NumberOfFractions { get; private set; }

        public async Task<IOptimizationSetup> GetOptimizationSetupAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.OptimizationSetup is null ? null : new AsyncOptimizationSetup(_inner.OptimizationSetup, _service);
                return innerResult;
            });
        }

        public async Task<IPatientSupportDevice> GetPatientSupportDeviceAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.PatientSupportDevice is null ? null : new AsyncPatientSupportDevice(_inner.PatientSupportDevice, _service);
                return innerResult;
            });
        }

        public string PhotonCalculationModel { get; private set; }

        public Dictionary<string, string> PhotonCalculationOptions { get; private set; }

        public string PlanIntent { get; private set; }

        public bool PlanIsInTreatment { get; private set; }

        public DoseValue PlannedDosePerFraction { get; private set; }

        public string PlanningApprovalDate { get; private set; }

        public string PlanningApprover { get; private set; }

        public string PlanningApproverDisplayName { get; private set; }

        public string PlanNormalizationMethod { get; private set; }

        public VVector PlanNormalizationPoint { get; private set; }

        public PlanType PlanType { get; private set; }

        public async Task<IPlanSetup> GetPredecessorPlanAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.PredecessorPlan is null ? null : new AsyncPlanSetup(_inner.PredecessorPlan, _service);
                return innerResult;
            });
        }

        public string PredecessorPlanUID { get; private set; }

        public async Task<IReferencePoint> GetPrimaryReferencePointAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.PrimaryReferencePoint is null ? null : new AsyncReferencePoint(_inner.PrimaryReferencePoint, _service);
                return innerResult;
            });
        }

        public string ProtocolID { get; private set; }

        public string ProtocolPhaseID { get; private set; }

        public string ProtonCalculationModel { get; private set; }

        public Dictionary<string, string> ProtonCalculationOptions { get; private set; }

        public async Task<IReadOnlyList<IReferencePoint>> GetReferencePointsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.ReferencePoints?.Select(x => new AsyncReferencePoint(x, _service)).ToList());
        }


        public async Task<IRTPrescription> GetRTPrescriptionAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.RTPrescription is null ? null : new AsyncRTPrescription(_inner.RTPrescription, _service);
                return innerResult;
            });
        }

        public async Task<ISeries> GetSeriesAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Series is null ? null : new AsyncSeries(_inner.Series, _service);
                return innerResult;
            });
        }

        public string SeriesUID { get; private set; }

        public string TargetVolumeID { get; private set; }

        public DoseValue TotalDose { get; private set; }

        public string TreatmentApprovalDate { get; private set; }

        public string TreatmentApprover { get; private set; }

        public string TreatmentApproverDisplayName { get; private set; }

        public PatientOrientation TreatmentOrientation { get; private set; }

        public string TreatmentOrientationAsString { get; private set; }

        public double TreatmentPercentage { get; private set; }

        public async Task<IReadOnlyList<IPlanTreatmentSession>> GetTreatmentSessionsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.TreatmentSessions?.Select(x => new AsyncPlanTreatmentSession(x, _service)).ToList());
        }


        public string UID { get; private set; }

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
            return await _service.PostAsync(context => {
                var innerResult = _inner.VerifiedPlan is null ? null : new AsyncPlanSetup(_inner.VerifiedPlan, _service);
                return innerResult;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanSetup> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanSetup, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            Id = _inner.Id;
            Name = _inner.Name;
            Comment = _inner.Comment;
            PlanNormalizationValue = _inner.PlanNormalizationValue;
            ApprovalStatus = _inner.ApprovalStatus;
            ApprovalStatusAsString = _inner.ApprovalStatusAsString;
            CreationUserName = _inner.CreationUserName;
            DosePerFraction = _inner.DosePerFraction;
            ElectronCalculationModel = _inner.ElectronCalculationModel;
            ElectronCalculationOptions = _inner.ElectronCalculationOptions;
            IntegrityHash = _inner.IntegrityHash;
            IsDoseValid = _inner.IsDoseValid;
            IsTreated = _inner.IsTreated;
            NumberOfFractions = _inner.NumberOfFractions;
            PhotonCalculationModel = _inner.PhotonCalculationModel;
            PhotonCalculationOptions = _inner.PhotonCalculationOptions;
            PlanIntent = _inner.PlanIntent;
            PlanIsInTreatment = _inner.PlanIsInTreatment;
            PlannedDosePerFraction = _inner.PlannedDosePerFraction;
            PlanningApprovalDate = _inner.PlanningApprovalDate;
            PlanningApprover = _inner.PlanningApprover;
            PlanningApproverDisplayName = _inner.PlanningApproverDisplayName;
            PlanNormalizationMethod = _inner.PlanNormalizationMethod;
            PlanNormalizationPoint = _inner.PlanNormalizationPoint;
            PlanType = _inner.PlanType;
            PredecessorPlanUID = _inner.PredecessorPlanUID;
            ProtocolID = _inner.ProtocolID;
            ProtocolPhaseID = _inner.ProtocolPhaseID;
            ProtonCalculationModel = _inner.ProtonCalculationModel;
            ProtonCalculationOptions = _inner.ProtonCalculationOptions;
            SeriesUID = _inner.SeriesUID;
            TargetVolumeID = _inner.TargetVolumeID;
            TotalDose = _inner.TotalDose;
            TreatmentApprovalDate = _inner.TreatmentApprovalDate;
            TreatmentApprover = _inner.TreatmentApprover;
            TreatmentApproverDisplayName = _inner.TreatmentApproverDisplayName;
            TreatmentOrientation = _inner.TreatmentOrientation;
            TreatmentOrientationAsString = _inner.TreatmentOrientationAsString;
            TreatmentPercentage = _inner.TreatmentPercentage;
            UID = _inner.UID;
            UseGating = _inner.UseGating;
        }

        public static implicit operator VMS.TPS.Common.Model.API.PlanSetup(AsyncPlanSetup wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.PlanSetup IEsapiWrapper<VMS.TPS.Common.Model.API.PlanSetup>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.PlanSetup>.Service => _service;

        /* --- Skipped Members (Not generated) ---
           - PlanObjectiveStructures: No matching factory found (Not Implemented)
           - ApprovalHistory: No matching factory found (Not Implemented)
        */
    }
}
