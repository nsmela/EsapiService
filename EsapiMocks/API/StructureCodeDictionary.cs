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
            Keys = new List<string>();
        }

        // -- Collection Simulation --
        public List<KeyValuePair<string, StructureCode>> Items { get; set; } = new List<KeyValuePair<string, StructureCode>>();

        public bool ContainsKey(string key) => default;
        public bool TryGetValue(string key, out StructureCode value)
        {
            value = default;
            return default;
        }

        public IEnumerator<KeyValuePair<string, StructureCode>> GetEnumerator() => Items.GetEnumerator();
        public string ToString() => default;
        public string Name { get; set; }
        public string Version { get; set; }
        public IEnumerable<string> Keys { get; set; }
        public IEnumerable<StructureCode> Values { get; set; }
        public int Count => Items.Count;
        public KeyValuePair<string, StructureCode> this[int index] { get => Items[index]; set => Items[index] = value; }
    }
}
