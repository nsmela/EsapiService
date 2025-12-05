namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
    public class AsyncSeedCollection : ISeedCollection
    {
        internal readonly VMS.TPS.Common.Model.API.SeedCollection _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncSeedCollection(VMS.TPS.Common.Model.API.SeedCollection inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Color = inner.Color;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public System.Collections.Generic.IReadOnlyList<IBrachyFieldReferencePoint> BrachyFieldReferencePoints => _inner.BrachyFieldReferencePoints?.Select(x => new AsyncBrachyFieldReferencePoint(x, _service)).ToList();
        public System.Windows.Media.Color Color { get; }
        public System.Collections.Generic.IReadOnlyList<ISourcePosition> SourcePositions => _inner.SourcePositions?.Select(x => new AsyncSourcePosition(x, _service)).ToList();

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.SeedCollection> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.SeedCollection, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
