using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsapiService.Generators.Contexts;

public interface IContextService
{
    TargetContext BuildContext(INamedTypeSymbol symbol);
}

public class ContextService : IContextService
{
    private readonly NamespaceCollection _namedTypes;

    private static SymbolDisplayFormat DisplayFormat => SymbolDisplayFormat.FullyQualifiedFormat;

    private string InterfaceName(string name) => $"I{name}";
    private string WrapperName(string name) => $"Async{name}";
    private string NamespaceName() => _namedTypes.NamespaceName;

    public ContextService(NamespaceCollection namedTypes)
    {
        _namedTypes = namedTypes;
    }

    public TargetContext BuildContext(INamedTypeSymbol symbol)
    {
        // --- Inheritance --- //
        string baseName = string.Empty;
        string baseInterfaceName = string.Empty;
        string baseWrapperName = string.Empty;

        INamedTypeSymbol? baseType = symbol.BaseType;
        if (baseType is not null && _namedTypes.IsContained(baseType))
        {
            baseName = baseType.Name;
            baseWrapperName = WrapperName(baseType.Name);
            baseInterfaceName = InterfaceName(baseType.Name);
        }

        // --- Result --- //
        var context = new TargetContext
        {
            Name = symbol.ToDisplayString(DisplayFormat),
            InterfaceName = InterfaceName(symbol.Name),
            WrapperName = WrapperName(symbol.Name),
            IsAbstract = symbol.IsAbstract,

            // --- Inheritance --- //
            BaseName = baseName,
            BaseInterface = baseInterfaceName,
            BaseWrapperName = baseWrapperName,
        };

        return context;
    }

    // --- Private Helper Methods --- //
    private List<ISymbol> GetMembers(INamedTypeSymbol symbol)
    {
        // We INCLUDE shadowed properties here so the generator can decide to use 'new'
        return symbol.GetMembers()
           .Where(m => m.ContainingType.Equals(symbol, SymbolEqualityComparer.Default)
                    && m.DeclaredAccessibility == Accessibility.Public
                    && !m.IsStatic
                    && !m.IsImplicitlyDeclared)
           .ToList();
    }
}
