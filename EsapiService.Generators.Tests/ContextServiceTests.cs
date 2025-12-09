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
        Assert.That(result.BaseName, Is.EqualTo("Varian.ESAPI.PlanSetup"),
                    "BaseName should be the Fully Qualified Name to ensure type safety.");
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
        Assert.That(collectionProp.WrapperName, Is.EqualTo("IReadOnlyList<AsyncStructure>"));
        Assert.That(collectionProp.InterfaceName, Is.EqualTo("IReadOnlyList<IStructure>"));

        // 2. Verify that lists of primitives (strings) remain SimplePropertyContext
        var notesProp = result.Members.OfType<SimpleCollectionPropertyContext>().FirstOrDefault(m => m.Name == "Notes");

        Assert.That(notesProp, Is.Not.Null, "IReadOnlyList<string> should be detected as SimpleCollectionPropertyContext");
        Assert.That(notesProp.InnerType, Is.EqualTo("string"));
        Assert.That(notesProp.WrapperName, Is.EqualTo("IReadOnlyList<string>"));
        Assert.That(notesProp.InterfaceName, Is.EqualTo("IReadOnlyList<string>"));
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
            MetadataReference.CreateFromFile(typeof(IEnumerable<>).Assembly.Location)
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
        Assert.That(ctx.WrapperName, Is.EqualTo("IReadOnlyList<string>"));
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

    [Test]
    public void BuildContext_Excludes_Overrides_When_Base_Is_Wrapped() {
        // Arrange
        var code = @"
        namespace Varian.ESAPI 
        {
            public class BaseClass 
            { 
                public virtual string Id { get; } 
            }
            
            public class DerivedClass : BaseClass 
            { 
                // This override should be skipped in the Derived wrapper
                // because BaseWrapper will already expose 'Id'.
                public override string Id { get; } 
                
                // This unique property should be kept
                public string UniqueProp { get; }
            }
        }
    ";

        var parseOptions = CSharpParseOptions.Default.WithDocumentationMode(DocumentationMode.Parse);
        var tree = CSharpSyntaxTree.ParseText(code, parseOptions);
        var compilation = CSharpCompilation.Create("TestAssembly", new[] { tree },
            new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });

        var baseSym = compilation.GetSymbolsWithName("BaseClass").OfType<INamedTypeSymbol>().First();
        var derivedSym = compilation.GetSymbolsWithName("DerivedClass").OfType<INamedTypeSymbol>().First();

        // Wrap BOTH
        var service = new ContextService(new NamespaceCollection(new[] { baseSym, derivedSym }));

        // Act
        var context = service.BuildContext(derivedSym);

        // Assert
        // 1. Should have UniqueProp
        Assert.That(context.Members.Any(m => m.Name == "UniqueProp"), Is.True);

        // 2. Should NOT have Id (inherited/overridden)
        Assert.That(context.Members.Any(m => m.Name == "Id"), Is.False, "Override 'Id' should be excluded because BaseClass is wrapped.");
    }

    [Test]
    public void BuildContext_Converts_MethodParameter_Types_To_Interfaces() {
        // Arrange
        var code = @"
        using System.Collections.Generic;
        namespace Varian.ESAPI {
            public class Structure {}
            public class PlanSetup { 
                // Method with 1. Direct Wrapped Param, 2. List of Wrapped Params
                public void AddStructure(Structure s, List<Structure> list) {} 
            }
        }
    ";

        var parseOptions = CSharpParseOptions.Default.WithDocumentationMode(DocumentationMode.Parse);
        var tree = CSharpSyntaxTree.ParseText(code, parseOptions);
        var compilation = CSharpCompilation.Create("TestAssembly", new[] { tree },
            new[] {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(List<>).Assembly.Location)
            });

        var structureSym = compilation.GetSymbolsWithName("Structure").OfType<INamedTypeSymbol>().First();
        var planSym = compilation.GetSymbolsWithName("PlanSetup").OfType<INamedTypeSymbol>().First();

        // Wrap both so detection works
        var service = new ContextService(new NamespaceCollection(new[] { structureSym, planSym }));

        // Act
        var context = service.BuildContext(planSym);
        var method = context.Members.OfType<VoidMethodContext>().First(m => m.Name == "AddStructure");

        // Assert
        // Current State (Fail): Likely returns "Structure s, List<Structure> list"
        // Expected State (Pass): "IStructure s, IReadOnlyList<IStructure> list"

        // Check Signature contains the Interface types
        Assert.That(method.Signature, Contains.Substring("IStructure s"));
        Assert.That(method.Signature, Contains.Substring("IReadOnlyList<IStructure> list"));
    }

    [Test]
    public void BuildContext_Simplifies_Verbose_Namespaces() {
        // Arrange
        var code = @"
        using System.Collections.Generic;
        namespace Varian.ESAPI {
            public class PlanSetup { 
                // Only testing Collection simplification now
                public IEnumerable<string> Notes { get; }
            }
        }
    ";

        var parseOptions = CSharpParseOptions.Default.WithDocumentationMode(DocumentationMode.Parse);
        var tree = CSharpSyntaxTree.ParseText(code, parseOptions);

        // FIX: Add references so Roslyn sees IEnumerable<T> as a Generic Type
        var references = new[] {
        MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        MetadataReference.CreateFromFile(typeof(System.Collections.Generic.IEnumerable<>).Assembly.Location),
        MetadataReference.CreateFromFile(typeof(System.Linq.Enumerable).Assembly.Location)
    };

        var compilation = CSharpCompilation.Create("TestAssembly", new[] { tree }, references);

        var planSym = compilation.GetSymbolsWithName("PlanSetup").OfType<INamedTypeSymbol>().First();
        var service = new ContextService(new NamespaceCollection(new[] { planSym }));

        // Act
        var context = service.BuildContext(planSym);

        // Assert
        // Check List Simplification (Should strip System.Collections.Generic.)
        var notes = context.Members.OfType<SimpleCollectionPropertyContext>().First();

        Assert.That(notes.InterfaceName, Is.EqualTo("IReadOnlyList<string>"));
    }

    [Test]
    public void BuildContext_Excludes_Members_Existing_In_Base_Wrapper() {
        // Arrange
        var code = @"
        namespace Varian.ESAPI {
            public class ApiDataObject {
                public string Id { get; set; }
            }

            // StructureSet inherits Id. 
            // Even if it overrides it or shadows it, we want to skip it 
            // because the Base Wrapper (AsyncApiDataObject) will handle 'Id'.
            public class StructureSet : ApiDataObject {
                public new string Id { get; set; } // Shadowing/Overriding
                public string UniqueField { get; set; }
            }
        }
    ";

        var parseOptions = CSharpParseOptions.Default.WithDocumentationMode(DocumentationMode.Parse);
        var tree = CSharpSyntaxTree.ParseText(code, parseOptions);
        var compilation = CSharpCompilation.Create("TestAssembly", new[] { tree },
            new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });

        var baseSym = compilation.GetSymbolsWithName("ApiDataObject").OfType<INamedTypeSymbol>().First();
        var derivedSym = compilation.GetSymbolsWithName("StructureSet").OfType<INamedTypeSymbol>().First();

        // Critical: BOTH must be known types
        var service = new ContextService(new NamespaceCollection(new[] { baseSym, derivedSym }));

        // Act
        var context = service.BuildContext(derivedSym);

        // Assert
        // 1. Should contain the unique field
        Assert.That(context.Members.Any(m => m.Name == "UniqueField"), Is.True);

        // 2. Should NOT contain 'Id' (Fail: It currently includes it)
        Assert.That(context.Members.Any(m => m.Name == "Id"), Is.False,
            "Member 'Id' should be excluded because it exists in the wrapped base class.");
    }

    [Test]
    public void BuildContext_Detects_ReadWrite_Access_For_SimpleProperties() {
        // Arrange
        var code = @"
        namespace Varian.ESAPI {
            public class PlanSetup { 
                // Target: Has a setter
                public string Comment { get; set; }
                
                // Control: Read Only
                public string Id { get; }
            }
        }
    ";

        // ... (Standard Setup) ...
        var parseOptions = CSharpParseOptions.Default.WithDocumentationMode(DocumentationMode.Parse);
        var tree = CSharpSyntaxTree.ParseText(code, parseOptions);
        var compilation = CSharpCompilation.Create("TestAssembly", new[] { tree },
            new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });

        var sym = compilation.GetSymbolsWithName("PlanSetup").OfType<INamedTypeSymbol>().First();
        var service = new ContextService(new NamespaceCollection(new[] { sym }));

        // Act
        var result = service.BuildContext(sym);

        // Assert
        var comment = result.Members.OfType<SimplePropertyContext>().First(m => m.Name == "Comment");
        var id = result.Members.OfType<SimplePropertyContext>().First(m => m.Name == "Id");

        Assert.That(comment.IsReadOnly, Is.False, "Public Setter should result in IsReadOnly = false");
        Assert.That(id.IsReadOnly, Is.True, "No Setter should result in IsReadOnly = true");
    }

    [Test]
    public void BuildContext_Treats_Nullable_As_SimpleProperty_Not_Collection() {
        // Arrange
        var code = @"
        using System;
        using System.Collections.Generic;
        namespace Varian.ESAPI {
            public class Patient { 
                public DateTime? DateOfBirth { get; set; }
                public List<string> History { get; set; }
            }
        }
    ";

        var parseOptions = CSharpParseOptions.Default.WithDocumentationMode(DocumentationMode.Parse);
        var tree = CSharpSyntaxTree.ParseText(code, parseOptions);

        var references = new[] {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.Collections.Generic.IEnumerable<>).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.Linq.Enumerable).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.DateTime).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.Nullable<>).Assembly.Location)
        };

        // FIX: Add CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        var compilation = CSharpCompilation.Create("TestAssembly",
            new[] { tree },
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)); // <--- THIS FIXES THE ERROR

        // Debug: Check for compilation errors
        var diags = compilation.GetDiagnostics();
        if (diags.Any(d => d.Severity == DiagnosticSeverity.Error)) {
            Assert.Fail($"Test Compilation Error: {diags.First().GetMessage()}");
        }

        var sym = compilation.GetSymbolsWithName("Patient").OfType<INamedTypeSymbol>().First();
        var service = new ContextService(new NamespaceCollection(new[] { sym }));

        // Act
        var result = service.BuildContext(sym);

        // Assert
        var dob = result.Members.FirstOrDefault(m => m.Name == "DateOfBirth");

        // This will FAIL if the ContextService fix is missing
        Assert.That(dob, Is.InstanceOf<SimplePropertyContext>(),
            "DateTime? should be treated as a Simple Property, but was detected as a Collection.");
    }

    [Test]
    public void BuildContext_Captures_OriginalSignature_For_Mocks() {
        // Arrange
        var code = @"
        namespace Varian.ESAPI {
            public class Structure {}
            public class PlanSetup { 
                // Target: Method with a wrapped parameter.
                // The Generator needs two versions of this signature:
                // 1. For Wrapper/Interface: (IStructure s)
                // 2. For Mocks: (Structure s)
                public void AddStructure(Structure s) {} 
            }
        }
        ";

        var parseOptions = CSharpParseOptions.Default.WithDocumentationMode(DocumentationMode.Parse);
        var tree = CSharpSyntaxTree.ParseText(code, parseOptions);
        var compilation = CSharpCompilation.Create("TestAssembly", new[] { tree },
            new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });

        var structSym = compilation.GetSymbolsWithName("Structure").OfType<INamedTypeSymbol>().First();
        var planSym = compilation.GetSymbolsWithName("PlanSetup").OfType<INamedTypeSymbol>().First();

        var service = new ContextService(new NamespaceCollection(new[] { structSym, planSym }));

        // Act
        var context = service.BuildContext(planSym);
        var method = context.Members.OfType<VoidMethodContext>().First(m => m.Name == "AddStructure");

        // Assert
        // 1. Wrapper Signature (Existing behavior)
        Assert.That(method.Signature, Contains.Substring("IStructure s"),
            "Wrapper signature (Signature property) must use Interface types.");

        // 2. Mock Signature (New requirement)
        // Note: This asserts that you have added the 'OriginalSignature' property to your Context records.
        Assert.That(method.OriginalSignature, Contains.Substring("Structure s"),
            "Mock signature (OriginalSignature property) must use Concrete types.");

        Assert.That(method.OriginalSignature, Does.Not.Contain("IStructure"),
            "Mock signature should not use Interface types.");
    }

    [Test]
    public void BuildContext_DoesNotMangle_SystemWindows_Namespaces() {
        // Arrange
        var code = @"
        namespace Varian.ESAPI {
            public class Image { 
                // Target: A type in System.Windows.Media (referenced in ESAPI)
                // BUG: SimplifyTypeString() was turning 'System.Windows.Media.Color' into 'Windows.Media.Color'
                // FIX: It should remain 'System.Windows.Media.Color' (or 'Color' if we handled imports logic here, but safety first).
                public System.Windows.Media.Color PixelColor { get; set; }
            }
        }
        namespace System.Windows.Media { public struct Color {} }
        ";

        var parseOptions = CSharpParseOptions.Default.WithDocumentationMode(DocumentationMode.Parse);
        var tree = CSharpSyntaxTree.ParseText(code, parseOptions);

        var references = new[] {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        };

        var compilation = CSharpCompilation.Create("TestAssembly", new[] { tree }, references);
        var sym = compilation.GetSymbolsWithName("Image").OfType<INamedTypeSymbol>().First();
        var service = new ContextService(new NamespaceCollection(new[] { sym }));

        // Act
        var result = service.BuildContext(sym);

        // Assert
        var prop = result.Members.OfType<SimplePropertyContext>().First(m => m.Name == "PixelColor");

        // We accept either the Fully Qualified Name (Safe) or the Short Name (Clean)
        // We REJECT the Broken Name ("Windows.Media.Color")
        Assert.That(prop.Symbol, Is.Not.EqualTo("Windows.Media.Color"),
            "The generator created an invalid namespace 'Windows.Media...' by stripping 'System.' incorrectly.");

        Assert.That(prop.Symbol, Is.EqualTo("System.Windows.Media.Color"),
            "The generator should preserve the full namespace for types it doesn't explicitly simplify.");
    }

    [Test]
    public void BuildContext_Captures_All_Property_Types_Including_Nullable() {
        // Arrange
        // We define a class with a mix of types to ensure nothing falls through the cracks.
        var code = @"
        using System;
        using System.Collections.Generic;
        namespace Varian.ESAPI {
            public class Inventory { 
                // 1. Simple Reference Type
                public string Name { get; set; }

                // 2. Simple Value Type
                public int Count { get; set; }

                // 3. Nullable Value Type (The suspected missing item)
                public DateTime? ExpirationDate { get; set; }

                // 4. Collection
                public List<string> Tags { get; set; }
            }
        }
        ";

        var parseOptions = CSharpParseOptions.Default.WithDocumentationMode(DocumentationMode.Parse);
        var tree = CSharpSyntaxTree.ParseText(code, parseOptions);

        var references = new[] {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.Collections.Generic.IEnumerable<>).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.Linq.Enumerable).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.DateTime).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.Nullable<>).Assembly.Location)
        };

        var compilation = CSharpCompilation.Create("TestAssembly", new[] { tree }, references);
        var sym = compilation.GetSymbolsWithName("Inventory").OfType<INamedTypeSymbol>().First();
        var service = new ContextService(new NamespaceCollection(new[] { sym }));

        // Act
        var result = service.BuildContext(sym);

        // Assert
        var memberNames = result.Members.Select(m => m.Name).ToList();

        // Check 1: Ensure all expected properties are present
        Assert.That(memberNames, Contains.Item("Name"), "Missing Simple String property");
        Assert.That(memberNames, Contains.Item("Count"), "Missing Simple Int property");
        Assert.That(memberNames, Contains.Item("Tags"), "Missing List property");

        // Check 2: The Critical Regression
        Assert.That(memberNames, Contains.Item("ExpirationDate"),
            "FAILURE: The 'DateTime?' property was completely excluded from the context. Check the if/else logic in GetMembers.");

        // Check 3: Verify it was categorized correctly (if it exists)
        var dateProp = result.Members.FirstOrDefault(m => m.Name == "ExpirationDate");
        if (dateProp != null) {
            Assert.That(dateProp, Is.InstanceOf<SimplePropertyContext>(),
                "ExpirationDate should be categorized as SimplePropertyContext.");
        }


    }

    [Test]
    public void BuildContext_WalksInheritanceChain_ToFindValidBase() {
        // Arrange
        var code = @"
        namespace Varian.ESAPI {
            public class ApiDataObject {} 
            internal class HiddenIntermediate : ApiDataObject {} // Generator skips this
            public class Image : HiddenIntermediate {} // Target
        }
        ";

        var parseOptions = CSharpParseOptions.Default.WithDocumentationMode(DocumentationMode.Parse);
        var tree = CSharpSyntaxTree.ParseText(code, parseOptions);
        var compilation = CSharpCompilation.Create("TestAssembly", new[] { tree },
            new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });

        var apiSym = compilation.GetSymbolsWithName("ApiDataObject").OfType<INamedTypeSymbol>().First();
        var imgSym = compilation.GetSymbolsWithName("Image").OfType<INamedTypeSymbol>().First();

        // We only tell the service about the Public types (skipping HiddenIntermediate)
        var service = new ContextService(new NamespaceCollection(new[] { apiSym, imgSym }));

        // Act
        var result = service.BuildContext(imgSym);

        // Assert
        Assert.That(result.BaseWrapperName, Is.EqualTo("AsyncApiDataObject"),
            "ContextService should have skipped 'HiddenIntermediate' and found 'AsyncApiDataObject' as the base.");
    }
}