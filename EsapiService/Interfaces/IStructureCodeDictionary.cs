namespace VMS.TPS.Common.Model.API
{
    public interface IStructureCodeDictionary
    {
        bool ContainsKey(string key);

        System.Collections.Generic.IReadOnlyList<System.Collections.Generic.KeyValuePair<string, VMS.TPS.Common.Model.API.StructureCode>> GetEnumerator();
        string ToString();
        string Name { get; }
        string Version { get; }
        System.Collections.Generic.IReadOnlyList<string> Keys { get; }
        System.Collections.Generic.IReadOnlyList<IStructureCode> Values { get; }
        int Count { get; }
        IStructureCode this[] { get; }
    }
}
