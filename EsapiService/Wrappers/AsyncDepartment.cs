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
    public class AsyncDepartment : AsyncApiDataObject, IDepartment, IEsapiWrapper<VMS.TPS.Common.Model.API.Department>
    {
        internal new readonly VMS.TPS.Common.Model.API.Department _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncDepartment(VMS.TPS.Common.Model.API.Department inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

        }


        // Simple Method
        public Task<string> GetFullNameAsync() => 
            _service.PostAsync(context => _inner.GetFullName());

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Department> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Department, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();
        }

        public static implicit operator VMS.TPS.Common.Model.API.Department(AsyncDepartment wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Department IEsapiWrapper<VMS.TPS.Common.Model.API.Department>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Department>.Service => _service;
    }
}
