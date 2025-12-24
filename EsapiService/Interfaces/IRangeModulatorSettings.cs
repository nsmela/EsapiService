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
    public interface IRangeModulatorSettings : ISerializableObject
    {
        // --- Simple Properties --- //
        double IsocenterToRangeModulatorDistance { get; } // simple property
        double RangeModulatorGatingStartValue { get; } // simple property
        double RangeModulatorGatingStarWaterEquivalentThickness { get; } // simple property
        double RangeModulatorGatingStopValue { get; } // simple property
        double RangeModulatorGatingStopWaterEquivalentThickness { get; } // simple property

        // --- Accessors --- //
        Task<IRangeModulator> GetReferencedRangeModulatorAsync(); // read complex property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.RangeModulatorSettings object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.RangeModulatorSettings> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.RangeModulatorSettings object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RangeModulatorSettings, T> func);
    }
}
