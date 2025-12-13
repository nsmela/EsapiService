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
    public interface IScriptEnvironment
    {
        // --- Simple Properties --- //
        string ApplicationName { get; }
        string VersionInfo { get; }
        string ApiVersionInfo { get; }
        IEnumerable<ApplicationScript> Scripts { get; }
        IEnumerable<ApplicationPackage> Packages { get; }

        // --- Methods --- //
        Task ExecuteScriptAsync(System.Reflection.Assembly scriptAssembly, IScriptContext scriptContext, System.Windows.Window window); // void method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ScriptEnvironment object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ScriptEnvironment> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ScriptEnvironment object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ScriptEnvironment, T> func);

        /* --- Skipped Members (Not generated) ---
           - .ctor: Explicitly ignored by name
        */
    }
}
