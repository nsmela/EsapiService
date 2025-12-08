using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
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


        public async Task<IReadOnlyList<IIonControlPointPair>> GetEnumeratorAsync()
        {
            return await _service.RunAsync(() => 
                _inner.GetEnumerator()?.Select(x => new AsyncIonControlPointPair(x, _service)).ToList());
        }


        public async Task<IIonControlPointPair> Getthis[]Async()
        {
            return await _service.RunAsync(() => 
                _inner.this[] is null ? null : new AsyncIonControlPointPair(_inner.this[], _service));
        }

        public int Count { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonControlPointPairCollection> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonControlPointPairCollection, T> func) => _service.RunAsync(() => func(_inner));
    }
}
