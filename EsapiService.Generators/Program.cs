using EsapiService.Generators.Contexts;
using EsapiService.Generators.Generators;
using EsapiService.Generators.Generators.Wrappers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace EsapiService.Generators {
    class Program {
        static void Main(string[] args) {
            try {
                //RunDebugScan();
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
            string mocksDir = Path.Combine(solutionRoot, "EsapiMocks", "API");

            Console.WriteLine($"Solution Root: {solutionRoot}");
            Console.WriteLine($"Target DLL: {esapiDllPath}");
            Console.WriteLine($"Output Directory: {baseOutputDir}");
            Console.WriteLine($"Mock Directory: {mocksDir}");

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
            if (Directory.Exists(mocksDir)) Directory.Delete(mocksDir, true);

            if (!Directory.Exists(interfacesDir)) Directory.CreateDirectory(interfacesDir);
            if (!Directory.Exists(wrappersDir)) Directory.CreateDirectory(wrappersDir);
            if (!Directory.Exists(mocksDir)) Directory.CreateDirectory(mocksDir);

            // 5. Generate Static Support Files
            // TODO

            // 6. Generate Wrappers & Interfaces
            Console.WriteLine($"Found {targetSymbols.Count} classes to wrap.");

            foreach (var symbol in targetSymbols) {
                Console.Write($"Generating {symbol.Name}...");
                try {
                    var context = contextService.BuildContext(symbol);

                    // A. Interface -> /EsapiService/Interfaces/IClassName.cs
                    string interfaceCode = InterfaceGenerator.Generate(context);
                    File.WriteAllText(Path.Combine(interfacesDir, $"I{symbol.Name}.cs"), interfaceCode);

                    // B. Wrapper -> /EsapiService/Wrappers/AsyncClassName.cs
                    string wrapperCode = WrapperClassGenerator.Generate(context);
                    File.WriteAllText(Path.Combine(wrappersDir, $"Async{symbol.Name}.cs"), wrapperCode);

                    // C. Mocks -> /EsapiMocks/API/ClassName.cs
                    string mockCode = MockGenerator.Generate(context);
                    File.WriteAllText(Path.Combine(mocksDir, $"{symbol.Name}.cs"), mockCode);

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

        static (Compilation, List<INamedTypeSymbol>) LoadFromAssembly(string path) {
            var reference = MetadataReference.CreateFromFile(path);
            var refs = new List<MetadataReference> {
                reference,
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IEnumerable<>).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Linq.Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Nullable<>).Assembly.Location)
            };

            var compilation = CSharpCompilation.Create("EsapiGenerator", references: refs);
            var assemblySymbol = (IAssemblySymbol)compilation.GetAssemblyOrModuleSymbol(reference);

            var targets = new List<INamedTypeSymbol>();

            // 1. Scan API Namespace (Classes, Structs, Enums)
            var apiNs = GetNamespaceRecursively(assemblySymbol.GlobalNamespace, "VMS.TPS.Common.Model.API");
            if (apiNs != null)
                targets.AddRange(GetExportableTypes(apiNs));

            // 2. Scan Root Model Namespace (For CalibrationProtocolStatus, etc.)
            var modelNs = GetNamespaceRecursively(assemblySymbol.GlobalNamespace, "VMS.TPS.Common.Model");
            if (modelNs != null)
                targets.AddRange(GetExportableTypes(modelNs));

            return (compilation, targets.Distinct(SymbolEqualityComparer.Default).Cast<INamedTypeSymbol>().ToList());
        }

        // Helper to filter types we want to generate
        static IEnumerable<INamedTypeSymbol> GetExportableTypes(INamespaceSymbol ns) {
            return ns.GetTypeMembers().Where(t =>
                (t.TypeKind == TypeKind.Class ||
                 t.TypeKind == TypeKind.Struct) && 
                t.DeclaredAccessibility == Accessibility.Public &&
                !t.IsStatic);
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

        static void RunDebugScan() {
            Console.WriteLine("=== ESAPI DEBUG SCANNER ===");

            // 1. Paths configuration
            string solutionRoot = FindSolutionRoot(AppContext.BaseDirectory);
            string libsFolder = Path.Combine(solutionRoot, "libs");
            string esapiDllPath = Path.Combine(libsFolder, "VMS.TPS.Common.Model.API.dll");
            string typesDllPath = Path.Combine(libsFolder, "VMS.TPS.Common.Model.Types.dll");

            Console.WriteLine($"Checking Libraries at: {libsFolder}");

            // 2. Load Compilation
            var references = new List<MetadataReference>
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IEnumerable<>).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Linq.Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Nullable<>).Assembly.Location)
            };

            if (File.Exists(esapiDllPath)) {
                Console.WriteLine($"[OK] Found API DLL");
                references.Add(MetadataReference.CreateFromFile(esapiDllPath));
            } else Console.WriteLine($"[ERR] Missing API DLL: {esapiDllPath}");

            if (File.Exists(typesDllPath)) {
                Console.WriteLine($"[OK] Found Types DLL");
                references.Add(MetadataReference.CreateFromFile(typesDllPath));
            } else Console.WriteLine($"[ERR] Missing Types DLL: {typesDllPath}");

            var compilation = CSharpCompilation.Create("EsapiScanner", references: references);

            // 3. Start Scanning from the Root Model Namespace
            Console.WriteLine("\nScanning Namespace: VMS.TPS.Common.Model...");
            var rootNs = GetNamespaceRecursively(compilation.GlobalNamespace, "VMS.TPS.Common.Model");

            if (rootNs == null) {
                Console.WriteLine("❌ CRITICAL: Could not find namespace 'VMS.TPS.Common.Model'");
                return;
            }

            // 4. Print Tree
            PrintNamespaceTree(rootNs, 0);

            Console.WriteLine("\n=== SCAN COMPLETE ===");
        }

        static void PrintNamespaceTree(INamespaceSymbol ns, int indent) {
            string pad = new string(' ', indent * 2);
            Console.WriteLine($"{pad} Namespace: {ns.Name}");

            // 1. Print Types in this Namespace
            foreach (var type in ns.GetTypeMembers().OrderBy(t => t.Name)) {
                if (!IsExportable(type)) continue;

                PrintType(type, indent + 1);
            }

            // 2. Recurse into Child Namespaces
            foreach (var childNs in ns.GetNamespaceMembers().OrderBy(n => n.Name)) {
                PrintNamespaceTree(childNs, indent + 1);
            }
        }

        static void PrintType(INamedTypeSymbol type, int indent) {
            string pad = new string(' ', indent * 2);
            string icon = type.TypeKind == TypeKind.Enum ? "Enum" :
                          type.TypeKind == TypeKind.Struct ? "Struct" : "[]";

            Console.WriteLine($"{pad}{icon} {type.Name} ({type.TypeKind})");

            // CHECK FOR NESTED TYPES
            var nestedTypes = type.GetTypeMembers()
                                  .Where(IsExportable)
                                  .OrderBy(t => t.Name);

            foreach (var nested in nestedTypes) {
                string nestedPad = new string(' ', (indent + 2) * 2);
                Console.WriteLine($"{nestedPad} NESTED: {nested.Name} ({nested.TypeKind})");

                // If nested type has children (rare, but possible)
                foreach (var deepNested in nested.GetTypeMembers().Where(IsExportable)) {
                    Console.WriteLine($"{nestedPad} {deepNested.Name}");
                }
            }
        }

        static bool IsExportable(INamedTypeSymbol t) {
            return t.DeclaredAccessibility == Accessibility.Public;
        }

    }
}