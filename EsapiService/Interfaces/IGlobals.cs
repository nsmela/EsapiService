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
    public interface IGlobals
    {

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Globals object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Globals> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Globals object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Globals, T> func);

        /* --- Skipped Members (Not generated) ---
           - Initialize: References non-wrapped Varian API type
           - SetMaximumNumberOfLoggedApiCalls: Static members are not supported
           - GetLoggedApiCalls: Static members are not supported
           - EnableApiAccessTrace: Static members are not supported
           - DisableApiAccessTrace: Static members are not supported
           - AddCustomLogEntry: Static members are not supported
           - AbortNow: Static members are not supported
           - DefaultMaximumNumberOfLoggedApiCalls: Static members are not supported
        */
    }
}
