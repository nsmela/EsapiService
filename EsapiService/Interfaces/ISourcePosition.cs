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
        double DwellTime { get; } // simple property
        bool? DwellTimeLock { get; } // simple property
        Task SetDwellTimeLockAsync(bool? value);
        double NominalDwellTime { get; } // simple property
        Task SetNominalDwellTimeAsync(double value);
        double[,] Transform { get; } // simple property
        VVector Translation { get; } // simple property

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

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.SourcePosition object
        /// </summary>
        new void Refresh();
    }
}
