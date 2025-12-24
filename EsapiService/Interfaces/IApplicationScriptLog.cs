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
    public interface IApplicationScriptLog : IApiDataObject
    {
        // --- Simple Properties --- //
        string CourseId { get; } // simple property
        string PatientId { get; } // simple property
        string PlanSetupId { get; } // simple property
        string PlanUID { get; } // simple property
        string ScriptFullName { get; } // simple property
        string StructureSetId { get; } // simple property
        string StructureSetUID { get; } // simple property

        // --- Accessors --- //
        Task<IApplicationScript> GetScriptAsync(); // read complex property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ApplicationScriptLog object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ApplicationScriptLog> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ApplicationScriptLog object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ApplicationScriptLog, T> func);

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.ApplicationScriptLog object
        /// </summary>
        new void Refresh();
    }
}
