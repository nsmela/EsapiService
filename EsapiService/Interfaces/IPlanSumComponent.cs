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
    public partial interface IPlanSumComponent : IApiDataObject
    {
        // --- Simple Properties --- //
        string PlanSetupId { get; } // simple property
        PlanSumOperation PlanSumOperation { get; } // simple property
        double PlanWeight { get; } // simple property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.PlanSumComponent object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanSumComponent> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.PlanSumComponent object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanSumComponent, T> func);

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
