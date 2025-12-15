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

            var dllsFound = new List<string>();

            if (Directory.Exists(libsFolder))
            {
                // Capture ALL Varian DLLs
                dllsFound.AddRange(Directory.GetFiles(libsFolder, "VMS.TPS.*.dll"));
            }
            GenerateProjectProps(solutionRoot, dllsFound);

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

        // --- Helpers ---

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

        static (Compilation, List<INamedTypeSymbol>) LoadFromAssembly(string path)
        {
            string libDir = Path.GetDirectoryName(path);

            // 1. Define Core References
            // Note: Varian ESAPI is .NET Framework 4.5/4.8. 
            // If running this generator in .NET Core/6/8, we might need to be careful with mscorlib vs System.Private.CoreLib.
            var refs = new List<MetadataReference> {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IEnumerable<>).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Linq.Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Nullable<>).Assembly.Location)
            };

            // 2. Load ALL Varian DLLs from the libs folder (Dependencies)
            if (Directory.Exists(libDir))
            {
                var varianDlls = Directory.GetFiles(libDir, "VMS.TPS.*.dll");
                foreach (var dll in varianDlls)
                {
                    // Avoid double-loading the target API dll (we add it specifically later)
                    if (Path.GetFileName(dll).Equals("VMS.TPS.Common.Model.API.dll", StringComparison.OrdinalIgnoreCase))
                        continue;

                    refs.Add(MetadataReference.CreateFromFile(dll));
                    Console.WriteLine($"Loaded Dependency: {Path.GetFileName(dll)}");
                }
            }

            // 3. Add the Target Reference
            var reference = MetadataReference.CreateFromFile(path);
            refs.Add(reference);

            // 4. Create Compilation
            var compilation = CSharpCompilation.Create("EsapiGenerator", references: refs);

            // 5. Retrieve the Assembly Symbol (Robust Strategy)
            IAssemblySymbol assemblySymbol = null;

            // Try direct lookup
            var symbol = compilation.GetAssemblyOrModuleSymbol(reference);
            if (symbol is IAssemblySymbol asm)
            {
                assemblySymbol = asm;
            } else
            {
                // Fallback: Find by Name
                Console.WriteLine("[WARN] Direct symbol lookup failed. Searching by name 'VMS.TPS.Common.Model.API'...");
                assemblySymbol = compilation.References
                    .Select(compilation.GetAssemblyOrModuleSymbol)
                    .OfType<IAssemblySymbol>()
                    .FirstOrDefault(a => a.Name == "VMS.TPS.Common.Model.API");
            }

            // 6. Handle Failure
            if (assemblySymbol == null)
            {
                Console.WriteLine("[FATAL] Could not load assembly symbol.");
                Console.WriteLine("--- Compilation Diagnostics ---");
                foreach (var diag in compilation.GetDiagnostics().Where(d => d.Severity == DiagnosticSeverity.Error))
                {
                    Console.WriteLine(diag.GetMessage());
                }
                throw new Exception("Failed to load VMS.TPS.Common.Model.API assembly symbol.");
            }

            Console.WriteLine($"[SUCCESS] Loaded Assembly: {assemblySymbol.Name}");

            var targets = new List<INamedTypeSymbol>();

            // 7. Scan API Namespace
            var apiNs = GetNamespaceRecursively(compilation.GlobalNamespace, "VMS.TPS.Common.Model.API");
            if (apiNs != null)
                targets.AddRange(GetExportableTypes(apiNs));

            return (compilation, targets.Distinct(SymbolEqualityComparer.Default).Cast<INamedTypeSymbol>().ToList());
        }

        // Helper to filter types we want to generate
        static IEnumerable<INamedTypeSymbol> GetExportableTypes(INamespaceSymbol ns) {
            var members = ns.GetTypeMembers().ToList();
            var filterMembers = ns.GetTypeMembers()
                .Where(t =>
                    t.TypeKind == TypeKind.Class
                    && t.BaseType?.Name != "Enum"
                    && t.DeclaredAccessibility == Accessibility.Public
                    && !t.IsStatic)
                .ToList();


            if (filterMembers.Count < members.Count) { 
                Console.WriteLine("Filtered members:");

                foreach (var member in members.Except(filterMembers))
                {
                    bool isClass = member.TypeKind == TypeKind.Class;
                    bool isEnum = member.BaseType?.Name != "Enum";
                    bool isPublic = member.DeclaredAccessibility == Accessibility.Public;
                    bool isStatic = member.IsStatic;
                    Console.WriteLine($"-- {member.Name} [Class: {isClass}] [Enum: {isEnum}] [Public: {isPublic}] [Static: {isStatic}]");
                }
            }

            // 1. Get Top-Level Types
            return filterMembers;
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

        // generate the props file for EsapiService
        static void GenerateProjectProps(string solutionRoot, List<string> dllPaths)
        {
            string propsPath = Path.Combine(solutionRoot, "EsapiReferences.generated.props");

            using (var writer = new StreamWriter(propsPath))
            {
                writer.WriteLine("<Project xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">");
                writer.WriteLine("  <ItemGroup>");

                foreach (var dll in dllPaths)
                {
                    string fileName = Path.GetFileNameWithoutExtension(dll);
                    // Create a relative path from the solution root
                    // Assuming libs is at solution level
                    string relPath = $"$(MSBuildThisFileDirectory)libs\\{Path.GetFileName(dll)}";

                    writer.WriteLine($"    <Reference Include=\"{fileName}\">");
                    writer.WriteLine($"      <HintPath>{relPath}</HintPath>");
                    writer.WriteLine("    </Reference>");
                }

                writer.WriteLine("  </ItemGroup>");
                writer.WriteLine("</Project>");
            }
            Console.WriteLine($"Generated MSBuild props: {propsPath}");
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
                Console.WriteLine(" CRITICAL: Could not find namespace 'VMS.TPS.Common.Model'");
                return;
            }

            // 4. Print Tree
            PrintNamespaceTree(rootNs, 0);

            Console.WriteLine("\n=== SCAN COMPLETE ===");
        }

        static void PrintNamespaceTree(INamespaceSymbol ns, int indent)
        {
            string pad = new string(' ', indent * 2);
            Console.WriteLine($"{pad} Namespace: {ns.Name}");

            // 1. Print Types in this Namespace
            foreach (var type in ns.GetTypeMembers().OrderBy(t => t.Name))
            {

                // COMMENT THIS OUT so we see everything in the scan
                // if (!IsExportable(type)) continue; 

                PrintType(type, indent + 1);
            }

            // 2. Recurse into Child Namespaces
            foreach (var childNs in ns.GetNamespaceMembers().OrderBy(n => n.Name))
            {
                PrintNamespaceTree(childNs, indent + 1);
            }
        }

        static void PrintType(INamedTypeSymbol type, int indent)
        {
            string pad = new string(' ', indent * 2);

            // --- DIAGNOSTIC CHECK ---
            // Replicate the logic from GetExportableTypes exactly
            bool isClass = type.TypeKind == TypeKind.Class;
            bool isNotEnum = type.BaseType?.Name != "Enum";
            bool isPublic = type.DeclaredAccessibility == Accessibility.Public;
            bool isNotStatic = !type.IsStatic;

            bool shouldExport = isClass && isNotEnum && isPublic && isNotStatic;

            string status = shouldExport ? "[INCLUDE]" : "[SKIP]";
            Console.WriteLine($"{pad}{status} {type.Name} ({type.TypeKind})");

            // If it's Department (or any skipped item), tell us WHY
            if (type.Name == "Department" || !shouldExport)
            {
                Console.WriteLine($"{pad}    Debug Info:");
                Console.WriteLine($"{pad}      - IsClass: {isClass} ({type.TypeKind})");
                Console.WriteLine($"{pad}      - IsNotEnum: {isNotEnum} (Base: {type.BaseType?.Name})");
                Console.WriteLine($"{pad}      - IsPublic: {isPublic} ({type.DeclaredAccessibility})");
                Console.WriteLine($"{pad}      - IsNotStatic: {isNotStatic}");
                Console.WriteLine($"{pad}      - Namespace: {type.ContainingNamespace}");
                Console.WriteLine($"{pad}      - Assembly: {type.ContainingAssembly?.Name}");
            }

            // CHECK FOR NESTED TYPES
            var nestedTypes = type.GetTypeMembers()
                                  .OrderBy(t => t.Name);

            foreach (var nested in nestedTypes)
            {
                // Recurse
                PrintType(nested, indent + 1);
            }
        }

        static bool IsExportable(INamedTypeSymbol t) {
            return t.DeclaredAccessibility == Accessibility.Public;
        }

    }
}