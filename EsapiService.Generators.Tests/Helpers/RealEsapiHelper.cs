using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace EsapiService.Generators.Tests.Helpers;

public static class RealEsapiHelper
{
    private static Compilation _cachedCompilation;

    public static Compilation GetCompilation()
    {
        if (_cachedCompilation != null)
            return _cachedCompilation;

        // 1. Find the 'libs' folder
        string solutionRoot = FindSolutionRoot(AppContext.BaseDirectory);
        string libsFolder = Path.Combine(solutionRoot, "libs");

        if (!Directory.Exists(libsFolder))
            throw new DirectoryNotFoundException($"Could not find 'libs' folder at: {libsFolder}");

        // 2. Locate the DLLs
        string apiPath = Path.Combine(libsFolder, "VMS.TPS.Common.Model.API.dll");
        string typesPath = Path.Combine(libsFolder, "VMS.TPS.Common.Model.Types.dll");
        // Important: Include Interface/Calculation if you use them, but API/Types are minimum

        if (!File.Exists(apiPath))
            throw new FileNotFoundException("ESAPI DLLs not found. Please ensure they are in the 'libs' folder.");

        // 3. Create References
        var refs = new MetadataReference[]
        {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location), // mscorlib
                MetadataReference.CreateFromFile(apiPath),
                MetadataReference.CreateFromFile(typesPath)
        };

        // 4. Create Compilation
        _cachedCompilation = CSharpCompilation.Create("EsapiIntegrationTests")
            .WithReferences(refs);

        return _cachedCompilation;
    }

    public static ISymbol GetSymbol(string typeName, string memberName)
    {
        var comp = GetCompilation();
        var type = comp.GetTypeByMetadataName(typeName);

        if (type == null)
            throw new Exception($"Could not find type '{typeName}' in the ESAPI DLLs.");

        var member = type.GetMembers(memberName).FirstOrDefault();

        if (member == null)
            throw new Exception($"Could not find member '{memberName}' on type '{typeName}'.");

        return member;
    }

    private static string FindSolutionRoot(string startPath)
    {
        var dir = new DirectoryInfo(startPath);
        while (dir != null)
        {
            if (dir.GetDirectories("libs").Any())
                return dir.FullName;
            dir = dir.Parent;
        }
        throw new Exception("Could not locate Solution Root (containing 'libs').");
    }
}
