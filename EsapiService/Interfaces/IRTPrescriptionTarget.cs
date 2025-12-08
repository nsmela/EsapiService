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
    public interface IRTPrescriptionTarget : IApiDataObject
    {
        // --- Simple Properties --- //
        DoseValue DosePerFraction { get; }
        int NumberOfFractions { get; }
        string TargetId { get; }
        RTPrescriptionTargetType Type { get; }
        double Value { get; }

        // --- Collections --- //
        Task<IReadOnlyList<IRTPrescriptionConstraint>> GetConstraintsAsync();

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.RTPrescriptionTarget object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.RTPrescriptionTarget> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.RTPrescriptionTarget object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RTPrescriptionTarget, T> func);
    }
}
