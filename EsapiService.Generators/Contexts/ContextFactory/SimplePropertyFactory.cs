using Microsoft.CodeAnalysis;

namespace EsapiService.Generators.Contexts.ContextFactory;
public class SimplePropertyFactory : IMemberContextFactory {
    public IEnumerable<IMemberContext> Create(ISymbol symbol, CompilationSettings settings) {
        // 1. Guard Clauses
        if (symbol is not IPropertySymbol property)
            yield break;

        if (property.IsIndexer)
            yield break;

        // 2. Filter out Complex types
        // If it is a wrapped type, ComplexPropertyFactory should handle it.
        if (property.Type is INamedTypeSymbol namedType && settings.NamedTypes.IsContained(namedType))
            yield break;

        // 3. Filter out Collections
        // If it is a collection, CollectionPropertyFactory or SimpleCollectionPropertyFactory handles it.
        // Note: We treat string as a Simple Property, not a collection.
        if (IsCollection(property.Type))
            yield break;

        // 4. Preparation
        string name = property.Name;
        string symbolType = SimplifyTypeString(property.Type.ToDisplayString(settings.Naming.DisplayFormat));
        string xmlDocs = symbol.GetDocumentationCommentXml(expandIncludes: true) ?? string.Empty;

        bool isReadOnly = property.SetMethod is null ||
                          property.SetMethod.DeclaredAccessibility != Accessibility.Public;

        // 5. Create Context
        yield return new SimplePropertyContext(
            Name: name,
            Symbol: symbolType,
            XmlDocumentation: xmlDocs,
            IsReadOnly: isReadOnly
        );
    }

    // --- Helpers ---

    private static bool IsCollection(ITypeSymbol typeSymbol) {
        // String implements IEnumerable<char>, but we treat it as a scalar simple type
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