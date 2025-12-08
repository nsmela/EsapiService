using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;
using Esapi.Interfaces;

namespace Esapi.Interfaces
{
    public interface IStudy : IApiDataObject
    {
        // --- Simple Properties --- //
        string UID { get; }

        // --- Collections --- //
        IReadOnlyList<DateTime> CreationDateTime { get; }
        Task<IReadOnlyList<IImage>> GetImages3DAsync();
        Task<IReadOnlyList<ISeries>> GetSeriesAsync();

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Study object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Study> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Study object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Study, T> func);
    }
}
