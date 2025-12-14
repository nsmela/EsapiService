using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using EsapiService.Generators.Contexts;

namespace EsapiService.Generators.Contexts.ContextFactory
{
    public class SimplePropertyFactory : IMemberContextFactory
    {
        public IEnumerable<IMemberContext> Create(ISymbol symbol, CompilationSettings settings)
        {
            // 1. Guard Clauses - Handle Property OR Field
            ITypeSymbol type = null;
            bool isReadOnly = false;

            if (symbol is IPropertySymbol property)
            {
                if (property.IsIndexer)
                    yield break;
                type = property.Type;

                // Property is ReadOnly if it has no setter or the setter is not public
                isReadOnly = property.SetMethod is null ||
                             property.SetMethod.DeclaredAccessibility != Accessibility.Public;
            } else if (symbol is IFieldSymbol field)
            {
                // Fields are valid too (e.g. structs often use public fields)
                type = field.Type;

                // Field is ReadOnly if it's marked 'readonly' or 'const'
                isReadOnly = field.IsReadOnly || field.IsConst;
            } else
            {
                // Not a property or field -> Not handled by this factory
                yield break;
            }

            // 2. Filter out Complex types (Wrapped Objects)
            // If it's a wrapped type, ComplexPropertyFactory should handle it.
            if (type is INamedTypeSymbol namedType && settings.NamedTypes.IsContained(namedType))
                yield break;

            // 3. Filter out Collections (BUT ALLOW ARRAYS)
            // Arrays are treated as simple properties (snapshots), not collections.
            if (type.Kind != SymbolKind.ArrayType && IsCollection(type))
                yield break;

            // 4. Preparation
            string name = symbol.Name;
            string symbolType = SimplifyTypeString(type.ToDisplayString(settings.Naming.DisplayFormat));
            string xmlDocs = symbol.GetDocumentationCommentXml(expandIncludes: true) ?? string.Empty;

            // 5. Create Context
            yield return new SimplePropertyContext(
                Name: name,
                Symbol: symbolType,
                XmlDocumentation: xmlDocs,
                IsReadOnly: isReadOnly
            );
        }

        // --- Helpers ---

        private static bool IsCollection(ITypeSymbol typeSymbol)
        {
            if (typeSymbol.SpecialType == SpecialType.System_String)
                return false;

            return typeSymbol.AllInterfaces.Any(i =>
                i.ToDisplayString() == "System.Collections.IEnumerable" ||
                (i.IsGenericType && i.ConstructedFrom.ToDisplayString() == "System.Collections.Generic.IEnumerable<T>"));
        }

        private string SimplifyTypeString(string typeName)
        {
            string s = typeName
                .Replace("global::", "")
                .Replace("System.Collections.Generic.", "")
                .Replace("System.Threading.Tasks.", "")
                .Replace("VMS.TPS.Common.Model.API.", "")
                .Replace("VMS.TPS.Common.Model.Types.", "")
                .Replace("VMS.TPS.Common.Model.", "");

            if (s.StartsWith("System.Nullable<") && s.EndsWith(">"))
            {
                s = s.Substring(16, s.Length - 17) + "?";
            }

            return s.Replace("System.DateTime", "DateTime")
                    .Replace("System.String", "string")
                    .Replace("System.Double", "double")
                    .Replace("System.Int32", "int")
                    .Replace("System.Boolean", "bool")
                    .Replace("System.Void", "void")
                    .Replace("System.Object", "object")
                    .Replace("System.Action", "Action")
                    .Replace("System.Func", "Func");
        }
    }
}