using EsapiService.Generators.Contexts;
using System.Text;

namespace EsapiService.Generators.Generators {
    public static class InterfaceGenerator {
        public static string Generate(ClassContext context) {
            var sb = new StringBuilder();

            // 1. Determine Namespace (derived from the fully qualified class name)
            // e.g. "Varian.ESAPI.PlanSetup" -> "Varian.ESAPI"
            string namespaceName = GetNamespace(context.Name);

            sb.AppendLine($"namespace {namespaceName}");
            sb.AppendLine("{");

            // 2. Class Declaration
            sb.Append($"    public interface {context.InterfaceName}");

            // 3. Inheritance
            if (!string.IsNullOrEmpty(context.BaseInterface)) {
                sb.Append($" : {context.BaseInterface}");
            }

            sb.AppendLine();
            sb.AppendLine("    {");

            // 4. Members
            foreach (var member in context.Members) {
                sb.AppendLine(GenerateMember(member));
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private static string GenerateMember(IMemberContext member) {
            return member switch {
                SimplePropertyContext m => $"        {m.Symbol} {m.Name} {{ get; }}",

                // For Complex properties, we return the INTERFACE type (e.g., IPlanSetup), not the concrete type
                ComplexPropertyContext m => $"        {m.InterfaceName} {m.Name} {{ get; }}",

                // For Collections, we return the INTERFACE collection (e.g., IEnumerable<IStructure>)
                CollectionPropertyContext m => $"        {m.InterfaceName} {m.Name} {{ get; }}",

                MethodContext m => $"        {m.Symbol} {m.Name}{m.Signature};",

                _ => string.Empty
            };
        }

        private static string GetNamespace(string fullyQualifiedName) {
            int lastDotIndex = fullyQualifiedName.LastIndexOf('.');
            if (lastDotIndex > 0) {
                return fullyQualifiedName.Substring(0, lastDotIndex);
            }
            return "EsapiService.Interfaces"; // Fallback
        }
    }
}