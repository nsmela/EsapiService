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
    public class AsyncStudy : AsyncApiDataObject, IStudy, IEsapiWrapper<VMS.TPS.Common.Model.API.Study>
    {
        internal new readonly VMS.TPS.Common.Model.API.Study _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncStudy(VMS.TPS.Common.Model.API.Study inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            CreationDateTime = inner.CreationDateTime;
            UID = inner.UID;
        }

        public DateTime? CreationDateTime { get; }

        public async Task<IReadOnlyList<IImage>> GetImages3DAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Images3D?.Select(x => new AsyncImage(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<ISeries>> GetSeriesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Series?.Select(x => new AsyncSeries(x, _service)).ToList());
        }


        public string UID { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Study> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Study, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.Study(AsyncStudy wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Study IEsapiWrapper<VMS.TPS.Common.Model.API.Study>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Study>.Service => _service;
    }
}
