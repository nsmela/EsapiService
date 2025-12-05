namespace EsapiService.Wrappers
{
    public class AsyncUser : IUser
    {
        internal readonly VMS.TPS.Common.Model.API.User _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncUser(VMS.TPS.Common.Model.API.User inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Id = inner.Id;
            IsServiceUser = inner.IsServiceUser;
            Language = inner.Language;
            Name = inner.Name;
        }

        public string ToString() => _inner.ToString();
        public bool Equals(object obj) => _inner.Equals(obj);
        public int GetHashCode() => _inner.GetHashCode();
        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public string Id { get; }
        public bool IsServiceUser { get; }
        public string Language { get; }
        public string Name { get; }
    }
}
