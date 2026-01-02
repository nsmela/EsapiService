using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using EsapiService.Generators.Contexts;

namespace EsapiService.Generators.Contexts.ContextFactory {
    public class IgnoredNameFactory : IMemberContextFactory {
        // Honest Configuration: Explicitly define what we ignore and why
        private static readonly HashSet<string> _ignoredNames = new()
        {
            "GetEnumerator",
            "Equals",
            "GetHashCode",
            "GetType",
            "ToString",
            ".ctor", // Constructors are handled separately, not as members
            "op_Implicit",
            "op_Explicit",
            "IsValid",
            "IsNotValid"
        };

        public IEnumerable<IMemberContext> Create(ISymbol symbol, CompilationSettings settings) {
            // 1. Check Name
            if (_ignoredNames.Contains(symbol.Name)) {
                yield return new SkippedMemberContext(symbol.Name, "Explicitly ignored by name");
                yield break;
            }

            // 2. Check Overrides (Optional, depending on your strictness)
            // Generally, we skip overrides because the base wrapper handles the virtual member.
            if (symbol.IsOverride) {
                yield return new SkippedMemberContext(symbol.Name, "Member is an override");
                yield break;
            }

            // 3. Check Base Class Shadowing
            // If the base class is wrapped, and it has this member, we usually skip it 
            // to let the base wrapper handle it (unless we want to shadow it).
            //if (IsShadowingBaseMember(symbol, settings)) {
            //    yield return new SkippedMemberContext(symbol.Name, "Shadows base member in wrapped base class");
            //    yield break;
            //}
        }

        private bool IsShadowingBaseMember(ISymbol symbol, CompilationSettings settings) {
            // If we don't have a base type, or the base type isn't wrapped, we aren't shadowing a wrapped member
            if (symbol.ContainingType.BaseType is not INamedTypeSymbol baseType ||
                !settings.NamedTypes.IsContained(baseType)) {
                return false;
            }

            // Check if the base type has a member with the same name
            // (A robust check might also compare signatures, but name collision is usually enough to skip in wrappers)
            foreach (var baseMember in baseType.GetMembers(symbol.Name)) {
                if (baseMember.DeclaredAccessibility == Accessibility.Public && !baseMember.IsStatic) {
                    return true;
                }
            }

            return false;
        }
    }
}