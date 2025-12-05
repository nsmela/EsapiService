namespace EsapiService.Wrappers
{
    public class AsyncIonSpotCollection : IIonSpotCollection
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

        public IReadOnlyList<IIonSpot> GetEnumerator() => _inner.GetEnumerator()?.Select(x => new AsyncIonSpot(x, _service)).ToList();
        public IIonSpot this[] => _inner.this[] is null ? null : new AsyncIonSpot(_inner.this[], _service);

        public int Count { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonSpotCollection> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonSpotCollection, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
