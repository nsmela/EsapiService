using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class StructureCodeDictionary
    {
        public StructureCodeDictionary()
        {
            Values = new List<StructureCode>();
        }

        public static string SchemeNameFma { get; set; }
        public static string SchemeNameRadLex { get; set; }
        public static string SchemeNameSrt { get; set; }
        public static string SchemeNameVmsStructCode { get; set; }
        public bool ContainsKey(string key) => default;
        public bool TryGetValue(string key, out StructureCode value)
        {
            value = default;
            return default;
        }

        public string Name { get; set; }
        public string Version { get; set; }
        public IEnumerable<StructureCode> Values { get; set; }
        public int Count { get; set; }


        /* --- Skipped Members (Not generated) ---
           - GetEnumerator: Explicitly ignored by name
           - Keys: No matching factory found (Not Implemented)
        */
    }
}
