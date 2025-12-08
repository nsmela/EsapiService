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
    private ImmutableList<IMemberContext> GetMembers(INamedTypeSymbol symbol) {
        var members = new List<IMemberContext>();

        // 1. Check inheritance
        bool isBaseWrapped = symbol.BaseType is not null && _namedTypes.IsContained(symbol.BaseType);

        // FIX: Collect names of public members in the base type to detect shadowing ('new' keyword)
        var baseMemberNames = isBaseWrapped
            ? symbol.BaseType.GetMembers()
                  .Where(m => m.DeclaredAccessibility == Accessibility.Public && !m.IsStatic)
                  .Select(m => m.Name)
                  .ToHashSet()
            : new HashSet<string>();

        var rawMembers = symbol.GetMembers()
            .Where(m => m.ContainingType.Equals(symbol, SymbolEqualityComparer.Default)
                    && m.DeclaredAccessibility == Accessibility.Public
                    && !m.IsStatic
                    && !m.IsImplicitlyDeclared);

        foreach (var member in rawMembers) {
            // 2. EXCLUDE OVERRIDES / SHADOWED MEMBERS
            if (isBaseWrapped) {
                // Case A: Explicit Override
                if (member.IsOverride) continue;

                // Case B: Shadowing ('new') - FIX FOR TEST FAILURE
                // If a property with the same name exists in the wrapped base, skip it.
                if (member is IPropertySymbol && baseMemberNames.Contains(member.Name)) {
                    continue;
                }
            }

            string xmlDocs = member.GetDocumentationCommentXml(expandIncludes: true);

            // --- METHODS ---
            if (member is IMethodSymbol method && method.MethodKind == MethodKind.Ordinary) {
                // Check for ref/out parameters
                if (method.Parameters.Any(p => p.RefKind == RefKind.Out || p.RefKind == RefKind.Ref)) {
                    var parameters = method.Parameters.Select(CreateParameterContext).ToImmutableList();
                    var tupleSignature = BuildTupleSignature(method, parameters);

                    // NEW: Determine Return Wrapping
                    string wrapperReturnTypeName = "";
                    bool isReturnWrappable = false;

                    // If it returns a Known Type, we must wrap it
                    if (!method.ReturnsVoid &&
                        method.ReturnType is INamedTypeSymbol retSym &&
                        _namedTypes.IsContained(retSym)) {
                        isReturnWrappable = true;
                        wrapperReturnTypeName = WrapperName(retSym.Name);
                    }

                    members.Add(new OutParameterMethodContext(
                        Name: method.Name,
                        Symbol: method.ReturnType.ToDisplayString(DisplayFormat),
                        OriginalReturnType: SimplifyTypeString(method.ReturnType.ToDisplayString(DisplayFormat)),
                        ReturnsVoid: method.ReturnsVoid,
                        Parameters: parameters,
                        ReturnTupleSignature: tupleSignature,
                        XmlDocumentation: xmlDocs,
                        // NEW assignments
                        WrapperReturnTypeName: wrapperReturnTypeName,
                        IsReturnWrappable: isReturnWrappable
                    ));
                    continue;
                }

                // Standard Methods
                string args = string.Join(", ", method.Parameters.Select(p =>
                    $"{GetParameterTypeString(p)} {p.Name}"));

                string signature = $"({args})";
                string callArgs = string.Join(", ", method.Parameters.Select(p => p.Name));

                if (method.ReturnsVoid) {
                    members.Add(new VoidMethodContext(method.Name, method.ReturnType.ToDisplayString(DisplayFormat), xmlDocs, signature, callArgs));
                } else if (method.ReturnType is INamedTypeSymbol retType && _namedTypes.IsContained(retType)) {
                    members.Add(new ComplexMethodContext(
                        Name: method.Name,
                        Symbol: method.ReturnType.ToDisplayString(DisplayFormat),
                        WrapperName: WrapperName(retType.Name),
                        InterfaceName: InterfaceName(retType.Name),
                        Signature: signature,
                        CallParameters: callArgs,
                        XmlDocumentation: xmlDocs
                    ));
                } else if (method.ReturnType is INamedTypeSymbol genericRet
                           && genericRet.IsGenericType
                           && genericRet.TypeArguments.Length == 1) {
                    var inner = genericRet.TypeArguments[0];
                    string containerName = "IReadOnlyList";

                    if (inner is INamedTypeSymbol innerNamed && _namedTypes.IsContained(innerNamed)) {
                        members.Add(new ComplexCollectionMethodContext(
                            Name: method.Name,
                            Symbol: method.ReturnType.ToDisplayString(DisplayFormat),
                            InterfaceName: $"{containerName}<{InterfaceName(innerNamed.Name)}>",
                            WrapperName: WrapperName(innerNamed.Name),
                            Signature: signature,
                            CallParameters: callArgs,
                            XmlDocumentation: xmlDocs
                        ));
                    } else {
                        members.Add(new SimpleCollectionMethodContext(
                            Name: method.Name,
                            Symbol: method.ReturnType.ToDisplayString(DisplayFormat),
                            InterfaceName: $"{containerName}<{SimplifyTypeString(inner.ToDisplayString(DisplayFormat))}>",
                            Signature: signature,
                            CallParameters: callArgs,
                            XmlDocumentation: xmlDocs
                        ));
                    }
                } else {
                    members.Add(new SimpleMethodContext(
                        Name: method.Name,
                        Symbol: method.ReturnType.ToDisplayString(DisplayFormat),
                        ReturnType: SimplifyTypeString(method.ReturnType.ToDisplayString(DisplayFormat)),
                        Signature: signature,
                        CallParameters: callArgs,
                        XmlDocumentation: xmlDocs
                    ));
                }
                continue;
            }

            // --- PROPERTIES ---
            if (member is IPropertySymbol property) {
                // Check Write Accessibility
                bool isReadOnly = property.SetMethod is null ||
                                  property.SetMethod.DeclaredAccessibility != Accessibility.Public;

                // A. Collections
                if (property.Type is INamedTypeSymbol genericType
                    && genericType.IsGenericType
                    && genericType.TypeArguments.Length == 1) {
                    var inner = genericType.TypeArguments[0];
                    string containerName = "IReadOnlyList";

                    if (inner is INamedTypeSymbol innerNamed && _namedTypes.IsContained(innerNamed)) {
                        string innerInterface = InterfaceName(innerNamed.Name);
                        members.Add(new CollectionPropertyContext(
                            Name: property.Name,
                            Symbol: SimplifyTypeString(property.Type.ToDisplayString(DisplayFormat)),
                            InnerType: SimplifyTypeString(inner.ToDisplayString(DisplayFormat)),
                            WrapperName: $"{containerName}<{WrapperName(innerNamed.Name)}>",
                            InterfaceName: $"{containerName}<{innerInterface}>",
                            WrapperItemName: WrapperName(innerNamed.Name),
                            InterfaceItemName: innerInterface,
                            XmlDocumentation: xmlDocs
                        ));
                    } else {
                        string simpleInner = SimplifyTypeString(inner.ToDisplayString(DisplayFormat));
                        members.Add(new SimpleCollectionPropertyContext(
                            Name: property.Name,
                            Symbol: SimplifyTypeString(property.Type.ToDisplayString(DisplayFormat)),
                            InnerType: simpleInner,
                            WrapperName: $"{containerName}<{simpleInner}>",
                            InterfaceName: $"{containerName}<{simpleInner}>",
                            XmlDocumentation: xmlDocs
                        ));
                    }
                }
                // B. Complex Properties (Wrappers)
                else if (property.Type is INamedTypeSymbol namedType && _namedTypes.IsContained(namedType)) {
                    members.Add(new ComplexPropertyContext(
                        Name: property.Name,
                        Symbol: SimplifyTypeString(property.Type.ToDisplayString(DisplayFormat)),
                        WrapperName: WrapperName(namedType.Name),
                        InterfaceName: InterfaceName(namedType.Name),
                        IsReadOnly: isReadOnly,
                        XmlDocumentation: xmlDocs
                    ));
                }
                // C. Simple Properties
                else {
                    members.Add(new SimplePropertyContext(
                        Name: property.Name,
                        Symbol: SimplifyTypeString(property.Type.ToDisplayString(DisplayFormat)),
                        IsReadOnly: isReadOnly,
                        XmlDocumentation: xmlDocs
                    ));
                }
            }
        }
        return members.ToImmutableList();
    }

    private string GetParameterTypeString(IParameterSymbol p) {
        var ctx = CreateParameterContext(p);
        return ctx.InterfaceType;
    }

    private ParameterContext CreateParameterContext(IParameterSymbol p) {
        var type = p.Type;
        string typeName = SimplifyTypeString(type.ToDisplayString(DisplayFormat));

        // 1. Check Direct Wrapper (Structure -> IStructure)
        if (type is INamedTypeSymbol named && _namedTypes.IsContained(named)) {
            return new ParameterContext(
                Name: p.Name,
                Type: typeName,
                InterfaceType: InterfaceName(named.Name), // IStructure
                WrapperType: WrapperName(named.Name),     // AsyncStructure
                IsWrappable: true,
                IsOut: p.RefKind == RefKind.Out,
                IsRef: p.RefKind == RefKind.Ref
            );
        }

        // 2. Check Collection of Wrappers (List<Structure> -> IReadOnlyList<IStructure>)
        if (type is INamedTypeSymbol generic && generic.IsGenericType && generic.TypeArguments.Length == 1) {
            var inner = generic.TypeArguments[0];
            if (inner is INamedTypeSymbol innerNamed && _namedTypes.IsContained(innerNamed)) {
                string innerInterface = InterfaceName(innerNamed.Name);
                return new ParameterContext(
                   Name: p.Name,
                   Type: typeName,
                   InterfaceType: $"IReadOnlyList<{innerInterface}>", // Interface Signature
                   WrapperType: $"IReadOnlyList<{WrapperName(innerNamed.Name)}>",
                   IsWrappable: true,
                   IsOut: p.RefKind == RefKind.Out,
                   IsRef: p.RefKind == RefKind.Ref
               );
            }
        }

        // 3. Simple / Unknown
        return new ParameterContext(
            Name: p.Name,
            Type: typeName,
            InterfaceType: typeName, // string, int, etc.
            WrapperType: "",
            IsWrappable: false,
            IsOut: p.RefKind == RefKind.Out,
            IsRef: p.RefKind == RefKind.Ref
        );
    }

    // Helper to construct the ValueTuple signature string
    // e.g. "(bool Result, IReadOnlyList<string> messages)"
    private string BuildTupleSignature(IMethodSymbol method, ImmutableList<ParameterContext> parameters) {
        var tupleParts = new List<string>();

        // Part A: The actual return value
        if (!method.ReturnsVoid) {
            string retType = SimplifyTypeString(method.ReturnType.ToDisplayString(DisplayFormat));

            // If the return type is Wrappable, use the Interface name
            if (method.ReturnType is INamedTypeSymbol retSym && _namedTypes.IsContained(retSym)) {
                retType = InterfaceName(retSym.Name);
            }

            tupleParts.Add($"{retType} Result");
        }

        // Part B: The 'out' and 'ref' parameters
        foreach (var p in parameters.Where(x => x.IsOut || x.IsRef)) {
            // We use InterfaceType because the user consumes the Wrapper/Interface
            tupleParts.Add($"{p.InterfaceType} {p.Name}");
        }

        return $"({string.Join(", ", tupleParts)})";
    }

    // Global String Simplifier
    private string SimplifyTypeString(string typeName) {
        return typeName
            .Replace("global::", "")
            .Replace("System.Collections.Generic.", "") // Remove List namespace
            .Replace("System.Threading.Tasks.", "")     // Remove Task namespace
            .Replace("System.", "")                     // Remove Nullable, etc.
            .Replace("VMS.TPS.Common.Model.API.", "")   // Remove Varian API namespace
            .Replace("VMS.TPS.Common.Model.Types.", ""); // Remove Varian Types namespace
    }
}
