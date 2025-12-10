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
    public interface ISourcePosition : IApiDataObject
    {
        // --- Simple Properties --- //
        double DwellTime { get; }
        bool? DwellTimeLock { get; }
        Task SetDwellTimeLockAsync(bool? value);
        double NominalDwellTime { get; }
        Task SetNominalDwellTimeAsync(double value);
        double[,] Transform { get; }

        // --- Accessors --- //
        Task<IRadioactiveSource> GetRadioactiveSourceAsync(); // read complex property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.SourcePosition object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.SourcePosition> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.SourcePosition object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.SourcePosition, T> func);
    }
}
