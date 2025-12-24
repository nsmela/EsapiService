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
    public interface IExternalBeamTreatmentUnit : IApiDataObject
    {
        // --- Simple Properties --- //
        string MachineDepartmentName { get; } // simple property
        string MachineModel { get; } // simple property
        string MachineModelName { get; } // simple property
        string MachineScaleDisplayName { get; } // simple property
        double SourceAxisDistance { get; } // simple property

        // --- Accessors --- //
        Task<ITreatmentUnitOperatingLimits> GetOperatingLimitsAsync(); // read complex property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ExternalBeamTreatmentUnit object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ExternalBeamTreatmentUnit> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ExternalBeamTreatmentUnit object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ExternalBeamTreatmentUnit, T> func);

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.ExternalBeamTreatmentUnit object
        /// </summary>
        new void Refresh();
    }
}
