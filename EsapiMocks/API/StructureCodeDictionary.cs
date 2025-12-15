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

        public bool ContainsKey(string key) => default;
        public bool TryGetValue(string key, out StructureCode value)
        {
            value = default;
            return default;
        }

        public string Name { get; set; }
        public string Version { get; set; }
        public IEnumerable<string> Keys { get; set; }
        public IEnumerable<StructureCode> Values { get; set; }
        public int Count { get; set; }

    }
}
