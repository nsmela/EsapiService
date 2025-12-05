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
    public interface IBrachySolidApplicator : IApiDataObject
    {
        // --- Simple Properties --- //
        string ApplicatorSetName { get; }
        string ApplicatorSetType { get; }
        string Category { get; }
        int GroupNumber { get; }
        string Note { get; }
        string PartName { get; }
        string PartNumber { get; }
        string Summary { get; }
        string UID { get; }
        string Vendor { get; }
        string Version { get; }

        // --- Collections --- //
        Task<IReadOnlyList<ICatheter>> GetCathetersAsync();

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.BrachySolidApplicator object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.BrachySolidApplicator> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.BrachySolidApplicator object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BrachySolidApplicator, T> func);
    }
}
