using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Hospital : ApiDataObject
    {
        public Hospital()
        {
        }

        public DateTime? CreationDateTime { get; set; }
        public string Location { get; set; }
    }
}
