namespace EsapiService.Wrappers
{
    public class AsyncApplicationScript : IApplicationScript
    {
        internal readonly VMS.TPS.Common.Model.API.ApplicationScript _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncApplicationScript(VMS.TPS.Common.Model.API.ApplicationScript inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            ApprovalStatus = inner.ApprovalStatus;
            ApprovalStatusDisplayText = inner.ApprovalStatusDisplayText;
            AssemblyName = inner.AssemblyName;
            IsReadOnlyScript = inner.IsReadOnlyScript;
            IsWriteableScript = inner.IsWriteableScript;
            PublisherName = inner.PublisherName;
            ScriptType = inner.ScriptType;
            StatusUserIdentity = inner.StatusUserIdentity;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public VMS.TPS.Common.Model.Types.ApplicationScriptApprovalStatus ApprovalStatus { get; }
        public string ApprovalStatusDisplayText { get; }
        public System.Reflection.AssemblyName AssemblyName { get; }
        public System.Collections.Generic.IReadOnlyList<System.DateTime> ExpirationDate => _inner.ExpirationDate?.ToList();
        public bool IsReadOnlyScript { get; }
        public bool IsWriteableScript { get; }
        public string PublisherName { get; }
        public VMS.TPS.Common.Model.Types.ApplicationScriptType ScriptType { get; }
        public System.Collections.Generic.IReadOnlyList<System.DateTime> StatusDate => _inner.StatusDate?.ToList();
        public VMS.TPS.Common.Model.Types.UserIdentity StatusUserIdentity { get; }
    }
}
