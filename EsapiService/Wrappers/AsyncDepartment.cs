namespace EsapiService.Wrappers
{
    public class AsyncDepartment : IDepartment
    {
        internal readonly VMS.TPS.Common.Model.API.Department _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncDepartment(VMS.TPS.Common.Model.API.Department inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public string GetFullName() => _inner.GetFullName();
    }
}
