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
    public partial interface IRangeShifterSettings : ISerializableObject
    {
        // --- Simple Properties --- //
        double IsocenterToRangeShifterDistance { get; } // simple property
        string RangeShifterSetting { get; } // simple property
        double RangeShifterWaterEquivalentThickness { get; } // simple property

        // --- Accessors --- //
        Task<IRangeShifter> GetReferencedRangeShifterAsync(); // read complex property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.RangeShifterSettings object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.RangeShifterSettings> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.RangeShifterSettings object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RangeShifterSettings, T> func);

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
