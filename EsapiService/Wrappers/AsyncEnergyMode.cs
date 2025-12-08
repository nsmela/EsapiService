using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncEnergyMode : IEnergyMode
    {
        internal readonly VMS.TPS.Common.Model.API.EnergyMode _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncEnergyMode(VMS.TPS.Common.Model.API.EnergyMode inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            IsElectron = inner.IsElectron;
            IsPhoton = inner.IsPhoton;
            IsProton = inner.IsProton;
        }

        public bool IsElectron { get; }
        public bool IsPhoton { get; }
        public bool IsProton { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.EnergyMode> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.EnergyMode, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
