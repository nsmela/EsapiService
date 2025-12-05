    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
    public class AsyncPlanSetup : IPlanSetup
    {
        internal readonly VMS.TPS.Common.Model.API.PlanSetup _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncPlanSetup(VMS.TPS.Common.Model.API.PlanSetup inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

        }

        public string Id => _inner.Id;
        public async Task SetIdAsync(string value) => _service.RunAsync(() => _inner.Id = value);
        public ICourse Course => _inner.Course is null ? null : new AsyncCourse(_inner.Course, _service);

        public System.Collections.Generic.IReadOnlyList<IStructure> Structures => _inner.Structures?.Select(x => new AsyncStructure(x, _service)).ToList();
        public void Calculate() => _inner.Calculate();
    }
}
