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
        DateTime? CalibrationProtocolDateTime { get; }
        string CalibrationProtocolDescription { get; }
        string CalibrationProtocolId { get; }
        string CalibrationProtocolImageMatchWarning { get; }
        DateTime? CalibrationProtocolLastModifiedDateTime { get; }
        string ContrastBolusAgentIngredientName { get; }
        DateTime? CreationDateTime { get; }
        string DisplayUnit { get; }
        string FOR { get; }
        bool HasUserOrigin { get; }
        string ImageType { get; }
        string ImagingDeviceId { get; }
        string ImagingOrientationAsString { get; }
        bool IsProcessed { get; }
        int Level { get; }
        string UID { get; }
        string UserOriginComments { get; }
        int Window { get; }
        double XRes { get; }
        int XSize { get; }
        double YRes { get; }
        int YSize { get; }
        double ZRes { get; }
        int ZSize { get; }

        // --- Accessors --- //
        Task<ISeries> GetSeriesAsync(); // read complex property

        // --- Methods --- //
        Task CalculateDectProtonStoppingPowersAsync(IImage rhoImage, IImage zImage, int planeIndex, double[,] preallocatedBuffer); // void method
        Task<IStructureSet> CreateNewStructureSetAsync(); // complex method
        Task<bool> GetProtonStoppingPowerCurveAsync(SortedList<double, double> protonStoppingPowerCurve); // simple method
        Task GetVoxelsAsync(int planeIndex, int[,] preallocatedBuffer); // void method
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
    }
}
