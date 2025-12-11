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
        string Name { get; }
        string Version { get; }
        int Count { get; }

        // --- Collections --- //
        IReadOnlyList<string> Keys { get; } // simple collection property
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
    }
}
