namespace EsapiService.Wrappers
{
    public class AsyncAddOnMaterial : IAddOnMaterial
    {
        internal readonly VMS.TPS.Common.Model.API.AddOnMaterial _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncAddOnMaterial(VMS.TPS.Common.Model.API.AddOnMaterial inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
    }
}
