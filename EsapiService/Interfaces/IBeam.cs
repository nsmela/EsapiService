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
        new string Id { get; set; } // simple property
        new string Name { get; set; } // simple property
        new string Comment { get; set; } // simple property
        MetersetValue Meterset { get; } // simple property
        int BeamNumber { get; } // simple property
        double ArcLength { get; } // simple property
        bool AreControlPointJawsMoving { get; } // simple property
        double AverageSSD { get; } // simple property
        BeamTechnique BeamTechnique { get; } // simple property
        double CollimatorRotation { get; } // simple property
        DateTime? CreationDateTime { get; } // simple property
        int DoseRate { get; } // simple property
        double DosimetricLeafGap { get; } // simple property
        string EnergyModeDisplayName { get; } // simple property
        GantryDirection GantryDirection { get; } // simple property
        bool HasAllMLCLeavesClosed { get; } // simple property
        bool IsGantryExtended { get; } // simple property
        bool IsGantryExtendedAtStopAngle { get; } // simple property
        bool IsImagingTreatmentField { get; } // simple property
        bool IsIMRT { get; } // simple property
        VVector IsocenterPosition { get; } // simple property
        bool IsSetupField { get; } // simple property
        double MetersetPerGy { get; } // simple property
        MLCPlanType MLCPlanType { get; } // simple property
        double MLCTransmissionFactor { get; } // simple property
        string MotionCompensationTechnique { get; } // simple property
        string MotionSignalSource { get; } // simple property
        double NormalizationFactor { get; } // simple property
        string NormalizationMethod { get; } // simple property
        double PlannedSSD { get; } // simple property
        string SetupNote { get; set; } // simple property
        SetupTechnique SetupTechnique { get; } // simple property
        double SSD { get; } // simple property
        double SSDAtStopAngle { get; } // simple property
        string ToleranceTableLabel { get; } // simple property
        double TreatmentTime { get; } // simple property
        double WeightFactor { get; } // simple property

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
        Task<IReadOnlyList<IBlock>> GetBlocksAsync(); // collection property context
        Task<IReadOnlyList<IBolus>> GetBolusesAsync(); // collection property context
        Task<IReadOnlyList<IBeamCalculationLog>> GetCalculationLogsAsync(); // collection property context
        Task<IReadOnlyList<IFieldReferencePoint>> GetFieldReferencePointsAsync(); // collection property context
        Task<IReadOnlyList<ITray>> GetTraysAsync(); // collection property context
        Task<IReadOnlyList<IWedge>> GetWedgesAsync(); // collection property context

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

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        new bool IsValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        new bool IsNotValid();
    }
}
