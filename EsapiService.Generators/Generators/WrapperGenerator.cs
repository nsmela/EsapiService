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

            if (!string.IsNullOrEmpty(context.XmlDocumentation)) {
                sb.AppendLine(context.XmlDocumentation);
            }

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

            // Determine if we are shadowing a base wrapper member
            bool hasBase = !string.IsNullOrEmpty(context.BaseWrapperName);

            sb.AppendLine($"        internal readonly {context.Name} _inner;");
            sb.AppendLine();
            sb.AppendLine($"        // Store the inner ESAPI object reference");
            sb.AppendLine($"        // internal so other wrappers can access it");
            sb.AppendLine($"        // new to override any inherited _inner fields");
            string newModifier = hasBase ? "new " : "";
            sb.AppendLine($"        internal {newModifier}readonly IEsapiService _service;");
            sb.AppendLine();

            // 5. Constructor
            sb.Append($"        public {context.WrapperName}({context.Name} inner, IEsapiService service)");

            // If we have a base wrapper, we MUST call the base constructor
            if (hasBase) {
                sb.Append($" : base(inner, service)");
            }

            sb.AppendLine(); sb.AppendLine("        {");
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

            // 7. RunAsync
            sb.AppendLine();
            sb.AppendLine($"        public Task RunAsync(Action<{context.Name}> action) => _service.RunAsync(() => action(_inner));");
            sb.AppendLine($"        public Task<T> RunAsync<T>(Func<{context.Name}, T> func) => _service.RunAsync(() => func(_inner));");

            sb.AppendLine("    }");
            sb.AppendLine("}");

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private static string GenerateMember(IMemberContext member) {
            var sb = new StringBuilder();

            sb.Append( member switch {
                // Simple Property: Direct forwarding
                // public string Id => _inner.Id;
                SimplePropertyContext m => GenerateSimpleProperty(m),

                // Complex Property: Null check + Wrap
                // public ICourse Course => _inner.Course is null ? null : new AsyncCourse(_inner.Course);
                ComplexPropertyContext m => GenerateComplexProperty(m),

                // Collection Property: Null check + Select + ToList
                // public IReadOnlyList<IStructure> Structures => _inner.Structures?.Select(x => new AsyncStructure(x)).ToList();
                CollectionPropertyContext m =>
                    $"        public {m.InterfaceName} {m.Name} => _inner.{m.Name}?.Select(x => new {m.WrapperItemName}(x, _service)).ToList();",

                // Simple Collection ->Just convert to List(No wrapping of items)
                SimpleCollectionPropertyContext m =>
                     $"        public {m.InterfaceName} {m.Name} => _inner.{m.Name}?.ToList();",

                // Method: Direct forwarding
                // Void: Just forward
                VoidMethodContext m =>
                    $"        public void {m.Name}{m.Signature} => _inner.{m.Name}({m.CallParameters});",

                // Simple: Just forward
                SimpleMethodContext m =>
                    $"        public {m.ReturnType} {m.Name}{m.Signature} => _inner.{m.Name}({m.CallParameters});",

                // Complex: Wrap result in new Wrapper(result, _service)
                // Note: We need a null check on the return value if it's nullable, 
                // but for Varian ESAPI methods usually return null or object.
                ComplexMethodContext m =>
                    $"        public {m.InterfaceName} {m.Name}{m.Signature} => _inner.{m.Name}({m.CallParameters}) is var result && result is null ? null : new {m.WrapperName}(result, _service);",

                // Simple Collection: ToList()
                SimpleCollectionMethodContext m =>
                    $"        public {m.InterfaceName} {m.Name}{m.Signature} => _inner.{m.Name}({m.CallParameters})?.ToList();",

                // Complex Collection: Select(wrap) -> ToList()
                ComplexCollectionMethodContext m =>
                    $"        public {m.InterfaceName} {m.Name}{m.Signature} => _inner.{m.Name}({m.CallParameters})?.Select(x => new {m.WrapperName}(x, _service)).ToList();",

                OutParameterMethodContext m => GenerateOutParameterMethod(m),

                _ => string.Empty
            });

            return sb.ToString();
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

        private static string GenerateComplexProperty(ComplexPropertyContext m) {
            var sb = new StringBuilder();

            // 1. Getter (Null check + Wrap)
            sb.AppendLine($"        public {m.InterfaceName} {m.Name} => _inner.{m.Name} is null ? null : new {m.WrapperName}(_inner.{m.Name}, _service);");

            // 2. Async Setter (Unwrap + Assign)
            if (!m.IsReadOnly) {
                // Strategy: Cast interface to Wrapper to access internal _inner
                // Usage: await plan.SetCourseAsync(myCourseWrapper);
                sb.AppendLine($"        public System.Threading.Tasks.Task Set{m.Name}Async({m.InterfaceName} value)");
                sb.AppendLine($"        {{");
                sb.AppendLine($"            // Unwrap the interface to get the Varian object");
                sb.AppendLine($"            if (value is {m.WrapperName} wrapper)");
                sb.AppendLine($"            {{");
                sb.AppendLine($"                 return _service.RunAsync(() => _inner.{m.Name} = wrapper._inner);");
                sb.AppendLine($"            }}");
                sb.AppendLine($"            throw new System.ArgumentException(\"Value must be of type {m.WrapperName}\");");
                sb.AppendLine($"        }}");
            }

            return sb.ToString();
        }

        // Add inside the class
        private static string GenerateOutParameterMethod(OutParameterMethodContext m) {
            var sb = new StringBuilder();

            // 1. Input Args
            // EXCLUDE 'out' (they are outputs only).
            // INCLUDE 'ref' (they are inputs, but passed by value to the wrapper).
            var inputArgs = m.Parameters
                .Where(p => !p.IsOut)
                .Select(p => $"{p.InterfaceType} {p.Name}");

            sb.AppendLine($"        public async System.Threading.Tasks.Task<{m.ReturnTupleSignature}> {m.Name}Async({string.Join(", ", inputArgs)})");
            sb.AppendLine("        {");

            // 2. Prepare Temp Variables
            foreach (var p in m.Parameters) {
                if (p.IsOut) {
                    // out vars need declaration: e.g. Varian.ESAPI.PlanSetup plan_temp;
                    sb.AppendLine($"            {p.Type} {p.Name}_temp;");
                } else if (p.IsRef) {
                    // ref vars need initialization: e.g. double norm_temp = norm;
                    // NOTE: If p is a Wrapped type (e.g. AsyncPlanSetup), unwrap it (.inner)!
                    string valueSource = p.IsWrappable ? $"{p.Name}._inner" : p.Name;
                    sb.AppendLine($"            {p.Type} {p.Name}_temp = {valueSource};");
                }
            }

            // 3. Build the Call String
            // e.g. _inner.Calculate(ref norm_temp, out plan_temp);
            var callArgs = m.Parameters.Select(p =>
            {
                if (p.IsOut) return $"out {p.Name}_temp";
                if (p.IsRef) return $"ref {p.Name}_temp";

                // Standard inputs
                return p.IsWrappable ? $"{p.Name}._inner" : p.Name;
            });

            sb.Append("            var result = await _service.RunAsync(() => _inner.");
            sb.Append(m.Name);
            sb.Append("(");
            sb.Append(string.Join(", ", callArgs));
            sb.AppendLine("));");

            // 4. Build Return Tuple
            var returnParts = new List<string>();

            // A. The Result
            if (!m.ReturnsVoid) {
                // Just return result (assuming simple return for now)
                // If complex return types are needed, check IsWrappable here too
                returnParts.Add("result");
            }

            // B. The Out/Ref values (Returned back to user)
            foreach (var p in m.Parameters.Where(x => x.IsOut || x.IsRef)) {
                if (p.IsWrappable) {
                    // Wrap it up: new AsyncPlanSetup(plan_temp, _service)
                    returnParts.Add($"{p.Name}_temp is null ? null : new {p.WrapperType}({p.Name}_temp, _service)");
                } else {
                    // Return raw value
                    returnParts.Add($"{p.Name}_temp");
                }
            }

            sb.AppendLine($"            return ({string.Join(", ", returnParts)});");
            sb.Append("        }"); // End method

            return sb.ToString();
        }

        private static string GetNamespace(string fullyQualifiedName) {
            // Logic to determine output namespace.
            // For Varian.ESAPI.PlanSetup, you might want EsapiService.Wrappers.
            // For now, mirroring the input namespace or a fixed one:
            return "EsapiService.Wrappers";
        }
    }
}