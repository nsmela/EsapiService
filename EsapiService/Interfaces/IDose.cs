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
    public interface IDose : IApiDataObject
    {
        // --- Simple Properties --- //
        DoseValue DoseMax3D { get; }
        VVector DoseMax3DLocation { get; }
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
        Task<ISeries> GetSeriesAsync();

        // --- Collections --- //
        Task<IReadOnlyList<IIsodose>> GetIsodosesAsync();

        // --- Methods --- //
        Task<DoseProfile> GetDoseProfileAsync(VVector start, VVector stop, double[] preallocatedBuffer);
        Task<DoseValue> GetDoseToPointAsync(VVector at);
        Task GetVoxelsAsync(int planeIndex, int[,] preallocatedBuffer);
        Task<DoseValue> VoxelToDoseValueAsync(int voxelValue);

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
