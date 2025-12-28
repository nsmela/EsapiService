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
        public Task SetTreatmentOrderAsync(IReadOnlyList<IBeam> orderedBeams) 
        {
            _service.PostAsync(context => _inner.SetTreatmentOrder((orderedBeams.Select(x => ((AsyncBeam)x)._inner))));
            return Task.CompletedTask;
        }

        // Simple Void Method
        public Task AddReferencePointAsync(IReferencePoint refPoint) 
        {
            _service.PostAsync(context => _inner.AddReferencePoint(((AsyncReferencePoint)refPoint)._inner));
            return Task.CompletedTask;
        }

        // Simple Void Method
        public Task ClearCalculationModelAsync(CalculationType calculationType) 
        {
            _service.PostAsync(context => _inner.ClearCalculationModel(calculationType));
            return Task.CompletedTask;
        }

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
        public Task MoveToCourseAsync(ICourse destinationCourse) 
        {
            _service.PostAsync(context => _inner.MoveToCourse(((AsyncCourse)destinationCourse)._inner));
            return Task.CompletedTask;
        }

        // Simple Void Method
        public Task RemoveReferencePointAsync(IReferencePoint refPoint) 
        {
            _service.PostAsync(context => _inner.RemoveReferencePoint(((AsyncReferencePoint)refPoint)._inner));
            return Task.CompletedTask;
        }

        // Simple Void Method
        public Task SetCalculationModelAsync(CalculationType calculationType, string model) 
        {
            _service.PostAsync(context => _inner.SetCalculationModel(calculationType, model));
            return Task.CompletedTask;
        }

        // Simple Method
        public Task<bool> SetCalculationOptionAsync(string calculationModel, string optionName, string optionValue) => 
            _service.PostAsync(context => _inner.SetCalculationOption(calculationModel, optionName, optionValue));

        // Simple Void Method
        public Task SetPrescriptionAsync(int numberOfFractions, DoseValue dosePerFraction, double treatmentPercentage) 
        {
            _service.PostAsync(context => _inner.SetPrescription(numberOfFractions, dosePerFraction, treatmentPercentage));
            return Task.CompletedTask;
        }

        // Simple Method
        public Task<bool> SetTargetStructureIfNoDoseAsync(IStructure newTargetStructure, System.Text.StringBuilder errorHint) => 
            _service.PostAsync(context => _inner.SetTargetStructureIfNoDose(((AsyncStructure)newTargetStructure)._inner, errorHint));

        public new string Id
        {
            get => _inner.Id;
            set => _inner.Id = value;
        }


        public new string Name
        {
            get => _inner.Name;
            set => _inner.Name = value;
        }


        public new string Comment
        {
            get => _inner.Comment;
            set => _inner.Comment = value;
        }


        public double PlanNormalizationValue
        {
            get => _inner.PlanNormalizationValue;
            set => _inner.PlanNormalizationValue = value;
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


        public PlanSetupApprovalStatus ApprovalStatus =>
            _inner.ApprovalStatus;


        public string ApprovalStatusAsString =>
            _inner.ApprovalStatusAsString;


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


        public string CreationUserName =>
            _inner.CreationUserName;


        public DoseValue DosePerFraction =>
            _inner.DosePerFraction;


        public async Task<IReadOnlyList<IEstimatedDVH>> GetDVHEstimatesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.DVHEstimates?.Select(x => new AsyncEstimatedDVH(x, _service)).ToList());
        }


        public string ElectronCalculationModel =>
            _inner.ElectronCalculationModel;


        public Dictionary<string, string> ElectronCalculationOptions =>
            _inner.ElectronCalculationOptions;


        public string IntegrityHash =>
            _inner.IntegrityHash;


        public bool IsDoseValid =>
            _inner.IsDoseValid;


        public bool IsTreated =>
            _inner.IsTreated;


        public int? NumberOfFractions =>
            _inner.NumberOfFractions;


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

        public string PhotonCalculationModel =>
            _inner.PhotonCalculationModel;


        public Dictionary<string, string> PhotonCalculationOptions =>
            _inner.PhotonCalculationOptions;


        public string PlanIntent =>
            _inner.PlanIntent;


        public bool PlanIsInTreatment =>
            _inner.PlanIsInTreatment;


        public DoseValue PlannedDosePerFraction =>
            _inner.PlannedDosePerFraction;


        public string PlanningApprovalDate =>
            _inner.PlanningApprovalDate;


        public string PlanningApprover =>
            _inner.PlanningApprover;


        public string PlanningApproverDisplayName =>
            _inner.PlanningApproverDisplayName;


        public string PlanNormalizationMethod =>
            _inner.PlanNormalizationMethod;


        public VVector PlanNormalizationPoint =>
            _inner.PlanNormalizationPoint;


        public PlanType PlanType =>
            _inner.PlanType;


        public async Task<IPlanSetup> GetPredecessorPlanAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.PredecessorPlan is null ? null : new AsyncPlanSetup(_inner.PredecessorPlan, _service);
                return innerResult;
            });
        }

        public string PredecessorPlanUID =>
            _inner.PredecessorPlanUID;


        public async Task<IReferencePoint> GetPrimaryReferencePointAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.PrimaryReferencePoint is null ? null : new AsyncReferencePoint(_inner.PrimaryReferencePoint, _service);
                return innerResult;
            });
        }

        public string ProtocolID =>
            _inner.ProtocolID;


        public string ProtocolPhaseID =>
            _inner.ProtocolPhaseID;


        public string ProtonCalculationModel =>
            _inner.ProtonCalculationModel;


        public Dictionary<string, string> ProtonCalculationOptions =>
            _inner.ProtonCalculationOptions;


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

        public string SeriesUID =>
            _inner.SeriesUID;


        public string TargetVolumeID =>
            _inner.TargetVolumeID;


        public DoseValue TotalDose =>
            _inner.TotalDose;


        public string TreatmentApprovalDate =>
            _inner.TreatmentApprovalDate;


        public string TreatmentApprover =>
            _inner.TreatmentApprover;


        public string TreatmentApproverDisplayName =>
            _inner.TreatmentApproverDisplayName;


        public PatientOrientation TreatmentOrientation =>
            _inner.TreatmentOrientation;


        public string TreatmentOrientationAsString =>
            _inner.TreatmentOrientationAsString;


        public double TreatmentPercentage =>
            _inner.TreatmentPercentage;


        public async Task<IReadOnlyList<IPlanTreatmentSession>> GetTreatmentSessionsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.TreatmentSessions?.Select(x => new AsyncPlanTreatmentSession(x, _service)).ToList());
        }


        public string UID =>
            _inner.UID;


        public bool UseGating
        {
            get => _inner.UseGating;
            set => _inner.UseGating = value;
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
