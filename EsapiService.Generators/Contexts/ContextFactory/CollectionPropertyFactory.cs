using Microsoft.CodeAnalysis;

namespace EsapiService.Generators.Contexts.ContextFactory;
public class CollectionPropertyFactory : IMemberContextFactory {
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

        // 2. Analyze Inner Type (Must be a Wrapped Type)
        var innerType = genericType.TypeArguments[0];
        if (innerType is not INamedTypeSymbol innerNamed || !settings.NamedTypes.IsContained(innerNamed)) {
            // If inner type is NOT wrapped, this is a SimpleCollectionProperty. Skip.
            yield break;
        }

        // 3. Preparation
        string name = property.Name;
        string symbolType = SimplifyTypeString(property.Type.ToDisplayString(settings.Naming.DisplayFormat));
        string xmlDocs = symbol.GetDocumentationCommentXml(expandIncludes: true) ?? string.Empty;

        // e.g. "Structure"
        string innerTypeName = SimplifyTypeString(innerType.ToDisplayString(settings.Naming.DisplayFormat));

        // e.g. "IStructure"
        string innerInterfaceName = settings.Naming.GetInterfaceName(innerNamed.Name);
        // e.g. "AsyncStructure"
        string innerWrapperName = settings.Naming.GetWrapperName(innerNamed.Name);

        // e.g. "IReadOnlyList<IStructure>"
        string collectionInterfaceName = settings.Naming.GetCollectionInterfaceName(innerInterfaceName);
        // e.g. "IReadOnlyList<AsyncStructure>"
        string collectionWrapperName = settings.Naming.GetCollectionWrapperName(innerWrapperName);

        // 4. Create Context
        yield return new CollectionPropertyContext(
            Name: name,
            Symbol: symbolType,
            XmlDocumentation: xmlDocs,
            InnerType: innerTypeName,
            WrapperName: collectionWrapperName,
            InterfaceName: collectionInterfaceName,
            WrapperItemName: innerWrapperName,
            InterfaceItemName: innerInterfaceName
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