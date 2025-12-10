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
        string Id { get; }
        Task SetIdAsync(string value);
        string Name { get; }
        Task SetNameAsync(string value);
        string Comment { get; }
        Task SetCommentAsync(string value);
        double PlanNormalizationValue { get; }
        Task SetPlanNormalizationValueAsync(double value);
        string ApprovalStatusAsString { get; }
        string CreationUserName { get; }
        string DBKey { get; }
        string ElectronCalculationModel { get; }
        Dictionary<string, string> ElectronCalculationOptions { get; }
        string IntegrityHash { get; }
        bool IsDoseValid { get; }
        bool IsTreated { get; }
        int? NumberOfFractions { get; }
        string PhotonCalculationModel { get; }
        Dictionary<string, string> PhotonCalculationOptions { get; }
        string PlanIntent { get; }
        bool PlanIsInTreatment { get; }
        string PlanningApprovalDate { get; }
        string PlanningApprover { get; }
        string PlanningApproverDisplayName { get; }
        string PlanNormalizationMethod { get; }
        string PredecessorPlanUID { get; }
        string ProtocolID { get; }
        string ProtocolPhaseID { get; }
        string ProtonCalculationModel { get; }
        Dictionary<string, string> ProtonCalculationOptions { get; }
        string SeriesUID { get; }
        string TargetVolumeID { get; }
        string TreatmentApprovalDate { get; }
        string TreatmentApprover { get; }
        string TreatmentApproverDisplayName { get; }
        string TreatmentOrientationAsString { get; }
        double TreatmentPercentage { get; }
        string UID { get; }
        bool UseGating { get; }
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
        IReadOnlyList<string> PlanObjectiveStructures { get; } // simple collection property
        Task<IReadOnlyList<IApplicationScriptLog>> GetApplicationScriptLogsAsync(); // collection proeprty context
        Task<IReadOnlyList<IBeam>> GetBeamsAsync(); // collection proeprty context
        Task<IReadOnlyList<IBeam>> GetBeamsInTreatmentOrderAsync(); // collection proeprty context
        Task<IReadOnlyList<IEstimatedDVH>> GetDVHEstimatesAsync(); // collection proeprty context
        Task<IReadOnlyList<IReferencePoint>> GetReferencePointsAsync(); // collection proeprty context
        Task<IReadOnlyList<IPlanTreatmentSession>> GetTreatmentSessionsAsync(); // collection proeprty context

        // --- Methods --- //
        Task<(IReadOnlyList<IProtocolPhasePrescription> prescriptions, IReadOnlyList<IProtocolPhaseMeasure> measures)> GetProtocolPrescriptionsAndMeasuresAsync(IReadOnlyList<IProtocolPhasePrescription> prescriptions, IReadOnlyList<IProtocolPhaseMeasure> measures); // out/ref parameter method
        Task SetTreatmentOrderAsync(IReadOnlyList<IBeam> orderedBeams); // void method
        Task AddReferencePointAsync(IReferencePoint refPoint); // void method
        Task<(bool Result, string optionValue)> GetCalculationOptionAsync(string calculationModel, string optionName); // out/ref parameter method
        Task<Dictionary<string, string>> GetCalculationOptionsAsync(string calculationModel); // simple method
        Task<string> GetDvhEstimationModelNameAsync(); // simple method
        Task<bool> IsEntireBodyAndBolusesCoveredByCalculationAreaAsync(); // simple method
        Task MoveToCourseAsync(ICourse destinationCourse); // void method
        Task RemoveReferencePointAsync(IReferencePoint refPoint); // void method
        Task<bool> SetCalculationOptionAsync(string calculationModel, string optionName, string optionValue); // simple method
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
    }
}
