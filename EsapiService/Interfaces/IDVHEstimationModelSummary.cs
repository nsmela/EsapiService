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
    public interface IDVHEstimationModelSummary : ISerializableObject
    {
        // --- Simple Properties --- //
        string Description { get; }
        bool IsPublished { get; }
        bool IsTrained { get; }
        string ModelDataVersion { get; }
        ParticleType ModelParticleType { get; }
        System.Guid ModelUID { get; }
        string Name { get; }
        int Revision { get; }
        string TreatmentSite { get; }

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.DVHEstimationModelSummary object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.DVHEstimationModelSummary> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.DVHEstimationModelSummary object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.DVHEstimationModelSummary, T> func);
    }
}
