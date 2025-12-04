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
    private string NamespaceName() => _namedTypes.

    public ContextService(NamespaceCollection namedTypes)
    {
        _namedTypes = namedTypes;
    }

    public TargetContext BuildContext(INamedTypeSymbol symbol)
    {
        var context = new TargetContext
        {
            Name = symbol.ToDisplayString(DisplayFormat),
            InterfaceName = InterfaceName(symbol.Name),
            WrapperName = WrapperName(symbol.Name),
            IsAbstract = symbol.IsAbstract,

            // --- Inheritance --- //
            BaseName = symbol.BaseType?.Name ?? string.Empty,
            BaseInterface = _namedTypes.InterfaceIfContained(symbol.BaseType) ?? string.Empty,
            BaseWrapperName = _namedTypes.WrapperIfContained(symbol.BaseType) ?? string.Empty,
        };

        return context;
    }
}
