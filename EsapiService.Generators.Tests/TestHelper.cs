using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;

namespace EsapiService.Generators.Tests
{
    public static class TestHelper
    {
        public static Compilation CreateCompilation(string source)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(source);

            var references = new List<MetadataReference>
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IEnumerable<>).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Linq.Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(Assembly.Load("System.Runtime").Location)
            };

            return CSharpCompilation.Create("TestAssembly",
                new[] { syntaxTree },
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        }

        public static ISymbol GetSymbol(string source, string symbolName)
        {
            var compilation = CreateCompilation(source);
            var tree = compilation.SyntaxTrees.First();
            var model = compilation.GetSemanticModel(tree);

            // Try to find the symbol in the syntax tree
            var symbol = compilation.GetSymbolsWithName(symbolName).FirstOrDefault();

            if (symbol == null)
            {
                // Fallback: look for members inside the first class defined
                var classNode = tree.GetRoot().DescendantNodes().OfType<Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax>().FirstOrDefault();
                if (classNode != null)
                {
                    var classSymbol = model.GetDeclaredSymbol(classNode);
                    symbol = classSymbol?.GetMembers(symbolName).FirstOrDefault();
                }
            }

            if (symbol == null)
            {
                throw new Exception($"Symbol '{symbolName}' not found in source code.");
            }

            return symbol;
        }

        public static INamedTypeSymbol GetClassSymbol(string source, string className)
        {
            var compilation = CreateCompilation(source);
            return compilation.GetSymbolsWithName(className).OfType<INamedTypeSymbol>().FirstOrDefault()
                   ?? throw new Exception($"Class '{className}' not found.");
        }
    }
}