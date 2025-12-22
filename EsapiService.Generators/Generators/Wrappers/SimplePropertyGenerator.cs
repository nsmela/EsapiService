using EsapiService.Generators.Contexts;
using System.Text;

namespace EsapiService.Generators.Generators.Wrappers;

public static class SimplePropertyGenerator {

    public static string Generate(SimplePropertyContext member) {
        var sb = new StringBuilder();

        // Property Definition (Auto-Property with Private Set)
        string setterMod = member.IsReadOnly ? "" : " private set;";
        string newMod = member.IsShadowing ? " new" : "";

        sb.AppendLine($"        public{newMod} {member.Symbol} {member.Name} {{ get;{setterMod} }}");

        // Async Setter (PostAsync)
        if (!member.IsReadOnly) {
            sb.AppendLine($"        public async Task {NamingConvention.GetAsyncSetterName(member.Name)}({member.Symbol} value)");
            sb.AppendLine($"        {{");
            // OPTIMIZATION: One trip to ESAPI thread to Set AND Get the confirmed value
            sb.AppendLine($"            {member.Name} = await _service.PostAsync(context => ");
            sb.AppendLine($"            {{");
            sb.AppendLine($"                _inner.{member.Name} = value;");
            sb.AppendLine($"                return _inner.{member.Name};");
            sb.AppendLine($"            }});");
            sb.AppendLine($"        }}");
        }

        return sb.ToString().TrimEnd();
    }
}
