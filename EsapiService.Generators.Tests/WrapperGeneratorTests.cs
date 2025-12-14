using EsapiService.Generators.Contexts;
using EsapiService.Generators.Generators.Wrappers;
using System.Collections.Immutable;
using NUnit.Framework;
using System.Collections.Generic;

namespace EsapiService.Generators.Tests
{
    [TestFixture]
    public class WrapperGeneratorTests
    {
        #region 1. Properties

        [Test]
        public void SimplePropertyGenerator_Generates_ReadWrite_Property()
        {
            // SimplePropertyContext(Name, Symbol, Xml, IsReadOnly)
            var ctx = new SimplePropertyContext("Id", "string", "/// <summary>Doc</summary>", IsReadOnly: false);

            var code = SimplePropertyGenerator.Generate(ctx);

            Assert.That(code, Does.Contain("public string Id { get; private set; }"));
            Assert.That(code, Does.Contain("public async Task SetIdAsync(string value)"));
            Assert.That(code, Does.Contain("_service.PostAsync(context =>"));
            Assert.That(code, Does.Contain("_inner.Id = value;"));
        }

        [Test]
        public void SimplePropertyGenerator_Generates_ReadOnly_Property()
        {
            var ctx = new SimplePropertyContext("CreationDateTime", "DateTime", "", IsReadOnly: true);

            var code = SimplePropertyGenerator.Generate(ctx);

            Assert.That(code, Does.Contain("public DateTime CreationDateTime { get; }"));
            Assert.That(code, Does.Not.Contain("SetCreationDateTimeAsync"));
        }

        [Test]
        public void ComplexPropertyGenerator_Generates_Wrapper_Projection()
        {
            // ComplexPropertyContext(Name, Symbol, Xml, WrapperName, InterfaceName, IsReadOnly)
            var ctx = new ComplexPropertyContext(
                "Course",
                "Course",
                "",
                "AsyncCourse",
                "ICourse",
                IsReadOnly: false);

            var code = ComplexPropertyGenerator.Generate(ctx);

            // Getter
            Assert.That(code, Does.Contain("public async Task<ICourse> GetCourseAsync()"));
            Assert.That(code, Does.Contain("new AsyncCourse(_inner.Course, _service)"));

            // Setter
            Assert.That(code, Does.Contain("public async Task SetCourseAsync(ICourse value)"));
            Assert.That(code, Does.Contain("if (value is IEsapiWrapper<Course> wrapper)"));
            Assert.That(code, Does.Contain("_inner.Course = wrapper.Inner"));
        }

        [Test]
        public void CollectionPropertyGenerator_Generates_List_Projection()
        {
            // CollectionPropertyContext(Name, Symbol, Xml, InnerType, WrapperName, InterfaceName, WrapperItemName, InterfaceItemName)
            var ctx = new CollectionPropertyContext(
                "Structures",
                "IEnumerable<Structure>",
                "",
                "Structure",                     // InnerType
                "IEnumerable<AsyncStructure>",   // WrapperName (Full type)
                "IEnumerable<IStructure>",       // InterfaceName (Full type)
                "AsyncStructure",                // WrapperItemName
                "IStructure"                     // InterfaceItemName
            );

            var code = CollectionPropertyGenerator.Generate(ctx);

            Assert.That(code, Does.Contain("public async Task<IEnumerable<IStructure>> GetStructuresAsync()"));
            Assert.That(code, Does.Contain("_inner.Structures?.Select(x => new AsyncStructure(x, _service)).ToList()"));
        }

        [Test]
        public void SimpleCollectionPropertyGenerator_Generates_Cached_List()
        {
            // SimpleCollectionPropertyContext(Name, Symbol, Xml, InnerType, WrapperName, InterfaceName)
            var ctx = new SimpleCollectionPropertyContext(
                "DoseValues",
                "IEnumerable<double>",
                "",
                "double",
                "IReadOnlyList<double>",
                "IReadOnlyList<double>"
            );

            var code = SimpleCollectionPropertyGenerator.Generate(ctx);

            Assert.That(code, Does.Contain("public IReadOnlyList<double> DoseValues { get; }"));
            Assert.That(code, Does.Not.Contain("PostAsync"), "Simple collections should be cached, not async fetched.");
        }

        #endregion

        #region 2. Methods

        [Test]
        public void VoidMethodGenerator_Generates_Task()
        {
            // VoidMethodContext(Name, Symbol, Xml, Signature, OriginalSig, CallParams, Parameters)
            var ctx = new VoidMethodContext("Calculate", "void", "", "()", "()", "", ImmutableList<ParameterContext>.Empty);

            var code = VoidMethodGenerator.Generate(ctx);

            Assert.That(code, Does.Contain("public Task CalculateAsync() =>"));
            Assert.That(code, Does.Contain("_service.PostAsync(context => _inner.Calculate());"));
        }

        [Test]
        public void SimpleMethodGenerator_Generates_TaskResult()
        {
            // SimpleMethodContext(Name, Symbol, Xml, ReturnType, Signature, OriginalSig, CallParams, Parameters)
            var ctx = new SimpleMethodContext(
                "GetDose",
                "double",
                "",
                "double", // ReturnType
                "()",
                "()",
                "",
                ImmutableList<ParameterContext>.Empty
            );

            var code = SimpleMethodGenerator.Generate(ctx);

            Assert.That(code, Does.Contain("public Task<double> GetDoseAsync() =>"));
            Assert.That(code, Does.Contain("_service.PostAsync(context => _inner.GetDose());"));
        }

        [Test]
        public void ComplexMethodGenerator_Generates_Wrapped_Result()
        {
            // ComplexMethodContext(Name, Symbol, Xml, WrapperName, InterfaceName, Signature, OriginalSig, CallParams, Parameters)
            var ctx = new ComplexMethodContext(
                "GetPlan",
                "PlanSetup",
                "",
                "AsyncPlanSetup",
                "IPlanSetup",
                "()",
                "()",
                "",
                ImmutableList<ParameterContext>.Empty
            );

            var code = ComplexMethodGenerator.Generate(ctx);

            Assert.That(code, Does.Contain("public async Task<IPlanSetup> GetPlanAsync()"));
            Assert.That(code, Does.Contain("new AsyncPlanSetup(result, _service)"));
        }

        [Test]
        public void SimpleCollectionMethodGenerator_Generates_AsyncAwait_For_Covariance()
        {
            // SimpleCollectionMethodContext(Name, Symbol, Xml, InterfaceName, Signature, OriginalSig, CallParams, Parameters)
            var ctx = new SimpleCollectionMethodContext(
                "GetHistory",
                "IEnumerable<string>",
                "",
                "IReadOnlyList<string>",
                "()",
                "()",
                "",
                ImmutableList<ParameterContext>.Empty
            );

            var code = SimpleCollectionMethodGenerator.Generate(ctx);

            Assert.That(code, Does.Contain("public async Task<IReadOnlyList<string>> GetHistoryAsync()"));
            Assert.That(code, Does.Contain("await _service.PostAsync"));
        }

        [Test]
        public void ComplexCollectionMethodGenerator_Generates_Projection_List()
        {
            // ComplexCollectionMethodContext(Name, Symbol, Xml, InterfaceName, WrapperName, WrapperItemName, Signature, OriginalSig, CallParams, Parameters)
            var ctx = new ComplexCollectionMethodContext(
                "GetBeams",
                "IEnumerable<Beam>",
                "",
                "IReadOnlyList<IBeam>",
                "IReadOnlyList<AsyncBeam>",
                "AsyncBeam",
                "()",
                "()",
                "",
                ImmutableList<ParameterContext>.Empty
            );

            var code = ComplexCollectionMethodGenerator.Generate(ctx);

            Assert.That(code, Does.Contain("public async Task<IReadOnlyList<IBeam>> GetBeamsAsync()"));
            Assert.That(code, Does.Contain("Select(x => new AsyncBeam(x, _service)).ToList()"));
        }

        #endregion

        #region 3. Advanced / Specialized

        [Test]
        public void OutParameterMethodGenerator_Unpacks_Tuple()
        {
            var param = new ParameterContext("val", "string", "string", "", false, true, false); // out string
            var ctx = new OutParameterMethodContext(
                "GetValue",
                "bool",
                "",
                "bool",
                false,
                ImmutableList.Create(param),
                "(bool result, string val)",
                "",
                false
            );

            var code = OutParameterMethodGenerator.Generate(ctx);

            Assert.That(code, Does.Contain("public async Task<(bool result, string val)> GetValueAsync()")); // Generator removes 'out' from input args
            Assert.That(code, Does.Contain("return (result, val_temp);"));
        }

        [Test]
        public void IndexPropertyGenerator_Generates_Get_Set_And_All()
        {
            var param = new ParameterContext("index", "int", "int", "", false, false, false);
            var ctx = new IndexerContext(
                "this[]",
                "ControlPoint",
                "",
                "AsyncControlPoint",
                "IControlPoint",
                ImmutableList.Create(param),
                IsReadOnly: false
            );

            var code = IndexPropertyGenerator.Generate(ctx);

            // 1. GetItem
            Assert.That(code, Does.Contain("public async Task<IControlPoint> GetItemAsync(int index)"));
            Assert.That(code, Does.Contain("new AsyncControlPoint(_inner[index], _service)"));

            // 2. SetItem
            Assert.That(code, Does.Contain("public async Task SetItemAsync(int index, IControlPoint value)"));

            // 3. GetAllItems
            Assert.That(code, Does.Contain("public async Task<IReadOnlyList<IControlPoint>> GetAllItemsAsync()"));
        }

        [Test]
        public void ConstructorGenerator_Eager_Initializes_Properties()
        {
            // Simulate a class with a simple property and a simple collection
            var prop = new SimplePropertyContext("Id", "string", "", false);

            var colProp = new SimpleCollectionPropertyContext(
                "DoseValues",
                "IEnumerable<double>",
                "",
                "double",
                "IReadOnlyList<double>",
                "IReadOnlyList<double>"
            );

            var members = ImmutableList.Create<IMemberContext>(prop, colProp);

            var ctx = new ClassContext
            {
                Name = "PlanSetup",
                WrapperName = "AsyncPlanSetup",
                BaseWrapperName = "AsyncPlanningItem", // Inherits
                Members = members
            };

            var code = ConstructorGenerator.Generate(ctx);

            Assert.That(code, Does.Contain("public AsyncPlanSetup(PlanSetup inner, IEsapiService service) : base(inner, service)"));
            // Eager Init Checks
            Assert.That(code, Does.Contain("Id = inner.Id;"));
            Assert.That(code, Does.Contain("DoseValues = inner.DoseValues?.ToList() ?? new List<double>();"));
        }

        #endregion
    }
}