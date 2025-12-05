namespace EsapiService.Wrappers
{
    public class AsyncStandardWedge : IStandardWedge
    {
        internal readonly VMS.TPS.Common.Model.API.StandardWedge _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncStandardWedge(VMS.TPS.Common.Model.API.StandardWedge inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
    }
}
