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
    public interface IEquipment
    {

        // --- Methods --- //
        Task<IReadOnlyList<IBrachyTreatmentUnit>> GetBrachyTreatmentUnitsAsync(); // complex collection method
        Task<IReadOnlyList<IExternalBeamTreatmentUnit>> GetExternalBeamTreatmentUnitsAsync(); // complex collection method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Equipment object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Equipment> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Equipment object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Equipment, T> func);
    }
}
