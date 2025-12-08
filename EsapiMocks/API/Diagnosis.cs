using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Diagnosis : ApiDataObject
    {
        public Diagnosis()
        {
        }

        public string ClinicalDescription { get; set; }
        public string Code { get; set; }
        public string CodeTable { get; set; }
    }
}
