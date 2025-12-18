using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Globals
    {
        public Globals()
        {
        }

        public static void SetMaximumNumberOfLoggedApiCalls(int apiLogCacheSize) { }
        public IEnumerable<string> GetLoggedApiCalls() => new List<string>();
        public static void EnableApiAccessTrace() { }
        public static void DisableApiAccessTrace() { }
        public static void AddCustomLogEntry(string message, LogSeverity logSeverity) { }
        public static bool AbortNow { get; set; }
        public static int DefaultMaximumNumberOfLoggedApiCalls { get; set; }

        /* --- Skipped Members (Not generated) ---
           - Initialize: References non-wrapped Varian API type
        */
    }
}
