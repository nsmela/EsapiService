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
    public interface IStudy : IApiDataObject
    {
        // --- Simple Properties --- //
        DateTime? CreationDateTime { get; } // simple property
        string UID { get; } // simple property

        // --- Collections --- //
        Task<IReadOnlyList<IImage>> GetImages3DAsync(); // collection proeprty context
        Task<IReadOnlyList<ISeries>> GetSeriesAsync(); // collection proeprty context

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Study object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Study> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Study object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Study, T> func);

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.Study object
        /// </summary>
        new void Refresh();
    }
}
