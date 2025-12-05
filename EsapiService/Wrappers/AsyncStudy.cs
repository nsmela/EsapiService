namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
    public class AsyncStudy : IStudy
    {
        internal readonly VMS.TPS.Common.Model.API.Study _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncStudy(VMS.TPS.Common.Model.API.Study inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            UID = inner.UID;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public System.Collections.Generic.IReadOnlyList<System.DateTime> CreationDateTime => _inner.CreationDateTime?.ToList();
        public System.Collections.Generic.IReadOnlyList<IImage> Images3D => _inner.Images3D?.Select(x => new AsyncImage(x, _service)).ToList();
        public System.Collections.Generic.IReadOnlyList<ISeries> Series => _inner.Series?.Select(x => new AsyncSeries(x, _service)).ToList();
        public string UID { get; }
    }
}
