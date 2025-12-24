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
    public class AsyncBeam : AsyncApiDataObject, IBeam, IEsapiWrapper<VMS.TPS.Common.Model.API.Beam>
    {
        internal new readonly VMS.TPS.Common.Model.API.Beam _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncBeam(VMS.TPS.Common.Model.API.Beam inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            Meterset = inner.Meterset;
            BeamNumber = inner.BeamNumber;
            ArcLength = inner.ArcLength;
            ArcOptimizationAperture = inner.ArcOptimizationAperture;
            AreControlPointJawsMoving = inner.AreControlPointJawsMoving;
            AverageSSD = inner.AverageSSD;
            BeamTechnique = inner.BeamTechnique;
            CollimatorRotation = inner.CollimatorRotation;
            CollimatorRotationAsString = inner.CollimatorRotationAsString;
            CreationDateTime = inner.CreationDateTime;
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
            SetupNote = inner.SetupNote;
            SetupTechnique = inner.SetupTechnique;
            SSD = inner.SSD;
            SSDAtStopAngle = inner.SSDAtStopAngle;
            ToleranceTableLabel = inner.ToleranceTableLabel;
            TreatmentTime = inner.TreatmentTime;
            WeightFactor = inner.WeightFactor;
        }


        // Simple Void Method
        public Task AddBolusAsync(IBolus bolus) 
        {
            _service.PostAsync(context => _inner.AddBolus(((AsyncBolus)bolus)._inner));
            Refresh();
            return Task.CompletedTask;
        }

        // Simple Method
        public Task<bool> RemoveBolusAsync(IBolus bolus) => 
            _service.PostAsync(context => _inner.RemoveBolus(((AsyncBolus)bolus)._inner));

        // Simple Void Method
        public Task AddBolusAsync(string bolusId) 
        {
            _service.PostAsync(context => _inner.AddBolus(bolusId));
            Refresh();
            return Task.CompletedTask;
        }

        // Simple Method
        public Task<bool> AddFlatteningSequenceAsync() => 
            _service.PostAsync(context => _inner.AddFlatteningSequence());

        // Simple Void Method
        public Task ApplyParametersAsync(IBeamParameters beamParams) 
        {
            _service.PostAsync(context => _inner.ApplyParameters(((AsyncBeamParameters)beamParams)._inner));
            Refresh();
            return Task.CompletedTask;
        }

        // Simple Method
        public Task<Dictionary<int, double>> CalculateAverageLeafPairOpeningsAsync() => 
            _service.PostAsync(context => _inner.CalculateAverageLeafPairOpenings());

        public async Task<(bool result, string message)> CanSetOptimalFluenceAsync(Fluence fluence)
        {
            var postResult = await _service.PostAsync(context => {
                string message_temp = default(string);
                var result = _inner.CanSetOptimalFluence(fluence, out message_temp);
                return (result, message_temp);
            });
            return (postResult.Item1,
                    postResult.Item2);
        }


        // Simple Method
        public Task<double> CollimatorAngleToUserAsync(double val) => 
            _service.PostAsync(context => _inner.CollimatorAngleToUser(val));

        // Simple Method
        public Task<int> CountSubfieldsAsync() => 
            _service.PostAsync(context => _inner.CountSubfields());

        public async Task<IImage> CreateOrReplaceDRRAsync(DRRCalculationParameters parameters)
        {
            return await _service.PostAsync(context => 
                _inner.CreateOrReplaceDRR(parameters) is var result && result is null ? null : new AsyncImage(result, _service));
        }

        // Simple Void Method
        public Task FitArcOptimizationApertureToCollimatorJawsAsync() 
        {
            _service.PostAsync(context => _inner.FitArcOptimizationApertureToCollimatorJaws());
            Refresh();
            return Task.CompletedTask;
        }

        // Simple Void Method
        public Task FitCollimatorToStructureAsync(FitToStructureMargins margins, IStructure structure, bool useAsymmetricXJaws, bool useAsymmetricYJaws, bool optimizeCollimatorRotation) 
        {
            _service.PostAsync(context => _inner.FitCollimatorToStructure(margins, ((AsyncStructure)structure)._inner, useAsymmetricXJaws, useAsymmetricYJaws, optimizeCollimatorRotation));
            Refresh();
            return Task.CompletedTask;
        }

        // Simple Void Method
        public Task FitMLCToOutlineAsync(System.Windows.Point[][] outline) 
        {
            _service.PostAsync(context => _inner.FitMLCToOutline(outline));
            Refresh();
            return Task.CompletedTask;
        }

        // Simple Void Method
        public Task FitMLCToOutlineAsync(System.Windows.Point[][] outline, bool optimizeCollimatorRotation, JawFitting jawFit, OpenLeavesMeetingPoint olmp, ClosedLeavesMeetingPoint clmp) 
        {
            _service.PostAsync(context => _inner.FitMLCToOutline(outline, optimizeCollimatorRotation, jawFit, olmp, clmp));
            Refresh();
            return Task.CompletedTask;
        }

        // Simple Void Method
        public Task FitMLCToStructureAsync(IStructure structure) 
        {
            _service.PostAsync(context => _inner.FitMLCToStructure(((AsyncStructure)structure)._inner));
            Refresh();
            return Task.CompletedTask;
        }

        // Simple Void Method
        public Task FitMLCToStructureAsync(FitToStructureMargins margins, IStructure structure, bool optimizeCollimatorRotation, JawFitting jawFit, OpenLeavesMeetingPoint olmp, ClosedLeavesMeetingPoint clmp) 
        {
            _service.PostAsync(context => _inner.FitMLCToStructure(margins, ((AsyncStructure)structure)._inner, optimizeCollimatorRotation, jawFit, olmp, clmp));
            Refresh();
            return Task.CompletedTask;
        }

        // Simple Method
        public Task<double> GantryAngleToUserAsync(double val) => 
            _service.PostAsync(context => _inner.GantryAngleToUser(val));

        // Simple Method
        public Task<double> GetCAXPathLengthInBolusAsync(IBolus bolus) => 
            _service.PostAsync(context => _inner.GetCAXPathLengthInBolus(((AsyncBolus)bolus)._inner));

        public async Task<IBeamParameters> GetEditableParametersAsync()
        {
            return await _service.PostAsync(context => 
                _inner.GetEditableParameters() is var result && result is null ? null : new AsyncBeamParameters(result, _service));
        }

        // Simple Method
        public Task<Fluence> GetOptimalFluenceAsync() => 
            _service.PostAsync(context => _inner.GetOptimalFluence());

        // Simple Method
        public Task<VVector> GetSourceLocationAsync(double gantryAngle) => 
            _service.PostAsync(context => _inner.GetSourceLocation(gantryAngle));

        // Simple Method
        public Task<double> GetSourceToBolusDistanceAsync(IBolus bolus) => 
            _service.PostAsync(context => _inner.GetSourceToBolusDistance(((AsyncBolus)bolus)._inner));

        // Simple Method
        public Task<System.Windows.Point[][]> GetStructureOutlinesAsync(IStructure structure, bool inBEV) => 
            _service.PostAsync(context => _inner.GetStructureOutlines(((AsyncStructure)structure)._inner, inBEV));

        // Simple Method
        public Task<string> JawPositionsToUserStringAsync(VRect<double> val) => 
            _service.PostAsync(context => _inner.JawPositionsToUserString(val));

        // Simple Method
        public Task<double> PatientSupportAngleToUserAsync(double val) => 
            _service.PostAsync(context => _inner.PatientSupportAngleToUser(val));

        // Simple Method
        public Task<bool> RemoveBolusAsync(string bolusId) => 
            _service.PostAsync(context => _inner.RemoveBolus(bolusId));

        // Simple Method
        public Task<bool> RemoveFlatteningSequenceAsync() => 
            _service.PostAsync(context => _inner.RemoveFlatteningSequence());

        // Simple Void Method
        public Task SetOptimalFluenceAsync(Fluence fluence) 
        {
            _service.PostAsync(context => _inner.SetOptimalFluence(fluence));
            Refresh();
            return Task.CompletedTask;
        }

        public MetersetValue Meterset { get; private set; }


        public int BeamNumber { get; private set; }


        public async Task<IApplicator> GetApplicatorAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Applicator is null ? null : new AsyncApplicator(_inner.Applicator, _service);
                return innerResult;
            });
        }

        public double ArcLength { get; private set; }


        public ArcOptimizationAperture ArcOptimizationAperture { get; private set; }
        public async Task SetArcOptimizationApertureAsync(ArcOptimizationAperture value)
        {
            ArcOptimizationAperture = await _service.PostAsync(context => 
            {
                _inner.ArcOptimizationAperture = value;
                return _inner.ArcOptimizationAperture;
            });
        }


        public bool AreControlPointJawsMoving { get; private set; }


        public double AverageSSD { get; private set; }


        public BeamTechnique BeamTechnique { get; private set; }


        public async Task<IReadOnlyList<IBlock>> GetBlocksAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Blocks?.Select(x => new AsyncBlock(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IBolus>> GetBolusesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Boluses?.Select(x => new AsyncBolus(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IBeamCalculationLog>> GetCalculationLogsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.CalculationLogs?.Select(x => new AsyncBeamCalculationLog(x, _service)).ToList());
        }


        public double CollimatorRotation { get; private set; }


        public string CollimatorRotationAsString { get; private set; }


        public async Task<ICompensator> GetCompensatorAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Compensator is null ? null : new AsyncCompensator(_inner.Compensator, _service);
                return innerResult;
            });
        }

        public async Task<IControlPointCollection> GetControlPointsAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.ControlPoints is null ? null : new AsyncControlPointCollection(_inner.ControlPoints, _service);
                return innerResult;
            });
        }

        public DateTime? CreationDateTime { get; private set; }


        public async Task<IBeamDose> GetDoseAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Dose is null ? null : new AsyncBeamDose(_inner.Dose, _service);
                return innerResult;
            });
        }

        public int DoseRate { get; private set; }


        public double DosimetricLeafGap { get; private set; }


        public async Task<IEnergyMode> GetEnergyModeAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.EnergyMode is null ? null : new AsyncEnergyMode(_inner.EnergyMode, _service);
                return innerResult;
            });
        }

        public string EnergyModeDisplayName { get; private set; }


        public async Task<IReadOnlyList<IFieldReferencePoint>> GetFieldReferencePointsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.FieldReferencePoints?.Select(x => new AsyncFieldReferencePoint(x, _service)).ToList());
        }


        public GantryDirection GantryDirection { get; private set; }


        public bool HasAllMLCLeavesClosed { get; private set; }


        public bool IsGantryExtended { get; private set; }


        public bool IsGantryExtendedAtStopAngle { get; private set; }


        public bool IsImagingTreatmentField { get; private set; }


        public bool IsIMRT { get; private set; }


        public VVector IsocenterPosition { get; private set; }


        public bool IsSetupField { get; private set; }


        public double MetersetPerGy { get; private set; }


        public async Task<IMLC> GetMLCAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.MLC is null ? null : new AsyncMLC(_inner.MLC, _service);
                return innerResult;
            });
        }

        public MLCPlanType MLCPlanType { get; private set; }


        public double MLCTransmissionFactor { get; private set; }


        public string MotionCompensationTechnique { get; private set; }


        public string MotionSignalSource { get; private set; }


        public double NormalizationFactor { get; private set; }


        public string NormalizationMethod { get; private set; }


        public async Task<IPlanSetup> GetPlanAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Plan is null ? null : new AsyncPlanSetup(_inner.Plan, _service);
                return innerResult;
            });
        }

        public double PlannedSSD { get; private set; }


        public async Task<IImage> GetReferenceImageAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.ReferenceImage is null ? null : new AsyncImage(_inner.ReferenceImage, _service);
                return innerResult;
            });
        }

        public string SetupNote { get; private set; }
        public async Task SetSetupNoteAsync(string value)
        {
            SetupNote = await _service.PostAsync(context => 
            {
                _inner.SetupNote = value;
                return _inner.SetupNote;
            });
        }


        public SetupTechnique SetupTechnique { get; private set; }


        public double SSD { get; private set; }


        public double SSDAtStopAngle { get; private set; }


        public async Task<ITechnique> GetTechniqueAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Technique is null ? null : new AsyncTechnique(_inner.Technique, _service);
                return innerResult;
            });
        }

        public string ToleranceTableLabel { get; private set; }


        public async Task<IReadOnlyList<ITray>> GetTraysAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Trays?.Select(x => new AsyncTray(x, _service)).ToList());
        }


        public double TreatmentTime { get; private set; }


        public async Task<IExternalBeamTreatmentUnit> GetTreatmentUnitAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.TreatmentUnit is null ? null : new AsyncExternalBeamTreatmentUnit(_inner.TreatmentUnit, _service);
                return innerResult;
            });
        }

        public async Task<IReadOnlyList<IWedge>> GetWedgesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Wedges?.Select(x => new AsyncWedge(x, _service)).ToList());
        }


        public double WeightFactor { get; private set; }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Beam> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Beam, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            Meterset = _inner.Meterset;
            BeamNumber = _inner.BeamNumber;
            ArcLength = _inner.ArcLength;
            ArcOptimizationAperture = _inner.ArcOptimizationAperture;
            AreControlPointJawsMoving = _inner.AreControlPointJawsMoving;
            AverageSSD = _inner.AverageSSD;
            BeamTechnique = _inner.BeamTechnique;
            CollimatorRotation = _inner.CollimatorRotation;
            CollimatorRotationAsString = _inner.CollimatorRotationAsString;
            CreationDateTime = _inner.CreationDateTime;
            DoseRate = _inner.DoseRate;
            DosimetricLeafGap = _inner.DosimetricLeafGap;
            EnergyModeDisplayName = _inner.EnergyModeDisplayName;
            GantryDirection = _inner.GantryDirection;
            HasAllMLCLeavesClosed = _inner.HasAllMLCLeavesClosed;
            IsGantryExtended = _inner.IsGantryExtended;
            IsGantryExtendedAtStopAngle = _inner.IsGantryExtendedAtStopAngle;
            IsImagingTreatmentField = _inner.IsImagingTreatmentField;
            IsIMRT = _inner.IsIMRT;
            IsocenterPosition = _inner.IsocenterPosition;
            IsSetupField = _inner.IsSetupField;
            MetersetPerGy = _inner.MetersetPerGy;
            MLCPlanType = _inner.MLCPlanType;
            MLCTransmissionFactor = _inner.MLCTransmissionFactor;
            MotionCompensationTechnique = _inner.MotionCompensationTechnique;
            MotionSignalSource = _inner.MotionSignalSource;
            NormalizationFactor = _inner.NormalizationFactor;
            NormalizationMethod = _inner.NormalizationMethod;
            PlannedSSD = _inner.PlannedSSD;
            SetupNote = _inner.SetupNote;
            SetupTechnique = _inner.SetupTechnique;
            SSD = _inner.SSD;
            SSDAtStopAngle = _inner.SSDAtStopAngle;
            ToleranceTableLabel = _inner.ToleranceTableLabel;
            TreatmentTime = _inner.TreatmentTime;
            WeightFactor = _inner.WeightFactor;
        }

        public static implicit operator VMS.TPS.Common.Model.API.Beam(AsyncBeam wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Beam IEsapiWrapper<VMS.TPS.Common.Model.API.Beam>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Beam>.Service => _service;

        /* --- Skipped Members (Not generated) ---
           - Id: Shadows base member in wrapped base class
           - Name: Shadows base member in wrapped base class
           - Comment: Shadows base member in wrapped base class
        */
    }
}
