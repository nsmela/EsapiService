namespace EsapiService.Wrappers
{
    public class AsyncSerializableObject : ISerializableObject
    {
        internal readonly VMS.TPS.Common.Model.API.SerializableObject _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncSerializableObject(VMS.TPS.Common.Model.API.SerializableObject inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

        }

        public System.Xml.Schema.XmlSchema GetSchema() => _inner.GetSchema();
        public void ReadXml(System.Xml.XmlReader reader) => _inner.ReadXml(reader);
        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
    }
}
