using EsapiService.Generators.Contexts;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

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
        Assert.That(result.Name, Is.EqualTo("Varian.ESAPI.PlanSetup"));
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

    [Test]
    public void BuildContext_Distinguishes_MemberTypes() {
        // Arrange
        var tree = CSharpSyntaxTree.ParseText(@"
        namespace Varian.ESAPI
        {
            public class PlanSetup { }
            public class Beam 
            { 
                public string Id { get; set; }
                public PlanSetup Plan { get; set; }
                public void Calculate(int options) { }
            }
        }
    ");

        _compilation = CSharpCompilation.Create("TestAssembly",
            new[] { tree },
            new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });

        var beamSymbol = GetSymbol("Beam");
        var planSymbol = GetSymbol("PlanSetup");

        // Inject 'PlanSetup' so it is recognized as a Complex Type
        var namespaceCollection = new NamespaceCollection(new[] { beamSymbol, planSymbol });
        var service = new ContextService(namespaceCollection);

        // Act
        var result = service.BuildContext(beamSymbol);

        // Assert
        Assert.That(result.Members.Count, Is.EqualTo(3));

        // 1. Verify Simple Property (Type check replaces .IsProperty check)
        var idProp = result.Members.OfType<SimplePropertyContext>().FirstOrDefault(m => m.Name == "Id");
        Assert.That(idProp, Is.Not.Null, "Id should be a SimplePropertyContext");
        Assert.That(idProp.Symbol, Is.EqualTo("string"));

        // 2. Verify Complex Property
        var planProp = result.Members.OfType<ComplexPropertyContext>().FirstOrDefault(m => m.Name == "Plan");
        Assert.That(planProp, Is.Not.Null, "Plan should be a ComplexPropertyContext");
        Assert.That(planProp.InterfaceName, Is.EqualTo("IPlanSetup"));

        // 3. Verify Method
        var method = result.Members.OfType<MethodContext>().FirstOrDefault(m => m.Name == "Calculate");
        Assert.That(method, Is.Not.Null, "Calculate should be a MethodContext");
    }

    [Test]
    public void BuildContext_Detects_Collections() {
        // Arrange
        var tree = CSharpSyntaxTree.ParseText(@"
        using System.Collections.Generic;
        namespace Varian.ESAPI
        {
            public class Structure { }
            
            public class PlanSetup 
            { 
                // Target: A collection of a wrapped type
                public IEnumerable<Structure> Structures { get; set; }
                
                // Control: A collection of a primitive (should remain Simple)
                public IEnumerable<string> Notes { get; set; }
            }
        }
    ");

        _compilation = CSharpCompilation.Create("TestAssembly",
            new[] { tree },
            new[] {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.Collections.Generic.IEnumerable<>).Assembly.Location)
            });

        var structureSym = GetSymbol("Structure");
        var planSym = GetSymbol("PlanSetup");

        // We only tell the service that 'Structure' is a known/wrappable type.
        var namespaceCollection = new NamespaceCollection(new[] { structureSym, planSym });
        var service = new ContextService(namespaceCollection);

        // Act
        var result = service.BuildContext(planSym);

        // Assert
        // 1. Verify we found the collection property
        var collectionProp = result.Members.OfType<CollectionPropertyContext>().FirstOrDefault(m => m.Name == "Structures");

        // THIS ASSERTION WILL FAIL (Result will be null or SimplePropertyContext)
        Assert.That(collectionProp, Is.Not.Null, "Failed to detect IEnumerable<Structure> as a CollectionPropertyContext");

        // Verify the Item details
        Assert.That(collectionProp.InnerType, Is.EqualTo("Varian.ESAPI.Structure"));
        Assert.That(collectionProp.WrapperItemName, Is.EqualTo("AsyncStructure"));
        Assert.That(collectionProp.InterfaceItemName, Is.EqualTo("IStructure"));

        // Verify the Full Collection types (Your new fields)
        Assert.That(collectionProp.WrapperName, Is.EqualTo("System.Collections.Generic.IEnumerable<AsyncStructure>"));
        Assert.That(collectionProp.InterfaceName, Is.EqualTo("System.Collections.Generic.IEnumerable<IStructure>"));

        // 2. Verify that lists of primitives (strings) remain SimplePropertyContext
        var notesProp = result.Members.OfType<SimplePropertyContext>().FirstOrDefault(m => m.Name == "Notes");
        Assert.That(notesProp, Is.Not.Null, "IEnumerable<string> should remain a SimplePropertyContext");
    }
}