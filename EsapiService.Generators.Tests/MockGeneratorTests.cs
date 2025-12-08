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
                // Case 1: Simple Property (ReadOnly in Source -> ReadWrite in Mock)
                new SimplePropertyContext("Id", "string", "", true),

                // Case 2: Collection (Needs Initialization in Constructor)
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
            // 1. Namespace Spoofing (Critical for substituting the real DLL)
            Assert.That(result, Contains.Substring("namespace VMS.TPS.Common.Model.API"));

            // 2. Class & Inheritance
            Assert.That(result, Contains.Substring("public class PlanSetup : PlanningItem"));

            // 3. Constructor & Collection Init
            //    Must have a public constructor
            Assert.That(result, Contains.Substring("public PlanSetup()"));
            //    Must initialize the list
            Assert.That(result, Contains.Substring("Structures = new List<Structure>();"));

            // 4. Property Mutability
            //    Must be { get; set; } even though input said IsReadOnly=true
            Assert.That(result, Contains.Substring("public string Id { get; set; }"));
            Assert.That(result, Contains.Substring("public IEnumerable<Structure> Structures { get; set; }"));
        }

        [Test]
        public void Generate_Handles_Methods_With_DummyImplementations() {
            // Arrange
            var members = ImmutableList.Create<IMemberContext>(
                // Case 1: Void Method
                new VoidMethodContext("Calculate", "void", "", "(int x)", "(int x)", "x"),

                // Case 2: Method with Return Value
                new SimpleMethodContext("GetDose", "double", "", "double", "()", "()", ""),

                // Case 3: Method with 'out' parameter (Tricky!)
                new OutParameterMethodContext(
                    Name: "GetValid",
                    Symbol: "bool",
                    XmlDocumentation: "",
                    OriginalReturnType: "bool",
                    ReturnsVoid: false,
                    Parameters: ImmutableList.Create(
                        new ParameterContext("msg", "string", "string", "", false, true, false) // out string msg
                    ),
                    ReturnTupleSignature: "Irrelevant"
                )
            );

            var context = new ClassContext { Name = "Varian.Plan", Members = members };

            // Act
            var result = MockGenerator.Generate(context);

            // Assert
            // Case 1: Void -> Empty body
            Assert.That(result, Contains.Substring("public void Calculate(int x) { }"));

            // Case 2: Return -> Returns default
            Assert.That(result, Contains.Substring("public double GetDose() => default;"));

            // Case 3: Out Param -> Must assign default before returning
            Assert.That(result, Contains.Substring("public bool GetValid(out string msg)"));
            Assert.That(result, Contains.Substring("msg = default;"));
            Assert.That(result, Contains.Substring("return default;"));
        }
    }
}