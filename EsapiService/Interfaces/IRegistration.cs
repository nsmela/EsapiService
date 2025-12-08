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
    public interface IRegistration : IApiDataObject
    {
        // --- Simple Properties --- //
        string RegisteredFOR { get; }
        string SourceFOR { get; }
        RegistrationApprovalStatus Status { get; }
        string StatusUserDisplayName { get; }
        string StatusUserName { get; }
        double[,] TransformationMatrix { get; }
        string UID { get; }

        // --- Collections --- //
        IReadOnlyList<DateTime> CreationDateTime { get; }
        IReadOnlyList<DateTime> StatusDateTime { get; }

        // --- Methods --- //
        Task<VVector> InverseTransformPointAsync(VVector pt);
        Task<VVector> TransformPointAsync(VVector pt);

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Registration object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Registration> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Registration object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Registration, T> func);
    }
}
