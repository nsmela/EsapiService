using EsapiService.Generators.Contexts;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace EsapiService.Generators.Generators {
    public static class MockGenerator {
        public static string Generate(ClassContext context) {
            var sb = new StringBuilder();

            // 1. Usings
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using VMS.TPS.Common.Model.Types;");
            sb.AppendLine();


            // 2. Namespace
            sb.AppendLine("namespace VMS.TPS.Common.Model.API"); // Or consider dynamic namespace if needed
            sb.AppendLine("{");

            // 3.0. Enum declaration
            if (context.IsEnum) {
                sb.AppendLine($"    public enum {ExtractSimpleName(context.Name)}");
                sb.AppendLine("    {");
                foreach (var m in context.EnumMembers) {
                    sb.AppendLine($"        {m},");
                }
                sb.AppendLine("    }");
                sb.AppendLine("}");
                return sb.ToString();
            }

            // 3. Class Declaration
            string className = ExtractSimpleName(context.Name);
            sb.Append($"    public class {className}");

            if (!string.IsNullOrEmpty(context.BaseName)) {
                sb.Append($" : {ExtractSimpleName(context.BaseName)}");
            }
            

            sb.AppendLine();
            sb.AppendLine("    {");

            // 4. Collection Detection
            // If this class looks like a collection (has GetEnumerator), we create a backing list
            string collectionItemType = DetectCollectionItemType(context.Members);

            // 5. Constructor
            sb.AppendLine($"        public {className}()");
            sb.AppendLine("        {");

            // Initialize standard collection properties to avoid NullRefs
            var cols = context.Members.OfType<CollectionPropertyContext>();
            var simpleCols = context.Members.OfType<SimpleCollectionPropertyContext>();

            foreach (var col in cols) {
                sb.AppendLine($"            {col.Name} = new List<{ExtractSimpleName(col.InnerType)}>();");
            }
            foreach (var col in simpleCols) {
                sb.AppendLine($"            {col.Name} = new List<{SimplifyType(col.InnerType)}>();");
            }
            sb.AppendLine("        }");
            sb.AppendLine();

            // 6. Backing Field for Collection Classes
            if (collectionItemType != null) {
                // We add a public 'Items' list that feeds the Enumerator, Count, and Indexer
                sb.AppendLine($"        // -- Collection Simulation --");
                sb.AppendLine($"        public List<{collectionItemType}> Items {{ get; set; }} = new List<{collectionItemType}>();");
                sb.AppendLine();
            }

            // 7. Members
            foreach (var member in context.Members) {
                sb.AppendLine(GenerateMember(member, collectionItemType));
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private static string GenerateMember(IMemberContext member, string collectionItemType) {
            // --- Special Handling for Collection Classes ---
            if (collectionItemType != null) {
                // Wire up 'Count' to the backing list
                if (member.Name == "Count") {
                    return "        public int Count => Items.Count;";
                }
                // Wire up Indexer 'this[]' to the backing list
                if (member.Name == "this[]") {
                    return $"        public {collectionItemType} this[int index] {{ get => Items[index]; set => Items[index] = value; }}";
                }
                // Wire up 'GetEnumerator' to the backing list
                if (member.Name == "GetEnumerator") {
                    return $"        public IEnumerator<{collectionItemType}> GetEnumerator() => Items.GetEnumerator();";
                }
            }

            // --- Standard Member Generation ---
            return member switch {
                // Properties: Always { get; set; }
                SimplePropertyContext m =>
                    $"        public {SimplifyType(m.Symbol)} {FixIndexer(m.Name)} {{ get; set; }}",

                ComplexPropertyContext m =>
                    $"        public {ExtractSimpleName(m.Symbol)} {FixIndexer(m.Name)} {{ get; set; }}",

                CollectionPropertyContext m =>
                    $"        public {SimplifyType(m.Symbol)} {FixIndexer(m.Name)} {{ get; set; }}",

                SimpleCollectionPropertyContext m =>
                    $"        public {SimplifyType(m.Symbol)} {FixIndexer(m.Name)} {{ get; set; }}",

                // Methods
                VoidMethodContext m =>
                    $"        public void {m.Name}{m.OriginalSignature} {{ }}",

                SimpleMethodContext m =>
                    $"        public {SimplifyType(m.ReturnType)} {m.Name}{m.OriginalSignature} => default;",

                ComplexMethodContext m =>
                    $"        public {ExtractSimpleName(m.Symbol)} {m.Name}{m.OriginalSignature} => default;",

                // Collection Returns: Handle IEnumerator vs List
                SimpleCollectionMethodContext m =>
                    GenerateCollectionReturn(m.Symbol, m.Name, m.OriginalSignature),

                ComplexCollectionMethodContext m =>
                    GenerateCollectionReturn(m.Symbol, m.Name, m.OriginalSignature),

                OutParameterMethodContext m =>
                    GenerateOutMethod(m),

                _ => string.Empty
            };
        }

        private static string DetectCollectionItemType(IEnumerable<IMemberContext> members) {
            // Look for 'GetEnumerator' to identify if this class IS a collection
            var method = members.OfType<IMemberContext>().FirstOrDefault(m => m.Name == "GetEnumerator");
            if (method == null) return null;

            string typeName = "";
            if (method is SimpleMethodContext sm) typeName = sm.ReturnType;
            else if (method is ComplexMethodContext cm) typeName = cm.Symbol;
            else if (method is SimpleCollectionMethodContext scm) typeName = scm.Symbol;
            else if (method is ComplexCollectionMethodContext ccm) typeName = ccm.Symbol;

            if (string.IsNullOrEmpty(typeName)) return null;

            // Extract 'ControlPoint' from 'IEnumerator<ControlPoint>'
            if (typeName.Contains("IEnumerator<")) {
                return ExtractGenericInner(typeName);
            }
            return null;
        }

        private static string GenerateCollectionReturn(string returnType, string name, string signature) {
            string simpleReturnType = SimplifyType(returnType);
            string innerType = ExtractGenericInner(returnType);
            string body = $"new List<{innerType}>()";

            // Check for IEnumerator return types
            if (simpleReturnType.StartsWith("IEnumerator")) {
                body += ".GetEnumerator()";
            }

            return $"        public {simpleReturnType} {name}{signature} => {body};";
        }

        private static string GenerateOutMethod(OutParameterMethodContext m) {
            var sb = new StringBuilder();

            var args = m.Parameters.Select(p => {
                string mod = "";
                if (p.IsOut) mod = "out ";
                if (p.IsRef) mod = "ref ";
                return $"{mod}{SimplifyType(p.Type)} {p.Name}";
            });

            string returnType = m.ReturnsVoid ? "void" : SimplifyType(m.OriginalReturnType);

            sb.AppendLine($"        public {returnType} {m.Name}({string.Join(", ", args)})");
            sb.AppendLine("        {");

            foreach (var p in m.Parameters.Where(x => x.IsOut)) {
                sb.AppendLine($"            {p.Name} = default;");
            }

            if (!m.ReturnsVoid) {
                sb.AppendLine("            return default;");
            }
            sb.AppendLine("        }");

            return sb.ToString();
        }

        // --- Helpers ---

        private static string FixIndexer(string name) {
            if (name == "this[]") return "this[int index]";
            return name;
        }

        private static string SimplifyType(string typeName) {
            if (string.IsNullOrEmpty(typeName)) return typeName;

            return typeName
                .Replace("System.Collections.Generic.", "")
                .Replace("VMS.TPS.Common.Model.API.", "")
                .Replace("VMS.TPS.Common.Model.Types.", "")
                .Replace("Varian.ESAPI.", "")
                .Replace("Varian.", "");
        }

        private static string ExtractSimpleName(string typeName) {
            return SimplifyType(typeName).Split('.').Last();
        }

        private static string ExtractGenericInner(string typeName) {
            int start = typeName.IndexOf('<');
            int end = typeName.LastIndexOf('>');
            if (start < 0 || end < 0) return "object";

            string inner = typeName.Substring(start + 1, end - start - 1);
            return SimplifyType(inner);
        }
    }
}