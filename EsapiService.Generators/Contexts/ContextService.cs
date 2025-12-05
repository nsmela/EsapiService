using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using System.Collections.Immutable;

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

            // --- XML Documentation --- //
            XmlDocumentation = symbol.GetDocumentationCommentXml(),
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

            // XML setup
            var xmlDocs = member.GetDocumentationCommentXml() ?? string.Empty;
            // 1. Handle Methods
            if (member is IMethodSymbol method && method.MethodKind == MethodKind.Ordinary) {
                // Prepare common strings
                string args = string.Join(", ", method.Parameters.Select(p =>
                    $"{p.Type.ToDisplayString(DisplayFormat)} {p.Name}"));
                string signature = $"({args})";
                string callArgs = string.Join(", ", method.Parameters.Select(p => p.Name));

                // A. Void
                if (method.ReturnsVoid) {
                    members.Add(new VoidMethodContext(
                        Name: method.Name,
                        Symbol: method.ReturnType.ToDisplayString(DisplayFormat), 
                        XmlDocumentation: xmlDocs,
                        Signature: signature, 
                        CallParameters: callArgs));
                    continue;
                }
                // B. Complex Return (Wrappable Varian Type)
                else if (method.ReturnType is INamedTypeSymbol returnType && _namedTypes.IsContained(returnType)) {
                    members.Add(new ComplexMethodContext(
                        Name: method.Name,
                        Symbol: method.ReturnType.ToDisplayString(DisplayFormat),
                        XmlDocumentation: xmlDocs,
                        WrapperName: WrapperName(returnType.Name),
                        InterfaceName: InterfaceName(returnType.Name),
                        Signature: signature,
                        CallParameters: callArgs
                    ));
                    continue;
                }
                // C. Collection Return (IEnumerable<T>)
                else if (method.ReturnType is INamedTypeSymbol genericRet
                         && genericRet.IsGenericType
                         && genericRet.TypeArguments.Length == 1) {
                    var innerType = genericRet.TypeArguments[0];
                    string containerName = "System.Collections.Generic.IReadOnlyList";

                    // C1. Complex Collection (IEnumerable<Structure>)
                    if (innerType is INamedTypeSymbol namedInner && _namedTypes.IsContained(namedInner)) {
                        string innerWrapper = WrapperName(namedInner.Name);
                        string innerInterface = InterfaceName(namedInner.Name);

                        members.Add(new ComplexCollectionMethodContext(
                            Name: method.Name,
                            Symbol: method.ReturnType.ToDisplayString(DisplayFormat),
                            XmlDocumentation: xmlDocs,
                            InterfaceName: $"{containerName}<{innerInterface}>",
                            WrapperItemName: innerWrapper,
                            Signature: signature,
                            CallParameters: callArgs
                        ));
                        continue;
                    }
                    // C2. Simple Collection (IEnumerable<string>)
                    else {
                        string innerTypeName = innerType.ToDisplayString(DisplayFormat);
                        members.Add(new SimpleCollectionMethodContext(
                            Name: method.Name,
                            Symbol: method.ReturnType.ToDisplayString(DisplayFormat),
                            XmlDocumentation: xmlDocs,
                            InterfaceName: $"{containerName}<{innerTypeName}>",
                            Signature: signature,
                            CallParameters: callArgs
                        ));
                        continue;
                    }
                }

                // D. Out or Ref input parameters
                if (method.Parameters.Any(p => p.RefKind == RefKind.Out || p.RefKind == RefKind.Ref)) {
                    var parameters = method.Parameters
                        .Select(CreateParameterContext)
                        .ToImmutableList();

                    // Calculate Tuple Signature
                    // Format: (ResultType Result, OutType1 Name1, OutType2 Name2)
                    var tupleParts = new List<string>();

                    // Part A: The actual return value (if not void)
                    if (!method.ReturnsVoid) {
                        string retType = method.ReturnType.ToDisplayString(DisplayFormat);
                        // Note: You might need wrapping logic here too for the return type
                        tupleParts.Add($"{retType} Result");
                    }

                    // Part B: The 'out' parameters
                    foreach (var outParam in parameters.Where(x => x.IsOut || x.IsRef)) {
                        // We use the InterfaceType because the user wants the Wrapped version back
                        tupleParts.Add($"{outParam.InterfaceType} {outParam.Name}");
                    }

                    string tupleSignature = $"({string.Join(", ", tupleParts)})";

                    members.Add(new OutParameterMethodContext(
                        Name: method.Name,
                        ReturnsVoid: method.ReturnsVoid,
                        Symbol: method.ReturnType.ToDisplayString(DisplayFormat),
                        XmlDocumentation: xmlDocs,
                        OriginalReturnType: method.ReturnType.ToDisplayString(DisplayFormat),
                        Parameters: parameters,
                        ReturnTupleSignature: tupleSignature
                    ));
                    continue;
                }

                // D. Simple Return (int, string, double)
                else {
                    members.Add(new SimpleMethodContext(
                        Name: method.Name,
                        Symbol: method.ReturnType.ToDisplayString(DisplayFormat),
                        XmlDocumentation: xmlDocs,
                        ReturnType: method.ReturnType.ToDisplayString(DisplayFormat),
                        Signature: signature,
                        CallParameters: callArgs
                    ));
                }
            }

            // 2. Skip if not a property
            if (member is not IPropertySymbol) { continue; }

            var property = member as IPropertySymbol;

            // 3. Complex Property
            if (property.Type is INamedTypeSymbol namedType && _namedTypes.IsContained(namedType)) {
                // Check for Setter
                bool isReadOnly = property.SetMethod is null ||
                                  property.SetMethod.DeclaredAccessibility != Accessibility.Public;

                members.Add(new ComplexPropertyContext(
                    Name: property.Name,
                    Symbol: property.Type.ToDisplayString(DisplayFormat),
                    XmlDocumentation: xmlDocs,
                    WrapperName: WrapperName(namedType.Name),
                    InterfaceName: InterfaceName(namedType.Name),
                    IsReadOnly: isReadOnly
                ));
            }

                        // 4. Collection of Wrappable Types (Generic Match)
                        // Checks if it is generic (e.g. IEnumerable<T>) and T is a known type
                        else if (property.Type is INamedTypeSymbol genericType
                    && genericType.IsGenericType
                    && genericType.TypeArguments.Length == 1) 
            {
                var innerTypeSymbol = genericType.TypeArguments[0];
                string containerName = "System.Collections.Generic.IReadOnlyList";

                // SUB-CASE B1: Complex Collection (Wrapped Inner Type)
                if (innerTypeSymbol is INamedTypeSymbol namedInner && _namedTypes.IsContained(namedInner)) {
                    string innerWrapper = WrapperName(namedInner.Name);
                    string innerInterface = InterfaceName(namedInner.Name);

                    members.Add(new CollectionPropertyContext(
                        Name: property.Name,
                        Symbol: property.Type.ToDisplayString(DisplayFormat),
                        XmlDocumentation: xmlDocs,
                        InnerType: namedInner.ToDisplayString(DisplayFormat),
                        WrapperName: $"{containerName}<{innerWrapper}>",
                        InterfaceName: $"{containerName}<{innerInterface}>",
                        WrapperItemName: innerWrapper,
                        InterfaceItemName: innerInterface
                    ));
                }
                // SUB-CASE B2: Simple Collection (Primitive Inner Type)
                else {
                    // e.g. string, int, double
                    string innerTypeName = innerTypeSymbol.ToDisplayString(DisplayFormat);

                    members.Add(new SimpleCollectionPropertyContext(
                        Name: property.Name,
                        Symbol: property.Type.ToDisplayString(DisplayFormat),
                        XmlDocumentation: xmlDocs,
                        InnerType: innerTypeName,
                        WrapperName: $"{containerName}<{innerTypeName}>",
                        InterfaceName: $"{containerName}<{innerTypeName}>"
                    ));
                }
            } 
            
            // 5. Simple Property
            else {

                // Check if property is ReadOnly (no setter, or private setter)
                bool isReadOnly = property.SetMethod is null ||
                                  property.SetMethod.DeclaredAccessibility != Accessibility.Public;

                members.Add(new SimplePropertyContext(
                    Name: property.Name,
                    Symbol: property.Type.ToDisplayString(DisplayFormat),
                    XmlDocumentation: xmlDocs,
                    IsReadOnly: isReadOnly
                ));
            }
            
        }

        return members.ToImmutableList();
    }

    private ParameterContext CreateParameterContext(IParameterSymbol p) {
        var type = p.Type;

        // Check if the type itself is one of our Wrappable types
        bool isWrappable = type is INamedTypeSymbol named && _namedTypes.IsContained(named);

        string typeName = type.ToDisplayString(DisplayFormat);
        string interfaceType = typeName;
        string wrapperType = "";

        if (isWrappable && type is INamedTypeSymbol nts) {
            interfaceType = InterfaceName(nts.Name);
            wrapperType = WrapperName(nts.Name);
        }
        // Note: If you want to force IReadOnlyList for 'out List<string>', add that logic here.
        // For now, we follow your example: out List<string> -> List<string>

        return new ParameterContext(
            Name: p.Name,
            Type: typeName,
            InterfaceType: interfaceType,
            WrapperType: wrapperType,
            IsWrappable: isWrappable,
            IsOut: p.RefKind == RefKind.Out,
            IsRef: p.RefKind == RefKind.Ref
        );
    }
}
