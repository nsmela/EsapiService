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
            Departments = new List<Department>();
        }

        public DateTime? CreationDateTime { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public string Location { get; set; }
    }
}
