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
        bool FillAllCavities { get; set; } // simple property
        bool KeepLargestParts { get; set; } // simple property
        int LowerHUThreshold { get; set; } // simple property
        int MREdgeThresholdHigh { get; set; } // simple property
        int MREdgeThresholdLow { get; set; } // simple property
        int NumberOfLargestPartsToKeep { get; set; } // simple property
        bool PreCloseOpenings { get; set; } // simple property
        double PreCloseOpeningsRadius { get; set; } // simple property
        bool PreDisconnect { get; set; } // simple property
        double PreDisconnectRadius { get; set; } // simple property
        bool Smoothing { get; set; } // simple property
        int SmoothingLevel { get; set; } // simple property

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
