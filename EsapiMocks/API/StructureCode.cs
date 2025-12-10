using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class StructureCode : SerializableObject
    {
        public StructureCode()
        {
        }

        public string Code { get; set; }
        public string CodeMeaning { get; set; }
        public string CodingScheme { get; set; }
        public string DisplayName { get; set; }
        public bool IsEncompassStructureCode { get; set; }
    }
}
