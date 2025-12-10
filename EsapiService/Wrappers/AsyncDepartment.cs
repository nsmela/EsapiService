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
    public class AsyncDepartment : AsyncApiDataObject, IDepartment
    {
        internal new readonly VMS.TPS.Common.Model.API.Department _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncDepartment(VMS.TPS.Common.Model.API.Department inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }


        public Task<string> GetFullNameAsync() => _service.PostAsync(context => _inner.GetFullName());

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Department> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Department, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
