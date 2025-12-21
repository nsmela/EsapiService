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
                new ComplexPropertyContext("Course", "Varian.Course", "ICourse", "ICourse", "AsyncCourse", "ICourse", true),
                new VoidMethodContext("Calculate", "Task", "", "(int options)", "(options)", "int options", null)
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
            Assert.That(result, Contains.Substring("Task<ICourse> GetCourseAsync();"));

            // 4. Check Method
            Assert.That(result, Contains.Substring("Task CalculateAsync(int options);"), "Verify Method is Async and returns Task");
        }

        [Test]
        public void Generate_HandlesCollections_ForSimpleAndComplexTypes() {
            // Arrange
            var members = ImmutableList.Create<IMemberContext>(
                // Case 1: Simple Collection (Notes)
                // Correct Type: SimpleCollectionPropertyContext
                // Correct Interface: IReadOnlyList<string>
                new SimpleCollectionPropertyContext(
                    Name: "Notes",
                    Symbol: "System.Collections.Generic.IEnumerable<string>",
                    InnerType: "string",
                    WrapperName: "IReadOnlyList<string>",
                    InterfaceName: "IReadOnlyList<string>",
                    XmlDocumentation: "/// <summary>Notes docs</summary>"
                ),

                // Case 2: Complex Collection (Structures)
                // Correct Type: CollectionPropertyContext
                // Correct Interface: IReadOnlyList<IStructure>
                new CollectionPropertyContext(
                    Name: "Structures",
                    Symbol: "System.Collections.Generic.IEnumerable<Varian.Structure>",
                    InnerType: "Varian.Structure",
                    WrapperName: "IReadOnlyList<AsyncStructure>",
                    InterfaceName: "IReadOnlyList<IStructure>", // <--- This dictates the return type
                    WrapperItemName: "AsyncStructure",
                    InterfaceItemName: "IStructure",
                    XmlDocumentation: "/// <summary>Structure docs</summary>"
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
            // 1. Verify Simple Collection -> Async Method
            // Expected: Task<IReadOnlyList<string>> GetNotesAsync();
            Assert.That(result, Contains.Substring("IReadOnlyList<string> Notes { get; }"));

            // 2. Verify Complex Collection -> Async Method
            // Expected: Task<IReadOnlyList<IStructure>> GetStructuresAsync();
            Assert.That(result, Contains.Substring("Task<IReadOnlyList<IStructure>> GetStructuresAsync();"));
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

        [Test]
        public void Generate_Includes_XmlDocumentation_On_AsyncMethods() {
            // Arrange
            var member = new ComplexPropertyContext(
                Name: "Course",
                Symbol: "Varian.ESAPI.Course",
                ReturnValue: "ICourse",
                WrapperName: "AsyncCourse",
                InterfaceName: "ICourse",
                IsReadOnly: true,
                XmlDocumentation: "/// <summary>Gets the Course.</summary>"
            );

            var context = new ClassContext {
                Name = "Varian.ESAPI.PlanSetup",
                InterfaceName = "IPlanSetup",
                Members = ImmutableList.Create<IMemberContext>(member)
            };

            // Act
            var result = InterfaceGenerator.Generate(context);

            // Assert
            // The doc should appear immediately before the method
            var expected = @"
        /// <summary>Gets the Course.</summary>
        Task<ICourse> GetCourseAsync();";

            // Normalize whitespace for easier assertion
            Assert.That(result.Replace("\r\n", "\n"), Contains.Substring(expected.Replace("\r\n", "\n").Trim()));
        }

        [Test]
        public void Generate_Includes_XmlDocumentation_On_Interface_Class() {
            // Arrange
            var context = new ClassContext {
                Name = "Varian.ESAPI.PlanSetup",
                InterfaceName = "IPlanSetup",
                // Simulate class-level docs extracted by ContextService
                XmlDocumentation = "/// <summary>\r\n/// Represents a Varian Plan.\r\n/// </summary>",
                Members = ImmutableList<IMemberContext>.Empty
            };

            // Act
            var result = InterfaceGenerator.Generate(context);

            // Assert
            var expected = @"
/// <summary>
/// Represents a Varian Plan.
/// </summary>
    public interface IPlanSetup";

            // Normalize newlines for cross-platform safety
            Assert.That(result.Replace("\r\n", "\n"), Contains.Substring(expected.Replace("\r\n", "\n")));
        }
    }
}