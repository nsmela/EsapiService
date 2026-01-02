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
    public interface IDVHEstimationModelStructure : ISerializableObject
    {
        // --- Simple Properties --- //
        string Id { get; } // simple property
        System.Guid ModelStructureGuid { get; } // simple property
        IReadOnlyList<StructureCode> StructureCodes { get; } // simple property
        DVHEstimationStructureType StructureType { get; } // simple property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.DVHEstimationModelStructure object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.DVHEstimationModelStructure> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.DVHEstimationModelStructure object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.DVHEstimationModelStructure, T> func);

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        new bool IsValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        new bool IsNotValid();

        /* --- Skipped Members (Not generated) ---
           - .ctor: Explicitly ignored by name
           - IsValid: Explicitly ignored by name
        */
    }
}
