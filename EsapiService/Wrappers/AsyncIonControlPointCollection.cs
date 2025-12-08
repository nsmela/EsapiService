using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncIonControlPointCollection : AsyncSerializableObject, IIonControlPointCollection
    {
        internal readonly VMS.TPS.Common.Model.API.IonControlPointCollection _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncIonControlPointCollection(VMS.TPS.Common.Model.API.IonControlPointCollection inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Count = inner.Count;
        }


        public async Task<IReadOnlyList<IIonControlPoint>> GetEnumeratorAsync()
        {
            return await _service.RunAsync(() => 
                _inner.GetEnumerator()?.Select(x => new AsyncIonControlPoint(x, _service)).ToList());
        }


        public async Task<IIonControlPoint> Getthis[]Async()
        {
            return await _service.RunAsync(() => 
                _inner.this[] is null ? null : new AsyncIonControlPoint(_inner.this[], _service));
        }

        public int Count { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonControlPointCollection> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonControlPointCollection, T> func) => _service.RunAsync(() => func(_inner));
    }
}
