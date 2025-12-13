using Microsoft.CodeAnalysis;

namespace EsapiService.Generators.Contexts.ContextFactory;
public class UnknownTypeFactory : IMemberContextFactory {
    public IEnumerable<IMemberContext> Create(ISymbol symbol, CompilationSettings settings) {
        // 1. Analyze the member for unknown types
        if (UsesUnknownApiType(symbol, settings)) {
            yield return new SkippedMemberContext(symbol.Name, "References non-wrapped Varian API type");
            yield break;
        }
    }

    // --- Logic extracted from ContextService ---

    private bool UsesUnknownApiType(ISymbol member, CompilationSettings settings) {
        foreach (var type in GetReferencedTypes(member)) {
            var leafType = GetLeafType(type);

            if (leafType is INamedTypeSymbol named &&
                named.ContainingNamespace?.ToDisplayString().StartsWith("VMS.TPS") == true &&
                !settings.NamedTypes.IsContained(named)) {

                // Explicitly allow types from the "Types" namespace (Value Objects)
                // to pass through without being skipped.
                if (named.ContainingNamespace.ToDisplayString() == "VMS.TPS.Common.Model.Types")
                    continue;

                // Allow Public Structs defined in API ---
                // If it's a struct (ValueType) and not a Class, it's likely a DTO we can use directly.
                if (named.TypeKind == TypeKind.Struct)
                    continue;

                // We found a Varian type that is NOT in our generation list.
                // We must skip this member.
                return true;
            }
        }
        return false;
    }

    private IEnumerable<ITypeSymbol> GetReferencedTypes(ISymbol member) {
        if (member is IPropertySymbol p) yield return p.Type;
        if (member is IFieldSymbol f) yield return f.Type;
        if (member is IMethodSymbol m) {
            yield return m.ReturnType;
            foreach (var param in m.Parameters) {
                yield return param.Type;
            }
        }
    }

    private ITypeSymbol GetLeafType(ITypeSymbol type) {
        if (type is IArrayTypeSymbol arr)
            return GetLeafType(arr.ElementType);

        if (type is INamedTypeSymbol named && named.IsGenericType && named.TypeArguments.Length > 0) {
            // Recursive check for nested generics if needed, 
            // but usually getting the first arg is enough for Collections
            return GetLeafType(named.TypeArguments[0]);
        }

        return type;
    }
}
