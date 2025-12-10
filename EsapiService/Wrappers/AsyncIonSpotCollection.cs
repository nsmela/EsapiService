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
    public class AsyncIonSpotCollection : AsyncSerializableObject, IIonSpotCollection
    {
        internal new readonly VMS.TPS.Common.Model.API.IonSpotCollection _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncIonSpotCollection(VMS.TPS.Common.Model.API.IonSpotCollection inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Count = inner.Count;
        }


        public async Task<IIonSpot> GetItemAsync(int index) // indexer context
        {
            return await _service.PostAsync(context => 
                _inner[index] is null ? null : new AsyncIonSpot(_inner[index], _service));
        }

        public async Task<IReadOnlyList<IIonSpot>> GetAllItemsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Select(x => new AsyncIonSpot(x, _service)).ToList());
        }

        public int Count { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonSpotCollection> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonSpotCollection, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.IonSpotCollection(AsyncIonSpotCollection wrapper) => wrapper._inner;
    }
}
