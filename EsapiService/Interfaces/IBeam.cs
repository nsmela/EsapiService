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
    public interface IBeam : IApiDataObject
    {
        // --- Simple Properties --- //
        MetersetValue Meterset { get; }
        int BeamNumber { get; }
        double ArcLength { get; }
        bool AreControlPointJawsMoving { get; }
        double AverageSSD { get; }
        BeamTechnique BeamTechnique { get; }
        IEnumerable<Block> Blocks { get; }
        IEnumerable<Bolus> Boluses { get; }
        IEnumerable<BeamCalculationLog> CalculationLogs { get; }
        double CollimatorRotation { get; }
        DateTime? CreationDateTime { get; }
        int DoseRate { get; }
        double DosimetricLeafGap { get; }
        string EnergyModeDisplayName { get; }
        IEnumerable<FieldReferencePoint> FieldReferencePoints { get; }
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
        IEnumerable<Tray> Trays { get; }
        double TreatmentTime { get; }
        IEnumerable<Wedge> Wedges { get; }
        double WeightFactor { get; }

        // --- Accessors --- //
        Task<IApplicator> GetApplicatorAsync(); // read complex property
        Task<ICompensator> GetCompensatorAsync(); // read complex property
        Task<IControlPointCollection> GetControlPointsAsync(); // read complex property
        Task<IBeamDose> GetDoseAsync(); // read complex property
        Task<IEnergyMode> GetEnergyModeAsync(); // read complex property
        Task<IMLC> GetMLCAsync(); // read complex property
        Task<IPlanSetup> GetPlanAsync(); // read complex property
        Task<IImage> GetReferenceImageAsync(); // read complex property
        Task<ITechnique> GetTechniqueAsync(); // read complex property
        Task<IExternalBeamTreatmentUnit> GetTreatmentUnitAsync(); // read complex property

        // --- Methods --- //
        Task AddBolusAsync(IBolus bolus); // void method
        Task<bool> RemoveBolusAsync(IBolus bolus); // simple method
        Task AddBolusAsync(string bolusId); // void method
        Task<bool> AddFlatteningSequenceAsync(); // simple method
        Task ApplyParametersAsync(IBeamParameters beamParams); // void method
        Task<Dictionary<int, double>> CalculateAverageLeafPairOpeningsAsync(); // simple method
        Task<(bool result, string message)> CanSetOptimalFluenceAsync(Fluence fluence); // out/ref parameter method
        Task<double> CollimatorAngleToUserAsync(double val); // simple method
        Task<int> CountSubfieldsAsync(); // simple method
        Task<IImage> CreateOrReplaceDRRAsync(DRRCalculationParameters parameters); // complex method
        Task FitCollimatorToStructureAsync(FitToStructureMargins margins, IStructure structure, bool useAsymmetricXJaws, bool useAsymmetricYJaws, bool optimizeCollimatorRotation); // void method
        Task FitMLCToOutlineAsync(System.Windows.Point[][] outline); // void method
        Task FitMLCToOutlineAsync(System.Windows.Point[][] outline, bool optimizeCollimatorRotation, JawFitting jawFit, OpenLeavesMeetingPoint olmp, ClosedLeavesMeetingPoint clmp); // void method
        Task FitMLCToStructureAsync(IStructure structure); // void method
        Task FitMLCToStructureAsync(FitToStructureMargins margins, IStructure structure, bool optimizeCollimatorRotation, JawFitting jawFit, OpenLeavesMeetingPoint olmp, ClosedLeavesMeetingPoint clmp); // void method
        Task<double> GantryAngleToUserAsync(double val); // simple method
        Task<double> GetCAXPathLengthInBolusAsync(IBolus bolus); // simple method
        Task<IBeamParameters> GetEditableParametersAsync(); // complex method
        Task<Fluence> GetOptimalFluenceAsync(); // simple method
        Task<VVector> GetSourceLocationAsync(double gantryAngle); // simple method
        Task<double> GetSourceToBolusDistanceAsync(IBolus bolus); // simple method
        Task<System.Windows.Point[][]> GetStructureOutlinesAsync(IStructure structure, bool inBEV); // simple method
        Task<string> JawPositionsToUserStringAsync(VRect<double> val); // simple method
        Task<double> PatientSupportAngleToUserAsync(double val); // simple method
        Task<bool> RemoveBolusAsync(string bolusId); // simple method
        Task<bool> RemoveFlatteningSequenceAsync(); // simple method
        Task SetOptimalFluenceAsync(Fluence fluence); // void method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Beam object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Beam> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Beam object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Beam, T> func);

        /* --- Skipped Members (Not generated) ---
           - Id: Shadows member in wrapped base class
           - Name: Shadows member in wrapped base class
           - Comment: Shadows member in wrapped base class
        */
    }
}
