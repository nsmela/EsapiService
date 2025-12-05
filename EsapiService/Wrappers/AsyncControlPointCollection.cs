namespace EsapiService.Wrappers
{
    public class AsyncControlPointCollection : IControlPointCollection
    {
        internal readonly VMS.TPS.Common.Model.API.ControlPointCollection _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncControlPointCollection(VMS.TPS.Common.Model.API.ControlPointCollection inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Count = inner.Count;
        }

        public IReadOnlyList<IControlPoint> GetEnumerator() => _inner.GetEnumerator()?.Select(x => new AsyncControlPoint(x, _service)).ToList();
        public IControlPoint this[] => _inner.this[] is null ? null : new AsyncControlPoint(_inner.this[], _service);

        public int Count { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ControlPointCollection> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ControlPointCollection, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
