using EsapiService.Generators.Contexts;
using System.Text;

namespace EsapiService.Generators.Generators {
    public static class WrapperGenerator {
        public static string Generate(ClassContext context) {
            var sb = new StringBuilder();

            // 1. Usings
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Threading.Tasks;");

            // Varian usings
            sb.AppendLine("using VMS.TPS.Common.Model.API;");
            sb.AppendLine("using VMS.TPS.Common.Model.Types;");
            sb.AppendLine("using Esapi.Interfaces;");
            sb.AppendLine("using Esapi.Services;");
            sb.AppendLine();

            // 2. Namespace
            string namespaceName = GetNamespace(context.Name);
            sb.AppendLine($"namespace {namespaceName}");
            sb.AppendLine("{");

            // 3. Class Declaration
            if (!string.IsNullOrEmpty(context.XmlDocumentation)) {
                sb.AppendLine(context.XmlDocumentation);
            }

            // e.g. public class AsyncPlanSetup : IPlanSetup
            sb.Append($"    public class {context.WrapperName}");

            bool hasBase = !string.IsNullOrEmpty(context.BaseWrapperName);
            bool hasInterface = !string.IsNullOrEmpty(context.InterfaceName);

            if (hasBase) {
                sb.Append($" : {context.BaseWrapperName}");
                if (hasInterface) {
                    sb.Append($", {context.InterfaceName}");
                }
            } else if (hasInterface) {
                sb.Append($" : {context.InterfaceName}");
            }

            sb.AppendLine();
            sb.AppendLine("    {");

            // 4. Fields
            // Determine if we are shadowing a base wrapper member

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

            sb.AppendLine(); 
            sb.AppendLine("        {");
            sb.AppendLine("            _inner = inner;");
            sb.AppendLine("            _service = service;");
            sb.AppendLine();

            foreach (var member in context.Members.OfType<SimplePropertyContext>()) {
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
            sb.AppendLine($"        public Task RunAsync(Action<{context.Name}> action) => _service.PostAsync((context) => action(_inner));");
            sb.AppendLine($"        public Task<T> RunAsync<T>(Func<{context.Name}, T> func) => _service.PostAsync<T>((context) => func(_inner));");

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private static string GenerateMember(IMemberContext member) {
            // methods to skip
            if (member.Name == "GetEnumerator") { return string.Empty; }

            var sb = new StringBuilder();
            sb.AppendLine();

            sb.Append( member switch {
                // Simple Property: Direct forwarding
                // public string Id => _inner.Id;
                SimplePropertyContext m => GenerateSimpleProperty(m),

                // Complex Property: Null check + Wrap
                // public ICourse Course => _inner.Course is null ? null : new AsyncCourse(_inner.Course);
                ComplexPropertyContext m => GenerateComplexProperty(m),

                // Collection Property: Async Method + Wrap Items
                // public IReadOnlyList<IStructure> Structures => _inner.Structures?.Select(x => new AsyncStructure(x)).ToList();
                CollectionPropertyContext m =>
                    GenerateCollectionProperty(m),

                // Simple Collection: Async Method + ToList
                SimpleCollectionPropertyContext m =>
                    GenerateSimpleCollectionProperty(m),

                // Methods
                // 1. Void Method -> Task NameAsync()
                //    Forward directly to RunAsync(Action)
                VoidMethodContext m =>
                    $"        public Task {m.Name}Async{m.Signature} => _service.PostAsync(context => _inner.{m.Name}({BuildCallArguments(m.Parameters)}));",

                // 2. Simple Return -> Task<T> NameAsync()
                //    Forward directly to RunAsync<T>(Func<T>)
                SimpleMethodContext m =>
                    $"        public Task<{m.ReturnType}> {m.Name}Async{m.Signature} => _service.PostAsync(context => _inner.{m.Name}({BuildCallArguments(m.Parameters)}));",

                // 3. Complex Return -> async Task<Interface> NameAsync()
                //    Execute, Check Null, Wrap
                ComplexMethodContext m =>
                    GenerateComplexMethod(m),

                // 4. Simple Collection Return -> Task<IReadOnlyList<T>>
                SimpleCollectionMethodContext m =>
                    $"        public Task<{m.InterfaceName}> {m.Name}Async{m.Signature} => _service.PostAsync(context => _inner.{m.Name}({BuildCallArguments(m.Parameters)})?.ToList());",

                // 5. Complex Collection Return -> async Task<IReadOnlyList<Interface>>
                ComplexCollectionMethodContext m =>
                    GenerateComplexCollectionMethod(m),

                OutParameterMethodContext m => GenerateOutParameterMethod(m),

                _ => string.Empty
            });

            return sb.ToString();
        }

        private static string BuildCallArguments(IEnumerable<ParameterContext> parameters) {
            // Transforms:
            // (IBolus bolus) -> ((AsyncBolus)bolus)._inner
            // (int x)        -> x

            var args = parameters.Select(p =>
            {
                if (p.IsWrappable) {
                    // Cast to the concrete Wrapper type and access _inner
                    return $"(({p.WrapperType}){p.Name})._inner";
                }
                return p.Name;
            });

            return string.Join(", ", args);
        }

        private static string GenerateSimpleProperty(SimplePropertyContext m) {
            var sb = new StringBuilder();

            // Property Definition (Auto-Property with Private Set)
            string setterMod = m.IsReadOnly ? "" : " private set;";
            sb.AppendLine($"        public {m.Symbol} {m.Name} {{ get;{setterMod} }}");

            // Async Setter
            if (!m.IsReadOnly) {
                sb.AppendLine($"        public async Task Set{m.Name}Async({m.Symbol} value)");
                sb.AppendLine($"        {{");
                // OPTIMIZATION: One trip to ESAPI thread to Set AND Get the confirmed value
                sb.AppendLine($"            {m.Name} = await _service.PostAsync(context => ");
                sb.AppendLine($"            {{");
                sb.AppendLine($"                _inner.{m.Name} = value;");
                sb.AppendLine($"                return _inner.{m.Name};");
                sb.AppendLine($"            }});");
                sb.AppendLine($"        }}");
            }

            return sb.ToString().TrimEnd();
        }

        private static string GenerateComplexProperty(ComplexPropertyContext m) {
            var sb = new StringBuilder();

            // --- Indexer Implementation ---
            if (m.Name == "this[]") {

                // Async Getter
                sb.AppendLine($"        public async Task<{m.InterfaceName}> GetItemAsync(int index)");
                sb.AppendLine($"        {{");
                sb.AppendLine($"            return await _service.PostAsync(context => ");
                sb.AppendLine($"                _inner[index] is null ? null : new {m.WrapperName}(_inner[index], _service));");
                sb.AppendLine($"        }}");
                sb.AppendLine();
                sb.AppendLine($"        public async Task<IReadOnlyList<{m.InterfaceName}>> GetAllItemsAsync()");
                sb.AppendLine($"        {{");
                sb.AppendLine($"            return await _service.PostAsync(context => ");
                sb.AppendLine($"                _inner.Select(x => new {m.WrapperName}(x, _service)).ToList());");
                sb.AppendLine($"        }}");
                return sb.ToString().TrimEnd();
            }

            // 1. Async Getter
            // Signature matches Interface: Task<ICourse> GetCourseAsync()
            sb.AppendLine($"        public async Task<{m.InterfaceName}> Get{m.Name}Async()");
            sb.AppendLine($"        {{");
            sb.AppendLine($"            return await _service.PostAsync(context => ");
            sb.AppendLine($"                _inner.{m.Name} is null ? null : new {m.WrapperName}(_inner.{m.Name}, _service));");
            sb.AppendLine($"        }}");

            // 2. Async Setter
            if (!m.IsReadOnly) {
                sb.AppendLine();
                sb.AppendLine($"        public async Task Set{m.Name}Async({m.InterfaceName} value)");
                sb.AppendLine($"        {{");
                sb.AppendLine($"            // Handle null assignment");
                sb.AppendLine($"            if (value is null)");
                sb.AppendLine($"            {{");
                sb.AppendLine($"                await _service.PostAsync(context => _inner.{m.Name} = null);");
                sb.AppendLine($"                return;");
                sb.AppendLine($"            }}");

                sb.AppendLine($"            // Unwrap the interface to get the Varian object");
                sb.AppendLine($"            if (value is {m.WrapperName} wrapper)");
                sb.AppendLine($"            {{");
                sb.AppendLine($"                 _service.PostAsync(context => _inner.{m.Name} = wrapper._inner);");
                sb.AppendLine($"                 return;");
                sb.AppendLine($"            }}");

                sb.AppendLine($"            throw new System.ArgumentException(\"Value must be of type {m.WrapperName}\");");
                sb.AppendLine($"        }}");
            }

            return sb.ToString().TrimEnd();
        }

        private static string GenerateCollectionProperty(CollectionPropertyContext m) {
            var sb = new StringBuilder();
            sb.AppendLine($"        public async Task<{m.InterfaceName}> Get{m.Name}Async()");
            sb.AppendLine($"        {{");
            sb.AppendLine($"            return await _service.PostAsync(context => ");
            // Note: We cast to Interface explicitly if needed, but List<Wrapper> : List<Interface> isn't covariant in .NET Framework lists easily.
            // However, IReadOnlyList<Wrapper> IS covariant to IReadOnlyList<Interface>.
            sb.AppendLine($"                _inner.{m.Name}?.Select(x => new {m.WrapperItemName}(x, _service)).ToList());");
            sb.AppendLine($"        }}");
            return sb.ToString();
        }

        private static string GenerateSimpleCollectionProperty(SimpleCollectionPropertyContext m) {
            var sb = new StringBuilder();
            sb.AppendLine($"        public Task<{m.InterfaceName}> Get{m.Name}Async()");
            sb.AppendLine($"        {{");
            sb.AppendLine($"            return _service.PostAsync(context => _inner.{m.Name}?.ToList());");
            sb.AppendLine($"        }}");
            return sb.ToString();
        }

        private static string GenerateComplexMethod(ComplexMethodContext m) {
            var sb = new StringBuilder();
            sb.AppendLine($"        public async Task<{m.InterfaceName}> {m.Name}Async{m.Signature}");
            sb.AppendLine($"        {{");
            sb.AppendLine($"            return await _service.PostAsync(context => ");
            sb.AppendLine($"                _inner.{m.Name}({BuildCallArguments(m.Parameters)}) is var result && result is null ? null : new {m.WrapperName}(result, _service));");
            sb.AppendLine($"        }}");
            return sb.ToString();
        }

        private static string GenerateComplexCollectionMethod(ComplexCollectionMethodContext m) {
            var sb = new StringBuilder();
            sb.AppendLine($"        public async Task<{m.InterfaceName}> {m.Name}Async{m.Signature}");
            sb.AppendLine($"        {{");
            sb.AppendLine($"            return await _service.PostAsync(context => ");
            // Convert to List of Wrappers
            sb.AppendLine($"                _inner.{m.Name}({BuildCallArguments(m.Parameters)})?.Select(x => new {m.WrapperName}(x, _service)).ToList());");
            sb.AppendLine($"        }}");
            return sb.ToString();
        }

        private static string GenerateOutParameterMethod(OutParameterMethodContext m) {
            var sb = new StringBuilder();

            // 1. Input Args
            var inputArgs = m.Parameters
                .Where(p => !p.IsOut)
                .Select(p => $"{p.InterfaceType} {p.Name}");

            sb.AppendLine($"        public async Task<{m.ReturnTupleSignature}> {m.Name}Async({string.Join(", ", inputArgs)})");
            sb.AppendLine("        {");

            // 2. Prepare Temp Variables
            foreach (var p in m.Parameters) {
                if (p.IsOut) {
                    sb.AppendLine($"            {p.Type} {p.Name}_temp;");
                } else if (p.IsRef) {
                    string valueSource = p.IsWrappable ? $"{p.Name}._inner" : p.Name;
                    sb.AppendLine($"            {p.Type} {p.Name}_temp = {valueSource};");
                }
            }

            // 3. Build the Call String
            var callArgs = m.Parameters.Select(p => {
                if (p.IsOut) return $"out {p.Name}_temp";
                if (p.IsRef) return $"ref {p.Name}_temp";
                return p.IsWrappable ? $"{p.Name}._inner" : p.Name;
            });

            // A: Handle Void vs Non-Void Result Assignment
            sb.Append("            "); // Indent
            if (!m.ReturnsVoid) {
                sb.Append("var result = ");
            }
            sb.Append("await _service.PostAsync(context => _inner.");
            sb.Append(m.Name);
            sb.Append("(");
            sb.Append(string.Join(", ", callArgs));
            sb.AppendLine("));");

            // 4. Build Return Tuple
            var returnParts = new List<string>();

            // A. The Result
            if (!m.ReturnsVoid) {
                // B: Wrap Result if Wrappable
                if (m.IsReturnWrappable) {
                    returnParts.Add($"result is null ? null : new {m.WrapperReturnTypeName}(result, _service)");
                } else {
                    returnParts.Add("result");
                }
            }

            // B. The Out/Ref values
            foreach (var p in m.Parameters.Where(x => x.IsOut || x.IsRef)) {
                if (p.IsWrappable) {
                    returnParts.Add($"{p.Name}_temp is null ? null : new {p.WrapperType}({p.Name}_temp, _service)");
                } else {
                    returnParts.Add($"{p.Name}_temp");
                }
            }

            sb.AppendLine($"            return ({string.Join(", ", returnParts)});");
            sb.Append("        }"); // End method

            return sb.ToString();
        }

        private static string GetNamespace(string fullyQualifiedName) {
            // Logic to determine output namespace.
            // using Esapi.Wrappers.
            return "Esapi.Wrappers";
        }
    }
}