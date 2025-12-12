using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Study : ApiDataObject
    {
        public Study()
        {
        }

        public DateTime? CreationDateTime { get; set; }
        public IEnumerable<Image> Images3D { get; set; }
        public IEnumerable<Series> Series { get; set; }
        public string UID { get; set; }
    }
}
