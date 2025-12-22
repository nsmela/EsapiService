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
            PlanNormalizationValue = inner.PlanNormalizationValue;
            ApprovalStatus = inner.ApprovalStatus;
            CreationUserName = inner.CreationUserName;
            DosePerFraction = inner.DosePerFraction;
            ElectronCalculationModel = inner.ElectronCalculationModel;
            ElectronCalculationOptions = inner.ElectronCalculationOptions;
            IsDoseValid = inner.IsDoseValid;
            IsTreated = inner.IsTreated;
            NumberOfFractions = inner.NumberOfFractions;
            PhotonCalculationModel = inner.PhotonCalculationModel;
            PhotonCalculationOptions = inner.PhotonCalculationOptions;
            PlanIntent = inner.PlanIntent;
            PlannedDosePerFraction = inner.PlannedDosePerFraction;
            PlanningApprovalDate = inner.PlanningApprovalDate;
            PlanningApprover = inner.PlanningApprover;
            PlanningApproverDisplayName = inner.PlanningApproverDisplayName;
            PlanNormalizationMethod = inner.PlanNormalizationMethod;
            PlanNormalizationPoint = inner.PlanNormalizationPoint;
            PlanType = inner.PlanType;
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


        public async Task<IReferencePoint> AddReferencePointAsync(IStructure structure, VVector? location, string id, string name)
        {
            return await _service.PostAsync(context => 
                _inner.AddReferencePoint(((AsyncStructure)structure)._inner, location, id, name) is var result && result is null ? null : new AsyncReferencePoint(result, _service));
        }


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

        // Simple Void Method
        public Task SetCalculationModelAsync(CalculationType calculationType, string model) =>
            _service.PostAsync(context => _inner.SetCalculationModel(calculationType, model));

        // Simple Method
        public Task<bool> SetCalculationOptionAsync(string calculationModel, string optionName, string optionValue) => 
            _service.PostAsync(context => _inner.SetCalculationOption(calculationModel, optionName, optionValue));

        // Simple Void Method
        public Task SetPrescriptionAsync(int numberOfFractions, DoseValue dosePerFraction, double treatmentPercentage) =>
            _service.PostAsync(context => _inner.SetPrescription(numberOfFractions, dosePerFraction, treatmentPercentage));

        public new string Id { get; private set; }
        public async Task SetIdAsync(string value)
        {
            Id = await _service.PostAsync(context => 
            {
                _inner.Id = value;
                return _inner.Id;
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


        public PlanSetupApprovalStatus ApprovalStatus { get; }

        public async Task<IReadOnlyList<IBeam>> GetBeamsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Beams?.Select(x => new AsyncBeam(x, _service)).ToList());
        }


        public async Task<ICourse> GetCourseAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Course is null ? null : new AsyncCourse(_inner.Course, _service);
                return innerResult;
            });
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

        public bool IsDoseValid { get; }

        public bool IsTreated { get; }

        public int? NumberOfFractions { get; }

        public async Task<IOptimizationSetup> GetOptimizationSetupAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.OptimizationSetup is null ? null : new AsyncOptimizationSetup(_inner.OptimizationSetup, _service);
                return innerResult;
            });
        }

        public string PhotonCalculationModel { get; }

        public Dictionary<string, string> PhotonCalculationOptions { get; }

        public string PlanIntent { get; }

        public DoseValue PlannedDosePerFraction { get; }

        public string PlanningApprovalDate { get; }

        public string PlanningApprover { get; }

        public string PlanningApproverDisplayName { get; }

        public string PlanNormalizationMethod { get; }

        public VVector PlanNormalizationPoint { get; }

        public PlanType PlanType { get; }

        public async Task<IPlanSetup> GetPredecessorPlanAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.PredecessorPlan is null ? null : new AsyncPlanSetup(_inner.PredecessorPlan, _service);
                return innerResult;
            });
        }

        public async Task<IReferencePoint> GetPrimaryReferencePointAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.PrimaryReferencePoint is null ? null : new AsyncReferencePoint(_inner.PrimaryReferencePoint, _service);
                return innerResult;
            });
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

        public string SeriesUID { get; }

        public string TargetVolumeID { get; }

        public DoseValue TotalDose { get; }

        public string TreatmentApprovalDate { get; }

        public string TreatmentApprover { get; }

        public string TreatmentApproverDisplayName { get; }

        public PatientOrientation TreatmentOrientation { get; }

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
