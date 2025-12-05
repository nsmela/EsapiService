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
        Task AddBolusAsync(VMS.TPS.Common.Model.API.Bolus bolus);
        Task<bool> RemoveBolusAsync(VMS.TPS.Common.Model.API.Bolus bolus);
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task AddBolusAsync(string bolusId);
        Task<bool> AddFlatteningSequenceAsync();
        Task ApplyParametersAsync(VMS.TPS.Common.Model.API.BeamParameters beamParams);
        Task<System.Collections.Generic.Dictionary<int, double>> CalculateAverageLeafPairOpeningsAsync();
        Task<(bool Result, string message)> CanSetOptimalFluenceAsync(VMS.TPS.Common.Model.Types.Fluence fluence);
        Task<double> CollimatorAngleToUserAsync(double val);
        Task<int> CountSubfieldsAsync();
        Task<IImage> CreateOrReplaceDRRAsync(VMS.TPS.Common.Model.Types.DRRCalculationParameters parameters);
        Task FitArcOptimizationApertureToCollimatorJawsAsync();
        Task FitCollimatorToStructureAsync(VMS.TPS.Common.Model.Types.FitToStructureMargins margins, VMS.TPS.Common.Model.API.Structure structure, bool useAsymmetricXJaws, bool useAsymmetricYJaws, bool optimizeCollimatorRotation);
        Task FitMLCToOutlineAsync(System.Windows.Point[][] outline);
        Task FitMLCToOutlineAsync(System.Windows.Point[][] outline, bool optimizeCollimatorRotation, VMS.TPS.Common.Model.Types.JawFitting jawFit, VMS.TPS.Common.Model.Types.OpenLeavesMeetingPoint olmp, VMS.TPS.Common.Model.Types.ClosedLeavesMeetingPoint clmp);
        Task FitMLCToStructureAsync(VMS.TPS.Common.Model.API.Structure structure);
        Task FitMLCToStructureAsync(VMS.TPS.Common.Model.Types.FitToStructureMargins margins, VMS.TPS.Common.Model.API.Structure structure, bool optimizeCollimatorRotation, VMS.TPS.Common.Model.Types.JawFitting jawFit, VMS.TPS.Common.Model.Types.OpenLeavesMeetingPoint olmp, VMS.TPS.Common.Model.Types.ClosedLeavesMeetingPoint clmp);
        Task<double> GantryAngleToUserAsync(double val);
        Task<double> GetCAXPathLengthInBolusAsync(VMS.TPS.Common.Model.API.Bolus bolus);
        Task<IBeamParameters> GetEditableParametersAsync();
        Task<VMS.TPS.Common.Model.Types.Fluence> GetOptimalFluenceAsync();
        Task<VMS.TPS.Common.Model.Types.VVector> GetSourceLocationAsync(double gantryAngle);
        Task<double> GetSourceToBolusDistanceAsync(VMS.TPS.Common.Model.API.Bolus bolus);
        Task<System.Windows.Point[][]> GetStructureOutlinesAsync(VMS.TPS.Common.Model.API.Structure structure, bool inBEV);
        Task<string> JawPositionsToUserStringAsync(VMS.TPS.Common.Model.Types.VRect<double> val);
        Task<double> PatientSupportAngleToUserAsync(double val);
        Task<bool> RemoveBolusAsync(string bolusId);
        Task<bool> RemoveFlatteningSequenceAsync();
        Task SetOptimalFluenceAsync(VMS.TPS.Common.Model.Types.Fluence fluence);
        string Id { get; }
        Task SetIdAsync(string value);
        string Name { get; }
        Task SetNameAsync(string value);
        string Comment { get; }
        Task SetCommentAsync(string value);
        VMS.TPS.Common.Model.Types.MetersetValue Meterset { get; }
        int BeamNumber { get; }
        Task<IApplicator> GetApplicatorAsync();
        double ArcLength { get; }
        VMS.TPS.Common.Model.Types.ArcOptimizationAperture ArcOptimizationAperture { get; }
        Task SetArcOptimizationApertureAsync(VMS.TPS.Common.Model.Types.ArcOptimizationAperture value);
        bool AreControlPointJawsMoving { get; }
        double AverageSSD { get; }
        VMS.TPS.Common.Model.Types.BeamTechnique BeamTechnique { get; }
        System.Collections.Generic.IReadOnlyList<IBlock> Blocks { get; }
        System.Collections.Generic.IReadOnlyList<IBolus> Boluses { get; }
        System.Collections.Generic.IReadOnlyList<IBeamCalculationLog> CalculationLogs { get; }
        double CollimatorRotation { get; }
        string CollimatorRotationAsString { get; }
        Task<ICompensator> GetCompensatorAsync();
        Task<IControlPointCollection> GetControlPointsAsync();
        System.Collections.Generic.IReadOnlyList<System.DateTime> CreationDateTime { get; }
        Task<IBeamDose> GetDoseAsync();
        int DoseRate { get; }
        double DosimetricLeafGap { get; }
        Task<IEnergyMode> GetEnergyModeAsync();
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
        Task<IMLC> GetMLCAsync();
        VMS.TPS.Common.Model.Types.MLCPlanType MLCPlanType { get; }
        double MLCTransmissionFactor { get; }
        string MotionCompensationTechnique { get; }
        string MotionSignalSource { get; }
        double NormalizationFactor { get; }
        string NormalizationMethod { get; }
        Task<IPlanSetup> GetPlanAsync();
        double PlannedSSD { get; }
        Task<IImage> GetReferenceImageAsync();
        string SetupNote { get; }
        Task SetSetupNoteAsync(string value);
        VMS.TPS.Common.Model.Types.SetupTechnique SetupTechnique { get; }
        double SSD { get; }
        double SSDAtStopAngle { get; }
        Task<ITechnique> GetTechniqueAsync();
        string ToleranceTableLabel { get; }
        System.Collections.Generic.IReadOnlyList<ITray> Trays { get; }
        double TreatmentTime { get; }
        Task<IExternalBeamTreatmentUnit> GetTreatmentUnitAsync();
        System.Collections.Generic.IReadOnlyList<IWedge> Wedges { get; }
        double WeightFactor { get; }

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
