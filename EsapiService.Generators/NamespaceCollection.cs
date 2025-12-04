using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsapiService.Generators;

public class NamespaceCollection
{
    private readonly ImmutableHashSet<ITypeSymbol> _namedTypes = [];

    // Constructor now accepts the list of types (Dependency Injection)
    public NamespaceCollection(IEnumerable<ITypeSymbol> types)
    {
        _namedTypes = types.ToImmutableHashSet<ITypeSymbol>(SymbolEqualityComparer.Default);
    }

    /// <summary>
    /// Determines if the Type is targeted for wrapping
    /// </summary>
    /// <param name="type"></param>
    /// <returns>is the NameTypeSymbol is a taget in the namespace</returns>
    public bool IsContained(ISymbol type) => 
        _namedTypes.Contains(type);

    public string InterfaceIfContained(ITypeSymbol type) =>
        _namedTypes.Contains(type)
        ? $"I{type.Name}"
        : type.Name;

    public string WrapperIfContained(ITypeSymbol type) =>
        _namedTypes.Contains(type)
        ? $"Async{type.Name}"
        : type.Name;

    public string NamespaceName => "NamespaceName";

}
