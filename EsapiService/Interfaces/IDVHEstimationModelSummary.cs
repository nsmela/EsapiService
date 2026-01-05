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
    public partial interface IDVHEstimationModelSummary : ISerializableObject
    {
        // --- Simple Properties --- //
        string Description { get; } // simple property
        bool IsPublished { get; } // simple property
        bool IsTrained { get; } // simple property
        string ModelDataVersion { get; } // simple property
        ParticleType ModelParticleType { get; } // simple property
        System.Guid ModelUID { get; } // simple property
        string Name { get; } // simple property
        int Revision { get; } // simple property
        string TreatmentSite { get; } // simple property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.DVHEstimationModelSummary object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.DVHEstimationModelSummary> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.DVHEstimationModelSummary object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.DVHEstimationModelSummary, T> func);

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
