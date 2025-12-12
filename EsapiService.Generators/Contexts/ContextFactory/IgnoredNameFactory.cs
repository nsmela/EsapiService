using Microsoft.CodeAnalysis;

namespace EsapiService.Generators.Contexts.ContextFactory;
public class IgnoredNameFactory : IMemberContextFactory {
    private static readonly HashSet<string> _ignored = new() { "GetEnumerator", "ToString", "Equals" };

    public IEnumerable<IMemberContext> Create(ISymbol symbol, CompilationSettings settings) {
        if (_ignored.Contains(symbol.Name)) {
            yield return new SkippedMemberContext(symbol.Name, "Explicitly Ignored Name");
        }
    }
}
