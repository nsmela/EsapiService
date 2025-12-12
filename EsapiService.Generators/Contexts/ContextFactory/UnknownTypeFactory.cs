using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsapiService.Generators.Contexts.ContextFactory;
public class UnknownTypeFactory : IMemberContextFactory {

    public IEnumerable<IMemberContext> Create(ISymbol symbol, CompilationSettings settings) {
        if (UsesUnknownApiType(symbol, settings.NamedTypes)) {
            yield return new SkippedMemberContext(symbol.Name, "References non-wrapped Varian type");
        }
    }

    // The key logic: Returns true if the member uses a VMS type that we DO NOT have in our whitelist.
    private bool UsesUnknownApiType(ISymbol member, NamespaceCollection namedTypes) {
        foreach (var type in GetReferencedTypes(member)) {
            var leafType = GetLeafType(type);

            if (leafType is INamedTypeSymbol named &&
                named.ContainingNamespace?.ToDisplayString().StartsWith("VMS.TPS") == true &&
                !namedTypes.IsContained(named)) {
                // We found a Varian type that is NOT in our generation list.
                // We must skip this member to avoid compilation errors.
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
            foreach (var param in m.Parameters) yield return param.Type;
        }
    }

    private ITypeSymbol GetLeafType(ITypeSymbol type) {
        if (type is IArrayTypeSymbol arr) return GetLeafType(arr.ElementType);
        if (type is INamedTypeSymbol named && named.IsGenericType && named.TypeArguments.Length > 0) {
            return GetLeafType(named.TypeArguments[0]);
        }
        return type;
    }

}
