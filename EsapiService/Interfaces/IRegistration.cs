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
    public partial interface IRegistration : IApiDataObject
    {
        // --- Simple Properties --- //
        DateTime? CreationDateTime { get; } // simple property
        string RegisteredFOR { get; } // simple property
        string SourceFOR { get; } // simple property
        RegistrationApprovalStatus Status { get; } // simple property
        DateTime? StatusDateTime { get; } // simple property
        string StatusUserDisplayName { get; } // simple property
        string StatusUserName { get; } // simple property
        double[,] TransformationMatrix { get; } // simple property
        string UID { get; } // simple property

        // --- Methods --- //
        Task<VVector> InverseTransformPointAsync(VVector pt); // simple method
        Task<VVector> TransformPointAsync(VVector pt); // simple method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Registration object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Registration> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Registration object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Registration, T> func);

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
