using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncSlot : ISlot
    {
        internal readonly VMS.TPS.Common.Model.API.Slot _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncSlot(VMS.TPS.Common.Model.API.Slot inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Number = inner.Number;
        }

        public int Number { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Slot> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Slot, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
