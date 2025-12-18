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
        // --- Simple Properties --- //
        bool AbortNow { get; } // simple property
        Task SetAbortNowAsync(bool value);
        int DefaultMaximumNumberOfLoggedApiCalls { get; } // simple property

        // --- Methods --- //
        Task SetMaximumNumberOfLoggedApiCallsAsync(int apiLogCacheSize); // void method
        Task<IReadOnlyList<string>> GetLoggedApiCallsAsync(); // simple collection method 
        Task EnableApiAccessTraceAsync(); // void method
        Task DisableApiAccessTraceAsync(); // void method
        Task AddCustomLogEntryAsync(string message, LogSeverity logSeverity); // void method

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
        */
    }
}
