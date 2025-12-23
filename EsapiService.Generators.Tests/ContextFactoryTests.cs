using EsapiService.Generators.Contexts;
using EsapiService.Generators.Contexts.ContextFactory;
using EsapiService.Generators.Tests.Helpers;
using Microsoft.CodeAnalysis;
using NUnit.Framework;
using System.Reflection.PortableExecutable;

namespace EsapiService.Generators.Tests
{
    [TestFixture]
    public class RealFactoryTests
    {
        private CompilationSettings _settings;

        [SetUp]
        public void Setup()
        {
            // Configure settings to mimic production (whitelist 'Course')
            var comp = RealEsapiHelper.GetCompilation();
            var courseSym = comp.GetTypeByMetadataName("VMS.TPS.Common.Model.API.Course");

            _settings = new CompilationSettings(
                new NamespaceCollection(new[] { courseSym }), // Whitelist Course
                new DefaultNamingStrategy()
            );
        }

        [Test]
        public void Integration_Patient_Courses_ShouldBe_Collection()
        {
            // 1. Get REAL Symbol from DLL
            // public IEnumerable<Course> Courses { get; }
            var symbol = RealEsapiHelper.GetSymbol("VMS.TPS.Common.Model.API.Patient", "Courses");

            // 2. Test Collection Factory (Should ACCEPT)
            var colFactory = new CollectionPropertyFactory();
            var colResult = colFactory.Create(symbol, _settings).FirstOrDefault();

            Assert.That(colResult, Is.Not.Null,
                "CollectionPropertyFactory FAILED to recognize real 'Patient.Courses' as a collection.");

            var ctx = colResult as CollectionPropertyContext;
            Assert.That(ctx.InnerType, Is.EqualTo("Course"));
            Assert.That(ctx.WrapperName, Does.Contain("IReadOnlyList"), "Should be wrapped as a list.");
        }

        [Test]
        public void Integration_Patient_Courses_ShouldReject_SimpleProperty()
        {
            // 1. Get REAL Symbol
            var symbol = RealEsapiHelper.GetSymbol("VMS.TPS.Common.Model.API.Patient", "Courses");

            // 2. Test Simple Factory (Should REJECT)
            // If this fails (returns result), it means the SimplePropertyFactory is "stealing" the property
            // because it doesn't realize it's a collection.
            var simpleFactory = new SimplePropertyFactory();
            var simpleResults = simpleFactory.Create(symbol, _settings).ToList();

            Assert.That(simpleResults, Is.Empty,
                "SimplePropertyFactory INCORRECTLY claimed 'Patient.Courses' as a simple scalar property.");
        }

        [Test]
        public void Integration_Patient_Id2_ShouldBe_SimpleProperty()
        {
            // Control Test: Verify normal properties still work
            var symbol = RealEsapiHelper.GetSymbol("VMS.TPS.Common.Model.API.Patient", "Id2");

            var factory = new SimplePropertyFactory();
            var result = factory.Create(symbol, _settings).FirstOrDefault();

            Assert.That(result, Is.Not.Null);
            Assert.That(((SimplePropertyContext)result).Name, Is.EqualTo("Id2"));
        }

        [Test]
        public void AsyncPlanSetup_Inherits_Correctly_And_Excludes_Base_Properties()
        {
            // 1. Arrange
            // Get the class symbol
            var apiObjSym = RealEsapiHelper.GetSymbol("VMS.TPS.Common.Model.API.ApiDataObject");
            var baseSym = RealEsapiHelper.GetSymbol("VMS.TPS.Common.Model.API.PlanningItem");
            var derivedSym = RealEsapiHelper.GetSymbol("VMS.TPS.Common.Model.API.PlanSetup");

            // We assume the service is set up to whitelist PlanSetup
            // Note: In a real run, you'd likely have PlanningItem whitelisted too.
            var settings = new CompilationSettings(
                new NamespaceCollection(new[] { apiObjSym, baseSym, derivedSym }), // Whitelist both
                new DefaultNamingStrategy()
            );

            var service = new ContextService(settings.NamedTypes, settings);

            // 2. Act
            var context = service.BuildContext(derivedSym);

            // 3. Assert - Inheritance
            // Expect: AsyncPlanSetup : AsyncPlanningItem
            Assert.That(context.BaseName, Does.Contain("PlanningItem"),
                "AsyncPlanSetup should inherit from PlanningItem.");

            // 4. Assert - Base Member Exclusion
            var idMember = context.Members.FirstOrDefault(m => m.Name == "Id") as SimplePropertyContext;

            Assert.That(idMember, Is.Not.Null,
                "The 'Id' member should be a SimplePropertyContext");

            Assert.That(idMember.IsShadowing,
                "The 'Id' property is inherited, so it should NOT be in the derived wrapper's member list.");
        }

        [Test]
        public void ContextService_Detects_Shadowing_And_Flags_It()
        {
            // 1. Arrange - use a Class that definitely shadows
            var apiObjSym = RealEsapiHelper.GetSymbol("VMS.TPS.Common.Model.API.ApiDataObject");
            var baseSym = RealEsapiHelper.GetSymbol("VMS.TPS.Common.Model.API.PlanningItem");
            var derivedSym = RealEsapiHelper.GetSymbol("VMS.TPS.Common.Model.API.PlanSetup");

            var settings = new CompilationSettings(
                new NamespaceCollection(new[] { derivedSym, baseSym, apiObjSym }),
                new DefaultNamingStrategy()
            );

            var service = new ContextService(settings.NamedTypes, settings);

            // 2. Act
            var context = service.BuildContext(derivedSym);

            // 3. Assert
            var valueMember = context.Members.FirstOrDefault(m => m.Name == "Id");

            Assert.That(valueMember, Is.Not.Null, "The shadowed property MUST be captured.");

            // Check if the property context knows it is shadowing
            // (You may need to cast to the specific property type, e.g. SimplePropertyContext)
            dynamic dynamicMember = valueMember;
            Assert.That(dynamicMember.IsShadowing, Is.True, "The context should have IsShadowing = true");
        }
    }

    [TestFixture]
    public class ContextFactoryTests
    {
        private CompilationSettings _settings;

        [SetUp]
        public void Setup()
        {
            // Empty settings by default (nothing wrapped)
            _settings = new CompilationSettings(
                new NamespaceCollection(new List<INamedTypeSymbol>()),
                new DefaultNamingStrategy());
        }

        [Test]
        public void UnknownTypeFactory_Allows_NestedStructs_PassThrough()
        {
            // Arrange
            var code = @"
using System.Collections.Generic;
namespace VMS.TPS.Common.Model.API {
    public class Calculation {
        public struct Algorithm { public string Name; }
        public List<Algorithm> GetInstalledAlgorithms() => null;
    }
}";
            // Get the 'GetInstalledAlgorithms' method symbol
            var compilation = TestHelper.CreateCompilation(code);
            var classSym = compilation.GetTypeByMetadataName("VMS.TPS.Common.Model.API.Calculation");
            var methodSym = classSym.GetMembers("GetInstalledAlgorithms").First();

            var factory = new UnknownTypeFactory();

            // Act
            // Algorithm is NOT in _settings (whitelist). 
            // We expect the factory to return NOTHING (meaning it passed the check), 
            // NOT a SkippedMemberContext.
            var results = factory.Create(methodSym, _settings).ToList();

            // Assert
            Assert.That(results, Is.Empty, "Should not produce a SkippedMemberContext for nested structs.");
        }

        [Test]
        public void OutParameterMethodFactory_Detects_OutParams()
        {
            // Arrange
            var code = @"
namespace VMS.TPS.Common.Model.API {
    public class PlanSetup {
        public bool GetValue(out string val) { val = """"; return true; }
    }
}";
            var methodSym = TestHelper.GetSymbol(code, "GetValue");
            var factory = new OutParameterMethodFactory();

            // Act
            var context = factory.Create(methodSym, _settings).FirstOrDefault() as OutParameterMethodContext;

            // Assert
            Assert.That(context, Is.Not.Null);
            Assert.That(context.Name, Is.EqualTo("GetValue"));
            Assert.That(context.ReturnTupleSignature, Does.Contain("(bool result, string val)").IgnoreCase); // Loose string check
        }

        [Test]
        public void SimpleCollectionMethodFactory_Handles_GenericList()
        {
            // Arrange
            var code = @"
using System.Collections.Generic;
namespace VMS.TPS.Common.Model.API {
    public class PlanSetup {
        public IEnumerable<string> GetHistory() => new List<string>();
    }
}";
            var methodSym = TestHelper.GetSymbol(code, "GetHistory");
            var factory = new SimpleCollectionMethodFactory();

            // Act
            var context = factory.Create(methodSym, _settings).FirstOrDefault() as SimpleCollectionMethodContext;

            // Assert
            Assert.That(context, Is.Not.Null);
            Assert.That(context.InterfaceName, Is.EqualTo("IReadOnlyList<string>"));
        }

        [Test]
        public void CollectionPropertyFactory_Rejects_Arrays()
        {
            // Arrange: Arrays should be handled by SimplePropertyFactory, not CollectionPropertyFactory
            var code = @"
namespace VMS.TPS.Common.Model.API {
    public class Registration {
        public double[,] TransformationMatrix { get; }
    }
}";
            var propSym = TestHelper.GetSymbol(code, "TransformationMatrix");
            var factory = new SimpleCollectionPropertyFactory(); // or CollectionPropertyFactory

            // Act
            var results = factory.Create(propSym, _settings).ToList();

            // Assert
            Assert.That(results, Is.Empty, "Collection factory should ignore arrays (they are Simple Properties).");
        }

        [Test]
        public void CollectionPropertyFactory_Claims_Direct_IEnumerable_Of_WrappedType() {
            // Scenario: public IEnumerable<Course> Courses { get; }
            // Expected: CollectionPropertyFactory should claim this.
            // Failure Mode: If IsCollection() only checks interfaces, it returns false for IEnumerable itself,
            //               causing this factory to skip it (and SimplePropertyFactory to wrongly grab it later).

            // Arrange
            var code = @"
using System.Collections.Generic;
namespace VMS.TPS.Common.Model.API {
    public class Course {}
    public class Patient {
        public IEnumerable<Course> Courses { get; }
    }
}";
            var compilation = TestHelper.CreateCompilation(code);
            var courseSym = compilation.GetTypeByMetadataName("VMS.TPS.Common.Model.API.Course");
            var patientSym = compilation.GetTypeByMetadataName("VMS.TPS.Common.Model.API.Patient");
            var propSym = patientSym.GetMembers("Courses").First();

            // Whitelist Course
            var settings = new CompilationSettings(
                new NamespaceCollection(new[] { courseSym }),
                new DefaultNamingStrategy());

            var factory = new CollectionPropertyFactory();

            // Act
            var context = factory.Create(propSym, settings).FirstOrDefault();

            // Assert
            Assert.That(context, Is.Not.Null, "CollectionPropertyFactory failed to claim 'IEnumerable<Wrapped>'.");
            Assert.That(context, Is.InstanceOf<CollectionPropertyContext>());

            var ctx = (CollectionPropertyContext)context;
            Assert.That(ctx.InnerType, Is.EqualTo("Course"));
        }

        [Test]
        public void CollectionPropertyFactory_Claims_List_Of_WrappedType() {
            // Scenario: public List<Course> Courses { get; }
            // This usually works because List<T> implements IEnumerable<T>, so AllInterfaces finds it.
            // We test this to confirm the logic works for implementations but fails for the interface itself.

            // Arrange
            var code = @"
using System.Collections.Generic;
namespace VMS.TPS.Common.Model.API {
    public class Course {}
    public class Patient {
        public List<Course> Courses { get; }
    }
}";
            var compilation = TestHelper.CreateCompilation(code);
            var courseSym = compilation.GetTypeByMetadataName("VMS.TPS.Common.Model.API.Course");
            var patientSym = compilation.GetTypeByMetadataName("VMS.TPS.Common.Model.API.Patient");
            var propSym = patientSym.GetMembers("Courses").First();

            var settings = new CompilationSettings(
                new NamespaceCollection(new[] { courseSym }),
                new DefaultNamingStrategy());

            var factory = new CollectionPropertyFactory();

            // Act
            var context = factory.Create(propSym, settings).FirstOrDefault();

            // Assert
            Assert.That(context, Is.Not.Null, "CollectionPropertyFactory should claim 'List<Wrapped>'.");
            Assert.That(context, Is.InstanceOf<CollectionPropertyContext>());
        }

        [Test]
        public void SimplePropertyFactory_Rejects_IEnumerable_Of_WrappedType() {
            // The "Negative" Test. 
            // If CollectionPropertyFactory skips it, SimplePropertyFactory MUST ALSO skip it.
            // If this fails (returns a context), it proves SimplePropertyFactory is the one generating the bad code.

            // Arrange
            var code = @"
using System.Collections.Generic;
namespace VMS.TPS.Common.Model.API {
    public class Course {}
    public class Patient {
        public IEnumerable<Course> Courses { get; }
    }
}";
            var compilation = TestHelper.CreateCompilation(code);
            var courseSym = compilation.GetTypeByMetadataName("VMS.TPS.Common.Model.API.Course");
            var patientSym = compilation.GetTypeByMetadataName("VMS.TPS.Common.Model.API.Patient");
            var propSym = patientSym.GetMembers("Courses").First();

            var settings = new CompilationSettings(
                new NamespaceCollection(new[] { courseSym }),
                new DefaultNamingStrategy());

            var factory = new SimplePropertyFactory();

            // Act
            var results = factory.Create(propSym, settings).ToList();

            // Assert
            Assert.That(results, Is.Empty, "SimplePropertyFactory improperly claimed a Collection!");
        }

        [Test]
        public void IgnoredNameFactory_Skips_Standard_Object_Methods()
        {
            // Arrange
            var code = @"
namespace VMS.TPS.Common.Model.API {
    public class TestClass {
        public override string ToString() => """";
        public override int GetHashCode() => 0;
        public new Type GetType() => null;
    }
}";
            var compilation = TestHelper.CreateCompilation(code);
            var classSym = compilation.GetTypeByMetadataName("VMS.TPS.Common.Model.API.TestClass");
            var factory = new IgnoredNameFactory();

            foreach (var methodName in new[] { "ToString", "GetHashCode", "GetType" })
            {
                var methodSym = classSym.GetMembers(methodName).First();

                // Act
                var result = factory.Create(methodSym, _settings).First();

                // Assert
                Assert.That(result, Is.InstanceOf<SkippedMemberContext>());
                Assert.That(((SkippedMemberContext)result).Reason, Does.Contain("Explicitly ignored"));
            }
        }

        [Test]
        public void IndexerFactory_Detects_Standard_Indexer()
        {
            // Arrange
            var code = @"
namespace VMS.TPS.Common.Model.API {
    public class PlanSetup {}
    public class PlanCollection {
        public PlanSetup this[int index] => null;
    }
}";
            var compilation = TestHelper.CreateCompilation(code);
            var planSym = compilation.GetTypeByMetadataName("VMS.TPS.Common.Model.API.PlanSetup");
            var classSym = compilation.GetTypeByMetadataName("VMS.TPS.Common.Model.API.PlanCollection");
            var indexerSym = classSym.GetMembers("this[]").First();

            var settings = new CompilationSettings(
                new NamespaceCollection(new[] { planSym }),
                new DefaultNamingStrategy());

            var factory = new IndexerFactory();

            // Act
            var context = factory.Create(indexerSym, settings).FirstOrDefault() as IndexerContext;

            // Assert
            Assert.That(context, Is.Not.Null);
            Assert.That(context.Name, Is.EqualTo("this[]"));
            Assert.That(context.WrapperName, Is.EqualTo("AsyncPlanSetup"));
            // Default EnumerableSource is empty (iterates the class itself)
            Assert.That(context.EnumerableSource, Is.Empty);
        }

        [Test]
        public void IndexerFactory_Detects_Dictionary_Values_Property()
        {
            // Arrange: Logic for dictionaries where we iterate .Values instead of the object
            var code = @"
using System.Collections.Generic;
namespace VMS.TPS.Common.Model.API {
    public class StructureCode {}
    public class DictionaryClass {
        public StructureCode this[string id] => null;
        public IEnumerable<StructureCode> Values { get; } // <--- The key hint
    }
}";
            var compilation = TestHelper.CreateCompilation(code);
            var structSym = compilation.GetTypeByMetadataName("VMS.TPS.Common.Model.API.StructureCode");
            var classSym = compilation.GetTypeByMetadataName("VMS.TPS.Common.Model.API.DictionaryClass");
            var indexerSym = classSym.GetMembers("this[]").First();

            var settings = new CompilationSettings(
                new NamespaceCollection(new[] { structSym }),
                new DefaultNamingStrategy());

            var factory = new IndexerFactory();

            // Act
            var context = factory.Create(indexerSym, settings).FirstOrDefault() as IndexerContext;

            // Assert
            Assert.That(context, Is.Not.Null);
            // Must detect that we should iterate over ".Values"
            Assert.That(context.EnumerableSource, Is.EqualTo(".Values"));
        }

        [Test]
        public void VoidMethodFactory_Creates_Context()
        {
            var code = @"
namespace VMS.TPS.Common.Model.API {
    public class TestClass {
        public void Calculate() {}
    }
}";
            var methodSym = TestHelper.GetSymbol(code, "Calculate");
            var factory = new VoidMethodFactory();

            var context = factory.Create(methodSym, _settings).FirstOrDefault() as VoidMethodContext;

            Assert.That(context, Is.Not.Null);
            Assert.That(context.Name, Is.EqualTo("Calculate"));
        }

        [Test]
        public void SimpleMethodFactory_Creates_Context_For_Primitives()
        {
            var code = @"
namespace VMS.TPS.Common.Model.API {
    public class TestClass {
        public double GetDose() => 0.0;
    }
}";
            var methodSym = TestHelper.GetSymbol(code, "GetDose");
            var factory = new SimpleMethodFactory();

            var context = factory.Create(methodSym, _settings).FirstOrDefault() as SimpleMethodContext;

            Assert.That(context, Is.Not.Null);
            Assert.That(context.ReturnType, Is.EqualTo("double"));
        }

        [Test]
        public void ComplexMethodFactory_Creates_Context_For_WrappedTypes()
        {
            var code = @"
namespace VMS.TPS.Common.Model.API {
    public class Course {}
    public class TestClass {
        public Course GetCourse() => null;
    }
}";
            var compilation = TestHelper.CreateCompilation(code);
            var courseSym = compilation.GetTypeByMetadataName("VMS.TPS.Common.Model.API.Course");
            var classSym = compilation.GetTypeByMetadataName("VMS.TPS.Common.Model.API.TestClass");
            var methodSym = classSym.GetMembers("GetCourse").First();

            var settings = new CompilationSettings(
                new NamespaceCollection(new[] { courseSym }),
                new DefaultNamingStrategy());

            var factory = new ComplexMethodFactory();

            var context = factory.Create(methodSym, settings).FirstOrDefault() as ComplexMethodContext;

            Assert.That(context, Is.Not.Null);
            Assert.That(context.InterfaceName, Is.EqualTo("ICourse"));
            Assert.That(context.WrapperName, Is.EqualTo("AsyncCourse"));
        }

        [Test]
        public void SimpleCollectionPropertyFactory_Handles_StringList()
        {
            // Arrange
            var code = @"
using System.Collections.Generic;
namespace VMS.TPS.Common.Model.API {
    public class PlanSetup {
        public IEnumerable<string> Comments { get; }
    }
}";
            var propertySym = TestHelper.GetSymbol(code, "Comments");
            var factory = new SimpleCollectionPropertyFactory();

            // Act
            var context = factory.Create(propertySym, _settings).FirstOrDefault() as SimpleCollectionPropertyContext;

            // Assert
            Assert.That(context, Is.Not.Null);
            Assert.That(context.InnerType, Is.EqualTo("string"));
            Assert.That(context.InterfaceName, Is.EqualTo("IReadOnlyList<string>"));
        }

        [Test]
        public void ComplexCollectionMethodFactory_Handles_Wrapped_Returns()
        {
            // Arrange
            var code = @"
using System.Collections.Generic;
namespace VMS.TPS.Common.Model.API {
    public class Beam {}
    public class PlanSetup {
        public IEnumerable<Beam> GetBeams() => null;
    }
}";
            var compilation = TestHelper.CreateCompilation(code);
            var beamSym = compilation.GetTypeByMetadataName("VMS.TPS.Common.Model.API.Beam");
            var planSym = compilation.GetTypeByMetadataName("VMS.TPS.Common.Model.API.PlanSetup");
            var methodSym = planSym.GetMembers("GetBeams").First();

            var settings = new CompilationSettings(
                new NamespaceCollection(new[] { beamSym }),
                new DefaultNamingStrategy());

            var factory = new ComplexCollectionMethodFactory();

            // Act
            var context = factory.Create(methodSym, settings).FirstOrDefault() as ComplexCollectionMethodContext;

            // Assert
            Assert.That(context, Is.Not.Null);
            Assert.That(context.WrapperItemName, Is.EqualTo("AsyncBeam"));
            Assert.That(context.InterfaceName, Is.EqualTo("IReadOnlyList<IBeam>"));
        }
    }
}