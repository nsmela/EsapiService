using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using System.Collections.Immutable;
using System.Collections.Generic;
using System.Linq;

namespace EsapiService.Generators.Contexts;

public interface IContextService {
    ClassContext BuildContext(INamedTypeSymbol symbol);
}

public class ContextService : IContextService {
    private readonly NamespaceCollection _namedTypes;

    // Configuration: Members to always exclude
    private static readonly HashSet<string> _ignoredMembers = new() {
        "GetEnumerator", "Equals", "GetHashCode", "GetType", "ToString"
    };

    private static SymbolDisplayFormat DisplayFormat =>
        SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.Omitted);



    public ContextService(NamespaceCollection namedTypes) {
        _namedTypes = namedTypes;
    }

    public ClassContext BuildContext(INamedTypeSymbol symbol) {
        INamedTypeSymbol baseSymbol = symbol.BaseType;

        while (baseSymbol != null &&
               !_namedTypes.IsContained(baseSymbol) &&
               baseSymbol.SpecialType != SpecialType.System_Object) {
            baseSymbol = baseSymbol.BaseType;
        }

        string baseName = string.Empty;
        string baseInterfaceName = string.Empty;
        string baseWrapperName = string.Empty;

        if (baseSymbol != null
                && baseSymbol.SpecialType != SpecialType.System_Object
                && _namedTypes.IsContained(baseSymbol)) {
            baseName = baseSymbol.ToDisplayString(DisplayFormat);
            baseInterfaceName = NamingConvention.GetInterfaceName(baseSymbol.Name);
            baseWrapperName = NamingConvention.GetWrapperName(baseSymbol.Name);
        }

        var nestedContexts = symbol.GetTypeMembers()
            .Where(t => t.DeclaredAccessibility == Accessibility.Public)
            .Select(t => BuildContext(t))
            .ToImmutableList();

        bool hasImplicitString = symbol.GetMembers()
            .OfType<IMethodSymbol>()
            .Any(m => m.MethodKind == MethodKind.Conversion &&
                      m.Name == "op_Implicit" &&
                      m.ReturnType.SpecialType == SpecialType.System_String);

        return new ClassContext {
            Name = symbol.ToDisplayString(DisplayFormat),
            InterfaceName = NamingConvention.GetInterfaceName(symbol.Name),
            WrapperName = NamingConvention.GetWrapperName(symbol.Name),
            IsAbstract = symbol.IsAbstract,
            BaseName = baseName,
            BaseInterface = baseInterfaceName,
            BaseWrapperName = baseWrapperName,
            Members = GetMembers(symbol),
            XmlDocumentation = symbol.GetDocumentationCommentXml(),
            IsEnum = symbol.TypeKind == TypeKind.Enum,
            EnumMembers = symbol.TypeKind == TypeKind.Enum
                ? symbol.GetMembers().OfType<IFieldSymbol>().Select(f => f.Name).ToList()
                : new List<string>(),
            IsStruct = symbol.IsValueType,
            NestedTypes = nestedContexts,
            HasImplicitStringConversion = hasImplicitString,
        };
    }

    private ImmutableList<IMemberContext> GetMembers(INamedTypeSymbol symbol) {
        var members = new List<IMemberContext>();

        bool isBaseWrapped = symbol.BaseType is not null && _namedTypes.IsContained(symbol.BaseType);

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
                    && !m.IsImplicitlyDeclared
                    // Filter Obsolete members
                    && !m.GetAttributes().Any(a => a.AttributeClass?.Name == "ObsoleteAttribute"
                                                || a.AttributeClass?.Name == "Obsolete"));

        foreach (var member in rawMembers) {
            if (member.IsOverride) continue;
            if (_ignoredMembers.Contains(member.Name)) continue;

            if (isBaseWrapped) {
                if (member.IsOverride) continue;
                if (member is IPropertySymbol && baseMemberNames.Contains(member.Name)) continue;
            }

            // SAFETY CHECK: Skip member if it uses a Varian type we aren't wrapping
            if (UsesUnknownApiType(member)) {
                continue;
            }

            string xmlDocs = member.GetDocumentationCommentXml(expandIncludes: true) ?? string.Empty;

            // --- FIELDS ---
            if (member is IFieldSymbol field) {
                string typeName = SimplifyTypeString(field.Type.ToDisplayString(DisplayFormat));
                bool isReadOnly = field.IsReadOnly || field.IsConst;
                members.Add(new SimplePropertyContext(field.Name, typeName, "", isReadOnly));
                continue;
            }

            // --- METHODS ---
            if (member is IMethodSymbol method && method.MethodKind == MethodKind.Ordinary) {
                var parameters = method.Parameters.Select(CreateParameterContext).ToImmutableList();

                if (parameters.Any(p => p.IsRef || p.IsOut)) {
                    var tupleSignature = BuildTupleSignature(method, parameters);
                    string wrapperReturnTypeName = "";
                    bool isReturnWrappable = false;
                    if (!method.ReturnsVoid && method.ReturnType is INamedTypeSymbol retSym && _namedTypes.IsContained(retSym)) {
                        isReturnWrappable = true;
                        wrapperReturnTypeName = NamingConvention.GetWrapperName(retSym.Name);
                    }

                    members.Add(new OutParameterMethodContext(
                        Name: method.Name,
                        Symbol: method.ReturnType.ToDisplayString(DisplayFormat),
                        OriginalReturnType: SimplifyTypeString(method.ReturnType.ToDisplayString(DisplayFormat)),
                        ReturnsVoid: method.ReturnsVoid,
                        Parameters: parameters,
                        ReturnTupleSignature: tupleSignature,
                        XmlDocumentation: xmlDocs,
                        WrapperReturnTypeName: wrapperReturnTypeName,
                        IsReturnWrappable: isReturnWrappable
                    ));
                    continue;
                }

                string args = string.Join(", ", parameters.Select(p => $"{p.InterfaceType} {p.Name}"));
                string signature = $"({args})";
                string originalArgs = string.Join(", ", method.Parameters.Select(p => $"{SimplifyTypeString(p.Type.ToDisplayString(DisplayFormat))} {p.Name}"));
                string originalSignature = $"({originalArgs})";
                string callArgs = string.Join(", ", method.Parameters.Select(p => p.Name));

                if (method.ReturnsVoid) {
                    members.Add(new VoidMethodContext(method.Name, method.ReturnType.ToDisplayString(DisplayFormat), xmlDocs, signature, originalSignature, callArgs, parameters));
                }
                else if (method.ReturnType is INamedTypeSymbol retType && _namedTypes.IsContained(retType)) {
                    members.Add(new ComplexMethodContext(method.Name, method.ReturnType.ToDisplayString(DisplayFormat), xmlDocs, NamingConvention.GetWrapperName(retType.Name), NamingConvention.GetInterfaceName(retType.Name), signature, originalSignature, callArgs, parameters));
                }
                else if (method.ReturnType is INamedTypeSymbol genericRet && genericRet.IsGenericType && genericRet.TypeArguments.Length == 1) {
                    var inner = genericRet.TypeArguments[0];
                    string containerName = "IReadOnlyList";
                    if (inner is INamedTypeSymbol innerNamed && _namedTypes.IsContained(innerNamed)) {
                        members.Add(new ComplexCollectionMethodContext(method.Name, method.ReturnType.ToDisplayString(DisplayFormat), xmlDocs, $"{containerName}<{NamingConvention.GetInterfaceName(innerNamed.Name)}>", NamingConvention.GetWrapperName(innerNamed.Name), signature, originalSignature, callArgs, parameters));
                    }
                    else {
                        members.Add(new SimpleCollectionMethodContext(method.Name, method.ReturnType.ToDisplayString(DisplayFormat), xmlDocs, $"{containerName}<{SimplifyTypeString(inner.ToDisplayString(DisplayFormat))}>", signature, originalSignature, callArgs, parameters));
                    }
                }
                else {
                    members.Add(new SimpleMethodContext(method.Name, method.ReturnType.ToDisplayString(DisplayFormat), xmlDocs, SimplifyTypeString(method.ReturnType.ToDisplayString(DisplayFormat)), signature, originalSignature, callArgs, parameters));
                }
                continue;
            }

            // --- PROPERTIES ---
            if (member is IPropertySymbol property) {
                var typeSymbol = property.Type;

                // Indexer Support
                if (property.IsIndexer) {
                    var parameters = property.Parameters.Select(CreateParameterContext).ToImmutableList();
                    bool isReadOnly = property.SetMethod is null || property.SetMethod.DeclaredAccessibility != Accessibility.Public;
                    if (typeSymbol is INamedTypeSymbol namedType && _namedTypes.IsContained(namedType)) {
                        members.Add(new IndexerContext(
                           Name: "this[]",
                           Symbol: SimplifyTypeString(property.Type.ToDisplayString(DisplayFormat)),
                           XmlDocumentation: xmlDocs,
                           WrapperName: NamingConvention.GetWrapperName(namedType.Name),
                           InterfaceName: NamingConvention.GetInterfaceName(namedType.Name),
                           Parameters: parameters,
                           IsReadOnly: isReadOnly
                       ));
                    }
                    continue;
                }

                // Unwrap collection/nullable
                if (typeSymbol is INamedTypeSymbol nts && nts.IsGenericType && nts.TypeArguments.Length > 0) {
                    typeSymbol = nts.TypeArguments[0];
                }

                bool isReadOnlyProp = property.SetMethod is null || property.SetMethod.DeclaredAccessibility != Accessibility.Public;
                bool isNullable = property.Type.Name == "Nullable" && property.Type.ContainingNamespace?.ToDisplayString() == "System";

                if (property.Type is INamedTypeSymbol genericType && genericType.IsGenericType && genericType.TypeArguments.Length == 1 && !isNullable) {
                    var inner = genericType.TypeArguments[0];
                    string containerName = "IReadOnlyList";
                    if (inner is INamedTypeSymbol innerNamed && _namedTypes.IsContained(innerNamed)) {
                        members.Add(new CollectionPropertyContext(property.Name, SimplifyTypeString(property.Type.ToDisplayString(DisplayFormat)), xmlDocs, SimplifyTypeString(inner.ToDisplayString(DisplayFormat)), $"{containerName}<{NamingConvention.GetWrapperName(innerNamed.Name)}>", $"{containerName}<{NamingConvention.GetInterfaceName(innerNamed.Name)}>", NamingConvention.GetWrapperName(innerNamed.Name), NamingConvention.GetInterfaceName(innerNamed.Name)));
                    }
                    else {
                        members.Add(new SimpleCollectionPropertyContext(property.Name, SimplifyTypeString(property.Type.ToDisplayString(DisplayFormat)), xmlDocs, SimplifyTypeString(inner.ToDisplayString(DisplayFormat)), $"{containerName}<{SimplifyTypeString(inner.ToDisplayString(DisplayFormat))}>", $"{containerName}<{SimplifyTypeString(inner.ToDisplayString(DisplayFormat))}>"));
                    }
                }
                else if (property.Type is INamedTypeSymbol namedType && _namedTypes.IsContained(namedType)) {
                    members.Add(new ComplexPropertyContext(property.Name, SimplifyTypeString(property.Type.ToDisplayString(DisplayFormat)), xmlDocs, NamingConvention.GetWrapperName(namedType.Name), NamingConvention.GetInterfaceName(namedType.Name), isReadOnlyProp));
                }
                else {
                    members.Add(new SimplePropertyContext(property.Name, SimplifyTypeString(property.Type.ToDisplayString(DisplayFormat)), xmlDocs, isReadOnlyProp));
                }
            }
        }
        return members.ToImmutableList();
    }

    // --- Helpers ---

    // The key logic: Returns true if the member uses a VMS type that we DO NOT have in our whitelist.
    private bool UsesUnknownApiType(ISymbol member) {
        foreach (var type in GetReferencedTypes(member)) {
            var leafType = GetLeafType(type);

            if (leafType is INamedTypeSymbol named &&
                named.ContainingNamespace?.ToDisplayString().StartsWith("VMS.TPS") == true &&
                !_namedTypes.IsContained(named)) {
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

    private ParameterContext CreateParameterContext(IParameterSymbol p) {
        var type = p.Type;
        string typeName = SimplifyTypeString(type.ToDisplayString(DisplayFormat));

        if (type is INamedTypeSymbol named && _namedTypes.IsContained(named)) {
            return new ParameterContext(p.Name, typeName, NamingConvention.GetInterfaceName(named.Name), NamingConvention.GetWrapperName(named.Name), true, p.RefKind == RefKind.Out, p.RefKind == RefKind.Ref);
        }

        if (type is INamedTypeSymbol generic && generic.IsGenericType && generic.TypeArguments.Length == 1) {
            var inner = generic.TypeArguments[0];
            if (inner is INamedTypeSymbol innerNamed && _namedTypes.IsContained(innerNamed)) {
                string innerInterface = NamingConvention.GetInterfaceName(innerNamed.Name);
                return new ParameterContext(p.Name, typeName, $"IReadOnlyList<{innerInterface}>", $"IReadOnlyList<{NamingConvention.GetWrapperName(innerNamed.Name)}>", true, p.RefKind == RefKind.Out, p.RefKind == RefKind.Ref);
            }
        }

        return new ParameterContext(p.Name, typeName, typeName, "", false, p.RefKind == RefKind.Out, p.RefKind == RefKind.Ref);
    }

    private string BuildTupleSignature(IMethodSymbol method, ImmutableList<ParameterContext> parameters) {
        var tupleParts = new List<string>();

        if (!method.ReturnsVoid) {
            string retType = SimplifyTypeString(method.ReturnType.ToDisplayString(DisplayFormat));
            if (method.ReturnType is INamedTypeSymbol retSym && _namedTypes.IsContained(retSym)) {
                retType = NamingConvention.GetInterfaceName(retSym.Name);
            }
            tupleParts.Add($"{retType} Result");
        }

        foreach (var p in parameters.Where(x => x.IsOut || x.IsRef)) {
            tupleParts.Add($"{p.InterfaceType} {p.Name}");
        }

        return $"({string.Join(", ", tupleParts)})";
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