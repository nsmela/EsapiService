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
    public class AsyncIonControlPointPairCollection : IIonControlPointPairCollection
    {
        internal readonly VMS.TPS.Common.Model.API.IonControlPointPairCollection _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncIonControlPointPairCollection(VMS.TPS.Common.Model.API.IonControlPointPairCollection inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

            Count = inner.Count;
        }


        public async Task<IIonControlPointPair> GetItemAsync(int index)
        {
            return await _service.PostAsync(context => 
                _inner[index] is null ? null : new AsyncIonControlPointPair(_inner[index], _service));
        }

        public async Task<IReadOnlyList<IIonControlPointPair>> GetAllItemsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Select(x => new AsyncIonControlPointPair(x, _service)).ToList());
        }

        public int Count { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonControlPointPairCollection> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonControlPointPairCollection, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
