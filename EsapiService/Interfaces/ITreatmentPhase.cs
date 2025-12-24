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
    public interface ITreatmentPhase : IApiDataObject
    {
        // --- Simple Properties --- //
        string OtherInfo { get; } // simple property
        int PhaseGapNumberOfDays { get; } // simple property
        string TimeGapType { get; } // simple property

        // --- Collections --- //
        Task<IReadOnlyList<IRTPrescription>> GetPrescriptionsAsync(); // collection proeprty context

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.TreatmentPhase object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.TreatmentPhase> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.TreatmentPhase object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.TreatmentPhase, T> func);

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.TreatmentPhase object
        /// </summary>
        new void Refresh();
    }
}
