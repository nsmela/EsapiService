using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncWedge : IWedge
    {
        internal readonly VMS.TPS.Common.Model.API.Wedge _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncWedge(VMS.TPS.Common.Model.API.Wedge inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Direction = inner.Direction;
            WedgeAngle = inner.WedgeAngle;
        }

        public double Direction { get; }
        public double WedgeAngle { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Wedge> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Wedge, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
