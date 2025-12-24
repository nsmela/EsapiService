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
    public interface IBeamCalculationLog : ISerializableObject
    {
        // --- Simple Properties --- //
        string Category { get; } // simple property

        // --- Accessors --- //
        Task<IBeam> GetBeamAsync(); // read complex property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.BeamCalculationLog object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.BeamCalculationLog> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.BeamCalculationLog object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BeamCalculationLog, T> func);

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.BeamCalculationLog object
        /// </summary>
        new void Refresh();

        /* --- Skipped Members (Not generated) ---
           - MessageLines: No matching factory found (Not Implemented)
        */
    }
}
