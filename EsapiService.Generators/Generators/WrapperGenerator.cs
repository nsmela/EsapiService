using EsapiService.Generators.Contexts;
using System;
using System.Linq;
using System.Text;

namespace EsapiService.Generators.Generators {
    public static class WrapperGenerator {
        public static string Generate(ClassContext context) {
            var sb = new StringBuilder();

            // 0. Usings
            if (context.Members.Any(m => m is SimplePropertyContext sp && !sp.IsReadOnly)) {
                sb.AppendLine("    using System.Threading.Tasks;");
            }

            // 1. Namespace
            string namespaceName = GetNamespace(context.Name);
            sb.AppendLine($"namespace {namespaceName}");
            sb.AppendLine("{");

            // 2. Add Usings if needed (Collections need Linq)
            if (context.Members.Any(m => m is CollectionPropertyContext)) {
                sb.AppendLine("    using System.Linq;");
                sb.AppendLine("    using System.Collections.Generic;");
            }

            // 3. Class Declaration
            // e.g. public class AsyncPlanSetup : IPlanSetup
            sb.Append($"    public class {context.WrapperName}");

            if (!string.IsNullOrEmpty(context.InterfaceName)) {
                sb.Append($" : {context.InterfaceName}");
            }

            // Handle Base Wrapper inheritance if it exists
            // (e.g. : AsyncPlanningItem, IPlanSetup)
            // For now, we'll stick to the interface implementation as requested by the test.

            sb.AppendLine();
            sb.AppendLine("    {");

            // 4. Fields
            // private readonly Varian.ESAPI.PlanSetup _inner;
            sb.AppendLine($"        private readonly {context.Name} _inner;");
            sb.AppendLine($"        private readonly IEsapiService _service;"); // NEW
            sb.AppendLine();

            // 5. Constructor
            sb.AppendLine($"        public {context.WrapperName}({context.Name} inner, IEsapiService service)");
            sb.AppendLine("        {");
            sb.AppendLine("            _inner = inner;");
            sb.AppendLine("            _service = service;");
            sb.AppendLine();

            foreach (var member in context.Members.OfType<SimplePropertyContext>().Where(m => m.IsReadOnly)) {
                sb.AppendLine($"            {member.Name} = inner.{member.Name};");
            }

            sb.AppendLine("        }");
            sb.AppendLine();

            // 6. Members
            foreach (var member in context.Members) {
                sb.AppendLine(GenerateMember(member));
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private static string GenerateMember(IMemberContext member) {
            return member switch {
                // Simple Property: Direct forwarding
                // public string Id => _inner.Id;
                SimplePropertyContext m => GenerateSimpleProperty(m),

                // Complex Property: Null check + Wrap
                // public ICourse Course => _inner.Course is null ? null : new AsyncCourse(_inner.Course);
                ComplexPropertyContext m =>
                    $"        public {m.InterfaceName} {m.Name} => _inner.{m.Name} is null ? null : new {m.WrapperName}(_inner.{m.Name});",

                // Collection Property: Null check + Select + ToList
                // public IReadOnlyList<IStructure> Structures => _inner.Structures?.Select(x => new AsyncStructure(x)).ToList();
                CollectionPropertyContext m =>
                    $"        public {m.InterfaceName} {m.Name} => _inner.{m.Name}?.Select(x => new {m.WrapperItemName}(x, _service)).ToList();",

                // Simple Collection ->Just convert to List(No wrapping of items)
                SimpleCollectionPropertyContext m =>
                     $"        public {m.InterfaceName} {m.Name} => _inner.{m.Name}?.ToList();",

                // Method: Direct forwarding
                // public void Calculate(int options) => _inner.Calculate(options);
                MethodContext m =>
                    $"        public {m.Symbol} {m.Name}{m.Signature} => _inner.{m.Name}({m.CallParameters});",

                _ => string.Empty
            };
        }

        private static string GenerateSimpleProperty(SimplePropertyContext m) {
            if (m.IsReadOnly) {
                // Cached in Constructor
                return $"        public {m.Symbol} {m.Name} {{ get; }}";
            } else {
                // Forwarding Getter + Async Setter
                var getter = $"        public {m.Symbol} {m.Name} => _inner.{m.Name};";
                var setter = $"        public async Task Set{m.Name}Async({m.Symbol} value) => _service.RunAsync(() => _inner.{m.Name} = value);";
                return $"{getter}\n{setter}";
            }
        }

        private static string GetNamespace(string fullyQualifiedName) {
            // Logic to determine output namespace.
            // For Varian.ESAPI.PlanSetup, you might want EsapiService.Wrappers.
            // For now, mirroring the input namespace or a fixed one:
            return "EsapiService.Wrappers";
        }
    }
}