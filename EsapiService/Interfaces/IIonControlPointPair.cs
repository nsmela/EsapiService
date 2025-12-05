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
    public interface IIonControlPointPair
    {
        // --- Simple Properties --- //
        double NominalBeamEnergy { get; }
        int StartIndex { get; }

        // --- Accessors --- //
        Task<IIonControlPointParameters> GetEndControlPointAsync();
        Task<IIonSpotParametersCollection> GetFinalSpotListAsync();
        Task<IIonSpotParametersCollection> GetRawSpotListAsync();
        Task<IIonControlPointParameters> GetStartControlPointAsync();

        // --- Methods --- //
        Task ResizeFinalSpotListAsync(int count);
        Task ResizeRawSpotListAsync(int count);

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.IonControlPointPair object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.IonControlPointPair> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.IonControlPointPair object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonControlPointPair, T> func);
    }
}
