using EsapiService.Generators.Contexts;
using System.Text;

namespace EsapiService.Generators.Generators.Wrappers;

public static class SimplePropertyGenerator {

    public static string Generate(SimplePropertyContext member) {
        var sb = new StringBuilder();

        string newMod = member.IsShadowing ? " new" : "";

        // getter only
        if (member.IsReadOnly) {
            sb.AppendLine(@$"        public{newMod} {member.Symbol} {member.Name} =>");
            sb.AppendLine($"            _inner.{member.Name};");
        }
        // getter and setter
        else {
            sb.AppendLine(@$"        public{newMod} {member.Symbol} {member.Name}");
            sb.AppendLine($"        {{");
            sb.AppendLine($"            get => _inner.{member.Name};");
            sb.AppendLine($"            set => _inner.{member.Name} = value;");
            sb.AppendLine($"        }}");
        }

        return sb.ToString();
    }
}
