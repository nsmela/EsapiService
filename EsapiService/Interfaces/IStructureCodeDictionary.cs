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
    public interface IStructureCodeDictionary
    {
        // --- Simple Properties --- //
        string Name { get; } // simple property
        string Version { get; } // simple property
        int Count { get; } // simple property

        // --- Collections --- //
        Task<IReadOnlyList<IStructureCode>> GetValuesAsync(); // collection proeprty context

        // --- Methods --- //
        Task<bool> ContainsKeyAsync(string key); // simple method
        Task<(bool result, IStructureCode value)> TryGetValueAsync(string key); // out/ref parameter method
        Task<IStructureCode> GetItemAsync(string key); // indexer

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.StructureCodeDictionary object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.StructureCodeDictionary> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.StructureCodeDictionary object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.StructureCodeDictionary, T> func);

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.StructureCodeDictionary object
        /// </summary>
        void Refresh();

        /* --- Skipped Members (Not generated) ---
           - SchemeNameFma: Static members are not supported
           - SchemeNameRadLex: Static members are not supported
           - SchemeNameSrt: Static members are not supported
           - SchemeNameVmsStructCode: Static members are not supported
           - GetEnumerator: Explicitly ignored by name
           - Keys: No matching factory found (Not Implemented)
        */
    }
}
