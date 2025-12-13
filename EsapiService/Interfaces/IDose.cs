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
    public interface IDose : IApiDataObject
    {
        // --- Simple Properties --- //
        DoseValue DoseMax3D { get; }
        VVector DoseMax3DLocation { get; }
        IEnumerable<Isodose> Isodoses { get; }
        VVector Origin { get; }
        string SeriesUID { get; }
        string UID { get; }
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
        Task<ISeries> GetSeriesAsync(); // read complex property

        // --- Methods --- //
        Task<DoseProfile> GetDoseProfileAsync(VVector start, VVector stop, double[] preallocatedBuffer); // simple method
        Task<DoseValue> GetDoseToPointAsync(VVector at); // simple method
        Task GetVoxelsAsync(int planeIndex, int[,] preallocatedBuffer); // void method
        Task<DoseValue> VoxelToDoseValueAsync(int voxelValue); // simple method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Dose object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Dose> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Dose object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Dose, T> func);
    }
}
