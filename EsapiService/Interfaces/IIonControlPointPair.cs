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
        // --- Simple Properties --- //
        double NominalBeamEnergy { get; } // simple property
        int StartIndex { get; } // simple property

        // --- Accessors --- //
        Task<IIonControlPointParameters> GetEndControlPointAsync(); // read complex property
        Task<IIonSpotParametersCollection> GetFinalSpotListAsync(); // read complex property
        Task<IIonSpotParametersCollection> GetRawSpotListAsync(); // read complex property
        Task<IIonControlPointParameters> GetStartControlPointAsync(); // read complex property

        // --- Methods --- //
        Task ResizeFinalSpotListAsync(int count); // void method
        Task ResizeRawSpotListAsync(int count); // void method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.IonControlPointPair object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.IonControlPointPair> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.IonControlPointPair object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonControlPointPair, T> func);

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.IonControlPointPair object
        /// </summary>
        void Refresh();
    }
}
