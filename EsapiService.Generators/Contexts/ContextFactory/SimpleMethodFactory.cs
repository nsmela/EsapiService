using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using EsapiService.Generators.Contexts;

namespace EsapiService.Generators.Contexts.ContextFactory {
    public class SimpleMethodFactory : IMemberContextFactory {
        public IEnumerable<IMemberContext> Create(ISymbol symbol, CompilationSettings settings) {
            // 1. Guard Clauses
            if (symbol is not IMethodSymbol method)
                yield break;

            if (method.MethodKind != MethodKind.Ordinary)
                yield break;

            // Skip if handled by VoidMethodFactory
            if (method.ReturnsVoid)
                yield break;

            // Skip if handled by OutParameterMethodFactory
            if (method.Parameters.Any(p => p.RefKind == RefKind.Out || p.RefKind == RefKind.Ref))
                yield break;

            // 2. Filter out Complex/Collection types (handled by other factories)
            var returnType = method.ReturnType;

            // If it IS a wrapped type, ComplexMethodFactory should have handled it.
            if (returnType is INamedTypeSymbol named && settings.NamedTypes.IsContained(named))
                yield break;

            // If it IS a generic collection, Collection factories should have handled it.
            if (returnType is INamedTypeSymbol genericRet 
                && genericRet.IsGenericType 
                && genericRet.TypeArguments.Length == 1
                && genericRet.OriginalDefinition.SpecialType != SpecialType.System_Nullable_T) {
                // Note: We strictly skip ALL generic collections here to avoid ambiguity.
                // SimpleCollectionMethodFactory handles IEnumerable<int>.
                yield break;
            }

            // 3. Preparation
            var parameters = method.Parameters
                .Select(p => CreateParameterContext(p, settings))
                .ToImmutableList();

            string args = string.Join(", ", parameters.Select(p => $"{p.InterfaceType} {p.Name}"));
            string originalArgs = string.Join(", ", method.Parameters.Select(p => $"{SimplifyTypeString(p.Type.ToDisplayString(settings.Naming.DisplayFormat))} {p.Name}"));
            string callArgs = string.Join(", ", method.Parameters.Select(p => p.Name));

            // 4. Create Context
            yield return new SimpleMethodContext(
                Name: method.Name,
                Symbol: method.ReturnType.ToDisplayString(settings.Naming.DisplayFormat),
                XmlDocumentation: symbol.GetDocumentationCommentXml(expandIncludes: true) ?? string.Empty,
                ReturnType: SimplifyTypeString(method.ReturnType.ToDisplayString(settings.Naming.DisplayFormat)),
                Signature: $"({args})",
                OriginalSignature: $"({originalArgs})",
                CallParameters: callArgs,
                Parameters: parameters,
                IsStatic: method.IsStatic
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