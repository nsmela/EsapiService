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
    public interface IStructure : IApiDataObject
    {
        // --- Simple Properties --- //
        VVector CenterPoint { get; }
        System.Windows.Media.Color Color { get; }
        Task SetColorAsync(System.Windows.Media.Color value);
        string DicomType { get; }
        bool HasCalculatedPlans { get; }
        bool HasSegment { get; }
        bool IsApproved { get; }
        bool IsEmpty { get; }
        bool IsHighResolution { get; }
        bool IsTarget { get; }
        System.Windows.Media.Media3D.MeshGeometry3D MeshGeometry { get; }
        int ROINumber { get; }
        double Volume { get; }

        // --- Accessors --- //
        Task<ISegmentVolume> GetSegmentVolumeAsync();
        Task SetSegmentVolumeAsync(ISegmentVolume value);
        Task<IStructureCode> GetStructureCodeAsync();
        Task SetStructureCodeAsync(IStructureCode value);

        // --- Collections --- //
        IReadOnlyList<StructureApprovalHistoryEntry> ApprovalHistory { get; }
        IReadOnlyList<StructureCodeInfo> StructureCodeInfos { get; }

        // --- Methods --- //
        Task AddContourOnImagePlaneAsync(VVector[] contour, int z);
        Task<ISegmentVolume> AndAsync(ISegmentVolume other);
        Task<ISegmentVolume> AsymmetricMarginAsync(AxisAlignedMargins margins);
        Task<bool> CanConvertToHighResolutionAsync();
        Task<(bool Result, string errorMessage)> CanEditSegmentVolumeAsync();
        Task<(bool Result, string errorMessage)> CanSetAssignedHUAsync();
        Task ClearAllContoursOnImagePlaneAsync(int z);
        Task ConvertDoseLevelToStructureAsync(IDose dose, DoseValue doseLevel);
        Task ConvertToHighResolutionAsync();
        Task<(bool Result, double huValue)> GetAssignedHUAsync();
        Task<VVector[][]> GetContoursOnImagePlaneAsync(int z);
        Task<int> GetNumberOfSeparatePartsAsync();
        Task<VVector[]> GetReferenceLinePointsAsync();
        Task<SegmentProfile> GetSegmentProfileAsync(VVector start, VVector stop, System.Collections.BitArray preallocatedBuffer);
        Task<bool> IsPointInsideSegmentAsync(VVector point);
        Task<ISegmentVolume> MarginAsync(double marginInMM);
        Task<ISegmentVolume> NotAsync();
        Task<ISegmentVolume> OrAsync(ISegmentVolume other);
        Task<bool> ResetAssignedHUAsync();
        Task SetAssignedHUAsync(double huValue);
        Task<ISegmentVolume> SubAsync(ISegmentVolume other);
        Task SubtractContourOnImagePlaneAsync(VVector[] contour, int z);
        Task<ISegmentVolume> XorAsync(ISegmentVolume other);

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Structure object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Structure> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Structure object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Structure, T> func);
    }
}
