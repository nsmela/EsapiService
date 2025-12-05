namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
    public class AsyncRTPrescriptionOrganAtRisk : IRTPrescriptionOrganAtRisk
    {
        internal readonly VMS.TPS.Common.Model.API.RTPrescriptionOrganAtRisk _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncRTPrescriptionOrganAtRisk(VMS.TPS.Common.Model.API.RTPrescriptionOrganAtRisk inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            OrganAtRiskId = inner.OrganAtRiskId;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public System.Collections.Generic.IReadOnlyList<IRTPrescriptionConstraint> Constraints => _inner.Constraints?.Select(x => new AsyncRTPrescriptionConstraint(x, _service)).ToList();
        public string OrganAtRiskId { get; }
    }
}
