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
    public interface IPlanUncertainty : IApiDataObject
    {
        // --- Simple Properties --- //
        double CalibrationCurveError { get; }
        string DisplayName { get; }

        // --- Accessors --- //
        Task<IDose> GetDoseAsync(); // read complex property

        // --- Collections --- //
        Task<IReadOnlyList<IBeamUncertainty>> GetBeamUncertaintiesAsync(); // collection proeprty context

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.PlanUncertainty object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanUncertainty> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.PlanUncertainty object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanUncertainty, T> func);
    }
}
