using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using EsapiService.Generators.Contexts;

namespace EsapiService.Generators.Contexts.ContextFactory {
    public class SimpleCollectionMethodFactory : IMemberContextFactory {
        public IEnumerable<IMemberContext> Create(ISymbol symbol, CompilationSettings settings) {
            // 1. Guard Clauses
            if (symbol is not IMethodSymbol method)
                yield break;

            if (method.MethodKind != MethodKind.Ordinary)
                yield break;

            if (method.ReturnsVoid)
                yield break;

            // Skip if handled by OutParameterMethodFactory
            if (method.Parameters.Any(p => p.RefKind == RefKind.Out || p.RefKind == RefKind.Ref))
                yield break;

            // 2. Analyze Return Type (Must be Generic Collection)
            if (method.ReturnType is not INamedTypeSymbol genericRet ||
                !genericRet.IsGenericType ||
                genericRet.TypeArguments.Length != 1) {
                yield break;
            }

            // 3. Analyze Inner Type (Must NOT be a Wrapped Type)
            var innerType = genericRet.TypeArguments[0];

            // If inner type IS wrapped, it belongs to ComplexCollectionMethodFactory. Skip here.
            if (innerType is INamedTypeSymbol innerNamed && settings.NamedTypes.IsContained(innerNamed)) {
                yield break;
            }

            // 4. Preparation
            var parameters = method.Parameters
                .Select(p => CreateParameterContext(p, settings))
                .ToImmutableList();

            string args = string.Join(", ", parameters.Select(p => $"{p.InterfaceType} {p.Name}"));
            string originalArgs = string.Join(", ", method.Parameters.Select(p => $"{SimplifyTypeString(p.Type.ToDisplayString(settings.Naming.DisplayFormat))} {p.Name}"));
            string callArgs = string.Join(", ", method.Parameters.Select(p => p.Name));

            // e.g. inner type string -> "IReadOnlyList<string>"
            // We use the naming strategy for consistency, but standard behavior is usually IReadOnlyList<T>
            string innerTypeName = SimplifyTypeString(innerType.ToDisplayString(settings.Naming.DisplayFormat));
            string interfaceName = settings.Naming.GetCollectionInterfaceName(innerTypeName);

            // 5. Create Context
            yield return new SimpleCollectionMethodContext(
                Name: method.Name,
                Symbol: method.ReturnType.ToDisplayString(settings.Naming.DisplayFormat),
                XmlDocumentation: symbol.GetDocumentationCommentXml(expandIncludes: true) ?? string.Empty,
                InterfaceName: interfaceName,
                Signature: $"({args})",
                OriginalSignature: $"({originalArgs})",
                CallParameters: callArgs,
                Parameters: parameters
            );
        }

        // --- Helpers ---

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