using EsapiService.Generators.Contexts;
using System.Text;

namespace EsapiService.Generators.Generators {
    public static class InterfaceGenerator {

        public static string Generate(ClassContext context) {
            var sb = new StringBuilder();

            // 1. Standard Usings (User Specified)
            sb.AppendLine(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;");
            sb.AppendLine();

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

            // Add the RunAsync "Escape Hatches"
            sb.AppendLine();
            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        /// Runs a function against the raw ESAPI {context.Name} object safely on the ESAPI thread.");
            sb.AppendLine($"        /// </summary>");
            sb.AppendLine($"        Task RunAsync(Action<{context.Name}> action);");

            sb.AppendLine();
            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        /// Runs a function against the raw ESAPI {context.Name} object safely on the ESAPI thread.");
            sb.AppendLine($"        /// </summary>");
            sb.AppendLine($"        Task<T> RunAsync<T>(Func<{context.Name}, T> func);");

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

                // 1. Void -> Task NameAsync()
                VoidMethodContext m =>
                    $"        Task {m.Name}Async{m.Signature};",

                // 2. Simple Return -> Task<T> NameAsync()
                SimpleMethodContext m =>
                    $"        Task<{m.ReturnType}> {m.Name}Async{m.Signature};",

                // 3. Complex Return -> Task<Interface> NameAsync()
                ComplexMethodContext m =>
                    $"        Task<{m.InterfaceName}> {m.Name}Async{m.Signature};",

                // 4. Simple Collection Return -> Task<IReadOnlyList<T>> NameAsync()
                SimpleCollectionMethodContext m =>
                    $"        Task<{m.InterfaceName}> {m.Name}Async{m.Signature};",

                // 5. Complex Collection Return -> Task<IReadOnlyList<Interface>> NameAsync()
                ComplexCollectionMethodContext m =>
                    $"        Task<{m.InterfaceName}> {m.Name}Async{m.Signature};",

                // 6. Out/Ref Parameters (Already handles Task in its specific helper)
                OutParameterMethodContext m => GenerateOutParameterMethod(m),

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
                sb.AppendLine($"        Task Set{m.Name}Async({m.Symbol} value);");
            }

            return sb.ToString().TrimEnd(); // Trim to avoid extra newlines if you prefer
        }

        private static string GenerateComplexProperty(ComplexPropertyContext m) {
            var sb = new StringBuilder();
            sb.AppendLine($"        Task<{m.InterfaceName}> Get{m.Name}Async();");

            if (!m.IsReadOnly) {
                sb.AppendLine($"        Task Set{m.Name}Async({m.InterfaceName} value);");
            }
            return sb.ToString().TrimEnd();
        }

        private static string GenerateOutParameterMethod(OutParameterMethodContext m) {
            // 1. Build Input Arguments
            // We exclude 'out' parameters entirely from the input.
            // We include 'ref' parameters, but they are passed by-value (no ref keyword).
            var inputArgs = m.Parameters
                .Where(p => !p.IsOut)
                .Select(p => $"{p.InterfaceType} {p.Name}");

            string argsString = string.Join(", ", inputArgs);

            // 2. Build Return Type
            // It is always an awaitable Task containing the Tuple signature we calculated earlier.
            // e.g. Task<(bool Result, double norm, string msg)>
            string returnType = $"Task<{m.ReturnTupleSignature}>";

            // 3. Assemble Signature
            // e.g. Task<...> CalculateAsync(double norm);
            return $"        {returnType} {m.Name}Async({argsString});";
        }

        private static string GetNamespace(string fullyQualifiedName) => "Esapi.Interfaces"; 
    }
}