using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class BrachySolidApplicator : ApiDataObject
    {
        public BrachySolidApplicator()
        {
            Catheters = new List<Catheter>();
        }

        public string ApplicatorSetName { get; set; }
        public string ApplicatorSetType { get; set; }
        public string Category { get; set; }
        public IEnumerable<Catheter> Catheters { get; set; }
        public int GroupNumber { get; set; }
        public string Note { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public string Summary { get; set; }
        public string UID { get; set; }
        public string Vendor { get; set; }
        public string Version { get; set; }
    }
}
