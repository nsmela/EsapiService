using Microsoft.CodeAnalysis;

namespace EsapiService.Generators.Contexts.ContextFactory {
    public class ComplexPropertyFactory : IMemberContextFactory {
        public IEnumerable<IMemberContext> Create(ISymbol symbol, CompilationSettings settings) {
            // 1. Guard Clauses
            if (symbol is not IPropertySymbol property)
                yield break;

            if (property.IsIndexer)
                yield break;

            // 2. Check if this is a Target Type
            // It must be either Wrapped (e.g. Structure) or Forced Complex (e.g. MeshGeometry3D)
            bool isWrapped = false;
            if (property.Type is INamedTypeSymbol namedType)
            {
                if (settings.NamedTypes.IsContained(namedType))
                {
                    isWrapped = true;
                } else if (settings.ForceComplexTypes.Contains(namedType.ToDisplayString()))
                {
                    isWrapped = false; // Complex, but NOT wrapped
                } else
                {
                    // Not a complex type we care about
                    yield break;
                }
            } else
            {
                yield break;
            }

            // Double check: Generic collections usually aren't handled here, but if a type is generic 
            // AND whitelisted (unlikely for Varian standard objects, but possible), we might need care.
            // For now, standard Varian objects are non-generic classes.
            if (namedType.IsGenericType)
                yield break;

            // 3. Preparation
            string name = property.Name;
            string rawTypeName = SimplifyTypeString(property.Type.ToDisplayString(settings.Naming.DisplayFormat));

            string returnType;
            string wrapperName = string.Empty; // Only used if Wrapped

            if (isWrapped)
            {
                // e.g. "IStructure"
                returnType = settings.Naming.GetInterfaceName(property.Type.Name);
                // e.g. "AsyncStructure"
                wrapperName = settings.Naming.GetWrapperName(property.Type.Name);
            } else
            {
                // Unwrapped Complex: Use the raw type name
                // e.g. "System.Windows.Media.Media3D.MeshGeometry3D"
                returnType = rawTypeName;
            }

            string xmlDocs = symbol.GetDocumentationCommentXml(expandIncludes: true) ?? string.Empty;

            bool isReadOnly = property.SetMethod is null ||
                              property.SetMethod.DeclaredAccessibility != Accessibility.Public;

            // e.g. "IPlanSetup"
            string interfaceName = settings.Naming.GetInterfaceName(namedType.Name);

            // 4. Create Context
            yield return new ComplexPropertyContext(
                Name: name,
                Symbol: rawTypeName,
                XmlDocumentation: xmlDocs,
                ReturnValue: returnType,
                WrapperName: wrapperName,
                InterfaceName: interfaceName,
                IsReadOnly: isReadOnly,
                IsWrapped: isWrapped,
                IsFreezable: IsFreezableType(symbol),
                IsStatic: symbol.IsStatic
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

        private bool IsFreezableType(ISymbol symbol) {
            // Check if the type inherits from System.Windows.Freezable
            // Since we might not have the full WPF reference context in the generator, 
            // checking the name and namespace is a robust fallback.

            var current = (symbol as IPropertySymbol)?.Type;
            while (current != null) {
                if (current.Name == "Freezable" && current.ContainingNamespace.Name == "Windows") {
                    return true;
                }
                // Also explicitly check for MeshGeometry3D as a known target
                if (current.Name == "MeshGeometry3D" && current.ContainingNamespace.Name == "Media3D") {
                    return true;
                }
                current = current.BaseType;
            }
            return false;
        }
    }
}