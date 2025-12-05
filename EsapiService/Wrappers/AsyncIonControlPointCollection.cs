namespace EsapiService.Wrappers
{
    public class AsyncIonControlPointCollection : IIonControlPointCollection
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

        public System.Collections.Generic.IReadOnlyList<IIonControlPoint> GetEnumerator() => _inner.GetEnumerator()?.Select(x => new AsyncIonControlPoint(x, _service)).ToList();
        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public IIonControlPoint this[] => _inner.this[] is null ? null : new AsyncIonControlPoint(_inner.this[], _service);

        public int Count { get; }
    }
}
