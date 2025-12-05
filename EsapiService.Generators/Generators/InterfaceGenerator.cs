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
            if (!string.IsNullOrEmpty(context.XmlDocumentation)) {
                sb.AppendLine(context.XmlDocumentation);
            }
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
            var sb = new StringBuilder();

            // Add XML Docs
            if (!string.IsNullOrEmpty(member.XmlDocumentation)) {
                sb.AppendLine($"        {member.XmlDocumentation}");
            }

            sb.Append(member switch {
                SimplePropertyContext m => GenerateSimpleProperty(m),

                // For Complex properties, we return the INTERFACE type (e.g., IPlanSetup), not the concrete type
                ComplexPropertyContext m => GenerateComplexProperty(m),

                // For Collections, we return the INTERFACE collection (e.g., IEnumerable<IStructure>)
                CollectionPropertyContext m => $"        {m.InterfaceName} {m.Name} {{ get; }}",

                // Simple Collection ->Use the InterfaceName (IReadOnlyList<string>)
                SimpleCollectionPropertyContext m =>
                     $"        {m.InterfaceName} {m.Name} {{ get; }}",

                VoidMethodContext m =>
                            $"        void {m.Name}{m.Signature};",

                SimpleMethodContext m =>
                    $"        {m.ReturnType} {m.Name}{m.Signature};",

                ComplexMethodContext m =>
                    $"        {m.InterfaceName} {m.Name}{m.Signature};",

                SimpleCollectionMethodContext m =>
                    $"        {m.InterfaceName} {m.Name}{m.Signature};",

                ComplexCollectionMethodContext m =>
                    $"        {m.InterfaceName} {m.Name}{m.Signature};",

                _ => string.Empty
            });

            return sb.ToString();
        }

        // Helper Method
        private static string GenerateSimpleProperty(SimplePropertyContext m) {
            var sb = new StringBuilder();
            // 1. Always generate the Getter
            sb.AppendLine($"        {m.Symbol} {m.Name} {{ get; }}");

            // 2. If not ReadOnly, generate the Async Setter signature
            if (!m.IsReadOnly) {
                sb.AppendLine($"        System.Threading.Tasks.Task Set{m.Name}Async({m.Symbol} value);");
            }

            return sb.ToString().TrimEnd(); // Trim to avoid extra newlines if you prefer
        }

        private static string GenerateComplexProperty(ComplexPropertyContext m) {
            var sb = new StringBuilder();
            sb.AppendLine($"        {m.InterfaceName} {m.Name} {{ get; }}");

            if (!m.IsReadOnly) {
                sb.AppendLine($"        System.Threading.Tasks.Task Set{m.Name}Async({m.InterfaceName} value);");
            }
            return sb.ToString().TrimEnd();
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