namespace VMS.TPS.Common.Model.API
{
    public interface IBeam : IApiDataObject
    {
        void AddBolus(VMS.TPS.Common.Model.API.Bolus bolus);
        bool RemoveBolus(VMS.TPS.Common.Model.API.Bolus bolus);
        void WriteXml(System.Xml.XmlWriter writer);
        void AddBolus(string bolusId);
        bool AddFlatteningSequence();
        void ApplyParameters(VMS.TPS.Common.Model.API.BeamParameters beamParams);
        System.Collections.Generic.Dictionary<int, double> CalculateAverageLeafPairOpenings();

        double CollimatorAngleToUser(double val);
        int CountSubfields();
        IImage CreateOrReplaceDRR(VMS.TPS.Common.Model.Types.DRRCalculationParameters parameters);
        void FitArcOptimizationApertureToCollimatorJaws();
        void FitCollimatorToStructure(VMS.TPS.Common.Model.Types.FitToStructureMargins margins, VMS.TPS.Common.Model.API.Structure structure, bool useAsymmetricXJaws, bool useAsymmetricYJaws, bool optimizeCollimatorRotation);
        void FitMLCToOutline(System.Windows.Point[][] outline);
        void FitMLCToOutline(System.Windows.Point[][] outline, bool optimizeCollimatorRotation, VMS.TPS.Common.Model.Types.JawFitting jawFit, VMS.TPS.Common.Model.Types.OpenLeavesMeetingPoint olmp, VMS.TPS.Common.Model.Types.ClosedLeavesMeetingPoint clmp);
        void FitMLCToStructure(VMS.TPS.Common.Model.API.Structure structure);
        void FitMLCToStructure(VMS.TPS.Common.Model.Types.FitToStructureMargins margins, VMS.TPS.Common.Model.API.Structure structure, bool optimizeCollimatorRotation, VMS.TPS.Common.Model.Types.JawFitting jawFit, VMS.TPS.Common.Model.Types.OpenLeavesMeetingPoint olmp, VMS.TPS.Common.Model.Types.ClosedLeavesMeetingPoint clmp);
        double GantryAngleToUser(double val);
        double GetCAXPathLengthInBolus(VMS.TPS.Common.Model.API.Bolus bolus);
        IBeamParameters GetEditableParameters();
        VMS.TPS.Common.Model.Types.Fluence GetOptimalFluence();
        VMS.TPS.Common.Model.Types.VVector GetSourceLocation(double gantryAngle);
        double GetSourceToBolusDistance(VMS.TPS.Common.Model.API.Bolus bolus);
        System.Windows.Point[][] GetStructureOutlines(VMS.TPS.Common.Model.API.Structure structure, bool inBEV);
        string JawPositionsToUserString(VMS.TPS.Common.Model.Types.VRect<double> val);
        double PatientSupportAngleToUser(double val);
        bool RemoveBolus(string bolusId);
        bool RemoveFlatteningSequence();
        void SetOptimalFluence(VMS.TPS.Common.Model.Types.Fluence fluence);
        string Id { get; }
        System.Threading.Tasks.Task SetIdAsync(string value);
        string Name { get; }
        System.Threading.Tasks.Task SetNameAsync(string value);
        string Comment { get; }
        System.Threading.Tasks.Task SetCommentAsync(string value);
        VMS.TPS.Common.Model.Types.MetersetValue Meterset { get; }
        int BeamNumber { get; }
        IApplicator Applicator { get; }
        double ArcLength { get; }
        VMS.TPS.Common.Model.Types.ArcOptimizationAperture ArcOptimizationAperture { get; }
        System.Threading.Tasks.Task SetArcOptimizationApertureAsync(VMS.TPS.Common.Model.Types.ArcOptimizationAperture value);
        bool AreControlPointJawsMoving { get; }
        double AverageSSD { get; }
        VMS.TPS.Common.Model.Types.BeamTechnique BeamTechnique { get; }
        System.Collections.Generic.IReadOnlyList<IBlock> Blocks { get; }
        System.Collections.Generic.IReadOnlyList<IBolus> Boluses { get; }
        System.Collections.Generic.IReadOnlyList<IBeamCalculationLog> CalculationLogs { get; }
        double CollimatorRotation { get; }
        string CollimatorRotationAsString { get; }
        ICompensator Compensator { get; }
        IControlPointCollection ControlPoints { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> CreationDateTime { get; }
        IBeamDose Dose { get; }
        int DoseRate { get; }
        double DosimetricLeafGap { get; }
        IEnergyMode EnergyMode { get; }
        string EnergyModeDisplayName { get; }
        System.Collections.Generic.IReadOnlyList<IFieldReferencePoint> FieldReferencePoints { get; }
        VMS.TPS.Common.Model.Types.GantryDirection GantryDirection { get; }
        bool HasAllMLCLeavesClosed { get; }
        bool IsGantryExtended { get; }
        bool IsGantryExtendedAtStopAngle { get; }
        bool IsImagingTreatmentField { get; }
        bool IsIMRT { get; }
        VMS.TPS.Common.Model.Types.VVector IsocenterPosition { get; }
        bool IsSetupField { get; }
        double MetersetPerGy { get; }
        IMLC MLC { get; }
        VMS.TPS.Common.Model.Types.MLCPlanType MLCPlanType { get; }
        double MLCTransmissionFactor { get; }
        string MotionCompensationTechnique { get; }
        string MotionSignalSource { get; }
        double NormalizationFactor { get; }
        string NormalizationMethod { get; }
        IPlanSetup Plan { get; }
        double PlannedSSD { get; }
        IImage ReferenceImage { get; }
        string SetupNote { get; }
        System.Threading.Tasks.Task SetSetupNoteAsync(string value);
        VMS.TPS.Common.Model.Types.SetupTechnique SetupTechnique { get; }
        double SSD { get; }
        double SSDAtStopAngle { get; }
        ITechnique Technique { get; }
        string ToleranceTableLabel { get; }
        System.Collections.Generic.IReadOnlyList<ITray> Trays { get; }
        double TreatmentTime { get; }
        IExternalBeamTreatmentUnit TreatmentUnit { get; }
        System.Collections.Generic.IReadOnlyList<IWedge> Wedges { get; }
        double WeightFactor { get; }
    }
}
