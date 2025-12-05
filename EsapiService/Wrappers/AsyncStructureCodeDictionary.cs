namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
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

        public bool ContainsKey(string key) => _inner.ContainsKey(key);
        public async System.Threading.Tasks.Task<(bool Result, IStructureCode value)> TryGetValueAsync(string key)
        {
            StructureCode value_temp;
            var result = await _service.RunAsync(() => _inner.TryGetValue(key, out value_temp));
            return (result, value_temp is null ? null : new AsyncStructureCode(value_temp, _service));
        }
        public IReadOnlyList<KeyValuePair<string, StructureCode>> GetEnumerator() => _inner.GetEnumerator()?.ToList();
        public string ToString() => _inner.ToString();
        public string Name { get; }
        public string Version { get; }
        public IReadOnlyList<string> Keys => _inner.Keys?.ToList();
        public IReadOnlyList<IStructureCode> Values => _inner.Values?.Select(x => new AsyncStructureCode(x, _service)).ToList();
        public int Count { get; }
        public IStructureCode this[] => _inner.this[] is null ? null : new AsyncStructureCode(_inner.this[], _service);


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.StructureCodeDictionary> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.StructureCodeDictionary, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
