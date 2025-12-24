using EsapiService.Generators.Contexts;
using System.Text;

namespace EsapiService.Generators.Generators {
    public static class InterfaceGenerator {

        public static string Generate(ClassContext context) {
            var sb = new StringBuilder();

            // 1. Standard Usings
            sb.AppendLine(@$"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;");
            sb.AppendLine();

            // 2. Namespace
            sb.AppendLine("namespace Esapi.Interfaces");
            sb.AppendLine("{");

            // 3. Class Documentation
            if (!string.IsNullOrEmpty(context.XmlDocumentation)) {
                sb.AppendLine(context.XmlDocumentation);
            }

            // 4. Class Declaration
            sb.Append($"    public interface {context.InterfaceName}");

            if (!string.IsNullOrEmpty(context.BaseInterface)) {
                sb.Append($" : {context.BaseInterface}");
            }

            sb.AppendLine();
            sb.AppendLine("    {");

            // 5. Members - Organized by Type

            // Group A: Simple Properties (Fast, Cached)
            var simpleProps = context.Members.OfType<SimplePropertyContext>();
            if (simpleProps.Any()) {
                sb.AppendLine("        // --- Simple Properties --- //");
                foreach (var m in simpleProps) sb.AppendLine(GenerateMember(m));
            }

            // Group B: Complex Properties (Async Wrappers)
            var complexProps = context.Members.OfType<ComplexPropertyContext>();
            if (complexProps.Any()) {
                sb.AppendLine();
                sb.AppendLine("        // --- Accessors --- //");
                foreach (var m in complexProps) sb.AppendLine(GenerateMember(m));
            }

            // Group C: Collections (Async Lists)
            var collections = context.Members.Where(m => m is CollectionPropertyContext or SimpleCollectionPropertyContext);
            if (collections.Any()) {
                sb.AppendLine();
                sb.AppendLine("        // --- Collections --- //");
                foreach (var m in collections) sb.AppendLine(GenerateMember(m));
            }

            // Group D: Methods (Logic)
            // Filter: Anything that isn't one of the above types
            var methods = context.Members.Where(m =>
                m.Name != "GetEnumerator" &&
                m is not SimplePropertyContext &&
                m is not ComplexPropertyContext &&
                m is not CollectionPropertyContext &&
                m is not SimpleCollectionPropertyContext);

            if (methods.Any()) {
                sb.AppendLine();
                sb.AppendLine("        // --- Methods --- //");
                foreach (var m in methods) sb.AppendLine(GenerateMember(m));
            }

            // 6. Escape Hatches (Always at the bottom)
            sb.AppendLine();
            sb.AppendLine("        // --- RunAsync --- //");
            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        /// Runs a function against the raw ESAPI {context.Name} object safely on the ESAPI thread.");
            sb.AppendLine($"        /// </summary>");
            sb.AppendLine($"        Task RunAsync(Action<{context.Name}> action);");

            sb.AppendLine();
            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        /// Runs a function against the raw ESAPI {context.Name} object safely on the ESAPI thread.");
            sb.AppendLine($"        /// </summary>");
            sb.AppendLine($"        Task<T> RunAsync<T>(Func<{context.Name}, T> func);");

            // 7. Skipped Members Report
            if (context.SkippedMembers.Any())
            {
                sb.AppendLine();
                sb.AppendLine("        /* --- Skipped Members (Not generated) ---");
                foreach (var skip in context.SkippedMembers)
                {
                    sb.AppendLine($"           - {skip.Name}: {skip.Reason}");
                }
                sb.AppendLine("        */");
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
                CollectionPropertyContext m =>
                     $"        Task<{m.InterfaceName}> {NamingConvention.GetAsyncGetterName(m.Name)}(); // collection proeprty context",

                // Simple Collection ->Use the InterfaceName (IReadOnlyList<string>)
                SimpleCollectionPropertyContext m =>
                     $"        {m.InterfaceName} {m.Name} {{ get; }} // simple collection property",

                IndexerContext m =>
               $"        Task<{m.InterfaceName}> GetItemAsync({string.Join(", ", m.Parameters.Select(p => $"{p.InterfaceType} {p.Name}"))}); // indexer",

                // 1. Void -> Task NameAsync()
                VoidMethodContext m =>
                    $"        Task {NamingConvention.GetMethodName(m.Name)}{m.Signature}; // void method",

                // 2. Simple Return -> Task<T> NameAsync()
                SimpleMethodContext m =>
                    $"        Task<{m.ReturnType}> {NamingConvention.GetMethodName(m.Name)}{m.Signature}; // simple method",

                // 3. Complex Return -> Task<Interface> NameAsync()
                ComplexMethodContext m =>
                    $"        Task<{m.InterfaceName}> {NamingConvention.GetMethodName(m.Name)}{m.Signature}; // complex method",

                // 4. Simple Collection Return -> Task<IReadOnlyList<T>> NameAsync()
                SimpleCollectionMethodContext m =>
                    $"        Task<{m.InterfaceName}> {NamingConvention.GetMethodName(m.Name)}{m.Signature}; // simple collection method ",

                // 5. Complex Collection Return -> Task<IReadOnlyList<Interface>> NameAsync()
                ComplexCollectionMethodContext m =>
                    $"        Task<{m.InterfaceName}> {NamingConvention.GetMethodName(m.Name)}{m.Signature}; // complex collection method",

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
            string newMod = m.IsShadowing ? "new " : "";
            string setMod = m.IsReadOnly ? "" : "set; ";
            sb.AppendLine($"        {newMod}{m.Symbol} {m.Name} {{ get; {setMod}}} // simple property");

            return sb.ToString().TrimEnd(); // Trim to avoid extra newlines if you prefer
        }

        private static string GenerateComplexProperty(ComplexPropertyContext m) {
            var sb = new StringBuilder();

            if (m.Name == "this[]") {
                // Handle Indexer: Convert 'this[]' to 'GetItemAsync(int index)'
                sb.AppendLine($"        Task<{m.ReturnValue}> GetItemAsync(int index); // this[]");
                if (!m.IsReadOnly) {
                    sb.AppendLine($"        Task SetItemAsync(int index, {m.InterfaceName} value);");
                }
                sb.AppendLine($"        Task<IReadOnlyList<{m.ReturnValue}>> GetAllItemsAsync();");

                return sb.ToString().TrimEnd();
            }

            sb.AppendLine($"        Task<{m.ReturnValue}> {NamingConvention.GetAsyncGetterName(m.Name)}(); // read complex property");

            if (!m.IsReadOnly) {
                sb.AppendLine($"        Task {NamingConvention.GetAsyncSetterName(m.Name)}({m.InterfaceName} value); // write complex property");
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
            return $"        {returnType} {NamingConvention.GetMethodName(m.Name)}({argsString}); // out/ref parameter method";
        }

    }
}