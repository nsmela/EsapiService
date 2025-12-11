using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncHospital : AsyncApiDataObject, IHospital, IEsapiWrapper<VMS.TPS.Common.Model.API.Hospital>
    {
        internal new readonly VMS.TPS.Common.Model.API.Hospital _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncHospital(VMS.TPS.Common.Model.API.Hospital inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            CreationDateTime = inner.CreationDateTime;
            Location = inner.Location;
        }

        public DateTime? CreationDateTime { get; }

        public async Task<IReadOnlyList<IDepartment>> GetDepartmentsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Departments?.Select(x => new AsyncDepartment(x, _service)).ToList());
        }


        public string Location { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Hospital> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Hospital, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.Hospital(AsyncHospital wrapper) => wrapper._inner;
        // Internal Explicit Implementation to expose _inner safely
        VMS.TPS.Common.Model.API.Hospital IEsapiWrapper<VMS.TPS.Common.Model.API.Hospital>.Inner => _inner;
    }
}
