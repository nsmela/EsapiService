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
    public interface IExternalBeamTreatmentUnit : IApiDataObject
    {
        // --- Simple Properties --- //
        string MachineDepartmentName { get; }
        string MachineModel { get; }
        string MachineModelName { get; }
        string MachineScaleDisplayName { get; }
        double SourceAxisDistance { get; }

        // --- Accessors --- //
        Task<ITreatmentUnitOperatingLimits> GetOperatingLimitsAsync();

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ExternalBeamTreatmentUnit object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ExternalBeamTreatmentUnit> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ExternalBeamTreatmentUnit object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ExternalBeamTreatmentUnit, T> func);
    }
}
