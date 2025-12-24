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
    public interface IApiDataObject : ISerializableObject
    {
        // --- Simple Properties --- //
        string Id { get; } // simple property
        string Name { get; } // simple property
        string Comment { get; } // simple property
        string HistoryUserName { get; } // simple property
        string HistoryUserDisplayName { get; } // simple property
        DateTime HistoryDateTime { get; } // simple property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ApiDataObject object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ApiDataObject> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ApiDataObject object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ApiDataObject, T> func);

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.ApiDataObject object
        /// </summary>
        new void Refresh();

        /* --- Skipped Members (Not generated) ---
           - op_Equality: Static members are not supported
           - op_Inequality: Static members are not supported
        */
    }
}
