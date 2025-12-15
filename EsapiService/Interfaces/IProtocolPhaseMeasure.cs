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
    public interface IProtocolPhaseMeasure : ISerializableObject
    {
        // --- Simple Properties --- //
        double TargetValue { get; } // simple property
        double ActualValue { get; } // simple property
        bool? TargetIsMet { get; } // simple property
        MeasureModifier Modifier { get; } // simple property
        string StructureId { get; } // simple property
        MeasureType Type { get; } // simple property
        string TypeText { get; } // simple property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ProtocolPhaseMeasure object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ProtocolPhaseMeasure> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ProtocolPhaseMeasure object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ProtocolPhaseMeasure, T> func);
    }
}
