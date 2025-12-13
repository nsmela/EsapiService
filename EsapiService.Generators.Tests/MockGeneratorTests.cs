using EsapiService.Generators.Contexts;
using EsapiService.Generators.Generators;
using NUnit.Framework;
using System.Collections.Immutable;

namespace EsapiService.Generators.Tests {
    [TestFixture]
    public class MockGeneratorTests {
        [Test]
        public void Generate_CreatesMutablePoco_WithConstructorInitialization() {
            // Arrange
            var members = ImmutableList.Create<IMemberContext>(
                new SimplePropertyContext("Id", "string", "", true),
                new CollectionPropertyContext(
                    Name: "Structures",
                    Symbol: "System.Collections.Generic.IEnumerable<Varian.Structure>",
                    XmlDocumentation: "",
                    InnerType: "Varian.Structure",
                    WrapperName: "Irrelevant",
                    InterfaceName: "Irrelevant",
                    WrapperItemName: "Irrelevant",
                    InterfaceItemName: "Irrelevant"
                )
            );

            var context = new ClassContext {
                Name = "Varian.ESAPI.PlanSetup",
                BaseName = "Varian.ESAPI.PlanningItem",
                Members = members
            };

            // Act
            var result = MockGenerator.Generate(context);

            // Assert
            Assert.That(result, Contains.Substring("namespace VMS.TPS.Common.Model.API"));
            Assert.That(result, Contains.Substring("public class PlanSetup : PlanningItem"));
            Assert.That(result, Contains.Substring("public PlanSetup()"));
            Assert.That(result, Contains.Substring("Structures = new List<Structure>();"));
            Assert.That(result, Contains.Substring("public string Id { get; set; }"));
        }

        [Test]
        public void Generate_Handles_Methods_With_DummyImplementations() {
            // Arrange

            // 1. Create Parameter Lists
            var xParam = new ParameterContext("x", "int", "int", "", false, false, false);
            var paramsCalculate = ImmutableList.Create(xParam);
            var paramsEmpty = ImmutableList<ParameterContext>.Empty;
            var paramsOut = ImmutableList.Create(new ParameterContext("msg", "string", "string", "", false, true, false));

            var members = ImmutableList.Create<IMemberContext>(
                // Case 1: Void Method
                // Now requires the 'paramsCalculate' list at the end
                new VoidMethodContext("Calculate", "void", "", "(int x)", "(int x)", "x", paramsCalculate),

                // Case 2: Method with Return Value
                // Now requires 'paramsEmpty' at the end
                new SimpleMethodContext("GetDose", "double", "", "double", "()", "()", "", paramsEmpty),

                // Case 3: Method with 'out' parameter
                new OutParameterMethodContext(
                    Name: "GetValid",
                    Symbol: "bool",
                    OriginalReturnType: "bool",
                    ReturnsVoid: false,
                    Parameters: paramsOut,
                    ReturnTupleSignature: "Irrelevant",
                    XmlDocumentation: "",
                    WrapperReturnTypeName: "",
                    IsReturnWrappable: false
                )
            );

            var context = new ClassContext { Name = "Varian.Plan", Members = members };

            // Act
            var result = MockGenerator.Generate(context);

            // Assert
            Assert.That(result, Contains.Substring("public void Calculate(int x) { }"));
            Assert.That(result, Contains.Substring("public double GetDose() => default;"));
            Assert.That(result, Contains.Substring("public bool GetValid(out string msg)"));
            Assert.That(result, Contains.Substring("msg = default;"));
            Assert.That(result, Contains.Substring("return default;"));
        }

        [Test]
        public void Generate_NestedStruct_GeneratesCorrectly() {
            // Arrange
            var nestedContext = new ClassContext {
                Name = "VMS.TPS.Common.Model.Types.Algorithm",
                IsStruct = true,
                Members = ImmutableList<IMemberContext>.Empty
            };

            var parentContext = new ClassContext {
                Name = "VMS.TPS.Common.Model.Types.Calculation",
                NestedTypes = ImmutableList.Create(nestedContext),
                Members = ImmutableList<IMemberContext>.Empty
            };

            // Act
            string result = MockGenerator.Generate(parentContext);

            // Assert
            // 1. Verify Parent Class Exists
            Assert.That(result, Contains.Substring("public class Calculation"));

            // 2. Verify Child Struct Exists
            Assert.That(result, Contains.Substring("public struct Algorithm"));

            // 3. Verify Nesting 
            int parentIndex = result.IndexOf("public class Calculation");
            int childIndex = result.IndexOf("public struct Algorithm");
            Assert.That(childIndex, Is.GreaterThan(parentIndex), "Struct 'Algorithm' should be defined inside 'Calculation'");
        }
    }
}