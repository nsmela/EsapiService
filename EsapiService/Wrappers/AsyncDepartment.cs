using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncDepartment : IDepartment
    {
        internal readonly VMS.TPS.Common.Model.API.Department _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncDepartment(VMS.TPS.Common.Model.API.Department inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }


        public Task<string> GetFullNameAsync() => _service.RunAsync(() => _inner.GetFullName());

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Department> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Department, T> func) => _service.RunAsync(() => func(_inner));
    }
}
