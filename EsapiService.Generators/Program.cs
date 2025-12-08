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

            // Base Output Directory
            string baseOutputDir = Path.Combine(solutionRoot, "EsapiService");

            // Subdirectories
            string interfacesDir = Path.Combine(baseOutputDir, "Interfaces");
            string wrappersDir = Path.Combine(baseOutputDir, "Wrappers");

            Console.WriteLine($"Solution Root: {solutionRoot}");
            Console.WriteLine($"Target DLL: {esapiDllPath}");
            Console.WriteLine($"Output Directory: {baseOutputDir}");

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

            // 4. Prepare Output Directories
            // Optional: Clean old files
            if (Directory.Exists(interfacesDir)) Directory.Delete(interfacesDir, true);
            if (Directory.Exists(wrappersDir)) Directory.Delete(wrappersDir, true);

            if (!Directory.Exists(interfacesDir)) Directory.CreateDirectory(interfacesDir);
            if (!Directory.Exists(wrappersDir)) Directory.CreateDirectory(wrappersDir);

            // 5. Generate Static Support Files
            // IEsapiService is an interface, so it goes in the Interfaces folder
            Console.WriteLine("Generating static support files...");
            File.WriteAllText(Path.Combine(interfacesDir, "IEsapiService.cs"), GenerateIEsapiService());

            // 6. Generate Wrappers & Interfaces
            Console.WriteLine($"Found {targetSymbols.Count} classes to wrap.");

            foreach (var symbol in targetSymbols) {
                Console.Write($"Generating {symbol.Name}...");
                try {
                    var context = contextService.BuildContext(symbol);

                    // A. Interface -> /Generated/Interfaces/IClassName.cs
                    string interfaceCode = InterfaceGenerator.Generate(context);
                    File.WriteAllText(Path.Combine(interfacesDir, $"I{symbol.Name}.cs"), interfaceCode);

                    // B. Wrapper -> /Generated/Wrappers/AsyncClassName.cs
                    string wrapperCode = WrapperGenerator.Generate(context);
                    File.WriteAllText(Path.Combine(wrappersDir, $"Async{symbol.Name}.cs"), wrapperCode);

                    Console.WriteLine(" Done.");
                } catch (Exception ex) {
                    Console.WriteLine($" Failed! {ex.Message}");
                }
            }

            Console.WriteLine("Generation Complete.");
        }

        // --- Helpers (Unchanged) ---

        static string FindSolutionRoot(string startPath) {
            var dir = new DirectoryInfo(startPath);
            while (dir != null) {
                if (Directory.Exists(Path.Combine(dir.FullName, "libs")) ||
                    dir.GetFiles("*.sln").Any()) {
                    return dir.FullName;
                }
                dir = dir.Parent;
            }
            return Directory.GetCurrentDirectory();
        }

        static string GenerateIEsapiService() {
            // Note: Namespace matches the folder structure implies EsapiService.Wrappers.Interfaces? 
            // Or keep it simple. For now, sticking to EsapiService.Wrappers to avoid breaking existing code refs.
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
            var refs = new List<MetadataReference> {
                reference,
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IEnumerable<>).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Linq.Enumerable).Assembly.Location)
            };

            var compilation = CSharpCompilation.Create("EsapiGenerator", references: refs);
            var assemblySymbol = (IAssemblySymbol)compilation.GetAssemblyOrModuleSymbol(reference);
            var namespaceSymbol = GetNamespaceRecursively(assemblySymbol.GlobalNamespace, "VMS.TPS.Common.Model.API");

            if (namespaceSymbol == null)
                throw new Exception("Could not find namespace 'VMS.TPS.Common.Model.API' in the DLL.");

            var targets = namespaceSymbol.GetTypeMembers()
                .Where(t => t.TypeKind == TypeKind.Class &&
                            t.DeclaredAccessibility == Accessibility.Public &&
                            !t.IsStatic)
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