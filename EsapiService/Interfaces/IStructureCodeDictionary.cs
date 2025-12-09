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
    public interface IStructureCodeDictionary
    {
        // --- Simple Properties --- //
        string Name { get; }
        string Version { get; }
        int Count { get; }

        // --- Accessors --- //
        Task<IStructureCode> GetItemAsync(int index);
        Task<IReadOnlyList<IStructureCode>> GetAllItemsAsync();

        // --- Collections --- //
        IReadOnlyList<string> Keys { get; }
        Task<IReadOnlyList<IStructureCode>> GetValuesAsync();

        // --- Methods --- //
        Task<bool> ContainsKeyAsync(string key);
        Task<(bool Result, IStructureCode value)> TryGetValueAsync(string key);

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
