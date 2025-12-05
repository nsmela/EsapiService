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
    public interface IStructure : IApiDataObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task AddContourOnImagePlaneAsync(VMS.TPS.Common.Model.Types.VVector[] contour, int z);
        Task<ISegmentVolume> AndAsync(VMS.TPS.Common.Model.API.SegmentVolume other);
        Task<ISegmentVolume> AsymmetricMarginAsync(VMS.TPS.Common.Model.Types.AxisAlignedMargins margins);
        Task<bool> CanConvertToHighResolutionAsync();
        Task<(bool Result, string errorMessage)> CanEditSegmentVolumeAsync();
        Task<(bool Result, string errorMessage)> CanSetAssignedHUAsync();
        Task ClearAllContoursOnImagePlaneAsync(int z);
        Task ConvertDoseLevelToStructureAsync(VMS.TPS.Common.Model.API.Dose dose, VMS.TPS.Common.Model.Types.DoseValue doseLevel);
        Task ConvertToHighResolutionAsync();
        Task<(bool Result, double huValue)> GetAssignedHUAsync();
        Task<VMS.TPS.Common.Model.Types.VVector[][]> GetContoursOnImagePlaneAsync(int z);
        Task<int> GetNumberOfSeparatePartsAsync();
        Task<VMS.TPS.Common.Model.Types.VVector[]> GetReferenceLinePointsAsync();
        Task<VMS.TPS.Common.Model.Types.SegmentProfile> GetSegmentProfileAsync(VMS.TPS.Common.Model.Types.VVector start, VMS.TPS.Common.Model.Types.VVector stop, System.Collections.BitArray preallocatedBuffer);
        Task<bool> IsPointInsideSegmentAsync(VMS.TPS.Common.Model.Types.VVector point);
        Task<ISegmentVolume> MarginAsync(double marginInMM);
        Task<ISegmentVolume> NotAsync();
        Task<ISegmentVolume> OrAsync(VMS.TPS.Common.Model.API.SegmentVolume other);
        Task<bool> ResetAssignedHUAsync();
        Task SetAssignedHUAsync(double huValue);
        Task<ISegmentVolume> SubAsync(VMS.TPS.Common.Model.API.SegmentVolume other);
        Task SubtractContourOnImagePlaneAsync(VMS.TPS.Common.Model.Types.VVector[] contour, int z);
        Task<ISegmentVolume> XorAsync(VMS.TPS.Common.Model.API.SegmentVolume other);
        string Id { get; }
        Task SetIdAsync(string value);
        string Name { get; }
        Task SetNameAsync(string value);
        string Comment { get; }
        Task SetCommentAsync(string value);
        System.Collections.Generic.IReadOnlyList<VMS.TPS.Common.Model.Types.StructureApprovalHistoryEntry> ApprovalHistory { get; }
        VMS.TPS.Common.Model.Types.VVector CenterPoint { get; }
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
        Task<ISegmentVolume> GetSegmentVolumeAsync();
        Task SetSegmentVolumeAsync(ISegmentVolume value);
        Task<IStructureCode> GetStructureCodeAsync();
        Task SetStructureCodeAsync(IStructureCode value);
        System.Collections.Generic.IReadOnlyList<VMS.TPS.Common.Model.Types.StructureCodeInfo> StructureCodeInfos { get; }
        double Volume { get; }

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
