namespace EsapiService.Wrappers
{
    public class AsyncIonSpotParametersCollection : IIonSpotParametersCollection
    {
        internal readonly VMS.TPS.Common.Model.API.IonSpotParametersCollection _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncIonSpotParametersCollection(VMS.TPS.Common.Model.API.IonSpotParametersCollection inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Count = inner.Count;
        }

        public IReadOnlyList<IIonSpotParameters> GetEnumerator() => _inner.GetEnumerator()?.Select(x => new AsyncIonSpotParameters(x, _service)).ToList();
        public IIonSpotParameters this[] => _inner.this[] is null ? null : new AsyncIonSpotParameters(_inner.this[], _service);

        public int Count { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonSpotParametersCollection> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonSpotParametersCollection, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
