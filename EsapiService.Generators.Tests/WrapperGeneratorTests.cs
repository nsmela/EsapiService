using EsapiService.Generators.Contexts;
using EsapiService.Generators.Generators;
using System.Collections.Immutable;

namespace EsapiService.Generators.Tests {
    public class WrapperGeneratorTests {
        [Test]
        public void Generate_CreatesCorrectWrapper_WithAllMemberTypes() {
            // Arrange
            var members = ImmutableList.Create<IMemberContext>(
                // 1. Simple Property
                new SimplePropertyContext("Id", "string", true),

                // 2. Complex Property
                new ComplexPropertyContext(
                    Name: "Course",
                    Symbol: "Varian.Course",
                    WrapperName: "AsyncCourse",
                    InterfaceName: "ICourse"
                ),

                // 3. Collection Property
                new CollectionPropertyContext(
                    Name: "Structures",
                    Symbol: "System.Collections.Generic.IEnumerable<Varian.Structure>",
                    InnerType: "Varian.Structure",
                    WrapperName: "System.Collections.Generic.IReadOnlyList<AsyncStructure>", 
                    InterfaceName: "System.Collections.Generic.IReadOnlyList<IStructure>",
                    WrapperItemName: "AsyncStructure",
                    InterfaceItemName: "IStructure"
                ),

                // 4. Method
                new MethodContext(
                    Name: "Calculate",
                    Symbol: "void",
                    Arguments: "int options",
                    Signature: "(int options)",
                    CallParameters: "options" // Used for the forwarding call
                )
            );

            var context = new ClassContext {
                Name = "Varian.ESAPI.PlanSetup",
                InterfaceName = "IPlanSetup",
                WrapperName = "AsyncPlanSetup",
                Members = members
            };

            // Act
            // (We haven't created WrapperGenerator yet, so this will require the class to exist)
            var result = WrapperGenerator.Generate(context);

            // Assert
            // Class Definition
            Assert.That(result, Contains.Substring("public class AsyncPlanSetup : IPlanSetup"));

            // Constructor
            Assert.That(result, Contains.Substring("private readonly Varian.ESAPI.PlanSetup _inner;"));
            Assert.That(result, Contains.Substring("public AsyncPlanSetup(Varian.ESAPI.PlanSetup inner, IEsapiService service)"));
            Assert.That(result, Contains.Substring("_inner = inner;"));

            // Simple Property (Forwarding)
            Assert.That(result, Contains.Substring("public string Id { get; }"));

            // Complex Property (Wrapping with Null Check)
            Assert.That(result, Contains.Substring("public ICourse Course => _inner.Course is null ? null : new AsyncCourse(_inner.Course);"));

            // Collection Property (Projection with Null Check)
            // Note: We need System.Linq for .Select()
            Assert.That(result, Contains.Substring("using System.Linq;"));
            // 1. Return type is IReadOnlyList
            // 2. Implementation uses .ToList() to materialize the IEnumerable
            Assert.That(result, Contains.Substring("public System.Collections.Generic.IReadOnlyList<IStructure> Structures => _inner.Structures?.Select(x => new AsyncStructure(x, _service)).ToList();"));

            // Method (Forwarding)
            Assert.That(result, Contains.Substring("public void Calculate(int options) => _inner.Calculate(options);"));
        }

        [Test]
        public void Generate_CreatesCorrectWrapper_WithServiceInjection() {
            // Arrange
            var members = ImmutableList.Create<IMemberContext>(
                new SimplePropertyContext("Id", "string", true)
            // We only need one member to verify the constructor logic
            );

            var context = new ClassContext {
                Name = "Varian.ESAPI.PlanSetup",
                InterfaceName = "IPlanSetup",
                WrapperName = "AsyncPlanSetup",
                Members = members
            };

            // Act
            var result = WrapperGenerator.Generate(context);

            // Assert
            // 1. Verify Fields
            Assert.That(result, Contains.Substring("private readonly Varian.ESAPI.PlanSetup _inner;"));
            Assert.That(result, Contains.Substring("private readonly IEsapiService _service;"));

            // 2. Verify Constructor Signature
            // Should look like: public AsyncPlanSetup(Varian.ESAPI.PlanSetup inner, IEsapiService service)
            Assert.That(result, Contains.Substring("public AsyncPlanSetup(Varian.ESAPI.PlanSetup inner, IEsapiService service)"));

            // 3. Verify Assignments
            Assert.That(result, Contains.Substring("_inner = inner;"));
            Assert.That(result, Contains.Substring("_service = service;"));
        }

        [Test]
        public void Generate_Handles_SimpleProperties_ReadOnly_And_ReadWrite() {
            // Arrange
            var members = ImmutableList.Create<IMemberContext>(
                // Case 1: Read-Only Property (e.g. Id)
                // Should be loaded in Constructor
                new SimplePropertyContext("Id", "string", IsReadOnly: true),

                // Case 2: Read/Write Property (e.g. Name)
                // Should generate an Async Setter
                new SimplePropertyContext("Name", "string", IsReadOnly: false)
            );

            var context = new ClassContext {
                Name = "Varian.ESAPI.PlanSetup",
                InterfaceName = "IPlanSetup",
                WrapperName = "AsyncPlanSetup",
                Members = members
            };

            // Act
            var result = WrapperGenerator.Generate(context);

            // Assert

            // 1. Verify Read-Only Logic (Constructor Loading)
            // The property should be an auto-prop (or private set)
            Assert.That(result, Contains.Substring("public string Id { get; }"));
            // The constructor should assign it
            Assert.That(result, Contains.Substring("Id = inner.Id;"));

            // 2. Verify Read/Write Logic (Async Setter)
            // The getter should still exist (likely forwarding)
            Assert.That(result, Contains.Substring("public string Name => _inner.Name;"));
            // The Async Setter should exist
            Assert.That(result, Contains.Substring("public async Task SetNameAsync(string value)"));
            // It should use the service to run the update
            Assert.That(result, Contains.Substring("_service.RunAsync(() => _inner.Name = value);"));
        }
    }
}