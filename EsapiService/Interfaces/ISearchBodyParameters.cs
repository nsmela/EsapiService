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
    public interface ISearchBodyParameters : ISerializableObject
    {
        // --- Simple Properties --- //
        bool FillAllCavities { get; } // simple property
        Task SetFillAllCavitiesAsync(bool value);
        bool KeepLargestParts { get; } // simple property
        Task SetKeepLargestPartsAsync(bool value);
        int LowerHUThreshold { get; } // simple property
        Task SetLowerHUThresholdAsync(int value);
        int MREdgeThresholdHigh { get; } // simple property
        Task SetMREdgeThresholdHighAsync(int value);
        int MREdgeThresholdLow { get; } // simple property
        Task SetMREdgeThresholdLowAsync(int value);
        int NumberOfLargestPartsToKeep { get; } // simple property
        Task SetNumberOfLargestPartsToKeepAsync(int value);
        bool PreCloseOpenings { get; } // simple property
        Task SetPreCloseOpeningsAsync(bool value);
        double PreCloseOpeningsRadius { get; } // simple property
        Task SetPreCloseOpeningsRadiusAsync(double value);
        bool PreDisconnect { get; } // simple property
        Task SetPreDisconnectAsync(bool value);
        double PreDisconnectRadius { get; } // simple property
        Task SetPreDisconnectRadiusAsync(double value);
        bool Smoothing { get; } // simple property
        Task SetSmoothingAsync(bool value);
        int SmoothingLevel { get; } // simple property
        Task SetSmoothingLevelAsync(int value);

        // --- Methods --- //
        Task LoadDefaultsAsync(); // void method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.SearchBodyParameters object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.SearchBodyParameters> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.SearchBodyParameters object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.SearchBodyParameters, T> func);
    }
}
