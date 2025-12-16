
using Microsoft.CodeAnalysis;

namespace EsapiService.Generators.Contexts;

// A simple, immutable container for shared configuration
public class CompilationSettings {
    public NamespaceCollection NamedTypes { get; }
    public INamingStrategy Naming { get; }

    // List of types to treat as Complex but Unwrapped
    public HashSet<string> ForceComplexTypes { get; }

    // You can add other global settings here later without breaking signatures
    // e.g., public bool IncludeObsoleteMembers { get; }
    // e.g., public SymbolDisplayFormat Format { get; }

    public CompilationSettings(
        NamespaceCollection namedTypes, 
        INamingStrategy naming,
        IEnumerable<string> forceComplexTypes = null) {
        NamedTypes = namedTypes;
        Naming = naming ?? new DefaultNamingStrategy();
        ForceComplexTypes = new HashSet<string>(forceComplexTypes ?? Enumerable.Empty<string>());
    }

    /// <summary>
    /// Checks if a type is considered "Complex" (either Wrapped or Forced).
    /// </summary>
    public bool IsComplexType(ITypeSymbol type)
    {
        if (type is not INamedTypeSymbol named)
            return false;

        // 1. Is it a Wrapped Type? (e.g. Structure)
        if (NamedTypes.IsContained(named))
            return true;

        // 2. Is it in the Force Complex whitelist? (e.g. MeshGeometry3D)
        // We check the full display name (e.g. System.Windows.Media.Media3D.MeshGeometry3D)
        string displayName = named.ToDisplayString();
        return ForceComplexTypes.Contains(displayName);
    }
}
