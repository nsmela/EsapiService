
namespace EsapiService.Generators.Contexts;

// A simple, immutable container for shared configuration
public class CompilationSettings {
    public NamespaceCollection NamedTypes { get; }
    public INamingStrategy Naming { get; }

    // You can add other global settings here later without breaking signatures
    // e.g., public bool IncludeObsoleteMembers { get; }
    // e.g., public SymbolDisplayFormat Format { get; }

    public CompilationSettings(NamespaceCollection namedTypes, INamingStrategy naming) {
        NamedTypes = namedTypes;
        Naming = naming ?? new DefaultNamingStrategy();
    }
}
