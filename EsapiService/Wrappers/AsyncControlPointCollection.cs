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

        public System.Collections.Generic.IReadOnlyList<IControlPoint> GetEnumerator() => _inner.GetEnumerator()?.Select(x => new AsyncControlPoint(x, _service)).ToList();
        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public IControlPoint this[] => _inner.this[] is null ? null : new AsyncControlPoint(_inner.this[], _service);

        public int Count { get; }
    }
}
