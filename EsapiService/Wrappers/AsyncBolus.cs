namespace EsapiService.Wrappers
{
    public class AsyncBolus : IBolus
    {
        internal readonly VMS.TPS.Common.Model.API.Bolus _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncBolus(VMS.TPS.Common.Model.API.Bolus inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Id = inner.Id;
            MaterialCTValue = inner.MaterialCTValue;
            Name = inner.Name;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public string Id { get; }
        public double MaterialCTValue { get; }
        public string Name { get; }
    }
}
