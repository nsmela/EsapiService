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
    public class AsyncSeedCollection : AsyncApiDataObject, ISeedCollection, IEsapiWrapper<VMS.TPS.Common.Model.API.SeedCollection>
    {
        internal new readonly VMS.TPS.Common.Model.API.SeedCollection _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncSeedCollection(VMS.TPS.Common.Model.API.SeedCollection inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            Color = inner.Color;
        }

        public async Task<IReadOnlyList<IBrachyFieldReferencePoint>> GetBrachyFieldReferencePointsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.BrachyFieldReferencePoints?.Select(x => new AsyncBrachyFieldReferencePoint(x, _service)).ToList());
        }


        public System.Windows.Media.Color Color { get; }

        public async Task<IReadOnlyList<ISourcePosition>> GetSourcePositionsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.SourcePositions?.Select(x => new AsyncSourcePosition(x, _service)).ToList());
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.SeedCollection> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.SeedCollection, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.SeedCollection(AsyncSeedCollection wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.SeedCollection IEsapiWrapper<VMS.TPS.Common.Model.API.SeedCollection>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.SeedCollection>.Service => _service;
    }
}
