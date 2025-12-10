using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class DVHEstimationModelStructure : SerializableObject
    {
        public DVHEstimationModelStructure()
        {
            StructureCodes = new List<StructureCode>();
        }

        public string Id { get; set; }
        public bool IsValid { get; set; }
        public System.Guid ModelStructureGuid { get; set; }
        public IReadOnlyList<StructureCode> StructureCodes { get; set; }
    }
}
