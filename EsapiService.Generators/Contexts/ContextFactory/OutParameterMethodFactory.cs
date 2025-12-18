using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace EsapiService.Generators.Contexts.ContextFactory {
    public class OutParameterMethodFactory : IMemberContextFactory {
        public IEnumerable<IMemberContext> Create(ISymbol symbol, CompilationSettings settings) {
            // 1. Guard Clauses (Fail Fast)
            if (symbol is not IMethodSymbol method)
                yield break;

            if (method.MethodKind != MethodKind.Ordinary)
                yield break;

            // Only handle methods that actually HAVE out or ref parameters
            if (!method.Parameters.Any(p => p.RefKind == RefKind.Out || p.RefKind == RefKind.Ref))
                yield break;

            // 2. Preparation
            var parameters = method.Parameters
                .Select(p => CreateParameterContext(p, settings))
                .ToImmutableList();

            var tupleSignature = BuildTupleSignature(method, parameters, settings);

            string wrapperReturnTypeName = "";
            bool isReturnWrappable = false;

            // 3. Analyze Return Type (Single Object vs Collection)
            if (!method.ReturnsVoid) {
                if (method.ReturnType is INamedTypeSymbol retSym && settings.NamedTypes.IsContained(retSym)) {
                    // Single Wrapped Object
                    isReturnWrappable = true;
                    wrapperReturnTypeName = settings.Naming.GetWrapperName(retSym.Name);
                }
                else if (method.ReturnType is INamedTypeSymbol genericRet &&
                         genericRet.IsGenericType &&
                         genericRet.TypeArguments.Length == 1) {
                    // Collection of Wrapped Objects
                    var inner = genericRet.TypeArguments[0];
                    if (inner is INamedTypeSymbol innerNamed && settings.NamedTypes.IsContained(innerNamed)) {
                        isReturnWrappable = true;
                        // Use the naming strategy to determine the collection wrapper name
                        var innerWrapperName = settings.Naming.GetWrapperName(innerNamed.Name);
                        wrapperReturnTypeName = settings.Naming.GetCollectionWrapperName(innerWrapperName);
                    }
                }
            }

            // 4. Create and Return Context
            yield return new OutParameterMethodContext(
                Name: method.Name,
                Symbol: method.ReturnType.ToDisplayString(settings.Naming.DisplayFormat),
                XmlDocumentation: symbol.GetDocumentationCommentXml(expandIncludes: true) ?? string.Empty,
                OriginalReturnType: SimplifyTypeString(method.ReturnType.ToDisplayString(settings.Naming.DisplayFormat)),
                ReturnsVoid: method.ReturnsVoid,
                Parameters: parameters,
                ReturnTupleSignature: tupleSignature,
                WrapperReturnTypeName: wrapperReturnTypeName,
                IsReturnWrappable: isReturnWrappable,
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
                    p.RefKind == RefKind.Out,
                    p.RefKind == RefKind.Ref);
            }

            // Case B: Collection of Wrapped Objects
            if (type is INamedTypeSymbol generic && generic.IsGenericType && generic.TypeArguments.Length == 1) {
                var inner = generic.TypeArguments[0];
                if (inner is INamedTypeSymbol innerNamed && settings.NamedTypes.IsContained(innerNamed)) {
                    string innerInterface = settings.Naming.GetInterfaceName(innerNamed.Name);
                    string innerWrapper = settings.Naming.GetWrapperName(innerNamed.Name);

                    return new ParameterContext(
                        Name: p.Name,
                        Type: typeName,
                        InterfaceType: settings.Naming.GetCollectionInterfaceName(innerInterface),
                        WrapperType: settings.Naming.GetCollectionWrapperName(innerWrapper),
                        IsWrappable: true,
                        IsOut: p.RefKind == RefKind.Out,
                        IsRef: p.RefKind == RefKind.Ref,
                        IsCollection: true,
                        InnerType: innerNamed.Name,
                        InnerWrapperType: innerWrapper);
                }
            }

            // Case C: Primitive / Non-Wrapped
            return new ParameterContext(
                Name: p.Name,
                Type: typeName,
                InterfaceType: typeName,
                IsOut: p.RefKind == RefKind.Out,
                IsRef: p.RefKind == RefKind.Ref);
        }

        private string BuildTupleSignature(IMethodSymbol method, ImmutableList<ParameterContext> parameters, CompilationSettings settings) {
            var tupleParts = new List<string>();

            // Part 1: The Method Return Value
            if (!method.ReturnsVoid) {
                string retType = SimplifyTypeString(method.ReturnType.ToDisplayString(settings.Naming.DisplayFormat));

                if (method.ReturnType is INamedTypeSymbol retSym && settings.NamedTypes.IsContained(retSym)) {
                    retType = settings.Naming.GetInterfaceName(retSym.Name);
                }
                else if (method.ReturnType is INamedTypeSymbol genericRet &&
                         genericRet.IsGenericType &&
                         genericRet.TypeArguments.Length == 1) {
                    var inner = genericRet.TypeArguments[0];
                    if (inner is INamedTypeSymbol innerNamed && settings.NamedTypes.IsContained(innerNamed)) {
                        var innerInterface = settings.Naming.GetInterfaceName(innerNamed.Name);
                        retType = settings.Naming.GetCollectionInterfaceName(innerInterface);
                    }
                }
                tupleParts.Add($"{retType} result");
            }

            // Part 2: The Out/Ref Parameters
            foreach (var p in parameters.Where(x => x.IsOut || x.IsRef)) {
                tupleParts.Add($"{p.InterfaceType} {p.Name}");
            }

            return $"({string.Join(", ", tupleParts)})";
        }

        private string SimplifyTypeString(string typeName) {
            // Standard cleanup logic
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