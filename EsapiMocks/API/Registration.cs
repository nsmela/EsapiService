using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Registration : ApiDataObject
    {
        public Registration()
        {
        }

        public DateTime? CreationDateTime { get; set; }
        public string RegisteredFOR { get; set; }
        public string SourceFOR { get; set; }
        public DateTime? StatusDateTime { get; set; }
        public string StatusUserDisplayName { get; set; }
        public string StatusUserName { get; set; }
        public string UID { get; set; }
    }
}
