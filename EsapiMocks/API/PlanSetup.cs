using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class PlanSetup : PlanningItem
    {
        public PlanSetup()
        {
        }

        public void GetProtocolPrescriptionsAndMeasures(ref List<ProtocolPhasePrescription> prescriptions, ref List<ProtocolPhaseMeasure> measures)
        {
        }

        public void SetTreatmentOrder(IEnumerable<Beam> orderedBeams) { }
        public void AddReferencePoint(ReferencePoint refPoint) { }
        public bool GetCalculationOption(string calculationModel, string optionName, out string optionValue)
        {
            optionValue = default;
            return default;
        }

        public Dictionary<string, string> GetCalculationOptions(string calculationModel) => default;
        public string GetDvhEstimationModelName() => default;
        public bool IsEntireBodyAndBolusesCoveredByCalculationArea() => default;
        public void MoveToCourse(Course destinationCourse) { }
        public void RemoveReferencePoint(ReferencePoint refPoint) { }
        public bool SetCalculationOption(string calculationModel, string optionName, string optionValue) => default;
        public bool SetTargetStructureIfNoDose(Structure newTargetStructure, System.Text.StringBuilder errorHint) => default;
        public string Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public double PlanNormalizationValue { get; set; }
        public IEnumerable<PlanUncertainty> PlanUncertainties { get; set; }
        public IEnumerable<string> PlanObjectiveStructures { get; set; }
        public IEnumerable<ApplicationScriptLog> ApplicationScriptLogs { get; set; }
        public string ApprovalStatusAsString { get; set; }
        public PlanningItem BaseDosePlanningItem { get; set; }
        public IEnumerable<Beam> Beams { get; set; }
        public IEnumerable<Beam> BeamsInTreatmentOrder { get; set; }
        public string CreationUserName { get; set; }
        public string DBKey { get; set; }
        public IEnumerable<EstimatedDVH> DVHEstimates { get; set; }
        public string ElectronCalculationModel { get; set; }
        public Dictionary<string, string> ElectronCalculationOptions { get; set; }
        public string IntegrityHash { get; set; }
        public bool IsDoseValid { get; set; }
        public bool IsTreated { get; set; }
        public int? NumberOfFractions { get; set; }
        public OptimizationSetup OptimizationSetup { get; set; }
        public PatientSupportDevice PatientSupportDevice { get; set; }
        public string PhotonCalculationModel { get; set; }
        public Dictionary<string, string> PhotonCalculationOptions { get; set; }
        public string PlanIntent { get; set; }
        public bool PlanIsInTreatment { get; set; }
        public string PlanningApprovalDate { get; set; }
        public string PlanningApprover { get; set; }
        public string PlanningApproverDisplayName { get; set; }
        public string PlanNormalizationMethod { get; set; }
        public PlanSetup PredecessorPlan { get; set; }
        public string PredecessorPlanUID { get; set; }
        public ReferencePoint PrimaryReferencePoint { get; set; }
        public string ProtocolID { get; set; }
        public string ProtocolPhaseID { get; set; }
        public string ProtonCalculationModel { get; set; }
        public Dictionary<string, string> ProtonCalculationOptions { get; set; }
        public IEnumerable<ReferencePoint> ReferencePoints { get; set; }
        public RTPrescription RTPrescription { get; set; }
        public Series Series { get; set; }
        public string SeriesUID { get; set; }
        public string TargetVolumeID { get; set; }
        public string TreatmentApprovalDate { get; set; }
        public string TreatmentApprover { get; set; }
        public string TreatmentApproverDisplayName { get; set; }
        public string TreatmentOrientationAsString { get; set; }
        public double TreatmentPercentage { get; set; }
        public IEnumerable<PlanTreatmentSession> TreatmentSessions { get; set; }
        public string UID { get; set; }
        public bool UseGating { get; set; }
        public PlanSetup VerifiedPlan { get; set; }
    }
}
