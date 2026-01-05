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
    public partial interface IReferencePoint : IApiDataObject
    {
        // --- Simple Properties --- //
        new string Id { get; set; } // simple property
        DoseValue DailyDoseLimit { get; set; } // simple property
        DoseValue SessionDoseLimit { get; set; } // simple property
        DoseValue TotalDoseLimit { get; set; } // simple property

        // --- Methods --- //
        Task<bool> AddLocationAsync(IImage Image, double x, double y, double z, System.Text.StringBuilder errorHint); // simple method
        Task<bool> ChangeLocationAsync(IImage Image, double x, double y, double z, System.Text.StringBuilder errorHint); // simple method
        Task<VVector> GetReferencePointLocationAsync(IImage Image); // simple method
        Task<VVector> GetReferencePointLocationAsync(IPlanSetup planSetup); // simple method
        Task<bool> HasLocationAsync(IPlanSetup planSetup); // simple method
        Task<bool> RemoveLocationAsync(IImage Image, System.Text.StringBuilder errorHint); // simple method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ReferencePoint object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ReferencePoint> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ReferencePoint object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ReferencePoint, T> func);

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
