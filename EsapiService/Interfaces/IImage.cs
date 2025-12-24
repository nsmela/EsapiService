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
        // --- Simple Properties --- //
        string ContrastBolusAgentIngredientName { get; } // simple property
        DateTime? CreationDateTime { get; } // simple property
        string DisplayUnit { get; } // simple property
        string FOR { get; } // simple property
        bool HasUserOrigin { get; } // simple property
        string ImageType { get; } // simple property
        string ImagingDeviceId { get; } // simple property
        PatientOrientation ImagingOrientation { get; } // simple property
        string ImagingOrientationAsString { get; } // simple property
        bool IsProcessed { get; } // simple property
        int Level { get; } // simple property
        SeriesModality Modality { get; } // simple property
        VVector Origin { get; } // simple property
        string UID { get; } // simple property
        VVector UserOrigin { get; } // simple property
        Task SetUserOriginAsync(VVector value);
        string UserOriginComments { get; } // simple property
        int Window { get; } // simple property
        VVector XDirection { get; } // simple property
        double XRes { get; } // simple property
        int XSize { get; } // simple property
        VVector YDirection { get; } // simple property
        double YRes { get; } // simple property
        int YSize { get; } // simple property
        VVector ZDirection { get; } // simple property
        double ZRes { get; } // simple property
        int ZSize { get; } // simple property

        // --- Accessors --- //
        Task<ISeries> GetSeriesAsync(); // read complex property

        // --- Methods --- //
        Task CalculateDectProtonStoppingPowersAsync(IImage rhoImage, IImage zImage, int planeIndex, double[,] preallocatedBuffer); // void method
        Task<IStructureSet> CreateNewStructureSetAsync(); // complex method
        Task<VVector> DicomToUserAsync(VVector dicom, IPlanSetup planSetup); // simple method
        Task<ImageProfile> GetImageProfileAsync(VVector start, VVector stop, double[] preallocatedBuffer); // simple method
        Task<bool> GetProtonStoppingPowerCurveAsync(SortedList<double, double> protonStoppingPowerCurve); // simple method
        Task GetVoxelsAsync(int planeIndex, int[,] preallocatedBuffer); // void method
        Task<VVector> UserToDicomAsync(VVector user, IPlanSetup planSetup); // simple method
        Task<double> VoxelToDisplayValueAsync(int voxelValue); // simple method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Image object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Image> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Image object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Image, T> func);

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.Image object
        /// </summary>
        new void Refresh();

        /* --- Skipped Members (Not generated) ---
           - Id: Shadows base member in wrapped base class
           - ApprovalHistory: No matching factory found (Not Implemented)
        */
    }
}
