using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncEnhancedDynamicWedge : AsyncDynamicWedge, IEnhancedDynamicWedge
    {
        internal readonly VMS.TPS.Common.Model.API.EnhancedDynamicWedge _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncEnhancedDynamicWedge(VMS.TPS.Common.Model.API.EnhancedDynamicWedge inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.EnhancedDynamicWedge> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.EnhancedDynamicWedge, T> func) => _service.RunAsync(() => func(_inner));
    }
}
