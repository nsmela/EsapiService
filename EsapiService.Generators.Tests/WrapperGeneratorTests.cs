using EsapiService.Generators.Contexts;
using EsapiService.Generators.Generators;
using System.Collections.Immutable;

namespace EsapiService.Generators.Tests {
    public class WrapperGeneratorTests {

        [Test]
        public void Generate_CreatesCorrectWrapper_WithServiceInjection() {
            // Arrange
            var members = ImmutableList.Create<IMemberContext>(
                new SimplePropertyContext("Id", "string", "", true)
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
            Assert.That(result, Contains.Substring("internal readonly Varian.ESAPI.PlanSetup _inner;"));
            Assert.That(result, Contains.Substring("internal readonly IEsapiService _service;"));

            // 2. Verify Constructor Signature
            // Should look like: public AsyncPlanSetup(Varian.ESAPI.PlanSetup inner, IEsapiService service)
            Assert.That(result, Contains.Substring("public AsyncPlanSetup(Varian.ESAPI.PlanSetup inner, IEsapiService service)"));

            // 3. Verify Assignments
            Assert.That(result, Contains.Substring("_inner = inner;"));
            Assert.That(result, Contains.Substring("_service = service;"));
        }

        [Test]
        public void Generate_Handles_Inheritance_Correctly() {
            // Arrange
            var context = new ClassContext {
                Name = "Varian.ESAPI.PlanSetup",
                InterfaceName = "IPlanSetup",
                WrapperName = "AsyncPlanSetup",
                BaseWrapperName = "AsyncPlanningItem", // Triggers inheritance
                Members = ImmutableList<IMemberContext>.Empty
            };

            // Act
            var result = WrapperGenerator.Generate(context);

            // Assert

            // 1. _inner: Should be declared WITHOUT 'new' (Previous rule)
            Assert.That(result, Contains.Substring("internal readonly Varian.ESAPI.PlanSetup _inner;"));
            Assert.That(result, Does.Not.Contain("internal new readonly Varian.ESAPI.PlanSetup"));

            // 2. _service: Should be declared WITH 'new' (New rule for derived classes)
            Assert.That(result, Contains.Substring("internal new readonly IEsapiService _service;"));

            // 3. Constructor Signature: Should call base
            Assert.That(result, Contains.Substring("public AsyncPlanSetup(Varian.ESAPI.PlanSetup inner, IEsapiService service) : base(inner, service)"));

            // 4. Constructor Body: Should assign BOTH fields
            Assert.That(result, Contains.Substring("_inner = inner;"));
            Assert.That(result, Contains.Substring("_service = service;"));
        }

        [Test]
        public void Generate_Handles_ReadWrite_SimpleProperties_With_Caching() {
            // Arrange
            var context = new ClassContext {
                Name = "Varian.ESAPI.PlanSetup",
                WrapperName = "AsyncPlanSetup",
                InterfaceName = "IPlanSetup",
                Members = ImmutableList.Create<IMemberContext>(
                    new SimplePropertyContext(
                        Name: "Comment",
                        Symbol: "string",
                        IsReadOnly: false,
                        XmlDocumentation: "/// <summary>Docs</summary>"
                    )
                )
            };

            // Act
            var result = WrapperGenerator.Generate(context);

            // Assert
            // 1. Check Constructor (Cached)
            Assert.That(result, Contains.Substring("Comment = inner.Comment;"));

            // 2. Check Async Setter Structure
            //    It should assign the property result of RunAsync
            Assert.That(result, Contains.Substring("Comment = await _service.RunAsync(() =>"));

            //    Inside the lambda, it should Set THEN Return
            Assert.That(result, Contains.Substring("_inner.Comment = value;"));
            Assert.That(result, Contains.Substring("return _inner.Comment;"));
        }

        [Test]
        public void Generate_Handles_ComplexProperties_As_AsyncMethods() {
            // Arrange
            var context = new ClassContext {
                Name = "Varian.ESAPI.PlanSetup",
                WrapperName = "AsyncPlanSetup",
                InterfaceName = "IPlanSetup",
                Members = ImmutableList.Create<IMemberContext>(
                    new ComplexPropertyContext(
                        Name: "Course",
                        Symbol: "Varian.ESAPI.Course",
                        WrapperName: "AsyncCourse",
                        InterfaceName: "ICourse",
                        IsReadOnly: false, // Read/Write to test both
                        XmlDocumentation: "/// <summary>Docs</summary>"
                    )
                )
            };

            // Act
            var result = WrapperGenerator.Generate(context);

            // Assert
            // 1. Verify GETTER is an Async Method
            Assert.That(result, Contains.Substring("public async Task<ICourse> GetCourseAsync()"));

            // 2. Verify Body: Runs on Service, Wraps Result
            Assert.That(result, Contains.Substring("return await _service.RunAsync(() =>"));
            Assert.That(result, Contains.Substring("_inner.Course is null ? null : new AsyncCourse(_inner.Course, _service));"));

            // 3. Verify SETTER (Unwrap and Assign)
            Assert.That(result, Contains.Substring("public async Task SetCourseAsync(ICourse value)"));
            Assert.That(result, Contains.Substring("if (value is AsyncCourse wrapper)"));
            Assert.That(result, Contains.Substring("_inner.Course = wrapper._inner"));
        }

        [Test]
        public void Generate_Handles_Collections_As_AsyncMethods() {
            // Arrange
            var context = new ClassContext {
                Name = "Varian.ESAPI.PlanSetup",
                WrapperName = "AsyncPlanSetup",
                InterfaceName = "IPlanSetup",
                Members = ImmutableList.Create<IMemberContext>(
                    // 1. Complex Collection (Wrappers)
                    new CollectionPropertyContext(
                        Name: "Structures",
                        Symbol: "System.Collections.Generic.IEnumerable<Varian.Structure>",
                        InnerType: "Varian.Structure",
                        WrapperName: "IReadOnlyList<AsyncStructure>", // wrapper type inside implementation
                        InterfaceName: "IReadOnlyList<IStructure>",   // return type
                        WrapperItemName: "AsyncStructure",
                        InterfaceItemName: "IStructure",
                        XmlDocumentation: ""
                    ),
                    // 2. Simple Collection (Strings)
                    new SimpleCollectionPropertyContext(
                        Name: "Notes",
                        Symbol: "System.Collections.Generic.IEnumerable<string>",
                        InnerType: "string",
                        WrapperName: "IReadOnlyList<string>",
                        InterfaceName: "IReadOnlyList<string>",
                        XmlDocumentation: ""
                    )
                )
            };

            // Act
            var result = WrapperGenerator.Generate(context);

            // Assert
            // 1. Complex Collection
            Assert.That(result, Contains.Substring("public async Task<IReadOnlyList<IStructure>> GetStructuresAsync()"));
            Assert.That(result, Contains.Substring("return await _service.RunAsync(() =>"));
            // Verify Projection: Select(x => new Wrapper(x, service))
            Assert.That(result, Contains.Substring("Select(x => new AsyncStructure(x, _service)).ToList()"));

            // 2. Simple Collection
            Assert.That(result, Contains.Substring("public async Task<IReadOnlyList<string>> GetNotesAsync()"));
            // Verify Conversion: ToList()
            Assert.That(result, Contains.Substring("_inner.Notes?.ToList()"));
        }

        [Test]
        public void Generate_Handles_Methods_As_AsyncMethods() {
            // Arrange
            var context = new ClassContext {
                Name = "Varian.ESAPI.PlanSetup",
                WrapperName = "AsyncPlanSetup",
                InterfaceName = "IPlanSetup",
                Members = ImmutableList.Create<IMemberContext>(
                    // 1. Void Method
                    new VoidMethodContext(
                        Name: "Calculate",
                        Symbol: "void",
                        Signature: "(int options)",
                        OriginalSignature: "(int options)",
                        CallParameters: "options",
                        XmlDocumentation: ""
                    ),
                    // 2. Simple Return
                    new SimpleMethodContext(
                        Name: "GetDoseAtPoint",
                        Symbol: "double",
                        ReturnType: "double",
                        Signature: "(VVector p)",
                        OriginalSignature: "(VVector p)",
                        CallParameters: "p",
                        XmlDocumentation: ""
                    ),
                    // 3. Complex Return
                    new ComplexMethodContext(
                        Name: "GetCourse",
                        Symbol: "Varian.ESAPI.Course",
                        WrapperName: "AsyncCourse",
                        InterfaceName: "ICourse",
                        Signature: "()",
                        OriginalSignature: "()",
                        CallParameters: "",
                        XmlDocumentation: ""
                    )
                )
            };

            // Act
            var result = WrapperGenerator.Generate(context);

            // Assert
            // 1. Void -> Task CalculateAsync(...)
            Assert.That(result, Contains.Substring("public Task CalculateAsync(int options)"));
            Assert.That(result, Contains.Substring("=> _service.RunAsync(() => _inner.Calculate(options));"));

            // 2. Simple Return -> Task<double> GetDoseAtPointAsync(...)
            Assert.That(result, Contains.Substring("public Task<double> GetDoseAtPointAsync(VVector p)"));
            Assert.That(result, Contains.Substring("=> _service.RunAsync(() => _inner.GetDoseAtPoint(p));"));

            // 3. Complex Return -> Async Task<ICourse> GetCourseAsync()
            //    (Must unwrap/wrap inside the body)
            Assert.That(result, Contains.Substring("public async Task<ICourse> GetCourseAsync()"));
            Assert.That(result, Contains.Substring("return await _service.RunAsync(() =>"));
            Assert.That(result, Contains.Substring("_inner.GetCourse() is var result && result is null ? null : new AsyncCourse(result, _service));"));
        }

        [Test]
        public void Generate_Handles_OutParameters_Void() {
            // Arrange: Void method with out parameter
            var members = ImmutableList.Create<IMemberContext>(
                new OutParameterMethodContext(
                    Name: "Calculate",
                    Symbol: "void",
                    XmlDocumentation: "",
                    OriginalReturnType: "void",
                    ReturnsVoid: true,
                    Parameters: ImmutableList.Create(
                        new ParameterContext("msg", "string", "string", "", false, true, false)
                    ),
                    ReturnTupleSignature: "(string msg)",
                    WrapperReturnTypeName: "",
                    IsReturnWrappable: false
                )
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
            // 1. Should NOT assign to a variable (because it returns void)
            Assert.That(result, Does.Not.Contain("var result ="), "Void methods must not assign result variable");

            // 2. Should just await the call
            Assert.That(result, Contains.Substring("await _service.RunAsync(() => _inner.Calculate(out msg_temp));"));

            // 3. Should return the out parameter
            Assert.That(result, Contains.Substring("return (msg_temp);"));
        }

        [Test]
        public void Generate_Handles_OutParameters_Returning_Wrappable() {
            // Arrange: Method returning Structure with out parameter
            var members = ImmutableList.Create<IMemberContext>(
                new OutParameterMethodContext(
                    Name: "AddStructure",
                    Symbol: "Varian.Structure",
                    XmlDocumentation: "",
                    OriginalReturnType: "Varian.Structure",
                    ReturnsVoid: false,
                    Parameters: ImmutableList.Create(
                        new ParameterContext("msg", "string", "string", "", false, true, false)
                    ),
                    ReturnTupleSignature: "(IStructure Result, string msg)",
                    WrapperReturnTypeName: "AsyncStructure",
                    IsReturnWrappable: true
                )
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
            // 1. MUST assign result (because it has a return value)
            Assert.That(result, Contains.Substring("var result = await _service.RunAsync"));

            // 2. Should wrap the result
            Assert.That(result, Contains.Substring("result is null ? null : new AsyncStructure(result, _service)"));

            // 3. Should return Tuple (Wrapper, OutParam)
            Assert.That(result, Contains.Substring("return (result is null ? null : new AsyncStructure(result, _service), msg_temp);"));
        }

    }
}