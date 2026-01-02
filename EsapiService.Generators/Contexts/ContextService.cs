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

    public ContextService(
        NamespaceCollection namedTypes, 
        CompilationSettings settings = null) {
        // 1. Setup the Environment
        // (If no naming strategy is provided, use the default)
        _settings = settings ?? new CompilationSettings(namedTypes, new DefaultNamingStrategy());

        // 2. Define the Pipeline (Order is critical!)
        _factories = [
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
        ];
    }

    public ClassContext BuildContext(INamedTypeSymbol symbol) {
        // 1. Resolve Inheritance
        // Find the nearest base class that IS in our whitelist
        INamedTypeSymbol? baseSymbol = symbol.BaseType;
        while (baseSymbol is not null 
                && !_settings.NamedTypes.IsContained(baseSymbol) 
                && baseSymbol.SpecialType != SpecialType.System_Object) {
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

    private (ImmutableList<IMemberContext> Valid, ImmutableList<SkippedMemberContext> Skipped) GetMemberContexts(INamedTypeSymbol symbol)
    {
        var validMembers = new List<IMemberContext>();
        var skippedMembers = new List<SkippedMemberContext>();

        // 1. Collect Base Member Names
        // We need to know what exists in the base classes to detect shadowing.
        var baseMemberNames = new HashSet<string>();
        var currentBase = symbol.BaseType;

        // Traverse up the inheritance chain ONLY for classes we are wrapping.
        while (currentBase is not null && _settings.NamedTypes.IsContained(currentBase))
        {
            foreach (var name in currentBase.MemberNames)
            {
                baseMemberNames.Add(name);
            }
            currentBase = currentBase.BaseType;
        }

        // 2. Get Members Declared in THIS Class (Roslyn excludes inherited members automatically)
        var rawMembers = symbol.GetMembers()
            .Where(m => m.ContainingType.Equals(symbol, SymbolEqualityComparer.Default)
                    && m.DeclaredAccessibility == Accessibility.Public
                    && !m.IsImplicitlyDeclared
                    && !m.GetAttributes().Any(a => a.AttributeClass?.Name == "ObsoleteAttribute" || a.AttributeClass?.Name == "Obsolete")
                    && !(m is IMethodSymbol method
                        && (method.MethodKind == MethodKind.PropertyGet || method.MethodKind == MethodKind.PropertySet)));

        foreach (var member in rawMembers)
        {
            // Basic overriding check (we rely on polymorphism, so we skip 'override')
            if (member.IsOverride)
                continue;

            // Detect Shadowing instead of skipping
            // If it is declared here AND exists in base, it is shadowing (hiding) the base.
            bool isShadowing = baseMemberNames.Contains(member.Name);

            // --- THE PIPELINE ---
            var context = _factories
                .SelectMany(f => f.Create(member, _settings))
                .FirstOrDefault();

            if (context is not null)
            {
                if (context is SkippedMemberContext skipped)
                {
                    skippedMembers.Add(skipped);
                } else
                {
                    // Apply the Shadowing Flag
                    // This requires your Context records (SimplePropertyContext, etc.) 
                    // to have a 'bool IsShadowing' property.
                    IMemberContext finalContext = context;                  

                    if (isShadowing)
                    {
                        // Use 'with' expressions to set the flag immutably
                        if (context is SimplePropertyContext spc)
                            finalContext = spc with { IsShadowing = true };
                        else if (context is ComplexPropertyContext cpc)
                            finalContext = cpc with { IsShadowing = true };
                        else if (context is SimpleMethodContext smc)
                            finalContext = smc with { IsShadowing = true };
                        else if (context is ComplexMethodContext cmc)
                            finalContext = cmc with { IsShadowing = true };
                        else if (context is CollectionPropertyContext cp)
                            finalContext = cp with { IsShadowing = true };
                        else if (context is VoidMethodContext vmc)
                            finalContext = vmc with { IsShadowing = true };
                    }

                    validMembers.Add(finalContext);
                }
            } else
            {
                skippedMembers.Add(new SkippedMemberContext(member.Name, "No matching factory found (Not Implemented)"));
            }
        }

        return (validMembers.ToImmutableList(), skippedMembers.ToImmutableList());
    }
}
