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
    public interface IDVHData : ISerializableObject
    {
        // --- Simple Properties --- //
        double Coverage { get; } // simple property
        DVHPoint[] CurveData { get; } // simple property
        DoseValue MaxDose { get; } // simple property
        VVector MaxDosePosition { get; } // simple property
        DoseValue MeanDose { get; } // simple property
        DoseValue MedianDose { get; } // simple property
        DoseValue MinDose { get; } // simple property
        VVector MinDosePosition { get; } // simple property
        double SamplingCoverage { get; } // simple property
        double StdDev { get; } // simple property
        double Volume { get; } // simple property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.DVHData object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.DVHData> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.DVHData object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.DVHData, T> func);
    }
}
