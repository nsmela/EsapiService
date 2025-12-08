using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;

namespace Esapi.Wrappers
{
    public class AsyncStudy : IStudy
    {
        internal readonly VMS.TPS.Common.Model.API.Study _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncStudy(VMS.TPS.Common.Model.API.Study inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            UID = inner.UID;
        }


        public async Task<IReadOnlyList<DateTime>> GetCreationDateTimeAsync()
        {
            return await _service.RunAsync(() => _inner.CreationDateTime?.ToList());
        }


        public async Task<IReadOnlyList<IImage>> GetImages3DAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Images3D?.Select(x => new AsyncImage(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<ISeries>> GetSeriesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Series?.Select(x => new AsyncSeries(x, _service)).ToList());
        }


        public string UID { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Study> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Study, T> func) => _service.RunAsync(() => func(_inner));
    }
}
