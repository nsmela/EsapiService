namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
    public class AsyncBrachySolidApplicator : IBrachySolidApplicator
    {
        internal readonly VMS.TPS.Common.Model.API.BrachySolidApplicator _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncBrachySolidApplicator(VMS.TPS.Common.Model.API.BrachySolidApplicator inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            ApplicatorSetName = inner.ApplicatorSetName;
            ApplicatorSetType = inner.ApplicatorSetType;
            Category = inner.Category;
            GroupNumber = inner.GroupNumber;
            Note = inner.Note;
            PartName = inner.PartName;
            PartNumber = inner.PartNumber;
            Summary = inner.Summary;
            UID = inner.UID;
            Vendor = inner.Vendor;
            Version = inner.Version;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public string ApplicatorSetName { get; }
        public string ApplicatorSetType { get; }
        public string Category { get; }
        public System.Collections.Generic.IReadOnlyList<ICatheter> Catheters => _inner.Catheters?.Select(x => new AsyncCatheter(x, _service)).ToList();
        public int GroupNumber { get; }
        public string Note { get; }
        public string PartName { get; }
        public string PartNumber { get; }
        public string Summary { get; }
        public string UID { get; }
        public string Vendor { get; }
        public string Version { get; }
    }
}
