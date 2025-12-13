using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using System.Collections.Immutable;
using EsapiService.Generators.Contexts.ContextFactory;

namespace EsapiService.Generators.Contexts;

public interface IContextService {
    ClassContext BuildContext(INamedTypeSymbol symbol);
}

public class ContextService : IContextService {
    private readonly CompilationSettings _settings;
    private readonly ImmutableList<IMemberContextFactory> _factories;

    public ContextService(NamespaceCollection namedTypes, INamingStrategy namingStrategy = null) {
        // 1. Setup the Environment
        // (If no naming strategy is provided, use the default)
        _settings = new CompilationSettings(namedTypes, namingStrategy ?? new DefaultNamingStrategy());

        // 2. Define the Pipeline (Order is critical!)
        _factories = ImmutableList.Create<IMemberContextFactory>(
            // --- Level 1: Guards (Fail Fast) ---
            new IgnoredNameFactory(),          // Explicitly ignored names (ToString, GetHashCode)
            new UnknownTypeFactory(),          // Members using types we don't know/wrap

            // --- Level 2: Specialized Members ---
            new IndexerFactory(),              // this[]
            new OutParameterMethodFactory(),   // Methods with 'out' or 'ref'

            // --- Level 3: Collections ---
            new ComplexCollectionMethodFactory(), // IEnumerable<PlanSetup> GetPlans()
            new SimpleCollectionMethodFactory(),  // IEnumerable<string> GetHistory()
            new CollectionPropertyFactory(),      // IEnumerable<Structure> Structures { get; }
            new SimpleCollectionPropertyFactory(),// IEnumerable<double> DoseValues { get; }

            // --- Level 4: Core Wrapped Types ---
            new ComplexMethodFactory(),        // PlanSetup GetPlan()
            new ComplexPropertyFactory(),      // Course Course { get; }

            // --- Level 5: Fallbacks (Primitives & Void) ---
            new VoidMethodFactory(),           // void Calculate()
            new SimpleMethodFactory(),         // string GetId()
            new SimplePropertyFactory()        // string Id { get; }
        );
    }

    public ClassContext BuildContext(INamedTypeSymbol symbol) {
        // 1. Resolve Inheritance
        // Find the nearest base class that IS in our whitelist
        INamedTypeSymbol baseSymbol = symbol.BaseType;
        while (baseSymbol != null &&
               !_settings.NamedTypes.IsContained(baseSymbol) &&
               baseSymbol.SpecialType != SpecialType.System_Object) {
            baseSymbol = baseSymbol.BaseType;
        }

        string baseName = string.Empty;
        string baseInterfaceName = string.Empty;
        string baseWrapperName = string.Empty;

        if (baseSymbol != null
                && baseSymbol.SpecialType != SpecialType.System_Object
                && _settings.NamedTypes.IsContained(baseSymbol)) {
            baseName = baseSymbol.ToDisplayString(_settings.Naming.DisplayFormat);
            baseInterfaceName = _settings.Naming.GetInterfaceName(baseSymbol.Name);
            baseWrapperName = _settings.Naming.GetWrapperName(baseSymbol.Name);
        }

        // 2. Process Nested Types (Recursion)
        var nestedContexts = symbol.GetTypeMembers()
            .Where(t => t.DeclaredAccessibility == Accessibility.Public)
            .Select(t => BuildContext(t))
            .ToImmutableList();

        // 3. Check for special string conversion
        bool hasImplicitString = symbol.GetMembers()
            .OfType<IMethodSymbol>()
            .Any(m => m.MethodKind == MethodKind.Conversion &&
                      m.Name == "op_Implicit" &&
                      m.ReturnType.SpecialType == SpecialType.System_String);

        // 4. Get Members (The Pipeline Run)
        var (validMembers, skippedMembers) = GetMemberContexts(symbol);

        // 5. Construct Context
        return new ClassContext {
            Name = symbol.ToDisplayString(_settings.Naming.DisplayFormat),
            InterfaceName = _settings.Naming.GetInterfaceName(symbol.Name),
            WrapperName = _settings.Naming.GetWrapperName(symbol.Name),
            IsAbstract = symbol.IsAbstract,
            IsSealed = symbol.IsSealed,
            BaseName = baseName,
            BaseInterface = baseInterfaceName,
            BaseWrapperName = baseWrapperName,
            Members = validMembers,
            SkippedMembers = skippedMembers, // Ensure ClassContext has this property!
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

    private (ImmutableList<IMemberContext> Valid, ImmutableList<SkippedMemberContext> Skipped) GetMemberContexts(INamedTypeSymbol symbol) {
        var validMembers = new List<IMemberContext>();
        var skippedMembers = new List<SkippedMemberContext>();

        // Determine shadowed members to avoid duplication if base is wrapped
        // Recursively check ALL wrapped base classes, not just the immediate parent.
        var baseMemberNames = new HashSet<string>();
        var currentBase = symbol.BaseType;

        while (currentBase != null && _settings.NamedTypes.IsContained(currentBase)) {
            foreach (var m in currentBase.GetMembers()) {
                if (m.DeclaredAccessibility == Accessibility.Public && !m.IsStatic) {
                    baseMemberNames.Add(m.Name);
                }
            }
            currentBase = currentBase.BaseType;
        }

        // Filter raw Roslyn symbols
        var rawMembers = symbol.GetMembers()
            .Where(m => m.ContainingType.Equals(symbol, SymbolEqualityComparer.Default)
                    && m.DeclaredAccessibility == Accessibility.Public
                    && !m.IsStatic
                    && !m.IsImplicitlyDeclared
                    && !m.GetAttributes().Any(a => a.AttributeClass?.Name == "ObsoleteAttribute"
                                                || a.AttributeClass?.Name == "Obsolete")
                    && !(m is IMethodSymbol method 
                        && (method.MethodKind == MethodKind.PropertyGet
                        || method.MethodKind == MethodKind.PropertySet)));

        foreach (var member in rawMembers) {
            // Basic overriding check
            if (member.IsOverride) continue;

            // Shadowing check for Properties:
            // If a base wrapper already defines this property (e.g. Id, Name), we skip generating it here.
            // This prevents "hides inherited member" warnings and ensures we use the base implementation.
            if (member is IPropertySymbol && baseMemberNames.Contains(member.Name)) {
                skippedMembers.Add(new SkippedMemberContext(member.Name, "Shadows member in wrapped base class"));
                continue;
            }

            // --- THE PIPELINE ---
            // Find the first factory that can create a context for this member
            var context = _factories
                .SelectMany(f => f.Create(member, _settings))
                .FirstOrDefault();

            if (context != null) {
                if (context is SkippedMemberContext skipped) {
                    skippedMembers.Add(skipped);
                }
                else {
                    validMembers.Add(context);
                }
            }
            else {
                // If NO factory claimed it, it's an unhandled edge case
                skippedMembers.Add(new SkippedMemberContext(member.Name, "No matching factory found (Not Implemented)"));
            }
        }

        return (validMembers.ToImmutableList(), skippedMembers.ToImmutableList());
    }
}
