using EsapiService.Generators.Contexts;
using EsapiService.Generators.Generators;
using System.Collections.Immutable;

namespace EsapiService.Generators.Tests {
    public class PropertySetterTests {
        [Test]
        public void Generators_Handle_ReadWrite_ComplexProperties() {
            // Arrange: A Context describing 'public Course Course { get; set; }'
            var member = new ComplexPropertyContext(
                Name: "Course",
                Symbol: "Varian.ESAPI.Course",
                XmlDocumentation: string.Empty,
                WrapperName: "AsyncCourse",
                InterfaceName: "ICourse",
                IsReadOnly: false // <--- Key Test Parameter
            );

            var context = new ClassContext {
                Name = "Varian.ESAPI.PlanSetup",
                InterfaceName = "IPlanSetup",
                WrapperName = "AsyncPlanSetup",
                Members = ImmutableList.Create<IMemberContext>(member)
            };

            // Act 1: Interface Generation
            var interfaceCode = InterfaceGenerator.Generate(context);

            // Assert 1: Interface has Async Setter
            Assert.That(interfaceCode, Contains.Substring("Task SetCourseAsync(ICourse value);"));

            // Act 2: Wrapper Generation
            var wrapperCode = WrapperGenerator.Generate(context);

            // Assert 2: Wrapper has Async Setter Implementation
            Assert.That(wrapperCode, Contains.Substring("public async Task SetCourseAsync(ICourse value)"));

            // Assert 3: Wrapper performs Type Checking (Unwrapping)
            Assert.That(wrapperCode, Contains.Substring("if (value is AsyncCourse wrapper)"));

            // Assert 4: Wrapper performs the actual assignment on _inner
            Assert.That(wrapperCode, Contains.Substring("_inner.Course = wrapper._inner"));
        }
    }
}