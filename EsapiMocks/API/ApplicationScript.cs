using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class ApplicationScript : ApiDataObject
    {
        public ApplicationScript()
        {
        }

        public ApplicationScriptApprovalStatus ApprovalStatus { get; set; }
        public string ApprovalStatusDisplayText { get; set; }
        public System.Reflection.AssemblyName AssemblyName { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsReadOnlyScript { get; set; }
        public bool IsWriteableScript { get; set; }
        public string PublisherName { get; set; }
        public ApplicationScriptType ScriptType { get; set; }
        public DateTime? StatusDate { get; set; }
        public UserIdentity StatusUserIdentity { get; set; }
    }
}
