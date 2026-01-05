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
    public partial interface ISourcePosition : IApiDataObject
    {
        // --- Simple Properties --- //
        double DwellTime { get; } // simple property
        bool? DwellTimeLock { get; set; } // simple property
        double NominalDwellTime { get; set; } // simple property
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
