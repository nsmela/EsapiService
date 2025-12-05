    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
    public class AsyncBeam : IBeam
    {
        internal readonly VMS.TPS.Common.Model.API.Beam _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncBeam(VMS.TPS.Common.Model.API.Beam inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Meterset = inner.Meterset;
            BeamNumber = inner.BeamNumber;
            ArcLength = inner.ArcLength;
            AreControlPointJawsMoving = inner.AreControlPointJawsMoving;
            AverageSSD = inner.AverageSSD;
            BeamTechnique = inner.BeamTechnique;
            CollimatorRotation = inner.CollimatorRotation;
            CollimatorRotationAsString = inner.CollimatorRotationAsString;
            DoseRate = inner.DoseRate;
            DosimetricLeafGap = inner.DosimetricLeafGap;
            EnergyModeDisplayName = inner.EnergyModeDisplayName;
            GantryDirection = inner.GantryDirection;
            HasAllMLCLeavesClosed = inner.HasAllMLCLeavesClosed;
            IsGantryExtended = inner.IsGantryExtended;
            IsGantryExtendedAtStopAngle = inner.IsGantryExtendedAtStopAngle;
            IsImagingTreatmentField = inner.IsImagingTreatmentField;
            IsIMRT = inner.IsIMRT;
            IsocenterPosition = inner.IsocenterPosition;
            IsSetupField = inner.IsSetupField;
            MetersetPerGy = inner.MetersetPerGy;
            MLCPlanType = inner.MLCPlanType;
            MLCTransmissionFactor = inner.MLCTransmissionFactor;
            MotionCompensationTechnique = inner.MotionCompensationTechnique;
            MotionSignalSource = inner.MotionSignalSource;
            NormalizationFactor = inner.NormalizationFactor;
            NormalizationMethod = inner.NormalizationMethod;
            PlannedSSD = inner.PlannedSSD;
            SetupTechnique = inner.SetupTechnique;
            SSD = inner.SSD;
            SSDAtStopAngle = inner.SSDAtStopAngle;
            ToleranceTableLabel = inner.ToleranceTableLabel;
            TreatmentTime = inner.TreatmentTime;
            WeightFactor = inner.WeightFactor;
        }

        public void AddBolus(IBolus bolus) => _inner.AddBolus(bolus);
        public bool RemoveBolus(IBolus bolus) => _inner.RemoveBolus(bolus);
        public void AddBolus(string bolusId) => _inner.AddBolus(bolusId);
        public bool AddFlatteningSequence() => _inner.AddFlatteningSequence();
        public void ApplyParameters(IBeamParameters beamParams) => _inner.ApplyParameters(beamParams);
        public Dictionary<int, double> CalculateAverageLeafPairOpenings() => _inner.CalculateAverageLeafPairOpenings();
        public async System.Threading.Tasks.Task<(bool Result, string message)> CanSetOptimalFluenceAsync(Fluence fluence)
        {
            string message_temp;
            var result = await _service.RunAsync(() => _inner.CanSetOptimalFluence(fluence, out message_temp));
            return (result, message_temp);
        }
        public double CollimatorAngleToUser(double val) => _inner.CollimatorAngleToUser(val);
        public int CountSubfields() => _inner.CountSubfields();
        public IImage CreateOrReplaceDRR(DRRCalculationParameters parameters) => _inner.CreateOrReplaceDRR(parameters) is var result && result is null ? null : new AsyncImage(result, _service);
        public void FitArcOptimizationApertureToCollimatorJaws() => _inner.FitArcOptimizationApertureToCollimatorJaws();
        public void FitCollimatorToStructure(FitToStructureMargins margins, IStructure structure, bool useAsymmetricXJaws, bool useAsymmetricYJaws, bool optimizeCollimatorRotation) => _inner.FitCollimatorToStructure(margins, structure, useAsymmetricXJaws, useAsymmetricYJaws, optimizeCollimatorRotation);
        public void FitMLCToOutline(Windows.Point[][] outline) => _inner.FitMLCToOutline(outline);
        public void FitMLCToOutline(Windows.Point[][] outline, bool optimizeCollimatorRotation, JawFitting jawFit, OpenLeavesMeetingPoint olmp, ClosedLeavesMeetingPoint clmp) => _inner.FitMLCToOutline(outline, optimizeCollimatorRotation, jawFit, olmp, clmp);
        public void FitMLCToStructure(IStructure structure) => _inner.FitMLCToStructure(structure);
        public void FitMLCToStructure(FitToStructureMargins margins, IStructure structure, bool optimizeCollimatorRotation, JawFitting jawFit, OpenLeavesMeetingPoint olmp, ClosedLeavesMeetingPoint clmp) => _inner.FitMLCToStructure(margins, structure, optimizeCollimatorRotation, jawFit, olmp, clmp);
        public double GantryAngleToUser(double val) => _inner.GantryAngleToUser(val);
        public double GetCAXPathLengthInBolus(IBolus bolus) => _inner.GetCAXPathLengthInBolus(bolus);
        public IBeamParameters GetEditableParameters() => _inner.GetEditableParameters() is var result && result is null ? null : new AsyncBeamParameters(result, _service);
        public Fluence GetOptimalFluence() => _inner.GetOptimalFluence();
        public VVector GetSourceLocation(double gantryAngle) => _inner.GetSourceLocation(gantryAngle);
        public double GetSourceToBolusDistance(IBolus bolus) => _inner.GetSourceToBolusDistance(bolus);
        public Windows.Point[][] GetStructureOutlines(IStructure structure, bool inBEV) => _inner.GetStructureOutlines(structure, inBEV);
        public string JawPositionsToUserString(VRect<double> val) => _inner.JawPositionsToUserString(val);
        public double PatientSupportAngleToUser(double val) => _inner.PatientSupportAngleToUser(val);
        public bool RemoveBolus(string bolusId) => _inner.RemoveBolus(bolusId);
        public bool RemoveFlatteningSequence() => _inner.RemoveFlatteningSequence();
        public void SetOptimalFluence(Fluence fluence) => _inner.SetOptimalFluence(fluence);
        public MetersetValue Meterset { get; }
        public int BeamNumber { get; }
        public IApplicator Applicator => _inner.Applicator is null ? null : new AsyncApplicator(_inner.Applicator, _service);

        public double ArcLength { get; }
        public ArcOptimizationAperture ArcOptimizationAperture => _inner.ArcOptimizationAperture;
        public async Task SetArcOptimizationApertureAsync(ArcOptimizationAperture value) => _service.RunAsync(() => _inner.ArcOptimizationAperture = value);
        public bool AreControlPointJawsMoving { get; }
        public double AverageSSD { get; }
        public BeamTechnique BeamTechnique { get; }
        public IReadOnlyList<IBlock> Blocks => _inner.Blocks?.Select(x => new AsyncBlock(x, _service)).ToList();
        public IReadOnlyList<IBolus> Boluses => _inner.Boluses?.Select(x => new AsyncBolus(x, _service)).ToList();
        public IReadOnlyList<IBeamCalculationLog> CalculationLogs => _inner.CalculationLogs?.Select(x => new AsyncBeamCalculationLog(x, _service)).ToList();
        public double CollimatorRotation { get; }
        public string CollimatorRotationAsString { get; }
        public ICompensator Compensator => _inner.Compensator is null ? null : new AsyncCompensator(_inner.Compensator, _service);

        public IControlPointCollection ControlPoints => _inner.ControlPoints is null ? null : new AsyncControlPointCollection(_inner.ControlPoints, _service);

        public IReadOnlyList<DateTime> CreationDateTime => _inner.CreationDateTime?.ToList();
        public IBeamDose Dose => _inner.Dose is null ? null : new AsyncBeamDose(_inner.Dose, _service);

        public int DoseRate { get; }
        public double DosimetricLeafGap { get; }
        public IEnergyMode EnergyMode => _inner.EnergyMode is null ? null : new AsyncEnergyMode(_inner.EnergyMode, _service);

        public string EnergyModeDisplayName { get; }
        public IReadOnlyList<IFieldReferencePoint> FieldReferencePoints => _inner.FieldReferencePoints?.Select(x => new AsyncFieldReferencePoint(x, _service)).ToList();
        public GantryDirection GantryDirection { get; }
        public bool HasAllMLCLeavesClosed { get; }
        public bool IsGantryExtended { get; }
        public bool IsGantryExtendedAtStopAngle { get; }
        public bool IsImagingTreatmentField { get; }
        public bool IsIMRT { get; }
        public VVector IsocenterPosition { get; }
        public bool IsSetupField { get; }
        public double MetersetPerGy { get; }
        public IMLC MLC => _inner.MLC is null ? null : new AsyncMLC(_inner.MLC, _service);

        public MLCPlanType MLCPlanType { get; }
        public double MLCTransmissionFactor { get; }
        public string MotionCompensationTechnique { get; }
        public string MotionSignalSource { get; }
        public double NormalizationFactor { get; }
        public string NormalizationMethod { get; }
        public IPlanSetup Plan => _inner.Plan is null ? null : new AsyncPlanSetup(_inner.Plan, _service);

        public double PlannedSSD { get; }
        public IImage ReferenceImage => _inner.ReferenceImage is null ? null : new AsyncImage(_inner.ReferenceImage, _service);

        public string SetupNote => _inner.SetupNote;
        public async Task SetSetupNoteAsync(string value) => _service.RunAsync(() => _inner.SetupNote = value);
        public SetupTechnique SetupTechnique { get; }
        public double SSD { get; }
        public double SSDAtStopAngle { get; }
        public ITechnique Technique => _inner.Technique is null ? null : new AsyncTechnique(_inner.Technique, _service);

        public string ToleranceTableLabel { get; }
        public IReadOnlyList<ITray> Trays => _inner.Trays?.Select(x => new AsyncTray(x, _service)).ToList();
        public double TreatmentTime { get; }
        public IExternalBeamTreatmentUnit TreatmentUnit => _inner.TreatmentUnit is null ? null : new AsyncExternalBeamTreatmentUnit(_inner.TreatmentUnit, _service);

        public IReadOnlyList<IWedge> Wedges => _inner.Wedges?.Select(x => new AsyncWedge(x, _service)).ToList();
        public double WeightFactor { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Beam> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Beam, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
