using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncStructureCodeDictionary : IStructureCodeDictionary
    {
        internal readonly VMS.TPS.Common.Model.API.StructureCodeDictionary _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncStructureCodeDictionary(VMS.TPS.Common.Model.API.StructureCodeDictionary inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

            Name = inner.Name;
            Version = inner.Version;
            Count = inner.Count;
            Keys = inner.Keys.ToList();
        }


        public Task<bool> ContainsKeyAsync(string key) => _service.PostAsync(context => _inner.ContainsKey(key));

        public async Task<(bool Result, IStructureCode value)> TryGetValueAsync(string key)
        {
            StructureCode value_temp = default(StructureCode);
            var result = await _service.PostAsync(context => _inner.TryGetValue(key, out value_temp));
            return (result, value_temp is null ? null : new AsyncStructureCode(value_temp, _service));
        }

        public string Name { get; }

        public string Version { get; }

        public IReadOnlyList<string> Keys { get; }


        public async Task<IReadOnlyList<IStructureCode>> GetValuesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Values?.Select(x => new AsyncStructureCode(x, _service)).ToList());
        }


        public int Count { get; }

        public async Task<IStructureCode> GetItemAsync(string key) // indexer context
        {
            return await _service.PostAsync(context => 
                _inner[key] is null ? null : new AsyncStructureCode(_inner[key], _service));
        }

        public async Task<IReadOnlyList<IStructureCode>> GetAllItemsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Select(x => new AsyncStructureCode(x, _service)).ToList());
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.StructureCodeDictionary> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.StructureCodeDictionary, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.StructureCodeDictionary(AsyncStructureCodeDictionary wrapper) => wrapper._inner;
    }
}
