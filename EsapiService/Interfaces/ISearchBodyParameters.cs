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
    public interface ISearchBodyParameters : ISerializableObject
    {
        // --- Simple Properties --- //
        bool FillAllCavities { get; }
        Task SetFillAllCavitiesAsync(bool value);
        bool KeepLargestParts { get; }
        Task SetKeepLargestPartsAsync(bool value);
        int LowerHUThreshold { get; }
        Task SetLowerHUThresholdAsync(int value);
        int MREdgeThresholdHigh { get; }
        Task SetMREdgeThresholdHighAsync(int value);
        int MREdgeThresholdLow { get; }
        Task SetMREdgeThresholdLowAsync(int value);
        int NumberOfLargestPartsToKeep { get; }
        Task SetNumberOfLargestPartsToKeepAsync(int value);
        bool PreCloseOpenings { get; }
        Task SetPreCloseOpeningsAsync(bool value);
        double PreCloseOpeningsRadius { get; }
        Task SetPreCloseOpeningsRadiusAsync(double value);
        bool PreDisconnect { get; }
        Task SetPreDisconnectAsync(bool value);
        double PreDisconnectRadius { get; }
        Task SetPreDisconnectRadiusAsync(double value);
        bool Smoothing { get; }
        Task SetSmoothingAsync(bool value);
        int SmoothingLevel { get; }
        Task SetSmoothingLevelAsync(int value);

        // --- Methods --- //
        Task LoadDefaultsAsync();

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
