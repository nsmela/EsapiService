namespace VMS.TPS.Common.Model.API
{
    public interface IPlanSetup : IPlanningItem
    {
        void GetProtocolPrescriptionsAndMeasures(System.Collections.Generic.List<VMS.TPS.Common.Model.API.ProtocolPhasePrescription> prescriptions, System.Collections.Generic.List<VMS.TPS.Common.Model.API.ProtocolPhaseMeasure> measures);
        IReferencePoint AddReferencePoint(bool target, System.Nullable<VMS.TPS.Common.Model.Types.VVector> location, string id);

        IPlanUncertainty AddPlanUncertaintyWithParameters(VMS.TPS.Common.Model.Types.PlanUncertaintyType uncertaintyType, bool planSpecificUncertainty, double HUConversionError, VMS.TPS.Common.Model.Types.VVector isocenterShift);
        void SetTreatmentOrder(System.Collections.Generic.IEnumerable<VMS.TPS.Common.Model.API.Beam> orderedBeams);
        void WriteXml(System.Xml.XmlWriter writer);
        void AddReferencePoint(VMS.TPS.Common.Model.API.ReferencePoint refPoint);
        void ClearCalculationModel(VMS.TPS.Common.Model.Types.CalculationType calculationType);
        string GetCalculationModel(VMS.TPS.Common.Model.Types.CalculationType calculationType);

        System.Collections.Generic.Dictionary<string, string> GetCalculationOptions(string calculationModel);
        string GetDvhEstimationModelName();
        bool IsEntireBodyAndBolusesCoveredByCalculationArea();
        void MoveToCourse(VMS.TPS.Common.Model.API.Course destinationCourse);
        void RemoveReferencePoint(VMS.TPS.Common.Model.API.ReferencePoint refPoint);
        void SetCalculationModel(VMS.TPS.Common.Model.Types.CalculationType calculationType, string model);
        bool SetCalculationOption(string calculationModel, string optionName, string optionValue);
        void SetPrescription(int numberOfFractions, VMS.TPS.Common.Model.Types.DoseValue dosePerFraction, double treatmentPercentage);
        bool SetTargetStructureIfNoDose(VMS.TPS.Common.Model.API.Structure newTargetStructure, System.Text.StringBuilder errorHint);
        string Id { get; }
        System.Threading.Tasks.Task SetIdAsync(string value);
        string Name { get; }
        System.Threading.Tasks.Task SetNameAsync(string value);
        string Comment { get; }
        System.Threading.Tasks.Task SetCommentAsync(string value);
        double PlanNormalizationValue { get; }
        System.Threading.Tasks.Task SetPlanNormalizationValueAsync(double value);
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
        IPlanningItem BaseDosePlanningItem { get; }
        System.Threading.Tasks.Task SetBaseDosePlanningItemAsync(IPlanningItem value);
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
        IOptimizationSetup OptimizationSetup { get; }
        IPatientSupportDevice PatientSupportDevice { get; }
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
        IPlanSetup PredecessorPlan { get; }
        string PredecessorPlanUID { get; }
        IReferencePoint PrimaryReferencePoint { get; }
        string ProtocolID { get; }
        string ProtocolPhaseID { get; }
        string ProtonCalculationModel { get; }
        System.Collections.Generic.Dictionary<string, string> ProtonCalculationOptions { get; }
        System.Collections.Generic.IReadOnlyList<IReferencePoint> ReferencePoints { get; }
        IRTPrescription RTPrescription { get; }
        ISeries Series { get; }
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
        System.Threading.Tasks.Task SetUseGatingAsync(bool value);
        IPlanSetup VerifiedPlan { get; }
    }
}
