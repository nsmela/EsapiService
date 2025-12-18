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
    public class AsyncStructureCodeDictionary : IStructureCodeDictionary, IEsapiWrapper<VMS.TPS.Common.Model.API.StructureCodeDictionary>
    {
        internal readonly VMS.TPS.Common.Model.API.StructureCodeDictionary _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

public AsyncStructureCodeDictionary(VMS.TPS.Common.Model.API.StructureCodeDictionary inner, IEsapiService service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            SchemeNameFma = inner.SchemeNameFma;
            SchemeNameRadLex = inner.SchemeNameRadLex;
            SchemeNameSrt = inner.SchemeNameSrt;
            SchemeNameVmsStructCode = inner.SchemeNameVmsStructCode;
            Name = inner.Name;
            Version = inner.Version;
            Count = inner.Count;
        }

        public string SchemeNameFma { get; }

        public string SchemeNameRadLex { get; }

        public string SchemeNameSrt { get; }

        public string SchemeNameVmsStructCode { get; }

        // Simple Method
        public Task<bool> ContainsKeyAsync(string key) => 
            _service.PostAsync(context => _inner.ContainsKey(key));

        public async Task<(bool result, IStructureCode value)> TryGetValueAsync(string key)
        {
            var postResult = await _service.PostAsync(context => {
                StructureCode value_temp = default(StructureCode);
                var result = _inner.TryGetValue(key, out value_temp);
                return (result, value_temp);
            });
            return (postResult.Item1,
                    postResult.Item2 == null ? null : new AsyncStructureCode(postResult.Item2, _service));
        }


        public string Name { get; }

        public string Version { get; }

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
                _inner.Values.Select(x => new AsyncStructureCode(x, _service)).ToList());
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.StructureCodeDictionary> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.StructureCodeDictionary, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.StructureCodeDictionary(AsyncStructureCodeDictionary wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.StructureCodeDictionary IEsapiWrapper<VMS.TPS.Common.Model.API.StructureCodeDictionary>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.StructureCodeDictionary>.Service => _service;

        /* --- Skipped Members (Not generated) ---
           - GetEnumerator: Explicitly ignored by name
           - Keys: No matching factory found (Not Implemented)
        */
    }
}
