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
        Task ExecuteScriptAsync(System.Reflection.Assembly scriptAssembly, VMS.TPS.Common.Model.API.ScriptContext scriptContext, System.Windows.Window window);
        string ApplicationName { get; }
        string VersionInfo { get; }
        string ApiVersionInfo { get; }
        System.Collections.Generic.IReadOnlyList<IApplicationScript> Scripts { get; }
        System.Collections.Generic.IReadOnlyList<IApplicationPackage> Packages { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ScriptEnvironment object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ScriptEnvironment> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ScriptEnvironment object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ScriptEnvironment, T> func);
    }
}
