using EsapiService.Generators.Contexts;
using System.Text;

namespace EsapiService.Generators.Generators.Wrappers;

public static class SimpleCollectionPropertyGenerator {
    public static string Generate(SimpleCollectionPropertyContext member) {
        var sb = new StringBuilder();
        sb.AppendLine("        // Simple Collection Property");
        sb.AppendLine($"        public {member.InterfaceName} {member.Name} {{ get; }}");
        return sb.ToString();
    }
}

