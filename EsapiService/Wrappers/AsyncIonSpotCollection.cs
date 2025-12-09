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
        internal readonly VMS.TPS.Common.Model.API.IonSpotCollection _inner;

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


        public async Task<IReadOnlyList<IIonSpot>> GetEnumeratorAsync()
        {
            return await _service.PostAsync(context => 
                _inner.GetEnumerator()?.Select(x => new AsyncIonSpot(x, _service)).ToList());
        }


        public async Task<IIonSpot> Getthis[]Async()
        {
            return await _service.PostAsync(context => 
                _inner.this[] is null ? null : new AsyncIonSpot(_inner.this[], _service));
        }

        public int Count { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonSpotCollection> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonSpotCollection, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
