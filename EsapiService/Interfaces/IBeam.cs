using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;
using Esapi.Interfaces;

namespace Esapi.Interfaces
{
    public interface IBeam : IApiDataObject
    {
        // --- Simple Properties --- //
        MetersetValue Meterset { get; }
        int BeamNumber { get; }
        double ArcLength { get; }
        ArcOptimizationAperture ArcOptimizationAperture { get; }
        Task SetArcOptimizationApertureAsync(ArcOptimizationAperture value);
        bool AreControlPointJawsMoving { get; }
        double AverageSSD { get; }
        BeamTechnique BeamTechnique { get; }
        double CollimatorRotation { get; }
        string CollimatorRotationAsString { get; }
        DateTime? CreationDateTime { get; }
        int DoseRate { get; }
        double DosimetricLeafGap { get; }
        string EnergyModeDisplayName { get; }
        GantryDirection GantryDirection { get; }
        bool HasAllMLCLeavesClosed { get; }
        bool IsGantryExtended { get; }
        bool IsGantryExtendedAtStopAngle { get; }
        bool IsImagingTreatmentField { get; }
        bool IsIMRT { get; }
        VVector IsocenterPosition { get; }
        bool IsSetupField { get; }
        double MetersetPerGy { get; }
        MLCPlanType MLCPlanType { get; }
        double MLCTransmissionFactor { get; }
        string MotionCompensationTechnique { get; }
        string MotionSignalSource { get; }
        double NormalizationFactor { get; }
        string NormalizationMethod { get; }
        double PlannedSSD { get; }
        string SetupNote { get; }
        Task SetSetupNoteAsync(string value);
        SetupTechnique SetupTechnique { get; }
        double SSD { get; }
        double SSDAtStopAngle { get; }
        string ToleranceTableLabel { get; }
        double TreatmentTime { get; }
        double WeightFactor { get; }

        // --- Accessors --- //
        Task<IApplicator> GetApplicatorAsync();
        Task<ICompensator> GetCompensatorAsync();
        Task<IControlPointCollection> GetControlPointsAsync();
        Task<IBeamDose> GetDoseAsync();
        Task<IEnergyMode> GetEnergyModeAsync();
        Task<IMLC> GetMLCAsync();
        Task<IPlanSetup> GetPlanAsync();
        Task<IImage> GetReferenceImageAsync();
        Task<ITechnique> GetTechniqueAsync();
        Task<IExternalBeamTreatmentUnit> GetTreatmentUnitAsync();

        // --- Collections --- //
        Task<IReadOnlyList<IBlock>> GetBlocksAsync();
        Task<IReadOnlyList<IBolus>> GetBolusesAsync();
        Task<IReadOnlyList<IBeamCalculationLog>> GetCalculationLogsAsync();
        Task<IReadOnlyList<IFieldReferencePoint>> GetFieldReferencePointsAsync();
        Task<IReadOnlyList<ITray>> GetTraysAsync();
        Task<IReadOnlyList<IWedge>> GetWedgesAsync();

        // --- Methods --- //
        Task AddBolusAsync(IBolus bolus);
        Task<bool> RemoveBolusAsync(IBolus bolus);
        Task AddBolusAsync(string bolusId);
        Task<bool> AddFlatteningSequenceAsync();
        Task ApplyParametersAsync(IBeamParameters beamParams);
        Task<Dictionary<int, double>> CalculateAverageLeafPairOpeningsAsync();
        Task<(bool Result, string message)> CanSetOptimalFluenceAsync(Fluence fluence);
        Task<double> CollimatorAngleToUserAsync(double val);
        Task<int> CountSubfieldsAsync();
        Task<IImage> CreateOrReplaceDRRAsync(DRRCalculationParameters parameters);
        Task FitArcOptimizationApertureToCollimatorJawsAsync();
        Task FitCollimatorToStructureAsync(FitToStructureMargins margins, IStructure structure, bool useAsymmetricXJaws, bool useAsymmetricYJaws, bool optimizeCollimatorRotation);
        Task FitMLCToOutlineAsync(System.Windows.Point[][] outline);
        Task FitMLCToOutlineAsync(System.Windows.Point[][] outline, bool optimizeCollimatorRotation, JawFitting jawFit, OpenLeavesMeetingPoint olmp, ClosedLeavesMeetingPoint clmp);
        Task FitMLCToStructureAsync(IStructure structure);
        Task FitMLCToStructureAsync(FitToStructureMargins margins, IStructure structure, bool optimizeCollimatorRotation, JawFitting jawFit, OpenLeavesMeetingPoint olmp, ClosedLeavesMeetingPoint clmp);
        Task<double> GantryAngleToUserAsync(double val);
        Task<double> GetCAXPathLengthInBolusAsync(IBolus bolus);
        Task<IBeamParameters> GetEditableParametersAsync();
        Task<Fluence> GetOptimalFluenceAsync();
        Task<VVector> GetSourceLocationAsync(double gantryAngle);
        Task<double> GetSourceToBolusDistanceAsync(IBolus bolus);
        Task<System.Windows.Point[][]> GetStructureOutlinesAsync(IStructure structure, bool inBEV);
        Task<string> JawPositionsToUserStringAsync(VRect<double> val);
        Task<double> PatientSupportAngleToUserAsync(double val);
        Task<bool> RemoveBolusAsync(string bolusId);
        Task<bool> RemoveFlatteningSequenceAsync();
        Task SetOptimalFluenceAsync(Fluence fluence);

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Beam object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Beam> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Beam object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Beam, T> func);
    }
}
