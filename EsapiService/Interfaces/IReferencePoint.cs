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
    public interface IReferencePoint : IApiDataObject
    {
        // --- Simple Properties --- //
        DoseValue DailyDoseLimit { get; }
        Task SetDailyDoseLimitAsync(DoseValue value);
        DoseValue SessionDoseLimit { get; }
        Task SetSessionDoseLimitAsync(DoseValue value);
        DoseValue TotalDoseLimit { get; }
        Task SetTotalDoseLimitAsync(DoseValue value);

        // --- Methods --- //
        Task<bool> AddLocationAsync(IImage Image, double x, double y, double z, System.Text.StringBuilder errorHint);
        Task<bool> ChangeLocationAsync(IImage Image, double x, double y, double z, System.Text.StringBuilder errorHint);
        Task<VVector> GetReferencePointLocationAsync(IImage Image);
        Task<VVector> GetReferencePointLocationAsync(IPlanSetup planSetup);
        Task<bool> HasLocationAsync(IPlanSetup planSetup);
        Task<bool> RemoveLocationAsync(IImage Image, System.Text.StringBuilder errorHint);

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
