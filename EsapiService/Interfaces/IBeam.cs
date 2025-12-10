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
        int BeamNumber { get; }
        double ArcLength { get; }
        bool AreControlPointJawsMoving { get; }
        double AverageSSD { get; }
        double CollimatorRotation { get; }
        string CollimatorRotationAsString { get; }
        DateTime? CreationDateTime { get; }
        int DoseRate { get; }
        double DosimetricLeafGap { get; }
        string EnergyModeDisplayName { get; }
        bool HasAllMLCLeavesClosed { get; }
        bool IsGantryExtended { get; }
        bool IsGantryExtendedAtStopAngle { get; }
        bool IsImagingTreatmentField { get; }
        bool IsIMRT { get; }
        bool IsSetupField { get; }
        double MetersetPerGy { get; }
        double MLCTransmissionFactor { get; }
        string MotionCompensationTechnique { get; }
        string MotionSignalSource { get; }
        double NormalizationFactor { get; }
        string NormalizationMethod { get; }
        double PlannedSSD { get; }
        string SetupNote { get; }
        Task SetSetupNoteAsync(string value);
        double SSD { get; }
        double SSDAtStopAngle { get; }
        string ToleranceTableLabel { get; }
        double TreatmentTime { get; }
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

        // --- Collections --- //
        Task<IReadOnlyList<IBlock>> GetBlocksAsync(); // collection proeprty context
        Task<IReadOnlyList<IBolus>> GetBolusesAsync(); // collection proeprty context
        Task<IReadOnlyList<IBeamCalculationLog>> GetCalculationLogsAsync(); // collection proeprty context
        Task<IReadOnlyList<IFieldReferencePoint>> GetFieldReferencePointsAsync(); // collection proeprty context
        Task<IReadOnlyList<ITray>> GetTraysAsync(); // collection proeprty context
        Task<IReadOnlyList<IWedge>> GetWedgesAsync(); // collection proeprty context

        // --- Methods --- //
        Task AddBolusAsync(IBolus bolus); // void method
        Task<bool> RemoveBolusAsync(IBolus bolus); // simple method
        Task AddBolusAsync(string bolusId); // void method
        Task<bool> AddFlatteningSequenceAsync(); // simple method
        Task ApplyParametersAsync(IBeamParameters beamParams); // void method
        Task<Dictionary<int, double>> CalculateAverageLeafPairOpeningsAsync(); // simple method
        Task<double> CollimatorAngleToUserAsync(double val); // simple method
        Task<int> CountSubfieldsAsync(); // simple method
        Task FitArcOptimizationApertureToCollimatorJawsAsync(); // void method
        Task FitMLCToOutlineAsync(System.Windows.Point[][] outline); // void method
        Task FitMLCToStructureAsync(IStructure structure); // void method
        Task<double> GantryAngleToUserAsync(double val); // simple method
        Task<double> GetCAXPathLengthInBolusAsync(IBolus bolus); // simple method
        Task<IBeamParameters> GetEditableParametersAsync(); // complex method
        Task<double> GetSourceToBolusDistanceAsync(IBolus bolus); // simple method
        Task<System.Windows.Point[][]> GetStructureOutlinesAsync(IStructure structure, bool inBEV); // simple method
        Task<string> JawPositionsToUserStringAsync(VRect<double> val); // simple method
        Task<double> PatientSupportAngleToUserAsync(double val); // simple method
        Task<bool> RemoveBolusAsync(string bolusId); // simple method
        Task<bool> RemoveFlatteningSequenceAsync(); // simple method

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
