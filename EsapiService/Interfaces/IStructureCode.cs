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
    public interface IStructureCode : ISerializableObject
    {
        // --- Simple Properties --- //
        string Code { get; } // simple property
        string CodeMeaning { get; } // simple property
        string CodingScheme { get; } // simple property
        string DisplayName { get; } // simple property
        bool IsEncompassStructureCode { get; } // simple property

        // --- Methods --- //
        Task<StructureCodeInfo> ToStructureCodeInfoAsync(); // simple method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.StructureCode object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.StructureCode> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.StructureCode object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.StructureCode, T> func);

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.StructureCode object
        /// </summary>
        new void Refresh();

        /* --- Skipped Members (Not generated) ---
           - Equals: Explicitly ignored by name
           - op_Equality: Static members are not supported
           - op_Inequality: Static members are not supported
        */
    }
}
