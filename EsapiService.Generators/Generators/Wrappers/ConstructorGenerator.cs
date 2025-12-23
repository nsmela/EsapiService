using EsapiService.Generators.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsapiService.Generators.Generators.Wrappers;
public static class ConstructorGenerator
{

    public static string Generate(ClassContext context)
    {
        var sb = new StringBuilder();
        var baseModifier = !string.IsNullOrEmpty(context.BaseWrapperName)
            ? $" : base(inner, service)"
            : string.Empty;

        sb.AppendLine($"        public {context.WrapperName}({context.Name} inner, IEsapiService service){baseModifier}");
        sb.AppendLine($"        {{");

        // Validation (Zoran's "Honest Functions" - don't accept nulls)
        sb.AppendLine("            if (inner is null) throw new ArgumentNullException(nameof(inner));");
        sb.AppendLine("            if (service is null) throw new ArgumentNullException(nameof(service));");
        sb.AppendLine();

        sb.AppendLine("            _inner = inner;");
        sb.AppendLine("            _service = service;");
        sb.AppendLine();

        // Eager Initialization of Simple Properties (Snapshotting)
        // We grab simple values immediately so we don't have to go back to the ESAPI thread for "string Id".
        foreach (var member in context.Members.OfType<SimplePropertyContext>())
        {
            sb.AppendLine($"            {member.Name} = inner.{member.Name};");
        }

        // Eager Initialization of Simple Collections (e.g. IEnumerable<string>)
        // FIX: Added '?' before .ToList() to handle null collections safely
        foreach (var member in context.Members.OfType<SimpleCollectionPropertyContext>())
        {
            sb.AppendLine($"            {member.Name} = inner.{member.Name}?.ToList() ?? new List<{member.InnerType}>();");
        }

        sb.AppendLine("        }");

        return sb.ToString();
    }
}