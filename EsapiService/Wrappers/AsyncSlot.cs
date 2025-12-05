namespace EsapiService.Wrappers
{
    public class AsyncSlot : ISlot
    {
        internal readonly VMS.TPS.Common.Model.API.Slot _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncSlot(VMS.TPS.Common.Model.API.Slot inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Number = inner.Number;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public int Number { get; }
    }
}
