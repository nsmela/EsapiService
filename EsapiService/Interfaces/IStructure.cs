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
        // --- Simple Properties --- //
        new string Id { get; set; } // simple property
        new string Name { get; set; } // simple property
        new string Comment { get; set; } // simple property
        VVector CenterPoint { get; } // simple property
        System.Windows.Media.Color Color { get; set; } // simple property
        string DicomType { get; } // simple property
        bool HasCalculatedPlans { get; } // simple property
        bool HasSegment { get; } // simple property
        bool IsApproved { get; } // simple property
        bool IsEmpty { get; } // simple property
        bool IsHighResolution { get; } // simple property
        bool IsTarget { get; } // simple property
        int ROINumber { get; } // simple property
        double Volume { get; } // simple property

        // --- Accessors --- //
        Task<System.Windows.Media.Media3D.MeshGeometry3D> GetMeshGeometryAsync(); // read complex property
        Task<ISegmentVolume> GetSegmentVolumeAsync(); // read complex property
        Task SetSegmentVolumeAsync(ISegmentVolume value); // write complex property
        Task<IStructureCode> GetStructureCodeAsync(); // read complex property
        Task SetStructureCodeAsync(IStructureCode value); // write complex property

        // --- Methods --- //
        Task AddContourOnImagePlaneAsync(VVector[] contour, int z); // void method
        Task<ISegmentVolume> AndAsync(ISegmentVolume other); // complex method
        Task<ISegmentVolume> AsymmetricMarginAsync(AxisAlignedMargins margins); // complex method
        Task<bool> CanConvertToHighResolutionAsync(); // simple method
        Task<(bool result, string errorMessage)> CanEditSegmentVolumeAsync(); // out/ref parameter method
        Task<(bool result, string errorMessage)> CanSetAssignedHUAsync(); // out/ref parameter method
        Task ClearAllContoursOnImagePlaneAsync(int z); // void method
        Task ConvertDoseLevelToStructureAsync(IDose dose, DoseValue doseLevel); // void method
        Task ConvertToHighResolutionAsync(); // void method
        Task<(bool result, double huValue)> GetAssignedHUAsync(); // out/ref parameter method
        Task<VVector[][]> GetContoursOnImagePlaneAsync(int z); // simple method
        Task<int> GetNumberOfSeparatePartsAsync(); // simple method
        Task<VVector[]> GetReferenceLinePointsAsync(); // simple method
        Task<SegmentProfile> GetSegmentProfileAsync(VVector start, VVector stop, System.Collections.BitArray preallocatedBuffer); // simple method
        Task<bool> IsPointInsideSegmentAsync(VVector point); // simple method
        Task<ISegmentVolume> MarginAsync(double marginInMM); // complex method
        Task<ISegmentVolume> NotAsync(); // complex method
        Task<ISegmentVolume> OrAsync(ISegmentVolume other); // complex method
        Task<bool> ResetAssignedHUAsync(); // simple method
        Task SetAssignedHUAsync(double huValue); // void method
        Task<ISegmentVolume> SubAsync(ISegmentVolume other); // complex method
        Task SubtractContourOnImagePlaneAsync(VVector[] contour, int z); // void method
        Task<ISegmentVolume> XorAsync(ISegmentVolume other); // complex method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Structure object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Structure> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Structure object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Structure, T> func);

        /* --- Skipped Members (Not generated) ---
           - op_Implicit: Explicitly ignored by name
           - ApprovalHistory: No matching factory found (Not Implemented)
           - StructureCodeInfos: No matching factory found (Not Implemented)
        */
    }
}
