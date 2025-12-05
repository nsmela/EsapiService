namespace EsapiService.Wrappers
{
    public class AsyncStructureCode : IStructureCode
    {
        internal readonly VMS.TPS.Common.Model.API.StructureCode _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncStructureCode(VMS.TPS.Common.Model.API.StructureCode inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Code = inner.Code;
            CodeMeaning = inner.CodeMeaning;
            CodingScheme = inner.CodingScheme;
            DisplayName = inner.DisplayName;
            IsEncompassStructureCode = inner.IsEncompassStructureCode;
        }

        public VMS.TPS.Common.Model.Types.StructureCodeInfo ToStructureCodeInfo() => _inner.ToStructureCodeInfo();
        public bool Equals(VMS.TPS.Common.Model.API.StructureCode other) => _inner.Equals(other);
        public bool Equals(object obj) => _inner.Equals(obj);
        public string ToString() => _inner.ToString();
        public int GetHashCode() => _inner.GetHashCode();
        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public string Code { get; }
        public string CodeMeaning { get; }
        public string CodingScheme { get; }
        public string DisplayName { get; }
        public bool IsEncompassStructureCode { get; }
    }
}
