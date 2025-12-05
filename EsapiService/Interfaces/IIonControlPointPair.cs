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
    public interface IIonControlPointPair
    {
        Task ResizeFinalSpotListAsync(int count);
        Task ResizeRawSpotListAsync(int count);
        Task<IIonControlPointParameters> GetEndControlPointAsync();
        Task<IIonSpotParametersCollection> GetFinalSpotListAsync();
        double NominalBeamEnergy { get; }
        Task<IIonSpotParametersCollection> GetRawSpotListAsync();
        Task<IIonControlPointParameters> GetStartControlPointAsync();
        int StartIndex { get; }

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
