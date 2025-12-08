using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;

namespace Esapi.Wrappers
{
    public class AsyncDynamicWedge : IDynamicWedge
    {
        internal readonly VMS.TPS.Common.Model.API.DynamicWedge _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncDynamicWedge(VMS.TPS.Common.Model.API.DynamicWedge inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.DynamicWedge> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.DynamicWedge, T> func) => _service.RunAsync(() => func(_inner));
    }
}
