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
    public partial interface IBrachySolidApplicator : IApiDataObject
    {
        // --- Simple Properties --- //
        string ApplicatorSetName { get; } // simple property
        string ApplicatorSetType { get; } // simple property
        string Category { get; } // simple property
        int GroupNumber { get; } // simple property
        string Note { get; } // simple property
        string PartName { get; } // simple property
        string PartNumber { get; } // simple property
        string Summary { get; } // simple property
        string UID { get; } // simple property
        string Vendor { get; } // simple property
        string Version { get; } // simple property

        // --- Collections --- //
        Task<IReadOnlyList<ICatheter>> GetCathetersAsync(); // collection property context

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.BrachySolidApplicator object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.BrachySolidApplicator> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.BrachySolidApplicator object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BrachySolidApplicator, T> func);

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        new bool IsValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        new bool IsNotValid();
    }
}
