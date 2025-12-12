using Microsoft.CodeAnalysis;

namespace EsapiService.Generators.Contexts.ContextFactory {
    public class SimpleCollectionPropertyFactory : IMemberContextFactory {
        public IEnumerable<IMemberContext> Create(ISymbol symbol, CompilationSettings settings) {
            // 1. Guard Clauses
            if (symbol is not IPropertySymbol property)
                yield break;

            if (property.IsIndexer)
                yield break;

            // Must be a generic type (e.g. IEnumerable<T>)
            if (property.Type is not INamedTypeSymbol genericType ||
                !genericType.IsGenericType ||
                genericType.TypeArguments.Length != 1) {
                yield break;
            }

            // Must be a collection type
            if (!IsCollection(genericType))
                yield break;

            // 2. Analyze Inner Type (Must NOT be a Wrapped Type)
            var innerType = genericType.TypeArguments[0];

            // If inner type IS wrapped, CollectionPropertyFactory should handle it.
            if (innerType is INamedTypeSymbol innerNamed && settings.NamedTypes.IsContained(innerNamed)) {
                yield break;
            }

            // 3. Preparation
            string name = property.Name;
            string symbolType = SimplifyTypeString(property.Type.ToDisplayString(settings.Naming.DisplayFormat));
            string xmlDocs = symbol.GetDocumentationCommentXml(expandIncludes: true) ?? string.Empty;

            // e.g. "string" or "double"
            string innerTypeName = SimplifyTypeString(innerType.ToDisplayString(settings.Naming.DisplayFormat));

            // e.g. "IReadOnlyList<string>"
            // We use the naming strategy for the container name, passing the simple type as the "inner" name
            string collectionInterfaceName = settings.Naming.GetCollectionInterfaceName(innerTypeName);
            string collectionWrapperName = settings.Naming.GetCollectionWrapperName(innerTypeName);

            // 4. Create Context
            yield return new SimpleCollectionPropertyContext(
                Name: name,
                Symbol: symbolType,
                XmlDocumentation: xmlDocs,
                InnerType: innerTypeName,
                WrapperName: collectionWrapperName,
                InterfaceName: collectionInterfaceName
            );
        }

        // --- Helpers ---

        private static bool IsCollection(ITypeSymbol typeSymbol) {
            if (typeSymbol.SpecialType == SpecialType.System_String)
                return false;

            // Check if it implements IEnumerable
            return typeSymbol.AllInterfaces.Any(i =>
                i.ToDisplayString() == "System.Collections.IEnumerable" ||
                (i.IsGenericType && i.ConstructedFrom.ToDisplayString() == "System.Collections.Generic.IEnumerable<T>"));
        }

        private string SimplifyTypeString(string typeName) {
            string s = typeName
                .Replace("global::", "")
                .Replace("System.Collections.Generic.", "")
                .Replace("System.Threading.Tasks.", "")
                .Replace("VMS.TPS.Common.Model.API.", "")
                .Replace("VMS.TPS.Common.Model.Types.", "")
                .Replace("VMS.TPS.Common.Model.", "");

            if (s.StartsWith("System.Nullable<") && s.EndsWith(">")) {
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