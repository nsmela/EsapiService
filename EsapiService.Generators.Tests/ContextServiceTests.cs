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
    public void BuildContext_InheritedClass_PopulatesBaseInfo() {
        // Arrange: Define a Parent class and a Child class
        var code = @"
        namespace Varian.ESAPI 
        {
            public class PlanSetup {} 
            public class Beam : PlanSetup {}
        }
    ";

        var parseOptions = CSharpParseOptions.Default.WithDocumentationMode(DocumentationMode.Parse);
        var tree = CSharpSyntaxTree.ParseText(code, parseOptions);

        _compilation = CSharpCompilation.Create("TestAssembly",
            new[] { tree },
            new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });

        var planSym = GetSymbol("PlanSetup");
        var beamSym = GetSymbol("Beam");

        // CRITICAL STEP: We must add BOTH types to the collection.
        // This tells the service: "PlanSetup is a target type, so if you see something inheriting from it, Wrap the inheritance too."
        var service = new ContextService(new NamespaceCollection(new[] { planSym, beamSym }));

        // Act
        var result = service.BuildContext(beamSym);

        // Assert
        // 1. Verify the Child Class context
        Assert.That(result.Name, Is.EqualTo("Varian.ESAPI.Beam"));
        Assert.That(result.WrapperName, Is.EqualTo("AsyncBeam"));

        // 2. Verify Inheritance Info
        // Because 'PlanSetup' is in our NamespaceCollection, these should be populated:
        Assert.That(result.BaseName, Is.EqualTo("PlanSetup"));
        Assert.That(result.BaseWrapperName, Is.EqualTo("AsyncPlanSetup"));
        Assert.That(result.BaseInterface, Is.EqualTo("IPlanSetup"));
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
        var method = result.Members.OfType<VoidMethodContext>().FirstOrDefault(m => m.Name == "Calculate");
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

        // Verify the Full Collection types
        Assert.That(collectionProp.WrapperName, Is.EqualTo("System.Collections.Generic.IReadOnlyList<AsyncStructure>"));
        Assert.That(collectionProp.InterfaceName, Is.EqualTo("System.Collections.Generic.IReadOnlyList<IStructure>"));

        // 2. Verify that lists of primitives (strings) remain SimplePropertyContext
        var notesProp = result.Members.OfType<SimpleCollectionPropertyContext>().FirstOrDefault(m => m.Name == "Notes");

        Assert.That(notesProp, Is.Not.Null, "IEnumerable<string> should be detected as SimpleCollectionPropertyContext");
        Assert.That(notesProp.InnerType, Is.EqualTo("string"));
        Assert.That(notesProp.WrapperName, Is.EqualTo("System.Collections.Generic.IReadOnlyList<string>"));
        Assert.That(notesProp.InterfaceName, Is.EqualTo("System.Collections.Generic.IReadOnlyList<string>"));
    }

    [Test]
    public void BuildContext_Detects_SimpleCollections_As_IReadOnlyList() {
        // Arrange
        var tree = CSharpSyntaxTree.ParseText(@"
        using System.Collections.Generic;
        namespace Varian.ESAPI
        {
            public class PlanSetup 
            { 
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

        var planSym = GetSymbol("PlanSetup");
        var namespaceCollection = new NamespaceCollection(new[] { planSym }); // 'string' is NOT here
        var service = new ContextService(namespaceCollection);

        // Act
        var result = service.BuildContext(planSym);

        // Assert
        var notesProp = result.Members.FirstOrDefault(m => m.Name == "Notes");

        // 1. Must be a Collection Context (Not SimplePropertyContext)
        Assert.That(notesProp, Is.InstanceOf<SimpleCollectionPropertyContext>());

        // 2. Must utilize IReadOnlyList
        var ctx = (SimpleCollectionPropertyContext)notesProp;
        Assert.That(ctx.WrapperName, Is.EqualTo("System.Collections.Generic.IReadOnlyList<string>"));
    }

    [Test]
    public void BuildContext_Detects_Methods_With_OutParameters() {
        // Arrange
        var tree = CSharpSyntaxTree.ParseText(@"
        using System.Collections.Generic;
        namespace Varian.ESAPI {
            public class BrachyPlan { 
                // Matches your example
                public bool ChangeTreatmentUnit(int unit, out List<string> messages) { messages = new List<string>(); return true; }
            }
        }
    ");

        _compilation = CSharpCompilation.Create("TestAssembly",
            new[] { tree },
            new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });

        var sym = GetSymbol("BrachyPlan");
        var service = new ContextService(new NamespaceCollection(new[] { sym }));

        // Act
        var result = service.BuildContext(sym);

        // Assert
        var methodCtx = result.Members.OfType<OutParameterMethodContext>().FirstOrDefault();

        Assert.That(methodCtx, Is.Not.Null);
        Assert.That(methodCtx.Name, Is.EqualTo("ChangeTreatmentUnit"));

        // Check Tuple Signature: Should contain the bool Result AND the List<string> messages
        Assert.That(methodCtx.ReturnTupleSignature, Contains.Substring("bool Result"));
        Assert.That(methodCtx.ReturnTupleSignature, Contains.Substring("List<string> messages")); // or System.Collections.Generic.List...

        // Check Parameters
        var outParam = methodCtx.Parameters.First(p => p.Name == "messages");
        Assert.That(outParam.IsOut, Is.True);
    }

    [Test]
    public void BuildContext_Detects_Mixed_Ref_And_Out_Parameters() {
        // Arrange
        var tree = CSharpSyntaxTree.ParseText(@"
        namespace Varian.ESAPI {
            public class DoseCalculator { 
                // Mixed ref (double) and out (string)
                public bool Calculate(ref double norm, out string msg) { msg = ""; return true; }
            }
        }
    ");

        _compilation = CSharpCompilation.Create("TestAssembly",
            new[] { tree },
            new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });

        var sym = GetSymbol("DoseCalculator");
        var service = new ContextService(new NamespaceCollection(new[] { sym }));

        // Act
        var result = service.BuildContext(sym);

        // Assert
        var methodCtx = result.Members.OfType<OutParameterMethodContext>().FirstOrDefault();

        Assert.That(methodCtx, Is.Not.Null);

        // 1. Check Return Tuple Signature
        // Should contain: Result, norm (ref), and msg (out)
        string tupleSig = methodCtx.ReturnTupleSignature;
        Assert.That(tupleSig, Contains.Substring("bool Result"));
        Assert.That(tupleSig, Contains.Substring("double norm"));
        Assert.That(tupleSig, Contains.Substring("string msg"));

        // 2. Check Parameters Context
        var pNorm = methodCtx.Parameters.First(p => p.Name == "norm");
        Assert.That(pNorm.IsRef, Is.True);
        Assert.That(pNorm.IsOut, Is.False);

        var pMsg = methodCtx.Parameters.First(p => p.Name == "msg");
        Assert.That(pMsg.IsOut, Is.True);
        Assert.That(pMsg.IsRef, Is.False);
    }

    [Test]
    public void BuildContext_Extracts_XmlDocumentation_For_All_Types() {
        // Arrange: Define a class with EVERY supported member type, all documented.
        var code = @"
        using System.Collections.Generic;
        namespace Varian.ESAPI 
        {
            public class Course {}
            public class Structure {}

            /// <summary>Class Documentation</summary>
            public class PlanSetup 
            { 
                /// <summary>Simple Property Documentation</summary>
                public string Id { get; }

                /// <summary>Complex Property Documentation</summary>
                public Course Course { get; }

                /// <summary>Collection Property Documentation</summary>
                public IEnumerable<Structure> Structures { get; }

                /// <summary>Simple Collection Documentation</summary>
                public IEnumerable<string> Notes { get; }

                /// <summary>Void Method Documentation</summary>
                public void Calculate() {}

                /// <summary>Complex Method Documentation</summary>
                public Course GetCourse() => null;
            }
        }
    ";

        // Use correct ParseOptions to ensure docs are read
        var parseOptions = CSharpParseOptions.Default.WithDocumentationMode(DocumentationMode.Parse);
        var tree = CSharpSyntaxTree.ParseText(code, parseOptions);

        _compilation = CSharpCompilation.Create("TestAssembly",
            new[] { tree },
            new[] {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(IEnumerable<>).Assembly.Location)
            });

        var planSym = GetSymbol("PlanSetup");
        var courseSym = GetSymbol("Course");
        var structSym = GetSymbol("Structure");

        // Register known types so they are detected as Complex/Collection
        var service = new ContextService(new NamespaceCollection(new[] { planSym, courseSym, structSym }));

        // Act
        var result = service.BuildContext(planSym);

        // Assert
        // 1. Class
        Assert.That(result.XmlDocumentation, Contains.Substring("Class Documentation"));

        // 2. Simple Property
        var simpleProp = result.Members.OfType<SimplePropertyContext>().First();
        Assert.That(simpleProp.XmlDocumentation, Contains.Substring("Simple Property Documentation"));

        // 3. Complex Property
        var complexProp = result.Members.OfType<ComplexPropertyContext>().First();
        Assert.That(complexProp.XmlDocumentation, Contains.Substring("Complex Property Documentation"));

        // 4. Collection Property
        var colProp = result.Members.OfType<CollectionPropertyContext>().First();
        Assert.That(colProp.XmlDocumentation, Contains.Substring("Collection Property Documentation"));

        // 5. Simple Collection Property
        var simpleColProp = result.Members.OfType<SimpleCollectionPropertyContext>().First();
        Assert.That(simpleColProp.XmlDocumentation, Contains.Substring("Simple Collection Documentation"));

        // 6. Void Method
        var voidMethod = result.Members.OfType<VoidMethodContext>().First();
        Assert.That(voidMethod.XmlDocumentation, Contains.Substring("Void Method Documentation"));

        // 7. Complex Method
        var complexMethod = result.Members.OfType<ComplexMethodContext>().First();
        Assert.That(complexMethod.XmlDocumentation, Contains.Substring("Complex Method Documentation"));
    }
}