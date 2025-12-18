using EsapiService.Generators.Contexts;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace EsapiService.Generators.Generators {
    public static class MockGenerator {
        // 1. Entry Point: Generates the FILE (Usings + Namespace)
        public static string Generate(ClassContext context) {
            var sb = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using VMS.TPS.Common.Model.Types;");
            sb.AppendLine();

            sb.AppendLine("namespace VMS.TPS.Common.Model.API");
            sb.AppendLine("{");

            // Delegate the actual class/struct creation to a helper
            sb.Append(GenerateTypeRecursively(context, indentationLevel: 1));

            sb.AppendLine("}"); // End Namespace

            return sb.ToString();
        }

        // 2. Recursive Helper: Generates the Type (Class/Struct) and its Children
        private static string GenerateTypeRecursively(ClassContext context, int indentationLevel) {
            var sb = new StringBuilder();
            string indent = new string(' ', indentationLevel * 4);

            // --- Enum Handling ---
            if (context.IsEnum) {
                sb.AppendLine($"{indent}public enum {ExtractSimpleName(context.Name)}");
                sb.AppendLine($"{indent}{{");
                foreach (var m in context.EnumMembers) {
                    sb.AppendLine($"{indent}    {m},");
                }
                sb.AppendLine($"{indent}}}");
                return sb.ToString();
            }

            // --- Class/Struct Declaration ---
            string simpleName = ExtractSimpleName(context.Name);
            string typeKind = context.IsStruct ? "struct" : "class";

            // Name Collision Fix: If nested struct has same name as parent, append "Type"
            // (Only relevant if this is actually nested, but safe to check generally)
            // Note: In C#, a nested type cannot have the same name as the enclosing type.
            // We handled the simplename extraction, but we might need manual aliasing if collision occurs.
            // For now, we assume Varian DLLs don't violate C# rules.

            sb.Append($"{indent}public {typeKind} {simpleName}");

            if (!string.IsNullOrEmpty(context.BaseName)) {
                sb.Append($" : {ExtractSimpleName(context.BaseName)}");
            }

            sb.AppendLine();
            sb.AppendLine($"{indent}{{");

            // --- Collection Detection ---
            string collectionItemType = DetectCollectionItemType(context.Members);

            // --- Constructor ---
            // Only generate constructors for classes, or structs if needed
            sb.AppendLine($"{indent}    public {simpleName}()");
            sb.AppendLine($"{indent}    {{");

            var cols = context.Members.OfType<CollectionPropertyContext>();
            var simpleCols = context.Members.OfType<SimpleCollectionPropertyContext>();

            foreach (var col in cols) {
                sb.AppendLine($"{indent}        {col.Name} = new List<{ExtractSimpleName(col.InnerType)}>();");
            }
            foreach (var col in simpleCols) {
                sb.AppendLine($"{indent}        {col.Name} = new List<{SimplifyType(col.InnerType)}>();");
            }
            sb.AppendLine($"{indent}    }}");
            sb.AppendLine();

            // --- Implicit Operators (For Structs acting like Strings) ---
            if (context.HasImplicitStringConversion) {
                sb.AppendLine($"{indent}    // Implicit conversion to string (mock behavior)");
                sb.AppendLine($"{indent}    public override string ToString() => string.Empty;");
                sb.AppendLine($"{indent}    public static implicit operator string({simpleName} val) => val.ToString();");
                // Optional: Allow assigning string to it for easy test setup
                sb.AppendLine($"{indent}    public static implicit operator {simpleName}(string val) => new {simpleName}();");
            }

            // --- Collection Backing Field ---
            if (collectionItemType != null) {
                sb.AppendLine($"{indent}    // -- Collection Simulation --");
                sb.AppendLine($"{indent}    public List<{collectionItemType}> Items {{ get; set; }} = new List<{collectionItemType}>();");
                sb.AppendLine();
            }

            // --- Members ---
            foreach (var member in context.Members) {
                sb.AppendLine(GenerateMember(member, collectionItemType, indent + "    "));
            }

            // --- RECURSIVE GENERATION OF NESTED TYPES ---
            if (context.NestedTypes != null && context.NestedTypes.Any()) {
                sb.AppendLine();
                sb.AppendLine($"{indent}    // --- Nested Types ---");
                foreach (var nested in context.NestedTypes) {
                    // Recurse with increased indentation
                    sb.Append(GenerateTypeRecursively(nested, indentationLevel + 1));
                }
            }

            // 10. Skipped Members Report
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

            sb.AppendLine($"{indent}}}"); // End Class/Struct
            return sb.ToString();
        }

        private static string GenerateMember(IMemberContext member, string collectionItemType, string indent) {
            // --- Special Handling for Collection Classes ---
            if (collectionItemType != null) {
                if (member.Name == "Count") { return $"{indent}public int Count => Items.Count;"; }
                if (member.Name == "this[]") { return $"{indent}public {collectionItemType} this[int index] {{ get => Items[index]; set => Items[index] = value; }}"; }
                if (member.Name == "GetEnumerator") { return $"{indent}public IEnumerator<{collectionItemType}> GetEnumerator() => Items.GetEnumerator();"; }
            }

            // Determine modifier
            string modifiers = member.IsStatic ? "public static" : "public";

            // --- Standard Member Generation ---
            return member switch {
                // Properties: Always { get; set; }
                SimplePropertyContext m =>
                    $"{indent}{modifiers} {SimplifyType(m.Symbol)} {FixIndexer(m.Name)} {{ get; set; }}",

                ComplexPropertyContext m =>
                    $"{indent}{modifiers} {ExtractSimpleName(m.Symbol)} {FixIndexer(m.Name)} {{ get; set; }}",

                CollectionPropertyContext m =>
                    $"{indent}{modifiers} {SimplifyType(m.Symbol)} {FixIndexer(m.Name)} {{ get; set; }}",

                SimpleCollectionPropertyContext m =>
                    $"{indent}{modifiers} {SimplifyType(m.Symbol)} {FixIndexer(m.Name)} {{ get; set; }}",

                // Methods
                VoidMethodContext m =>
                    $"{indent}{modifiers} void {m.Name}{m.OriginalSignature} {{ }}",

                SimpleMethodContext m =>
                    $"{indent}{modifiers} {SimplifyType(m.ReturnType)} {m.Name}{m.OriginalSignature} => default;",

                ComplexMethodContext m =>
                    $"{indent}{modifiers} {ExtractSimpleName(m.Symbol)} {m.Name}{m.OriginalSignature} => default;",

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

        // helper to ensure property types like 'CalculationModel.Algorithm' become just 'Algorithm' 
        // IF we are inside CalculationModel. However, usually SimplifyType handles global simplification.
        private static string SimplifyType(string typeName) {
            if (string.IsNullOrEmpty(typeName)) return typeName;

            return typeName
                .Replace("System.Collections.Generic.", "")
                .Replace("VMS.TPS.Common.Model.API.", "")
                .Replace("VMS.TPS.Common.Model.Types.", "")
                .Replace("Varian.ESAPI.", "")
                .Replace("Varian.", "");
        }

        // Ensure ExtractSimpleName handles nested types (e.g. CalculationModel.Algorithm -> Algorithm)
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