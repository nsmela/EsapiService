using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;

namespace Esapi.Wrappers
{
    public class AsyncAddOn : IAddOn
    {
        internal readonly VMS.TPS.Common.Model.API.AddOn _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncAddOn(VMS.TPS.Common.Model.API.AddOn inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }


        public async Task<IReadOnlyList<DateTime>> GetCreationDateTimeAsync()
        {
            return await _service.RunAsync(() => _inner.CreationDateTime?.ToList());
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.AddOn> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.AddOn, T> func) => _service.RunAsync(() => func(_inner));
    }
}
