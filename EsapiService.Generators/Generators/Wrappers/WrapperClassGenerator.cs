using EsapiService.Generators.Contexts;
using System.Reflection;
using System.Text;

namespace EsapiService.Generators.Generators.Wrappers {
    public static class WrapperClassGenerator {
        public static string Generate(ClassContext context)
        {
            var sb = new StringBuilder();

            // 1. Usings
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Threading.Tasks;");

            // Varian usings
            sb.AppendLine("using VMS.TPS.Common.Model.API;");
            sb.AppendLine("using VMS.TPS.Common.Model.Types;");
            sb.AppendLine($"using {NamingConvention.GetInterfaceNameSpace()};");
            sb.AppendLine("using Esapi.Services;");
            sb.AppendLine();

            // 2. Namespace
            string namespaceName = NamingConvention.GetWrapperNameSpace();
            sb.AppendLine($"namespace {namespaceName}");
            sb.AppendLine("{");

            // 3. Class Declaration
            if (!string.IsNullOrEmpty(context.XmlDocumentation))
            {
                sb.AppendLine(context.XmlDocumentation);
            }

            // e.g. public class AsyncPlanSetup : IPlanSetup
            var sealedModifier = context.IsSealed ? "sealed " : string.Empty;

            bool hasBase = !string.IsNullOrEmpty(context.BaseWrapperName);
            bool hasInterface = !string.IsNullOrEmpty(context.InterfaceName);

            var baseModifier = string.Empty;
            if (hasBase)
            {
                baseModifier = $" : {context.BaseWrapperName}";
                if (hasInterface)
                {
                    baseModifier += $", {context.InterfaceName}";
                }
                baseModifier += $", IEsapiWrapper<{context.Name}>";
            }
            else if (hasInterface)
            {
                baseModifier = $" : {context.InterfaceName}, IEsapiWrapper<{context.Name}>";
            }
            else
            {
                baseModifier = $" : IEsapiWrapper<{context.Name}>";
            }

            sb.AppendLine($"    public {sealedModifier}class {context.WrapperName}{baseModifier}");
            sb.AppendLine("    {");

            // 4. Fields
            // Determine if we are shadowing a base wrapper member
            string newModifier = hasBase ? "new " : "";

            sb.AppendLine($"        internal {newModifier}readonly {context.Name} _inner;");
            sb.AppendLine();
            sb.AppendLine($"        // Store the inner ESAPI object reference");
            sb.AppendLine($"        // internal so other wrappers can access it");
            sb.AppendLine($"        // new to override any inherited _inner fields");
            sb.AppendLine($"        internal {newModifier}readonly IEsapiService _service;");

            // 5. Constructor
            sb.AppendLine();
            sb.AppendLine(ConstructorGenerator.Generate(context));

            // 6. Members
            foreach (var member in context.Members)
            {
                sb.AppendLine(GenerateMember(member));
            }

            // 7. RunAsync
            sb.AppendLine();
            sb.AppendLine($"        public Task RunAsync(Action<{context.Name}> action) => _service.PostAsync((context) => action(_inner));");
            sb.AppendLine($"        public Task<T> RunAsync<T>(Func<{context.Name}, T> func) => _service.PostAsync<T>((context) => func(_inner));");
            

            // 9. Conversions
            sb.AppendLine();
            sb.AppendLine($"        public static implicit operator {context.Name}({context.WrapperName} wrapper) => wrapper._inner;");

            // 10. Explicit Implementation
            sb.AppendLine();
            sb.AppendLine($"        // Internal Explicit Implementation to expose _inner safely for covariance");
            sb.AppendLine($"        {context.Name} IEsapiWrapper<{context.Name}>.Inner => _inner;");

            sb.AppendLine();
            sb.AppendLine($"        // Explicit or Implicit implementation of Service");
            sb.AppendLine($"        // Since _service is private, we expose it via the interface");
            sb.AppendLine($"        IEsapiService IEsapiWrapper<{context.Name}>.Service => _service;");

            // 11. Skipped Members Report
            if (context.SkippedMembers.Any())
            {
                sb.AppendLine();
                sb.AppendLine("        /* --- Skipped Members (Not generated) ---");
                foreach (var skip in context.SkippedMembers)
                {
                    sb.AppendLine($"           - {skip.Name}: {skip.Reason}");
                }
                sb.AppendLine("        */");
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }
        

        private static string GenerateMember(IMemberContext member) {
            // methods to skip
            if (member.Name == "GetEnumerator") { return string.Empty; }

            var sb = new StringBuilder();
            sb.AppendLine();

            sb.Append(member switch {
                SimplePropertyContext m => SimplePropertyGenerator.Generate(m),

                // Complex Property: Null check + Wrap
                // public ICourse Course => _inner.Course is null ? null : new AsyncCourse(_inner.Course);
                ComplexPropertyContext m => ComplexPropertyGenerator.Generate(m),

                // Collection Property: Async Method + Wrap Items
                // public IReadOnlyList<IStructure> Structures => _inner.Structures?.Select(x => new AsyncStructure(x)).ToList();
                CollectionPropertyContext m => CollectionPropertyGenerator.Generate(m),

                // Simple Collection: Async Method + ToList
                SimpleCollectionPropertyContext m => SimpleCollectionPropertyGenerator.Generate(m),

                // if the class is a collection
                IndexerContext m => IndexPropertyGenerator.Generate(m),

                // Methods
                // 1. Void Method -> Task NameAsync()
                //    Forward directly to RunAsync(Action)
                VoidMethodContext m => VoidMethodGenerator.Generate(m),

                // 2. Simple Return -> Task<T> NameAsync()
                //    Forward directly to RunAsync<T>(Func<T>)
                SimpleMethodContext m => SimpleMethodGenerator.Generate(m),
                    
                // 3. Complex Return -> async Task<Interface> NameAsync()
                //    Execute, Check Null, Wrap
                ComplexMethodContext m => ComplexMethodGenerator.Generate(m),

                // 4. Simple Collection Return -> Task<IReadOnlyList<T>>
                SimpleCollectionMethodContext m => SimpleCollectionMethodGenerator.Generate(m),

                // 5. Complex Collection Return -> async Task<IReadOnlyList<Interface>>
                ComplexCollectionMethodContext m => ComplexCollectionMethodGenerator.Generate(m),

                OutParameterMethodContext m => OutParameterMethodGenerator.Generate(m),

                _ => string.Empty
            });

            return sb.ToString();
        }

    }
}
