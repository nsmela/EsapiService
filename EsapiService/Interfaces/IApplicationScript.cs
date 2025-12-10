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
    public interface IApplicationScript : IApiDataObject
    {
        // --- Simple Properties --- //
        string ApprovalStatusDisplayText { get; }
        System.Reflection.AssemblyName AssemblyName { get; }
        DateTime? ExpirationDate { get; }
        bool IsReadOnlyScript { get; }
        bool IsWriteableScript { get; }
        string PublisherName { get; }
        DateTime? StatusDate { get; }

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ApplicationScript object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ApplicationScript> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ApplicationScript object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ApplicationScript, T> func);
    }
}
