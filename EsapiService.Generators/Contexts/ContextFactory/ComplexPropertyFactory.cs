using Microsoft.CodeAnalysis;

namespace EsapiService.Generators.Contexts.ContextFactory {
    public class ComplexPropertyFactory : IMemberContextFactory {
        public IEnumerable<IMemberContext> Create(ISymbol symbol, CompilationSettings settings) {
            // 1. Guard Clauses
            if (symbol is not IPropertySymbol property)
                yield break;

            if (property.IsIndexer)
                yield break;

            // 2. Analyze Type
            // Must be a named type (e.g. PlanSetup) that is IN our whitelist
            if (property.Type is not INamedTypeSymbol namedType || !settings.NamedTypes.IsContained(namedType))
                yield break;

            // Double check: Generic collections usually aren't handled here, but if a type is generic 
            // AND whitelisted (unlikely for Varian standard objects, but possible), we might need care.
            // For now, standard Varian objects are non-generic classes.
            if (namedType.IsGenericType)
                yield break;

            // 3. Preparation
            string name = property.Name;
            string symbolType = SimplifyTypeString(property.Type.ToDisplayString(settings.Naming.DisplayFormat));
            string xmlDocs = symbol.GetDocumentationCommentXml(expandIncludes: true) ?? string.Empty;

            bool isReadOnly = property.SetMethod is null ||
                              property.SetMethod.DeclaredAccessibility != Accessibility.Public;

            // e.g. "AsyncPlanSetup"
            string wrapperName = settings.Naming.GetWrapperName(namedType.Name);
            // e.g. "IPlanSetup"
            string interfaceName = settings.Naming.GetInterfaceName(namedType.Name);

            // 4. Create Context
            yield return new ComplexPropertyContext(
                Name: name,
                Symbol: symbolType,
                XmlDocumentation: xmlDocs,
                WrapperName: wrapperName,
                InterfaceName: interfaceName,
                IsReadOnly: isReadOnly
            );
        }

        // --- Helpers ---

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