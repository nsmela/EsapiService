using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;

namespace Esapi.Wrappers
{
    public class AsyncHospital : IHospital
    {
        internal readonly VMS.TPS.Common.Model.API.Hospital _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncHospital(VMS.TPS.Common.Model.API.Hospital inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Location = inner.Location;
        }


        public async Task<IReadOnlyList<DateTime>> GetCreationDateTimeAsync()
        {
            return await _service.RunAsync(() => _inner.CreationDateTime?.ToList());
        }


        public async Task<IReadOnlyList<IDepartment>> GetDepartmentsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Departments?.Select(x => new AsyncDepartment(x, _service)).ToList());
        }


        public string Location { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Hospital> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Hospital, T> func) => _service.RunAsync(() => func(_inner));
    }
}
