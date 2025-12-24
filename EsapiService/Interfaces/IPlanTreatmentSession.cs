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
    public interface IPlanTreatmentSession : IApiDataObject
    {
        // --- Simple Properties --- //
        TreatmentSessionStatus Status { get; } // simple property

        // --- Accessors --- //
        Task<IPlanSetup> GetPlanSetupAsync(); // read complex property
        Task<ITreatmentSession> GetTreatmentSessionAsync(); // read complex property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.PlanTreatmentSession object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanTreatmentSession> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.PlanTreatmentSession object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanTreatmentSession, T> func);

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.PlanTreatmentSession object
        /// </summary>
        new void Refresh();
    }
}
