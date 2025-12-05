using EsapiService.Generators.Contexts;
using EsapiService.Generators.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace EsapiService.Generators {
    class Program {

        static void Main(string[] args) {
            try {
                RunGenerator();
            } catch (Exception ex) {
                Console.Error.WriteLine($"Fatal Error: {ex.Message}");
                Console.Error.WriteLine(ex.StackTrace);
            }
        }

        static void RunGenerator() {
            // 1. Paths configuration
            string solutionRoot = FindSolutionRoot(AppContext.BaseDirectory);
            string libsFolder = Path.Combine(solutionRoot, "libs");
            string esapiDllPath = Path.Combine(libsFolder, "VMS.TPS.Common.Model.API.dll");

            // Output goes to 'EsapiService.Wrappers' or similar folder in solution
            string outputDir = Path.Combine(solutionRoot, "EsapiService", "EsapiService.Wrappers");

            Console.WriteLine($"Solution Root: {solutionRoot}");
            Console.WriteLine($"Target DLL: {esapiDllPath}");
            Console.WriteLine($"Output Directory: {outputDir}");

            // 2. Load Symbols
            Compilation compilation;
            List<INamedTypeSymbol> targetSymbols;

            if (File.Exists(esapiDllPath)) {
                Console.WriteLine("DLL found. Loading Assembly...");
                (compilation, targetSymbols) = LoadFromAssembly(esapiDllPath);
            } else {
                Console.WriteLine("DLL not found in libs. Using Mock Mode (Source Parsing)...");
                (compilation, targetSymbols) = LoadMockSymbols();
            }

            // 3. Initialize Services
            var namespaceCollection = new NamespaceCollection(targetSymbols);
            var contextService = new ContextService(namespaceCollection);

            // 4. Prepare Output Directory
            if (Directory.Exists(outputDir)) {
                // Optional: Clean old files?
                Directory.Delete(outputDir, true);
            }
            Directory.CreateDirectory(outputDir);

            // 5. Generate Static Support Files (IEsapiService)
            Console.WriteLine("Generating static support files...");
            File.WriteAllText(Path.Combine(outputDir, "IEsapiService.cs"), GenerateIEsapiService());

            // 6. Generate Wrappers
            Console.WriteLine($"Found {targetSymbols.Count} classes to wrap.");

            foreach (var symbol in targetSymbols) {
                Console.Write($"Generating {symbol.Name}...");
                try {
                    var context = contextService.BuildContext(symbol);

                    // A. Interface
                    string interfaceCode = InterfaceGenerator.Generate(context);
                    File.WriteAllText(Path.Combine(outputDir, $"I{symbol.Name}.cs"), interfaceCode);

                    // B. Wrapper
                    string wrapperCode = WrapperGenerator.Generate(context);
                    File.WriteAllText(Path.Combine(outputDir, $"Async{symbol.Name}.cs"), wrapperCode);

                    Console.WriteLine(" Done.");
                } catch (Exception ex) {
                    Console.WriteLine($" Failed! {ex.Message}");
                }
            }

            Console.WriteLine("Generation Complete.");
        }

        // --- Helpers ---

        static string FindSolutionRoot(string startPath) {
            var dir = new DirectoryInfo(startPath);
            while (dir != null) {
                // Look for the libs folder OR the .sln file
                if (Directory.Exists(Path.Combine(dir.FullName, "libs")) ||
                    dir.GetFiles("*.sln").Any()) {
                    return dir.FullName;
                }
                dir = dir.Parent;
            }
            // Fallback to current if not found
            return Directory.GetCurrentDirectory();
        }

        static string GenerateIEsapiService() {
            return @"using System;
using System.Threading.Tasks;

namespace EsapiService.Wrappers
{
    public interface IEsapiService
    {
        /// <summary>
        /// Executes a void action on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action action);

        /// <summary>
        /// Executes a function on the ESAPI thread and returns the result.
        /// </summary>
        Task<T> RunAsync<T>(Func<T> function);
    }
}";
        }

        static (Compilation, List<INamedTypeSymbol>) LoadFromAssembly(string path) {
            var reference = MetadataReference.CreateFromFile(path);

            // We need basic system references for valid compilation
            var refs = new List<MetadataReference> {
                reference,
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IEnumerable<>).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Linq.Enumerable).Assembly.Location)
            };

            var compilation = CSharpCompilation.Create("EsapiGenerator", references: refs);

            var assemblySymbol = (IAssemblySymbol)compilation.GetAssemblyOrModuleSymbol(reference);

            // Navigate to VMS.TPS.Common.Model.API
            var namespaceSymbol = GetNamespaceRecursively(assemblySymbol.GlobalNamespace, "VMS.TPS.Common.Model.API");

            if (namespaceSymbol == null) {
                throw new Exception("Could not find namespace 'VMS.TPS.Common.Model.API' in the DLL.");
            }

            // Filter: Public Classes only
            var targets = namespaceSymbol.GetTypeMembers()
                .Where(t => t.TypeKind == TypeKind.Class &&
                            t.DeclaredAccessibility == Accessibility.Public &&
                            !t.IsStatic) // Usually we don't wrap static helpers this way
                .ToList();

            return (compilation, targets);
        }

        static (Compilation, List<INamedTypeSymbol>) LoadMockSymbols() {
            var code = @"
                using System.Collections.Generic;
                namespace VMS.TPS.Common.Model.API {
                    public class PlanSetup { 
                        public string Id { get; set; } 
                        public Course Course { get; }
                        public IEnumerable<Structure> Structures { get; }
                        public void Calculate() {}
                    }
                    public class Course {
                        public string Id { get; }
                    }
                    public class Structure {
                        public string Id { get; }
                    }
                }";

            var tree = CSharpSyntaxTree.ParseText(code);
            var compilation = CSharpCompilation.Create("MockAssembly", new[] { tree },
                new[] {
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(IEnumerable<>).Assembly.Location)
                });

            var targets = new List<INamedTypeSymbol>();
            var root = compilation.GlobalNamespace.GetNamespaceMembers().First(n => n.Name == "VMS");
            // Basic recursive finder for mock
            targets.AddRange(compilation.GetSymbolsWithName("PlanSetup").OfType<INamedTypeSymbol>());
            targets.AddRange(compilation.GetSymbolsWithName("Course").OfType<INamedTypeSymbol>());
            targets.AddRange(compilation.GetSymbolsWithName("Structure").OfType<INamedTypeSymbol>());

            return (compilation, targets);
        }

        static INamespaceSymbol GetNamespaceRecursively(INamespaceSymbol symbol, string fullNamespace) {
            if (symbol.ToDisplayString() == fullNamespace) return symbol;

            foreach (var child in symbol.GetNamespaceMembers()) {
                var result = GetNamespaceRecursively(child, fullNamespace);
                if (result != null) return result;
            }
            return null;
        }
    }
}