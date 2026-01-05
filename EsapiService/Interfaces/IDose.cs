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
    public partial interface IDose : IApiDataObject
    {
        // --- Simple Properties --- //
        DoseValue DoseMax3D { get; } // simple property
        VVector DoseMax3DLocation { get; } // simple property
        VVector Origin { get; } // simple property
        string SeriesUID { get; } // simple property
        string UID { get; } // simple property
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

        // --- Collections --- //
        Task<IReadOnlyList<IIsodose>> GetIsodosesAsync(); // collection property context

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

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        new bool IsValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        new bool IsNotValid();
    }
}
