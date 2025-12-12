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
        IEnumerable<Isodose> Isodoses { get; }
        string SeriesUID { get; }
        string UID { get; }
        double XRes { get; }
        int XSize { get; }
        double YRes { get; }
        int YSize { get; }
        double ZRes { get; }
        int ZSize { get; }

        // --- Accessors --- //
        Task<ISeries> GetSeriesAsync(); // read complex property

        // --- Methods --- //
        Task GetVoxelsAsync(int planeIndex, int[,] preallocatedBuffer); // void method

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
