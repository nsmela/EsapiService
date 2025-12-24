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
    public interface ITreatmentUnitOperatingLimits : ISerializableObject
    {

        // --- Accessors --- //
        Task<ITreatmentUnitOperatingLimit> GetCollimatorAngleAsync(); // read complex property
        Task<ITreatmentUnitOperatingLimit> GetGantryAngleAsync(); // read complex property
        Task<ITreatmentUnitOperatingLimit> GetMUAsync(); // read complex property
        Task<ITreatmentUnitOperatingLimit> GetPatientSupportAngleAsync(); // read complex property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimits object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimits> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimits object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimits, T> func);

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimits object
        /// </summary>
        new void Refresh();
    }
}
