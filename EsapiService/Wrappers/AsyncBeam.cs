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
            _inner = inner;
            _service = service;

            BeamNumber = inner.BeamNumber;
            ArcLength = inner.ArcLength;
            AreControlPointJawsMoving = inner.AreControlPointJawsMoving;
            AverageSSD = inner.AverageSSD;
            Blocks = inner.Blocks;
            Boluses = inner.Boluses;
            CalculationLogs = inner.CalculationLogs;
            CollimatorRotation = inner.CollimatorRotation;
            CollimatorRotationAsString = inner.CollimatorRotationAsString;
            CreationDateTime = inner.CreationDateTime;
            DoseRate = inner.DoseRate;
            DosimetricLeafGap = inner.DosimetricLeafGap;
            EnergyModeDisplayName = inner.EnergyModeDisplayName;
            FieldReferencePoints = inner.FieldReferencePoints;
            HasAllMLCLeavesClosed = inner.HasAllMLCLeavesClosed;
            IsGantryExtended = inner.IsGantryExtended;
            IsGantryExtendedAtStopAngle = inner.IsGantryExtendedAtStopAngle;
            IsImagingTreatmentField = inner.IsImagingTreatmentField;
            IsIMRT = inner.IsIMRT;
            IsSetupField = inner.IsSetupField;
            MetersetPerGy = inner.MetersetPerGy;
            MLCTransmissionFactor = inner.MLCTransmissionFactor;
            MotionCompensationTechnique = inner.MotionCompensationTechnique;
            MotionSignalSource = inner.MotionSignalSource;
            NormalizationFactor = inner.NormalizationFactor;
            NormalizationMethod = inner.NormalizationMethod;
            PlannedSSD = inner.PlannedSSD;
            SetupNote = inner.SetupNote;
            SSD = inner.SSD;
            SSDAtStopAngle = inner.SSDAtStopAngle;
            ToleranceTableLabel = inner.ToleranceTableLabel;
            Trays = inner.Trays;
            TreatmentTime = inner.TreatmentTime;
            Wedges = inner.Wedges;
            WeightFactor = inner.WeightFactor;
        }

        // Simple Void Method
        public Task AddBolusAsync(IBolus bolus) => _service.PostAsync(context => _inner.AddBolus(((AsyncBolus)bolus)._inner));

        // Simple Method
        public Task<bool> RemoveBolusAsync(IBolus bolus) => _service.PostAsync(context => _inner.RemoveBolus(((AsyncBolus)bolus)._inner));

        // Simple Void Method
        public Task AddBolusAsync(string bolusId) => _service.PostAsync(context => _inner.AddBolus(bolusId));

        // Simple Method
        public Task<bool> AddFlatteningSequenceAsync() => _service.PostAsync(context => _inner.AddFlatteningSequence());

        // Simple Void Method
        public Task ApplyParametersAsync(IBeamParameters beamParams) => _service.PostAsync(context => _inner.ApplyParameters(((AsyncBeamParameters)beamParams)._inner));

        // Simple Method
        public Task<Dictionary<int, double>> CalculateAverageLeafPairOpeningsAsync() => _service.PostAsync(context => _inner.CalculateAverageLeafPairOpenings());

        // Simple Method
        public Task<double> CollimatorAngleToUserAsync(double val) => _service.PostAsync(context => _inner.CollimatorAngleToUser(val));

        // Simple Method
        public Task<int> CountSubfieldsAsync() => _service.PostAsync(context => _inner.CountSubfields());

        // Simple Void Method
        public Task FitArcOptimizationApertureToCollimatorJawsAsync() => _service.PostAsync(context => _inner.FitArcOptimizationApertureToCollimatorJaws());

        // Simple Void Method
        public Task FitMLCToOutlineAsync(System.Windows.Point[][] outline) => _service.PostAsync(context => _inner.FitMLCToOutline(outline));

        // Simple Void Method
        public Task FitMLCToStructureAsync(IStructure structure) => _service.PostAsync(context => _inner.FitMLCToStructure(((AsyncStructure)structure)._inner));

        // Simple Method
        public Task<double> GantryAngleToUserAsync(double val) => _service.PostAsync(context => _inner.GantryAngleToUser(val));

        // Simple Method
        public Task<double> GetCAXPathLengthInBolusAsync(IBolus bolus) => _service.PostAsync(context => _inner.GetCAXPathLengthInBolus(((AsyncBolus)bolus)._inner));

        public async Task<IBeamParameters> GetEditableParametersAsync()
        {
            return await _service.PostAsync(context => 
                _inner.GetEditableParameters() is var result && result is null ? null : new AsyncBeamParameters(result, _service));
        }


        // Simple Method
        public Task<double> GetSourceToBolusDistanceAsync(IBolus bolus) => _service.PostAsync(context => _inner.GetSourceToBolusDistance(((AsyncBolus)bolus)._inner));

        // Simple Method
        public Task<System.Windows.Point[][]> GetStructureOutlinesAsync(IStructure structure, bool inBEV) => _service.PostAsync(context => _inner.GetStructureOutlines(((AsyncStructure)structure)._inner, inBEV));

        // Simple Method
        public Task<string> JawPositionsToUserStringAsync(VRect<double> val) => _service.PostAsync(context => _inner.JawPositionsToUserString(val));

        // Simple Method
        public Task<double> PatientSupportAngleToUserAsync(double val) => _service.PostAsync(context => _inner.PatientSupportAngleToUser(val));

        // Simple Method
        public Task<bool> RemoveBolusAsync(string bolusId) => _service.PostAsync(context => _inner.RemoveBolus(bolusId));

        // Simple Method
        public Task<bool> RemoveFlatteningSequenceAsync() => _service.PostAsync(context => _inner.RemoveFlatteningSequence());

        public int BeamNumber { get; }

        public async Task<IApplicator> GetApplicatorAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Applicator is null ? null : new AsyncApplicator(_inner.Applicator, _service));
        }

        public double ArcLength { get; }

        public bool AreControlPointJawsMoving { get; }

        public double AverageSSD { get; }

        public IEnumerable<Block> Blocks { get; }

        public IEnumerable<Bolus> Boluses { get; }

        public IEnumerable<BeamCalculationLog> CalculationLogs { get; }

        public double CollimatorRotation { get; }

        public string CollimatorRotationAsString { get; }

        public async Task<ICompensator> GetCompensatorAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Compensator is null ? null : new AsyncCompensator(_inner.Compensator, _service));
        }

        public async Task<IControlPointCollection> GetControlPointsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.ControlPoints is null ? null : new AsyncControlPointCollection(_inner.ControlPoints, _service));
        }

        public DateTime? CreationDateTime { get; }

        public async Task<IBeamDose> GetDoseAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Dose is null ? null : new AsyncBeamDose(_inner.Dose, _service));
        }

        public int DoseRate { get; }

        public double DosimetricLeafGap { get; }

        public async Task<IEnergyMode> GetEnergyModeAsync()
        {
            return await _service.PostAsync(context => 
                _inner.EnergyMode is null ? null : new AsyncEnergyMode(_inner.EnergyMode, _service));
        }

        public string EnergyModeDisplayName { get; }

        public IEnumerable<FieldReferencePoint> FieldReferencePoints { get; }

        public bool HasAllMLCLeavesClosed { get; }

        public bool IsGantryExtended { get; }

        public bool IsGantryExtendedAtStopAngle { get; }

        public bool IsImagingTreatmentField { get; }

        public bool IsIMRT { get; }

        public bool IsSetupField { get; }

        public double MetersetPerGy { get; }

        public async Task<IMLC> GetMLCAsync()
        {
            return await _service.PostAsync(context => 
                _inner.MLC is null ? null : new AsyncMLC(_inner.MLC, _service));
        }

        public double MLCTransmissionFactor { get; }

        public string MotionCompensationTechnique { get; }

        public string MotionSignalSource { get; }

        public double NormalizationFactor { get; }

        public string NormalizationMethod { get; }

        public async Task<IPlanSetup> GetPlanAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Plan is null ? null : new AsyncPlanSetup(_inner.Plan, _service));
        }

        public double PlannedSSD { get; }

        public async Task<IImage> GetReferenceImageAsync()
        {
            return await _service.PostAsync(context => 
                _inner.ReferenceImage is null ? null : new AsyncImage(_inner.ReferenceImage, _service));
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

        public double SSD { get; }

        public double SSDAtStopAngle { get; }

        public async Task<ITechnique> GetTechniqueAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Technique is null ? null : new AsyncTechnique(_inner.Technique, _service));
        }

        public string ToleranceTableLabel { get; }

        public IEnumerable<Tray> Trays { get; }

        public double TreatmentTime { get; }

        public async Task<IExternalBeamTreatmentUnit> GetTreatmentUnitAsync()
        {
            return await _service.PostAsync(context => 
                _inner.TreatmentUnit is null ? null : new AsyncExternalBeamTreatmentUnit(_inner.TreatmentUnit, _service));
        }

        public IEnumerable<Wedge> Wedges { get; }

        public double WeightFactor { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Beam> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Beam, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.Beam(AsyncBeam wrapper) => wrapper._inner;
        // Internal Explicit Implementation to expose _inner safely
        VMS.TPS.Common.Model.API.Beam IEsapiWrapper<VMS.TPS.Common.Model.API.Beam>.Inner => _inner;
    }
}
