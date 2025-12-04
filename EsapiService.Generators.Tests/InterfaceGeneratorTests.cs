using EsapiService.Generators.Contexts;
using EsapiService.Generators.Generators;
using NUnit.Framework;
using System.Collections.Immutable;

namespace EsapiService.Generators.Tests {
    public class InterfaceGeneratorTests {
        [Test]
        public void Generate_CreatesCorrectInterface_WithInheritanceAndMembers() {
            // Arrange: Manually build the Context (The "ViewModel")
            var members = ImmutableList.Create<IMemberContext>(
                new SimplePropertyContext("Id", "string"),
                new ComplexPropertyContext("Course", "Varian.Course", "AsyncCourse", "ICourse"),
                new MethodContext("Calculate", "void", "int options", "(int options)")
            );

            var context = new ClassContext {
                Name = "Varian.ESAPI.PlanSetup",
                InterfaceName = "IPlanSetup",
                BaseInterface = "IPlanningItem",
                Members = members
            };

            // Act
            var result = InterfaceGenerator.Generate(context);

            // Assert
            // 1. Check Definition & Inheritance
            Assert.That(result, Contains.Substring("public interface IPlanSetup : IPlanningItem"));

            // 2. Check Simple Property
            Assert.That(result, Contains.Substring("string Id { get; }"));

            // 3. Check Complex Property (Should use the InterfaceName, not Wrapper/Original)
            Assert.That(result, Contains.Substring("ICourse Course { get; }"));

            // 4. Check Method
            Assert.That(result, Contains.Substring("void Calculate(int options);"));
        }

        [Test]
        public void Generate_HandlesCollections_ForSimpleAndComplexTypes() {
            // Arrange
            var members = ImmutableList.Create<IMemberContext>(
                // Case 1: Simple Collection 
                // ContextService identifies IEnumerable<string> as a SimplePropertyContext
                new SimplePropertyContext(
                    Name: "Notes",
                    Symbol: "System.Collections.Generic.IEnumerable<string>"
                ),

                // Case 2: Complex Collection
                // ContextService identifies IEnumerable<Structure> as a CollectionPropertyContext
                new CollectionPropertyContext(
                    Name: "Structures",
                    Symbol: "System.Collections.Generic.IEnumerable<Varian.Structure>",
                    InnerType: "Varian.Structure",
                    WrapperName: "System.Collections.Generic.IEnumerable<AsyncStructure>",
                    InterfaceName: "System.Collections.Generic.IEnumerable<IStructure>",
                    WrapperItemName: "AsyncStructure",
                    InterfaceItemName: "IStructure"
                )
            );

            var context = new ClassContext {
                Name = "Varian.ESAPI.PlanSetup",
                InterfaceName = "IPlanSetup",
                Members = members
            };

            // Act
            var result = InterfaceGenerator.Generate(context);

            // Assert
            // 1. Verify Simple Collection (Output should match input Type)
            Assert.That(result, Contains.Substring("System.Collections.Generic.IEnumerable<string> Notes { get; }"));

            // 2. Verify Complex Collection (Output should use the InterfaceName)
            Assert.That(result, Contains.Substring("System.Collections.Generic.IEnumerable<IStructure> Structures { get; }"));
        }
    }
}