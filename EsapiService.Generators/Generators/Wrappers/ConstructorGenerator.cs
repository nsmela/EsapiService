using EsapiService.Generators.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsapiService.Generators.Generators.Wrappers;
public static class ConstructorGenerator {

    public static string Generate(ClassContext context) {
        var sb = new StringBuilder();
        var baseModifier = !string.IsNullOrEmpty(context.BaseWrapperName)
            ? $" : base(inner, service)"
            : string.Empty;

        sb.AppendLine($"        public {context.WrapperName}({context.Name} inner, IEsapiService service){baseModifier}");
        sb.AppendLine("        {");
        sb.AppendLine("            _inner = inner;");
        sb.AppendLine("            _service = service;");
        sb.AppendLine();

        foreach (var member in context.Members.OfType<SimplePropertyContext>()) {
            sb.AppendLine($"            {member.Name} = inner.{member.Name};");
        }

        foreach (var member in context.Members.OfType<SimpleCollectionPropertyContext>()) {
            sb.AppendLine($"            {member.Name} = inner.{member.Name}.ToList();");
        }

        sb.AppendLine("        }");

        return sb.ToString().Trim();
    }
}

