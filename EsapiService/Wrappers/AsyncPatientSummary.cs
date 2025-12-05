namespace EsapiService.Wrappers
{
    public class AsyncPatientSummary : IPatientSummary
    {
        internal readonly VMS.TPS.Common.Model.API.PatientSummary _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncPatientSummary(VMS.TPS.Common.Model.API.PatientSummary inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            FirstName = inner.FirstName;
            Id = inner.Id;
            Id2 = inner.Id2;
            LastName = inner.LastName;
            MiddleName = inner.MiddleName;
            Sex = inner.Sex;
            SSN = inner.SSN;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public System.Collections.Generic.IReadOnlyList<System.DateTime> CreationDateTime => _inner.CreationDateTime?.ToList();
        public System.Collections.Generic.IReadOnlyList<System.DateTime> DateOfBirth => _inner.DateOfBirth?.ToList();
        public string FirstName { get; }
        public string Id { get; }
        public string Id2 { get; }
        public string LastName { get; }
        public string MiddleName { get; }
        public string Sex { get; }
        public string SSN { get; }
    }
}
