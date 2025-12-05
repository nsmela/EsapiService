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
    public interface IImage : IApiDataObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task CalculateDectProtonStoppingPowersAsync(VMS.TPS.Common.Model.API.Image rhoImage, VMS.TPS.Common.Model.API.Image zImage, int planeIndex, double[,] preallocatedBuffer);
        Task<IStructureSet> CreateNewStructureSetAsync();
        Task<VMS.TPS.Common.Model.Types.VVector> DicomToUserAsync(VMS.TPS.Common.Model.Types.VVector dicom, VMS.TPS.Common.Model.API.PlanSetup planSetup);
        Task<VMS.TPS.Common.Model.Types.ImageProfile> GetImageProfileAsync(VMS.TPS.Common.Model.Types.VVector start, VMS.TPS.Common.Model.Types.VVector stop, double[] preallocatedBuffer);
        Task<bool> GetProtonStoppingPowerCurveAsync(System.Collections.Generic.SortedList<double, double> protonStoppingPowerCurve);
        Task GetVoxelsAsync(int planeIndex, int[,] preallocatedBuffer);
        Task<VMS.TPS.Common.Model.Types.VVector> UserToDicomAsync(VMS.TPS.Common.Model.Types.VVector user, VMS.TPS.Common.Model.API.PlanSetup planSetup);
        Task<double> VoxelToDisplayValueAsync(int voxelValue);
        string Id { get; }
        Task SetIdAsync(string value);
        System.Collections.Generic.IReadOnlyList<VMS.TPS.Common.Model.Types.ImageApprovalHistoryEntry> ApprovalHistory { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> CalibrationProtocolDateTime { get; }
        string CalibrationProtocolDescription { get; }
        string CalibrationProtocolId { get; }
        string CalibrationProtocolImageMatchWarning { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> CalibrationProtocolLastModifiedDateTime { get; }
        VMS.TPS.Common.Model.CalibrationProtocolStatus CalibrationProtocolStatus { get; }
        VMS.TPS.Common.Model.UserInfo CalibrationProtocolUser { get; }
        string ContrastBolusAgentIngredientName { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> CreationDateTime { get; }
        string DisplayUnit { get; }
        string FOR { get; }
        bool HasUserOrigin { get; }
        string ImageType { get; }
        string ImagingDeviceId { get; }
        VMS.TPS.Common.Model.Types.PatientOrientation ImagingOrientation { get; }
        string ImagingOrientationAsString { get; }
        bool IsProcessed { get; }
        int Level { get; }
        VMS.TPS.Common.Model.Types.SeriesModality Modality { get; }
        VMS.TPS.Common.Model.Types.VVector Origin { get; }
        Task<ISeries> GetSeriesAsync();
        string UID { get; }
        VMS.TPS.Common.Model.Types.VVector UserOrigin { get; }
        Task SetUserOriginAsync(VMS.TPS.Common.Model.Types.VVector value);
        string UserOriginComments { get; }
        int Window { get; }
        VMS.TPS.Common.Model.Types.VVector XDirection { get; }
        double XRes { get; }
        int XSize { get; }
        VMS.TPS.Common.Model.Types.VVector YDirection { get; }
        double YRes { get; }
        int YSize { get; }
        VMS.TPS.Common.Model.Types.VVector ZDirection { get; }
        double ZRes { get; }
        int ZSize { get; }

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
