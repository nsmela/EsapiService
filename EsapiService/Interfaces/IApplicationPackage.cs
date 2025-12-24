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
    public interface IApplicationPackage : IApiDataObject
    {
        // --- Simple Properties --- //
        ApplicationScriptApprovalStatus ApprovalStatus { get; } // simple property
        string Description { get; } // simple property
        DateTime? ExpirationDate { get; } // simple property
        string PackageId { get; } // simple property
        string PackageName { get; } // simple property
        string PackageVersion { get; } // simple property
        string PublisherData { get; } // simple property
        string PublisherName { get; } // simple property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ApplicationPackage object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ApplicationPackage> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ApplicationPackage object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ApplicationPackage, T> func);

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.ApplicationPackage object
        /// </summary>
        new void Refresh();
    }
}
