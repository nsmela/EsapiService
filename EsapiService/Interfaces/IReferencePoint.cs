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
    public interface IReferencePoint : IApiDataObject
    {
        // --- Simple Properties --- //
        DoseValue DailyDoseLimit { get; } // simple property
        Task SetDailyDoseLimitAsync(DoseValue value);
        string PatientVolumeId { get; } // simple property
        DoseValue SessionDoseLimit { get; } // simple property
        Task SetSessionDoseLimitAsync(DoseValue value);
        DoseValue TotalDoseLimit { get; } // simple property
        Task SetTotalDoseLimitAsync(DoseValue value);

        // --- Methods --- //
        Task<VVector> GetReferencePointLocationAsync(IPlanSetup planSetup); // simple method
        Task<bool> HasLocationAsync(IPlanSetup planSetup); // simple method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ReferencePoint object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ReferencePoint> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ReferencePoint object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ReferencePoint, T> func);
    }
}
