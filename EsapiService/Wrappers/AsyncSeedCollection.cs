using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;

namespace Esapi.Wrappers
{
    public class AsyncSeedCollection : ISeedCollection
    {
        internal readonly VMS.TPS.Common.Model.API.SeedCollection _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncSeedCollection(VMS.TPS.Common.Model.API.SeedCollection inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Color = inner.Color;
        }


        public async Task<IReadOnlyList<IBrachyFieldReferencePoint>> GetBrachyFieldReferencePointsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.BrachyFieldReferencePoints?.Select(x => new AsyncBrachyFieldReferencePoint(x, _service)).ToList());
        }


        public Windows.Media.Color Color { get; }

        public async Task<IReadOnlyList<ISourcePosition>> GetSourcePositionsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.SourcePositions?.Select(x => new AsyncSourcePosition(x, _service)).ToList());
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.SeedCollection> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.SeedCollection, T> func) => _service.RunAsync(() => func(_inner));
    }
}
