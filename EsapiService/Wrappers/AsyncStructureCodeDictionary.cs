using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
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
        }


        public Task<bool> ContainsKeyAsync(string key) => _service.RunAsync(() => _inner.ContainsKey(key));

        public async Task<(bool Result, IStructureCode value)> TryGetValueAsync(string key)
        {
            StructureCode value_temp;
            var result = await _service.RunAsync(() => _inner.TryGetValue(key, out value_temp));
            return (result, value_temp is null ? null : new AsyncStructureCode(value_temp, _service));
        }

        public Task<IReadOnlyList<KeyValuePair<string, StructureCode>>> GetEnumeratorAsync() => _service.RunAsync(() => _inner.GetEnumerator()?.ToList());

        public Task<string> ToStringAsync() => _service.RunAsync(() => _inner.ToString());

        public string Name { get; }

        public string Version { get; }

        public async Task<IReadOnlyList<string>> GetKeysAsync()
        {
            return await _service.RunAsync(() => _inner.Keys?.ToList());
        }


        public async Task<IReadOnlyList<IStructureCode>> GetValuesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Values?.Select(x => new AsyncStructureCode(x, _service)).ToList());
        }


        public int Count { get; }

        public async Task<IStructureCode> Getthis[]Async()
        {
            return await _service.RunAsync(() => 
                _inner.this[] is null ? null : new AsyncStructureCode(_inner.this[], _service));
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.StructureCodeDictionary> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.StructureCodeDictionary, T> func) => _service.RunAsync(() => func(_inner));
    }
}
