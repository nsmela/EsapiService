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
            _inner = inner;
            _service = service;

            BrachyFieldReferencePoints = inner.BrachyFieldReferencePoints;
            Color = inner.Color;
            SourcePositions = inner.SourcePositions;
        }

        public IEnumerable<BrachyFieldReferencePoint> BrachyFieldReferencePoints { get; }

        public System.Windows.Media.Color Color { get; }

        public IEnumerable<SourcePosition> SourcePositions { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.SeedCollection> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.SeedCollection, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.SeedCollection(AsyncSeedCollection wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.SeedCollection IEsapiWrapper<VMS.TPS.Common.Model.API.SeedCollection>.Inner => _inner;
    }
}
