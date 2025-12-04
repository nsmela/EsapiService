using EsapiService.Generators;
using EsapiService.Generators.Contexts;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NUnit.Framework;
using System.Collections.Immutable;
using System.Linq;

namespace EsapiService.Generators.Tests;

public class ContextServiceTests
{
    private Compilation _compilation;

    [OneTimeSetUp]
    public void SetupCompilation()
    {
        // Create a fake compilation to generate symbols for testing
        var tree = CSharpSyntaxTree.ParseText(@"
                namespace Varian.ESAPI
                {
                    public class PlanSetup : PlanningItem { }
                    public class Beam : PlanSetup { }
                }
            ");

        _compilation = CSharpCompilation.Create("TestAssembly",
            new[] { tree },
            new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });
    }

    private INamedTypeSymbol GetSymbol(string name) =>
        _compilation.GetSymbolsWithName(name).OfType<INamedTypeSymbol>().First();

    [Test]
    public void BuildContext_SimpleClass_ReturnsCorrectNames()
    {
        // Arrange
        var planSetupSymbol = GetSymbol("PlanSetup");
        var namespaceCollection = new NamespaceCollection([planSetupSymbol]);
        // Note: In real logic we need to inject 'PlanSetup' into NamespaceCollection 
        // but your current implementation of IsContained is based on a private list.
        // We might need to refactor NamespaceCollection to be testable, 
        // but for now let's assume we can reflect or constructor inject (see Refactor below).

        // For this test, we need a Mockable or Testable NamespaceCollection.
        // Let's assume we refactor NamespaceCollection to verify containment.

        var service = new ContextService(namespaceCollection);

        // Act
        var result = service.BuildContext(planSetupSymbol);

        // Assert
        Assert.That(result.Name, Is.EqualTo("global::Varian.ESAPI.PlanSetup"));
        Assert.That(result.InterfaceName, Is.EqualTo("IPlanSetup"));
        Assert.That(result.WrapperName, Is.EqualTo("AsyncPlanSetup"));
        Assert.That(result.IsAbstract, Is.False);
    }

    [Test]
    public void BuildContext_InheritedClass_PopulatesBaseInfo()
    {
        // Arrange
        var beamSymbol = GetSymbol("Beam");

        // We need to simulate that PlanSetup is a "Known Type"
        // Since _namedTypes is private in your code, we can't add to it without refactoring.
        // THIS HIGHLIGHTS A DESIGN ISSUE FOR TDD.
    }
}