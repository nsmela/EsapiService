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
        Task<bool> ContainsKeyAsync(string key);
        Task<(bool Result, IStructureCode value)> TryGetValueAsync(string key);
        Task<System.Collections.Generic.IReadOnlyList<System.Collections.Generic.KeyValuePair<string, VMS.TPS.Common.Model.API.StructureCode>>> GetEnumeratorAsync();
        Task<string> ToStringAsync();
        string Name { get; }
        string Version { get; }
        System.Collections.Generic.IReadOnlyList<string> Keys { get; }
        System.Collections.Generic.IReadOnlyList<IStructureCode> Values { get; }
        int Count { get; }
        Task<IStructureCode> Getthis[]Async();

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
