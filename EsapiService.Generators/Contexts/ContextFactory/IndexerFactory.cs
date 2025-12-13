using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace EsapiService.Generators.Contexts.ContextFactory {
    public class IndexerFactory : IMemberContextFactory {
        public IEnumerable<IMemberContext> Create(ISymbol symbol, CompilationSettings settings) {
            // 1. Guard Clauses
            if (symbol is not IPropertySymbol property)
                yield break;

            if (!property.IsIndexer)
                yield break;

            // 2. Analyze Return Type
            // The original logic strictly required the return type to be a wrapped type.
            // e.g. public Structure this[string id] => Supported
            // e.g. public int this[int i] => Skipped (unless 'int' was in namedTypes, which it isn't)
            if (property.Type is not INamedTypeSymbol returnType || !settings.NamedTypes.IsContained(returnType))
                yield break;

            // 3. Preparation
            var parameters = property.Parameters
                .Select(p => CreateParameterContext(p, settings))
                .ToImmutableList();

            bool isReadOnly = property.SetMethod is null ||
                              property.SetMethod.DeclaredAccessibility != Accessibility.Public;

            // 4. Determine Enumerable Source
            // Check if the containing type has a 'Values' property that is IEnumerable<ReturnType>
            string enumerableSource = DetermineEnumerableSource(property.ContainingType, returnType, settings);

            // 5. Create Context
            yield return new IndexerContext(
                Name: "this[]",
                Symbol: SimplifyTypeString(property.Type.ToDisplayString(settings.Naming.DisplayFormat)),
                XmlDocumentation: symbol.GetDocumentationCommentXml(expandIncludes: true) ?? string.Empty,
                WrapperName: settings.Naming.GetWrapperName(returnType.Name),
                InterfaceName: settings.Naming.GetInterfaceName(returnType.Name),
                Parameters: parameters,
                IsReadOnly: isReadOnly,
                EnumerableSource: enumerableSource
            );
        }

        // --- Helpers ---
        private string DetermineEnumerableSource(INamedTypeSymbol containingType, ITypeSymbol itemType, CompilationSettings settings) {
            // Look for a public property named "Values"
            var valuesProp = containingType.GetMembers("Values")
                .OfType<IPropertySymbol>()
                .FirstOrDefault(p => p.DeclaredAccessibility == Accessibility.Public && !p.IsStatic);

            if (valuesProp != null) {
                // Check if valuesProp.Type is IEnumerable<itemType>
                if (IsEnumerableOf(valuesProp.Type, itemType)) {
                    return ".Values";
                }
            }

            // Default: Iterate the object itself
            return "";
        }

        private bool IsEnumerableOf(ITypeSymbol enumerableType, ITypeSymbol itemType) {
            // Helper to check if type is IEnumerable<T> where T matches itemType
            // (Simplified check for exact match or generic match)
            if (enumerableType is INamedTypeSymbol named && named.IsGenericType) {
                // Direct check: IEnumerable<StructureCode>
                if (named.TypeArguments.Length == 1 &&
                    SymbolEqualityComparer.Default.Equals(named.TypeArguments[0], itemType)) {
                    // You might want to check if named.ConstructedFrom is IEnumerable<> or derives from it
                    // But usually checking the item type is sufficient context here.
                    return true;
                }
            }
            return false;
        }

        private ParameterContext CreateParameterContext(IParameterSymbol p, CompilationSettings settings) {
            var type = p.Type;
            string typeName = SimplifyTypeString(type.ToDisplayString(settings.Naming.DisplayFormat));

            // Case A: Single Wrapped Object
            if (type is INamedTypeSymbol named && settings.NamedTypes.IsContained(named)) {
                return new ParameterContext(
                    p.Name,
                    typeName,
                    settings.Naming.GetInterfaceName(named.Name),
                    settings.Naming.GetWrapperName(named.Name),
                    true,
                    false,
                    false
                );
            }

            // Case B: Collection of Wrapped Objects
            if (type is INamedTypeSymbol generic && generic.IsGenericType && generic.TypeArguments.Length == 1) {
                var inner = generic.TypeArguments[0];
                if (inner is INamedTypeSymbol innerNamed && settings.NamedTypes.IsContained(innerNamed)) {
                    string innerInterface = settings.Naming.GetInterfaceName(innerNamed.Name);
                    string innerWrapper = settings.Naming.GetWrapperName(innerNamed.Name);

                    return new ParameterContext(
                        p.Name,
                        typeName,
                        settings.Naming.GetCollectionInterfaceName(innerInterface),
                        settings.Naming.GetCollectionWrapperName(innerWrapper),
                        true,
                        false,
                        false,
                        true,
                        innerWrapper
                    );
                }
            }

            // Case C: Primitive
            return new ParameterContext(p.Name, typeName, typeName, "", false, false, false);
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
}