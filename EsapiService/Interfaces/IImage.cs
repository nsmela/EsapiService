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
    public interface IImage : IApiDataObject
    {
        // --- Simple Properties --- //
        string CalibrationProtocolDescription { get; }
        string CalibrationProtocolId { get; }
        string CalibrationProtocolImageMatchWarning { get; }
        VMS.TPS.Common.Model.CalibrationProtocolStatus CalibrationProtocolStatus { get; }
        VMS.TPS.Common.Model.UserInfo CalibrationProtocolUser { get; }
        string ContrastBolusAgentIngredientName { get; }
        string DisplayUnit { get; }
        string FOR { get; }
        bool HasUserOrigin { get; }
        string ImageType { get; }
        string ImagingDeviceId { get; }
        PatientOrientation ImagingOrientation { get; }
        string ImagingOrientationAsString { get; }
        bool IsProcessed { get; }
        int Level { get; }
        SeriesModality Modality { get; }
        VVector Origin { get; }
        string UID { get; }
        VVector UserOrigin { get; }
        Task SetUserOriginAsync(VVector value);
        string UserOriginComments { get; }
        int Window { get; }
        VVector XDirection { get; }
        double XRes { get; }
        int XSize { get; }
        VVector YDirection { get; }
        double YRes { get; }
        int YSize { get; }
        VVector ZDirection { get; }
        double ZRes { get; }
        int ZSize { get; }

        // --- Accessors --- //
        Task<ISeries> GetSeriesAsync();

        // --- Collections --- //
        IReadOnlyList<ImageApprovalHistoryEntry> ApprovalHistory { get; }
        IReadOnlyList<DateTime> CalibrationProtocolDateTime { get; }
        IReadOnlyList<DateTime> CalibrationProtocolLastModifiedDateTime { get; }
        IReadOnlyList<DateTime> CreationDateTime { get; }

        // --- Methods --- //
        Task CalculateDectProtonStoppingPowersAsync(IImage rhoImage, IImage zImage, int planeIndex, double[,] preallocatedBuffer);
        Task<IStructureSet> CreateNewStructureSetAsync();
        Task<VVector> DicomToUserAsync(VVector dicom, IPlanSetup planSetup);
        Task<ImageProfile> GetImageProfileAsync(VVector start, VVector stop, double[] preallocatedBuffer);
        Task<bool> GetProtonStoppingPowerCurveAsync(SortedList<double, double> protonStoppingPowerCurve);
        Task GetVoxelsAsync(int planeIndex, int[,] preallocatedBuffer);
        Task<VVector> UserToDicomAsync(VVector user, IPlanSetup planSetup);
        Task<double> VoxelToDisplayValueAsync(int voxelValue);

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Image object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Image> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Image object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Image, T> func);
    }
}
