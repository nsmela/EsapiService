using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsapiService.Generators.Contexts;

public interface IContextService
{
    ClassContext BuildContext(INamedTypeSymbol symbol);
}

public class ContextService : IContextService
{
    private readonly NamespaceCollection _namedTypes;

    private static SymbolDisplayFormat DisplayFormat => 
        SymbolDisplayFormat
            .FullyQualifiedFormat
            .WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.Omitted);

    private string InterfaceName(string name) => $"I{name}";
    private string WrapperName(string name) => $"Async{name}";
    private string NamespaceName() => _namedTypes.NamespaceName;

    public ContextService(NamespaceCollection namedTypes)
    {
        _namedTypes = namedTypes;
    }

    public ClassContext BuildContext(INamedTypeSymbol symbol)
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
        var context = new ClassContext {
            Name = symbol.ToDisplayString(DisplayFormat),
            InterfaceName = InterfaceName(symbol.Name),
            WrapperName = WrapperName(symbol.Name),
            IsAbstract = symbol.IsAbstract,

            // --- Inheritance --- //
            BaseName = baseName,
            BaseInterface = baseInterfaceName,
            BaseWrapperName = baseWrapperName,

            // --- Members --- //
            Members = GetMembers(symbol),
        };

        return context;
    }

    // --- Private Helper Methods --- //
    private ImmutableList<IMemberContext> GetMembers(INamedTypeSymbol symbol)
    {
        var members = new List<IMemberContext>();

        // We INCLUDE shadowed properties here so the generator can decide to use 'new'
        var rawMembers = symbol.GetMembers()
           .Where(m => m.ContainingType.Equals(symbol, SymbolEqualityComparer.Default)
                    && m.DeclaredAccessibility == Accessibility.Public
                    && !m.IsStatic
                    && !m.IsImplicitlyDeclared);

        foreach (var member in rawMembers) {
            
            // 1. Handle Methods
            if (member is IMethodSymbol method && method.MethodKind == MethodKind.Ordinary) {
                string args = string.Join(", ", method.Parameters.Select(p =>
                    $"{p.Type.ToDisplayString(DisplayFormat)} {p.Name}"));

                // Signature helps in generating the interface method declaration
                string signature = $"({args})";

                members.Add(new MethodContext(
                    Name: method.Name,
                    Symbol: method.ReturnType.ToDisplayString(DisplayFormat),
                    Arguments: args,
                    Signature: signature
                ));
                continue;
            }

            // 2. Skip if not a property
            if (member is not IPropertySymbol) { continue; }

            var property = member as IPropertySymbol;

            // 3. Complex Property
            if (_namedTypes.IsContained(property.Type)) {
                var typeSymbol = (INamedTypeSymbol)property.Type;
                members.Add(new ComplexPropertyContext(
                    Name: property.Name,
                    Symbol: property.Type.ToDisplayString(DisplayFormat),
                    WrapperName: WrapperName(typeSymbol.Name),
                    InterfaceName: InterfaceName(typeSymbol.Name)
                ));

            }

            // 4. Collection of Wrappable Types (Generic Match)
            // Checks if it is generic (e.g. IEnumerable<T>) and T is a known type
            else if (property.Type is INamedTypeSymbol genericType
                    && genericType.IsGenericType
                    && genericType.TypeArguments.Length == 1
                    && genericType.TypeArguments[0] is INamedTypeSymbol innerType
                    && _namedTypes.IsContained(innerType)) {
                // Extract the container name (e.g., "global::System.Collections.Generic.IEnumerable")
                // We strip off the existing <T> part from the OriginalDefinition display string
                string containerFull = genericType.OriginalDefinition.ToDisplayString(DisplayFormat);
                string containerName = containerFull.Split('<')[0];

                string innerWrapper = WrapperName(innerType.Name);
                string innerInterface = InterfaceName(innerType.Name);

                members.Add(new CollectionPropertyContext(
                    Name: property.Name,
                    Symbol: property.Type.ToDisplayString(DisplayFormat),
                    InnerType: innerType.ToDisplayString(DisplayFormat),
                    WrapperName: $"{containerName}<{innerWrapper}>",
                    InterfaceName: $"{containerName}<{innerInterface}>",
                    WrapperItemName: innerWrapper,
                    InterfaceItemName: innerInterface
                ));
            } 
            
            // 5. Simple Property
            else {
                // Simple Property
                members.Add(new SimplePropertyContext(
                    Name: property.Name,
                    Symbol: property.Type.ToDisplayString(DisplayFormat)
                ));
            }
            
        }

        return members.ToImmutableList();
    }
}
