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
        if (!IsCollection(genericType)) yield break;

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

    private static bool IsCollection(ITypeSymbol type)
    {
        // Strings are technically collections of chars, but we treat them as primitives
        if (type.SpecialType == SpecialType.System_String)
            return false;

        // Helper to check if a single symbol is IEnumerable
        bool IsIEnumerable(ITypeSymbol t)
        {
            // 1. Fast Check: System Types
            if (t.OriginalDefinition.SpecialType == SpecialType.System_Collections_Generic_IEnumerable_T ||
                t.OriginalDefinition.SpecialType == SpecialType.System_Collections_IEnumerable)
                return true;

            // 2. Robust Check: String Matching
            // This catches "IEnumerable<out T>" and other metadata variations
            var def = (t as INamedTypeSymbol)?.ConstructedFrom ?? t;
            var name = def.ToDisplayString();

            return name.StartsWith("System.Collections.Generic.IEnumerable") ||
                   name == "System.Collections.IEnumerable";
        }

        // Check the type itself AND its interfaces
        return IsIEnumerable(type) || type.AllInterfaces.Any(IsIEnumerable);
    }

    bool IsIEnumerable(ITypeSymbol t) {
        // DEBUG: Print exactly what we are checking
        // Console.WriteLine($"Checking: {t.ToDisplayString()} | Special: {t.OriginalDefinition.SpecialType}");

        // Exclude string if specified, as strings are technically IEnumerable<char>
        if (t.SpecialType == SpecialType.System_String) {
            return false;
        }

        // Check 1: Original Definition name
        var name = t.OriginalDefinition.Name;
        if (name == "IEnumerable")
            return true;

        // Check 2: Strings (Metadata sometimes misses SpecialType)
        string display = t.ToDisplayString(); // e.g. "System.Collections.IEnumerable"

        // Handle "ConstructedFrom" for generics
        if (t is INamedTypeSymbol nt && nt.IsGenericType) {
            // This converts "IEnumerable<Course>" -> "IEnumerable<T>"
            display = nt.ConstructedFrom.ToDisplayString();
        }

        // DEBUG: Verify the string format
        if (display.Contains("IEnumerable")) {
            // Console.WriteLine($"   -> Matched String: '{display}'");
        }

        return display == "System.Collections.IEnumerable" ||
               display == "System.Collections.Generic.IEnumerable<T>" ||
               // Add this fallback just in case of covariance "out T"
               display == "System.Collections.Generic.IEnumerable<out T>";
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