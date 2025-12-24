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
    public interface IIonSpotParameters : ISerializableObject
    {
        // --- Simple Properties --- //
        float Weight { get; } // simple property
        Task SetWeightAsync(float value);
        float X { get; } // simple property
        Task SetXAsync(float value);
        float Y { get; } // simple property
        Task SetYAsync(float value);

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.IonSpotParameters object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.IonSpotParameters> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.IonSpotParameters object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonSpotParameters, T> func);

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.IonSpotParameters object
        /// </summary>
        new void Refresh();
    }
}
