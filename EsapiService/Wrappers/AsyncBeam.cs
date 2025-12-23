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
            AreControlPointJawsMoving = inner.AreControlPointJawsMoving;
            AverageSSD = inner.AverageSSD;
            BeamTechnique = inner.BeamTechnique;
            CollimatorRotation = inner.CollimatorRotation;
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
        public Task AddBolusAsync(IBolus bolus) =>
            _service.PostAsync(context => _inner.AddBolus(((AsyncBolus)bolus)._inner));

        // Simple Method
        public Task<bool> RemoveBolusAsync(IBolus bolus) => 
            _service.PostAsync(context => _inner.RemoveBolus(((AsyncBolus)bolus)._inner));

        // Simple Void Method
        public Task AddBolusAsync(string bolusId) =>
            _service.PostAsync(context => _inner.AddBolus(bolusId));

        // Simple Method
        public Task<bool> AddFlatteningSequenceAsync() => 
            _service.PostAsync(context => _inner.AddFlatteningSequence());

        // Simple Void Method
        public Task ApplyParametersAsync(IBeamParameters beamParams) =>
            _service.PostAsync(context => _inner.ApplyParameters(((AsyncBeamParameters)beamParams)._inner));

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
        public Task FitCollimatorToStructureAsync(FitToStructureMargins margins, IStructure structure, bool useAsymmetricXJaws, bool useAsymmetricYJaws, bool optimizeCollimatorRotation) =>
            _service.PostAsync(context => _inner.FitCollimatorToStructure(margins, ((AsyncStructure)structure)._inner, useAsymmetricXJaws, useAsymmetricYJaws, optimizeCollimatorRotation));

        // Simple Void Method
        public Task FitMLCToOutlineAsync(System.Windows.Point[][] outline) =>
            _service.PostAsync(context => _inner.FitMLCToOutline(outline));

        // Simple Void Method
        public Task FitMLCToOutlineAsync(System.Windows.Point[][] outline, bool optimizeCollimatorRotation, JawFitting jawFit, OpenLeavesMeetingPoint olmp, ClosedLeavesMeetingPoint clmp) =>
            _service.PostAsync(context => _inner.FitMLCToOutline(outline, optimizeCollimatorRotation, jawFit, olmp, clmp));

        // Simple Void Method
        public Task FitMLCToStructureAsync(IStructure structure) =>
            _service.PostAsync(context => _inner.FitMLCToStructure(((AsyncStructure)structure)._inner));

        // Simple Void Method
        public Task FitMLCToStructureAsync(FitToStructureMargins margins, IStructure structure, bool optimizeCollimatorRotation, JawFitting jawFit, OpenLeavesMeetingPoint olmp, ClosedLeavesMeetingPoint clmp) =>
            _service.PostAsync(context => _inner.FitMLCToStructure(margins, ((AsyncStructure)structure)._inner, optimizeCollimatorRotation, jawFit, olmp, clmp));

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
        public Task SetOptimalFluenceAsync(Fluence fluence) =>
            _service.PostAsync(context => _inner.SetOptimalFluence(fluence));

        public MetersetValue Meterset { get; }

        public int BeamNumber { get; }

        public async Task<IApplicator> GetApplicatorAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Applicator is null ? null : new AsyncApplicator(_inner.Applicator, _service);
                return innerResult;
            });
        }

        public double ArcLength { get; }

        public bool AreControlPointJawsMoving { get; }

        public double AverageSSD { get; }

        public BeamTechnique BeamTechnique { get; }

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


        public double CollimatorRotation { get; }

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

        public DateTime? CreationDateTime { get; }

        public async Task<IBeamDose> GetDoseAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Dose is null ? null : new AsyncBeamDose(_inner.Dose, _service);
                return innerResult;
            });
        }

        public int DoseRate { get; }

        public double DosimetricLeafGap { get; }

        public async Task<IEnergyMode> GetEnergyModeAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.EnergyMode is null ? null : new AsyncEnergyMode(_inner.EnergyMode, _service);
                return innerResult;
            });
        }

        public string EnergyModeDisplayName { get; }

        public async Task<IReadOnlyList<IFieldReferencePoint>> GetFieldReferencePointsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.FieldReferencePoints?.Select(x => new AsyncFieldReferencePoint(x, _service)).ToList());
        }


        public GantryDirection GantryDirection { get; }

        public bool HasAllMLCLeavesClosed { get; }

        public bool IsGantryExtended { get; }

        public bool IsGantryExtendedAtStopAngle { get; }

        public bool IsImagingTreatmentField { get; }

        public bool IsIMRT { get; }

        public VVector IsocenterPosition { get; }

        public bool IsSetupField { get; }

        public double MetersetPerGy { get; }

        public async Task<IMLC> GetMLCAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.MLC is null ? null : new AsyncMLC(_inner.MLC, _service);
                return innerResult;
            });
        }

        public MLCPlanType MLCPlanType { get; }

        public double MLCTransmissionFactor { get; }

        public string MotionCompensationTechnique { get; }

        public string MotionSignalSource { get; }

        public double NormalizationFactor { get; }

        public string NormalizationMethod { get; }

        public async Task<IPlanSetup> GetPlanAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Plan is null ? null : new AsyncPlanSetup(_inner.Plan, _service);
                return innerResult;
            });
        }

        public double PlannedSSD { get; }

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

        public SetupTechnique SetupTechnique { get; }

        public double SSD { get; }

        public double SSDAtStopAngle { get; }

        public async Task<ITechnique> GetTechniqueAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Technique is null ? null : new AsyncTechnique(_inner.Technique, _service);
                return innerResult;
            });
        }

        public string ToleranceTableLabel { get; }

        public async Task<IReadOnlyList<ITray>> GetTraysAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Trays?.Select(x => new AsyncTray(x, _service)).ToList());
        }


        public double TreatmentTime { get; }

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


        public double WeightFactor { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Beam> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Beam, T> func) => _service.PostAsync<T>((context) => func(_inner));

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
