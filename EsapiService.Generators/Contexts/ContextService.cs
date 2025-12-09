using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using System.Collections.Immutable;
using System.Reflection.Emit;

namespace EsapiService.Generators.Contexts;

public interface IContextService
{
    ClassContext BuildContext(INamedTypeSymbol symbol);
}

public class ContextService : IContextService
{
    private readonly NamespaceCollection _namedTypes;

    // --- Filter Configuration --- //
    // Members to explicitly ignore by Name (e.g. Methods or Properties)
    private static readonly HashSet<string> _ignoredMembers = new()
    {
        "GetEnumerator"
    };

    // Types to explicitly ignore when encountered as Property Return Types
    private static readonly HashSet<string> _ignoredTypes = new()
    {
        "CalibrationProtocolStatus",
        "Department"
    };

    private static SymbolDisplayFormat DisplayFormat => 
        SymbolDisplayFormat
            .FullyQualifiedFormat
            .WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.Omitted);

    private string InterfaceName(string name) => $"I{name}";
    private string WrapperName(string name) => $"Async{name}";

    public ContextService(NamespaceCollection namedTypes)
    {
        _namedTypes = namedTypes;
    }

    public ClassContext BuildContext(INamedTypeSymbol symbol)
    {
        // --- Inheritance --- //
        INamedTypeSymbol baseSymbol = symbol.BaseType;

        // If the immediate base (e.g. HiddenIntermediate) is NOT in our generation list,
        // keep checking its base until we find one that IS in the list (e.g. ApiDataObject)
        // or we hit System.Object.
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
            baseInterfaceName = InterfaceName(baseSymbol.Name);
            baseWrapperName = WrapperName(baseSymbol.Name);
        }

        // --- Detect Nested Types --- //
        // Recursively build context for public nested types
        var nestedContexts = symbol.GetTypeMembers()
            .Where(t => t.DeclaredAccessibility == Accessibility.Public)
            .Select(t => BuildContext(t))
            .ToImmutableList();

        // --- Detect Implicit Conversions --- //
        // Check if the type has implicit operator string()
        bool hasImplicitString = symbol.GetMembers()
            .OfType<IMethodSymbol>()
            .Any(m => m.MethodKind == MethodKind.Conversion &&
                      m.Name == "op_Implicit" &&
                      m.ReturnType.SpecialType == SpecialType.System_String);

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

            // --- Enum stuff --- //
            IsEnum = symbol.TypeKind == TypeKind.Enum,
            EnumMembers = symbol.TypeKind == TypeKind.Enum
                ? symbol.GetMembers().OfType<IFieldSymbol>().Select(f => f.Name).ToList()
                : new List<string>(),

            // --- Struct stuff --- //
            IsStruct = symbol.IsValueType, // Capture if it's a struct
            NestedTypes = nestedContexts, // Assign nested types
            HasImplicitStringConversion = hasImplicitString,
        };

        return context;
    }

    // --- Private Helper Methods --- //
    private ImmutableList<IMemberContext> GetMembers(INamedTypeSymbol symbol) {
        var members = new List<IMemberContext>();

        // 1. Check inheritance
        bool isBaseWrapped = symbol.BaseType is not null && _namedTypes.IsContained(symbol.BaseType);

        // Collect names of public members in the base type to detect shadowing ('new' keyword)
        var baseMemberNames = isBaseWrapped
            ? symbol.BaseType.GetMembers()
                  .Where(m => m.DeclaredAccessibility == Accessibility.Public && !m.IsStatic)
                  .Select(m => m.Name)
                  .ToHashSet()
            : new HashSet<string>();

        // FETCH
        var rawMembers = symbol.GetMembers()
            .Where(m => m.ContainingType.Equals(symbol, SymbolEqualityComparer.Default)
                && m.DeclaredAccessibility == Accessibility.Public
                && !m.IsStatic
                && !m.IsImplicitlyDeclared
                && !m.GetAttributes().Any(a => a.AttributeClass?.Name == "ObsoleteAttribute"));

        // FILTER
        var filteredMembers = FilterAndCanonicalizeMembers(rawMembers);

        foreach (var member in filteredMembers) {
            // 1. FILTERS
            if (member.IsOverride) continue;
            if (member.DeclaredAccessibility != Accessibility.Public || member.IsStatic) { continue; }
            if (member is IPropertySymbol && baseMemberNames.Contains(member.Name)) { continue; }
            if (_ignoredMembers.Contains(member.Name)) { continue; }

            string xmlDocs = member.GetDocumentationCommentXml(expandIncludes: true) 
                ?? string.Empty;

            // --- FIELDS (Capture 'public string Name;' as a property) ---
            if (member is IFieldSymbol field) {
                // We treat public fields as Simple Properties for mocking purposes.
                // This ensures 'Algorithm.Name' exists in the generated mock.
                string typeName = SimplifyTypeString(field.Type.ToDisplayString(DisplayFormat));

                // Fields are read-only if they are 'readonly' or 'const'
                bool isReadOnly = field.IsReadOnly || field.IsConst;

                // For now, fields are always treated as Simple Types (no wrapping for fields in these structs)
                members.Add(new SimplePropertyContext(field.Name, typeName, "", isReadOnly));
                continue;
            }

            // --- METHODS ---
            if (member is IMethodSymbol method && method.MethodKind == MethodKind.Ordinary) {
                // 1. Always create the parameter contexts first
                var parameters = method
                    .Parameters
                    .Select(CreateParameterContext) // (p => CreateParameterContext(p))
                    .ToImmutableList();

                // Check for ref/out parameters
                if (parameters.Any(p => p.IsRef || p.IsOut)) {
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
                string args = string.Join(", ", parameters.Select(p =>
                    $"{p.InterfaceType} {p.Name}"));

                string signature = $"({args})";

                // Build Original Signature
                // We use the raw type string (simplified) instead of the Interface lookup
                string originalArgs = string.Join(", ", method.Parameters.Select(p =>
                    $"{SimplifyTypeString(p.Type.ToDisplayString(DisplayFormat))} {p.Name}"));
                string originalSignature = $"({originalArgs})";

                string callArgs = string.Join(", ", method.Parameters.Select(p => p.Name));

                if (method.ReturnsVoid) {
                    members.Add(new VoidMethodContext(method.Name, method.ReturnType.ToDisplayString(DisplayFormat), xmlDocs, signature, originalSignature, callArgs, parameters));
                } else if (method.ReturnType is INamedTypeSymbol retType && _namedTypes.IsContained(retType)) {
                    members.Add(new ComplexMethodContext(
                        Name: method.Name,
                        Symbol: method.ReturnType.ToDisplayString(DisplayFormat),
                        WrapperName: WrapperName(retType.Name),
                        InterfaceName: InterfaceName(retType.Name),
                        Signature: signature,
                        OriginalSignature: originalSignature,
                        CallParameters: callArgs,
                        Parameters: parameters,
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
                            OriginalSignature: originalSignature,
                            CallParameters: callArgs,
                            Parameters: parameters,
                            XmlDocumentation: xmlDocs
                        ));
                    } else {
                        members.Add(new SimpleCollectionMethodContext(
                            Name: method.Name,
                            Symbol: method.ReturnType.ToDisplayString(DisplayFormat),
                            InterfaceName: $"{containerName}<{SimplifyTypeString(inner.ToDisplayString(DisplayFormat))}>",
                            Signature: signature,
                            OriginalSignature: originalSignature,
                            CallParameters: callArgs,
                            Parameters: parameters,
                            XmlDocumentation: xmlDocs
                        ));
                    }
                } else {
                    members.Add(new SimpleMethodContext(
                        Name: method.Name,
                        Symbol: method.ReturnType.ToDisplayString(DisplayFormat),
                        ReturnType: SimplifyTypeString(method.ReturnType.ToDisplayString(DisplayFormat)),
                        Signature: signature,
                        OriginalSignature: originalSignature,
                        CallParameters: callArgs,
                        Parameters: parameters,
                        XmlDocumentation: xmlDocs
                    ));
                }
                continue;
            }

            // --- PROPERTIES ---
            if (member is IPropertySymbol property) {
                var typeSymbol = property.Type;

                if (typeSymbol.Name == "CalibrationProtocolStatus") {
                    continue;
                }

                // Unwrap collection/nullable to find the "real" type
                if (typeSymbol is INamedTypeSymbol nts && nts.IsGenericType && nts.TypeArguments.Length > 0) {
                    typeSymbol = nts.TypeArguments[0];
                }

                if (typeSymbol.ContainingNamespace?.ToDisplayString() == _namedTypes.NamespaceName
                    && typeSymbol is INamedTypeSymbol namedSym
                    && !_namedTypes.IsContained(namedSym)) {
                    // EXCEPTION TRAP
                    throw new Exception(
                        $"CRITICAL ERROR: Found property '{symbol.Name}.{property.Name}' " +
                        $"of type '{typeSymbol.Name}'. " +
                        $"This type belongs to '{_namedTypes.NamespaceName}' but was NOT included in the generation list. " +
                        $"\nDebug Info for '{typeSymbol.Name}': " +
                        $"\n - TypeKind: {typeSymbol.TypeKind}" +
                        $"\n - Accessibility: {typeSymbol.DeclaredAccessibility}" +
                        $"\n - IsStatic: {typeSymbol.IsStatic}" +
                        $"\n - IsAbstract: {typeSymbol.IsAbstract}" +
                        $"\n - ContainingAssembly: {typeSymbol.ContainingAssembly?.Name}"
                    );
                }
                // ------------------------------------------

                // Check Write Accessibility
                bool isReadOnly = property.SetMethod is null ||
                                  property.SetMethod.DeclaredAccessibility != Accessibility.Public;

                bool isNullable = property.Type.Name == "Nullable" &&
                                  property.Type.ContainingNamespace?.ToDisplayString() == "System";

                // A. Collections
                if (property.Type is INamedTypeSymbol genericType
                        && genericType.IsGenericType
                        && genericType.TypeArguments.Length == 1
                        && !isNullable) {

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
        // 1. Basic Namespace Cleanup
        string s = typeName
            .Replace("global::", "")
            .Replace("System.Collections.Generic.", "")
            .Replace("System.Threading.Tasks.", "")
            .Replace("VMS.TPS.Common.Model.API.", "")
            .Replace("VMS.TPS.Common.Model.Types.", "")
            .Replace("VMS.TPS.Common.Model.", "");

        // 2. Handle Nullable Syntax Sugar (System.Nullable<T> -> T?)
        // e.g. "System.Nullable<System.DateTime>" -> "System.DateTime?"
        if (s.StartsWith("System.Nullable<") && s.EndsWith(">")) {
            // Remove "System.Nullable<" (16 chars) and ">" (1 char)
            s = s.Substring(16, s.Length - 17) + "?";
        }

        // 3. Safe System Type Simplification (Allow List)
        // We only strip 'System.' from types we KNOW are safe, preserving 'System.Windows', 'System.IO', etc.
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

    private IEnumerable<ISymbol> FilterAndCanonicalizeMembers(IEnumerable<ISymbol> members) {
        var distinctMembers = new List<ISymbol>();
        var processedSignatures = new HashSet<string>();

        foreach (var member in members) {
            // 1. Filter Obsolete members immediately
            if (member.GetAttributes().Any(a => a.AttributeClass?.Name == "ObsoleteAttribute" || a.AttributeClass?.Name == "Obsolete")) {
                continue;
            }

            string signature;

            if (member is IPropertySymbol property) {
                if (property.IsIndexer) {
                    // Indexer Signature: P_this[]_int_string
                    // We must include parameters to distinguish between this[int] and this[string]
                    var paramSig = string.Join("_", property.Parameters.Select(p => SimplifyTypeString(p.Type.ToDisplayString(DisplayFormat))));
                    signature = $"P_{property.Name}_{paramSig}";
                } else {
                    // Standard Property: P_Id
                    signature = $"P_{property.Name}";
                }
            } else if (member is IMethodSymbol method) {
                // Method Signature: M_Calculate_int_double
                // Uses SimplifyTypeString to ensure 'System.Int32' and 'int' match
                var paramSig = string.Join("_", method.Parameters.Select(p => SimplifyTypeString(p.Type.ToDisplayString(DisplayFormat))));

                // Include Arity (number of generic types) to distinguish method<T> from method()
                signature = $"M_{method.Name}_{paramSig}_{method.TypeParameters.Length}";
            } else if (member is IFieldSymbol field) {
                // Field Signature: F_ConstantValue
                signature = $"F_{field.Name}";
            } else {
                // Skip events or other symbol types
                continue;
            }

            // 2. Canonicalization Check
            // If we haven't seen this signature before, add it.
            // If we HAVE seen it, this is likely a shadowed member (new) or duplicate.
            if (processedSignatures.Add(signature)) {
                distinctMembers.Add(member);
            }
        }

        return distinctMembers;
    }
}
