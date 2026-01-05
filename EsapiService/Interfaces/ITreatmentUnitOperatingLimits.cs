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
    public partial interface ITreatmentUnitOperatingLimits : ISerializableObject
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
