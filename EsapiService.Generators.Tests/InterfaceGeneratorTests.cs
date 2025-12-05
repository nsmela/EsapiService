using EsapiService.Generators.Contexts;
using EsapiService.Generators.Generators;
using System.Collections.Immutable;

namespace EsapiService.Generators.Tests {
    public class InterfaceGeneratorTests {
        [Test]
        public void Generate_CreatesCorrectInterface_WithInheritanceAndMembers() {
            // Arrange: Manually build the Context (The "ViewModel")
            var members = ImmutableList.Create<IMemberContext>(
                new SimplePropertyContext("Id", "string", "", true),
                new ComplexPropertyContext("Course", "Varian.Course", "", "AsyncCourse", "ICourse", true),
                new VoidMethodContext("Calculate", "Task", "", "(int options)", "(options)")
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
            // 1. Verify Usings
            Assert.That(result, Contains.Substring("using VMS.TPS.Common.Model.API;"));
            Assert.That(result, Contains.Substring("using Esapi.Services;"));

            // Verify namespace
            Assert.That(result, Contains.Substring("namespace Esapi.Interfaces"));

            // 1. Check Definition & Inheritance
            Assert.That(result, Contains.Substring("public interface IPlanSetup : IPlanningItem"));

            // 2. Check Simple Property
            Assert.That(result, Contains.Substring("string Id { get; }"));

            // 3. Check Complex Property (Should use the InterfaceName, not Wrapper/Original)
            Assert.That(result, Contains.Substring("System.Threading.Tasks.Task<ICourse> GetCourseAsync();"));

            // 4. Check Method
            Assert.That(result, Contains.Substring("System.Threading.Tasks.Task CalculateAsync(int options);"), "Verify Method is Async and returns Task");
        }

        [Test]
        public void Generate_HandlesCollections_ForSimpleAndComplexTypes() {
            // Arrange
            var members = ImmutableList.Create<IMemberContext>(
                // Case 1: Simple Collection 
                // ContextService identifies IEnumerable<string> as a SimplePropertyContext
                new SimplePropertyContext(
                    Name: "Notes",
                    Symbol: "System.Collections.Generic.IEnumerable<string>",
                    XmlDocumentation: string.Empty,
                    IsReadOnly: true
                ),

                // Case 2: Complex Collection
                // ContextService identifies IEnumerable<Structure> as a CollectionPropertyContext
                new CollectionPropertyContext(
                    Name: "Structures",
                    Symbol: "System.Collections.Generic.IEnumerable<Varian.Structure>",
                    XmlDocumentation: string.Empty,
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

        [Test]
        public void Generate_Adds_RunAsync_Methods_To_Interface() {
            // Arrange
            var context = new ClassContext {
                Name = "Varian.ESAPI.PlanSetup", // Fully qualified inner type
                InterfaceName = "IPlanSetup",
                Members = ImmutableList<IMemberContext>.Empty
            };

            // Act
            var result = InterfaceGenerator.Generate(context);

            // Assert
            // 1. Check Action overload
            Assert.That(result, Contains.Substring("Runs a function against the raw ESAPI Varian.ESAPI.PlanSetup object safely on the ESAPI thread."));
            Assert.That(result, Contains.Substring("Task RunAsync(Action<Varian.ESAPI.PlanSetup> action);"));

            // 2. Check Func overload
            Assert.That(result, Contains.Substring("Task<T> RunAsync<T>(Func<Varian.ESAPI.PlanSetup, T> func);"));
        }
    }
}